using System;
using System.Linq;

namespace Cloo.Extensions
{
    /// <summary>
    /// Contains extension methods using Cloo
    /// </summary>
    public static class ClooExtensions
    {
        /// <summary>
        /// Run kernel against all elements.
        /// </summary>
        /// <typeparam name="TSource">Struct type that corresponds to kernel function type</typeparam>
        /// <param name="array">Array of elements to process</param>
        /// <param name="kernelCode">The code of kernel function</param>
        /// <param name="kernelSelector">Method that selects kernel by function name, if null uses first</param>
        /// <param name="deviceSelector">Method that selects device by name, if null uses first</param>
        public static void ClooForEach<TSource>(this TSource[] array, string kernelCode, Func<string, bool> kernelSelector = null, Func<int, string, Version, bool> deviceSelector = null) where TSource : struct
        {
            kernelSelector = kernelSelector ?? ((k) => true);
            deviceSelector = deviceSelector ?? ((i, d, v) => true);

            var device = ComputePlatform.Platforms.SelectMany(p => p.Devices).Where((d, i) => deviceSelector(i, d.Name, d.Version)).First();

            var properties = new ComputeContextPropertyList(device.Platform);
            using (var context = new ComputeContext(new[] { device }, properties, null, IntPtr.Zero))
            using (var program = new ComputeProgram(context, kernelCode))
            {
                program.Build(new[] { device }, null, null, IntPtr.Zero);

                var kernels = program.CreateAllKernels().ToList();
                try
                {
                    var kernel = kernels.First((k) => kernelSelector(k.FunctionName));

                    using (var primesBuffer = new ComputeBuffer<TSource>(context, ComputeMemoryFlags.ReadWrite | ComputeMemoryFlags.UseHostPointer, array))
                    {
                        kernel.SetMemoryArgument(0, primesBuffer);

                        using (var queue = new ComputeCommandQueue(context, context.Devices[0], 0))
                        {
                            queue.Execute(kernel, null, new long[] { primesBuffer.Count }, null, null);
                            queue.Finish();

                            queue.ReadFromBuffer(primesBuffer, ref array, true, null);
                        }
                    }
                }
                finally
                {
                    kernels.ForEach(k => k.Dispose());
                }
            }
        }
    }
}
