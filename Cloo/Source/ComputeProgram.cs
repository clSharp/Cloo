#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL program.
    /// </summary>
    /// <remarks> An OpenCL program consists of a set of kernels. Programs may also contain auxiliary functions called by the kernel functions and constant data. </remarks>
    /// <seealso cref="ComputeKernel"/>
    public class ComputeProgram : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly ReadOnlyCollection<string> source;
        private ReadOnlyCollection<byte[]> binaries;
        private string buildOptions;
        private ComputeProgramBuildNotifier buildNotify;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only collection of program binaries associated with the <c>ComputeProgram.Devices</c>.
        /// </summary>
        /// <remarks> The bits returned can be an implementation-specific intermediate representation (a.k.a. IR) or device specific executable bits or both. The decision on which information is returned in the binary is up to the OpenCL implementation. </remarks>
        public ReadOnlyCollection<byte[]> Binaries { get { return binaries; } }

        /// <summary>
        /// Gets the <c>ComputeProgram</c> build options as specified in <paramref name="options"/> argument of <c>ComputeProgram.Build</c>.
        /// </summary>
        public string BuildOptions { get { return buildOptions; } }

        /// <summary>
        /// Gets the <c>ComputeContext</c> of the <c>ComputeProgram</c>.
        /// </summary>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets a read-only collection of <c>ComputeDevice</c>s associated with the <c>ComputeProgram</c>.
        /// </summary>
        /// <remarks> This collection contains <c>ComputeDevice</c>s from <c>ComputeProgram.Context.Devices</c>. </remarks>
        public ReadOnlyCollection<ComputeDevice> Devices { get { return devices; } }

        /// <summary>
        /// Gets a read-only collection of program source code strings specified when creating the <c>ComputeProgram</c> or <c>null</c> if <c>ComputeProgram</c> was created using program binaries.
        /// </summary>
        public ReadOnlyCollection<string> Source { get { return source; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeProgram</c> from a source code string.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c>. </param>
        /// <param name="source"> The source code for the <c>ComputeProgram</c>. </param>
        /// <remarks> The created <c>ComputeProgram</c> is associated with the <c>ComputeContext.Devices</c>. </remarks>
        public ComputeProgram(ComputeContext context, string source)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateProgramWithSource(
                    context.Handle,
                    1,
                    new string[] { source },
                    null,
                    &error);
                ComputeException.ThrowOnError(error);

                this.context = context;
                this.devices = context.Devices;
                this.source = new ReadOnlyCollection<string>(new string[] { source });
            }
        }

        /// <summary>
        /// Creates a new <c>ComputeProgram</c> from an array of source code strings.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c>. </param>
        /// <param name="source"> The source code lines for the <c>ComputeProgram</c>. </param>
        /// <remarks> The created <c>ComputeProgram</c> is associated with the <c>ComputeContext.Devices</c>. </remarks>
        public ComputeProgram(ComputeContext context, string[] source)
        {
            unsafe
            {
                IntPtr[] lengths = new IntPtr[source.Length];
                for (int i = 0; i < source.Length; i++)
                    lengths[i] = new IntPtr(source[i].Length);

                ComputeErrorCode error = ComputeErrorCode.Success;
                fixed (IntPtr* lengthsPtr = lengths)
                    Handle = CL10.CreateProgramWithSource(
                        context.Handle,
                        source.Length,
                        source,
                        null,
                        &error);
                ComputeException.ThrowOnError(error);

                this.context = context;
                this.devices = context.Devices;
                this.source = new ReadOnlyCollection<string>(source);
            }
        }

        /// <summary>
        /// Creates a new <c>ComputeProgram</c> from a specified list of binaries.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c>. </param>
        /// <param name="binaries"> A list of binaries, one for each item in <paramref name="devices"/>. </param>
        /// <param name="devices"> A subset of the <c>ComputeContext.Devices</c>. If <paramref name="devices"/> is <c>null</c>, OpenCL will associate every binary from <c>ComputeProgram.Binaries</c> with a corresponding <c>ComputeDevice</c> from <c>ComputeContext.Devices</c>. </param>
        public ComputeProgram(ComputeContext context, IList<byte[]> binaries, IList<ComputeDevice> devices)
        {
            unsafe
            {
                int count = binaries.Count;

                IntPtr[] deviceHandles = (devices != null) ?
                    Tools.ExtractHandles(devices) :
                    Tools.ExtractHandles(context.Devices);

                IntPtr[] binariesPtrs = new IntPtr[count];
                IntPtr[] binariesLengths = new IntPtr[count];
                int[] binariesStats = new int[count];
                ComputeErrorCode error = ComputeErrorCode.Success;
                GCHandle binariesPtrGCHandle = new GCHandle();
                GCHandle[] binariesGCHandles = new GCHandle[count];

                try
                {
                    binariesPtrGCHandle = GCHandle.Alloc(binariesPtrs, GCHandleType.Pinned);
                    for (int i = 0; i < count; i++)
                    {
                        binariesGCHandles[i] = GCHandle.Alloc(binaries[i], GCHandleType.Pinned);
                        binariesPtrs[i] = binariesGCHandles[i].AddrOfPinnedObject();
                        binariesLengths[i] = new IntPtr(binaries[i].Length);
                    }

                    byte** binariesPtr = (byte**)binariesPtrGCHandle.AddrOfPinnedObject();
                    fixed (int* binaryStatusPtr = binariesStats)
                    {
                        Handle = CL10.CreateProgramWithBinary(
                            context.Handle,
                            count,
                            deviceHandles,
                            binariesLengths,
                            binariesPtr,
                            binaryStatusPtr,
                            &error);
                        ComputeException.ThrowOnError(error);
                    }
                }
                finally
                {
                    binariesPtrGCHandle.Free();
                    for (int i = 0; i < count; i++)
                        binariesGCHandles[i].Free();
                }


                this.binaries = new ReadOnlyCollection<byte[]>(binaries);
                this.context = context;
                this.devices = new ReadOnlyCollection<ComputeDevice>(
                    (devices != null) ? devices : context.Devices);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Builds (compiles and links) a program executable from the program source or binary for all or some of the <c>ComputeProgram.Devices</c>.
        /// </summary>
        /// <param name="devices"> A subset or all of <c>ComputeProgram.Devices</c>. If <paramref name="devices"/> is <c>null</c>, the executable is built for every item of <c>ComputeProgram.Devices</c> for which a source or a binary has been loaded. </param>
        /// <param name="options"> A set of options for the OpenCL compiler. </param>
        /// <param name="notify"> A delegate instance that represents a reference to a notification routine. This routine is a callback function that an application can register and which will be called when the program executable has been built (successfully or unsuccessfully). If <paramref name="notify"/> is not <c>null</c>, <c>ComputeProgram.Build</c> does not need to wait for the build to complete and can return immediately. If <paramref name="notify"/> is <c>null</c>, <c>ComputeProgram.Build</c> does not return until the build has completed. The callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe and that the delegate instance doesn't get collected by the Garbage Collector until the build operation triggers the callback. </param>
        /// <param name="notifyDataPtr"> Optional user data that will be passed to <paramref name="notify"/>. </param>
        public void Build(ICollection<ComputeDevice> devices, string options, ComputeProgramBuildNotifier notify, IntPtr notifyDataPtr)
        {
            unsafe
            {
                IntPtr[] deviceHandles = Tools.ExtractHandles(devices);
                buildOptions = (options != null) ? options : "";
                buildNotify = notify;

                fixed (IntPtr* deviceHandlesPtr = deviceHandles)
                {
                    ComputeErrorCode error = CL10.BuildProgram(
                        Handle,
                        deviceHandles.Length,
                        deviceHandlesPtr,
                        options,
                        buildNotify,
                        notifyDataPtr);
                    ComputeException.ThrowOnError(error);
                }
                binaries = GetBinaries();
            }
        }

        /// <summary>
        /// Creates a <c>ComputeKernel</c> for every <c>kernel</c> function in <c>ComputeProgram</c>.
        /// </summary>
        /// <returns> The collection of created <c>ComputeKernel</c>s. </returns>
        /// <remarks> <c>ComputeKernel</c>s are not created for any <c>kernel</c> functions in <c>ComputeProgram</c> that do not have the same function definition across all <c>ComputeProgram.Devices</c> for which a program executable has been successfully built. </remarks>
        public ICollection<ComputeKernel> CreateAllKernels()
        {
            unsafe
            {
                ICollection<ComputeKernel> kernels = new Collection<ComputeKernel>();
                int kernelsCount = 0;
                IntPtr[] kernelHandles;

                ComputeErrorCode error = CL10.CreateKernelsInProgram(Handle, 0, null, &kernelsCount);
                ComputeException.ThrowOnError(error);

                kernelHandles = new IntPtr[kernelsCount];
                fixed (IntPtr* kernelHandlesPtr = kernelHandles)
                {
                    error = CL10.CreateKernelsInProgram(
                        Handle,
                        kernelsCount,
                        kernelHandlesPtr,
                        null);
                    ComputeException.ThrowOnError(error);
                }
                for (int i = 0; i < kernelsCount; i++)
                    kernels.Add(new ComputeKernel(kernelHandles[i], this));

                return kernels;
            }
        }

        /// <summary>
        /// Creates a <c>ComputeKernel</c> for a kernel function of a specified name.
        /// </summary>
        /// <returns> The created <c>ComputeKernel</c>. </returns>
        public ComputeKernel CreateKernel(string functionName)
        {
            return new ComputeKernel(functionName, this);
        }

        /// <summary>
        /// Gets the build log of the <c>ComputeProgram</c> for a specified <c>ComputeDevice</c>.
        /// </summary>
        /// <param name="device"> The <c>ComputeDevice</c> building the <c>ComputeProgram</c>. Must be one of <c>ComputeProgram.Devices</c>. </param>
        /// <returns> The build log of the <c>ComputeProgram</c> for <paramref name="device"/>. </returns>
        public string GetBuildLog(ComputeDevice device)
        {
            unsafe
            {
                return GetStringInfo<ComputeProgramBuildInfo>(
                    device,
                    ComputeProgramBuildInfo.BuildLog,
                    CL10.GetProgramBuildInfo);
            }
        }

        /// <summary>
        /// Gets the <c>ComputeProgramBuildStatus</c> of the <c>ComputeProgram</c> for a specified <c>ComputeDevice</c>.
        /// </summary>
        /// <param name="device"> The <c>ComputeDevice</c> building the <c>ComputeProgram</c>. Must be one of <c>ComputeProgram.Devices</c>. </param>
        /// <returns> The <c>ComputeProgramBuildStatus</c> of the <c>ComputeProgram</c> for <paramref name="device"/>. </returns>
        public ComputeProgramBuildStatus GetBuildStatus(ComputeDevice device)
        {
            unsafe
            {
                return (ComputeProgramBuildStatus)GetInfo<ComputeProgramBuildInfo, uint>(
                    device,
                    ComputeProgramBuildInfo.Status,
                    CL10.GetProgramBuildInfo);
            }
        }

        /// <summary>
        /// Gets the string representation of the <c>ComputeProgram</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeProgram</c>. </returns>
        public override string ToString()
        {
            return "ComputeProgram" + base.ToString();
        }

        #endregion

        #region Protected methods

        protected override void Dispose(bool manual)
        {
            if (Handle != IntPtr.Zero)
            {
                CL10.ReleaseProgram(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Private methods

        private ReadOnlyCollection<byte[]> GetBinaries()
        {
            unsafe
            {
                IntPtr[] binaryLengths = GetArrayInfo<ComputeProgramInfo, IntPtr>(
                    ComputeProgramInfo.BinarySizes, CL10.GetProgramInfo);

                GCHandle[] binariesGCHandles = new GCHandle[binaryLengths.Length];
                IntPtr[] binariesPtrs = new IntPtr[binaryLengths.Length];
                IList<byte[]> binaries = new List<byte[]>();
                GCHandle binariesPtrsGCHandle = GCHandle.Alloc(binariesPtrs, GCHandleType.Pinned);

                try
                {
                    for (int i = 0; i < binaryLengths.Length; i++)
                    {
                        byte[] binary = new byte[binaryLengths[i].ToInt64()];
                        binariesGCHandles[i] = GCHandle.Alloc(binary, GCHandleType.Pinned);
                        binariesPtrs[i] = binariesGCHandles[i].AddrOfPinnedObject();
                        binaries.Add(binary);
                    }

                    ComputeErrorCode error = CL10.GetProgramInfo(
                        Handle,
                        ComputeProgramInfo.Binaries,
                        new IntPtr(binariesPtrs.Length * IntPtr.Size),
                        binariesPtrsGCHandle.AddrOfPinnedObject(),
                        null);
                    ComputeException.ThrowOnError(error);
                }
                finally
                {
                    for (int i = 0; i < binaryLengths.Length; i++)
                        binariesGCHandles[i].Free();
                    binariesPtrsGCHandle.Free();
                }

                return new ReadOnlyCollection<byte[]>(binaries);
            }
        }

        #endregion
    }

    /// <summary>
    /// A callback function that can be registered by the application to report the <c>ComputeProgram</c> build status.
    /// </summary>
    /// <param name="programHandle"> The handle of the <c>ComputeProgram</c> being built. </param>
    /// <param name="notifyDataPtr"> The pointer to the optional user data specified in <paramref name="notifyDataPtr"/> argument of <c>ComputeProgram.Build</c>. </param>
    /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)] // BUG: Cdecl not working on Win+Stream v2.2
    public delegate void ComputeProgramBuildNotifier(IntPtr programHandle, IntPtr notifyDataPtr);
}