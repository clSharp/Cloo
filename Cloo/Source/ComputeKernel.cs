#region License

/*

Copyright (c) 2009 - 2011 Fatjon Sakiqi

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
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL kernel.
    /// </summary>
    /// <remarks> A kernel object encapsulates a specific kernel function declared in a program and the argument values to be used when executing this kernel function. </remarks>
    /// <seealso cref="ComputeCommandQueue"/>
    /// <seealso cref="ComputeProgram"/>
    public class ComputeKernel : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly string functionName;
        private readonly ComputeProgram program;
        private readonly Dictionary<int, ComputeResource> tracker;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="ComputeContext"/> associated with the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <value> The <see cref="ComputeContext"/> associated with the <see cref="ComputeKernel"/>. </value>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the function name of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <value> The function name of the <see cref="ComputeKernel"/>. </value>
        public string FunctionName { get { return functionName; } }

        /// <summary>
        /// Gets the <see cref="ComputeProgram"/> that the <see cref="ComputeKernel"/> belongs to.
        /// </summary>
        /// <value> The <see cref="ComputeProgram"/> that the <see cref="ComputeKernel"/> belongs to. </value>
        public ComputeProgram Program { get { return program; } }

        #endregion

        #region Constructors

        internal ComputeKernel(IntPtr handle, ComputeProgram program)
        {
            unsafe
            {
                Handle = handle;
                context = program.Context;
                functionName = GetStringInfo<ComputeKernelInfo>(ComputeKernelInfo.FunctionName, CL10.GetKernelInfo);
                this.program = program;
                tracker = new Dictionary<int, ComputeResource>();
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        internal ComputeKernel(string functionName, ComputeProgram program)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateKernel(
                    program.Handle,
                    functionName,
                    &error);
                ComputeException.ThrowOnError(error);
                context = program.Context;
                this.functionName = functionName;
                this.program = program;
                tracker = new Dictionary<int, ComputeResource>();
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the amount of local memory in bytes used by the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="device"> One of the <see cref="ComputeKernel.Program.Device"/>s. </param>
        /// <returns> The amount of local memory in bytes used by the <see cref="ComputeKernel"/>. </returns>
        public long GetLocalMemorySize(ComputeDevice device)
        {
            unsafe
            {
                return (long)GetInfo<ComputeKernelWorkGroupInfo, ulong>(
                    device, ComputeKernelWorkGroupInfo.LocalMemorySize, CL10.GetKernelWorkGroupInfo);
            }
        }

        /// <summary>
        /// Gets the compile work-group size specified by the <c>__attribute__((reqd_work_group_size(X, Y, Z)))</c> qualifier.
        /// </summary>
        /// <param name="device"> One of the <see cref="ComputeKernel.Program.Device"/>s. </param>
        /// <returns> The compile work-group size specified by the <c>__attribute__((reqd_work_group_size(X, Y, Z)))</c> qualifier. If no such qualifier is specified, (0, 0, 0) is returned. </returns>
        public long[] GetCompileWorkGroupSize(ComputeDevice device)
        {
            unsafe
            {
                return Tools.ConvertArray(
                    GetArrayInfo<ComputeKernelWorkGroupInfo, IntPtr>(
                        device, ComputeKernelWorkGroupInfo.CompileWorkGroupSize, CL10.GetKernelWorkGroupInfo));
            }
        }

        /// <summary>
        /// Gets the preferred multiple of workgroup size for launch. 
        /// </summary>
        /// <param name="device"> One of the <see cref="ComputeKernel.Program.Device"/>s. </param>
        /// <returns> The preferred multiple of workgroup size for launch. </returns>
        /// <remarks> The returned value is a performance hint. Specifying a workgroup size that is not a multiple of the value returned by this query as the value of the local work size argument to ComputeCommandQueue.Execute will not fail to enqueue the kernel for execution unless the work-group size specified is larger than the device maximum. </remarks>
        /// <remarks> Requires OpenCL 1.1. </remarks>
        public long GetPreferredWorkGroupSizeMultiple(ComputeDevice device)
        {
            unsafe
            {
                return (long)GetInfo<ComputeKernelWorkGroupInfo, IntPtr>(
                    device, ComputeKernelWorkGroupInfo.PreferredWorkGroupSizeMultiple, CL10.GetKernelWorkGroupInfo);
            }
        }

        /// <summary>
        /// Gets the minimum amount of memory, in bytes, used by each work-item in the kernel.
        /// </summary>
        /// <param name="device"> One of the <see cref="ComputeKernel.Program.Device"/>s. </param>
        /// <returns> The minimum amount of memory, in bytes, used by each work-item in the kernel. </returns>
        /// <remarks> The returned value may include any private memory needed by an implementation to execute the kernel, including that used by the language built-ins and variable declared inside the kernel with the <c>__private</c> or <c>private</c> qualifier. </remarks>
        public long GetPrivateMemorySize(ComputeDevice device)
        {
            unsafe
            {
                return (long)GetInfo<ComputeKernelWorkGroupInfo, ulong>(
                    device, ComputeKernelWorkGroupInfo.PrivateMemorySize, CL10.GetKernelWorkGroupInfo);
            }
        }

        /// <summary>
        /// Gets the maximum work-group size that can be used to execute the <see cref="ComputeKernel"/> on a <see cref="ComputeDevice"/>.
        /// </summary>
        /// <param name="device"> One of the <see cref="ComputeKernel.Program.Device"/>s. </param>
        /// <returns> The maximum work-group size that can be used to execute the <see cref="ComputeKernel"/> on <paramref name="device"/>. </returns>
        public long GetWorkGroupSize(ComputeDevice device)
        {
            unsafe
            {
                return (long)GetInfo<ComputeKernelWorkGroupInfo, IntPtr>(
                        device, ComputeKernelWorkGroupInfo.WorkGroupSize, CL10.GetKernelWorkGroupInfo);
            }
        }

        /// <summary>
        /// Sets an argument of the <see cref="ComputeKernel"/> (no argument tracking).
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="dataSize"> The size of the argument data in bytes. </param>
        /// <param name="dataAddr"> A pointer to the data that should be used as the argument value. </param>
        /// <remarks> 
        /// Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel.
        /// <br/>
        /// Note that this method does not provide argument tracking. It is up to the user to reference the kernel arguments (i.e. prevent them from being garbage collected) until the kernel has finished execution.
        /// </remarks>
        public void SetArgument(int index, IntPtr dataSize, IntPtr dataAddr)
        {
            ComputeErrorCode error = CL10.SetKernelArg(
                Handle,
                index,
                dataSize,
                dataAddr);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Sets the size in bytes of an argument specfied with the <c>local</c> or <c>__local</c> address space qualifier.
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="dataSize"> The size of the argument data in bytes. </param>
        /// <remarks> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetLocalArgument(int index, long dataSize)
        {
            SetArgument(index, new IntPtr(dataSize), IntPtr.Zero);
        }

        /// <summary>
        /// Sets a <c>T*</c>, <c>image2d_t</c> or <c>image3d_t</c> argument of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="memObj"> The <see cref="ComputeMemory"/> that is passed as the argument. </param>
        /// <remarks> This method will automatically track <paramref name="memObj"/> to prevent it from being collected by the GC.<br/> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetMemoryArgument(int index, ComputeMemory memObj)
        {
            SetMemoryArgument(index, memObj, true);
        }

        /// <summary>
        /// Sets a <c>T*</c>, <c>image2d_t</c> or <c>image3d_t</c> argument of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="memObj"> The <see cref="ComputeMemory"/> that is passed as the argument. </param>
        /// <param name="track"> Specify whether the kernel should prevent garbage collection of the <paramref name="memObj"/> until <see cref="ComputeKernel"/> execution. This is useful if the application code doesn't refer to <paramref name="memObj"/> after this call or forces a <c>GC.Collect()</c> between this call and the <see cref="ComputeKernel"/> execution. </param>
        /// <remarks> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetMemoryArgument(int index, ComputeMemory memObj, bool track)
        {
            if (track) tracker[index] = memObj;

            SetValueArgument<IntPtr>(index, memObj.Handle);
        }

        /// <summary>
        /// Sets a <c>sampler_t</c> argument of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="sampler"> The <see cref="ComputeSampler"/> that is passed as the argument. </param>
        /// <remarks> This method will automatically track <paramref name="sampler"/> to prevent it from being collected by the GC.<br/> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetSamplerArgument(int index, ComputeSampler sampler)
        {
            SetSamplerArgument(index, sampler, true);
        }

        /// <summary>
        /// Sets a <c>sampler_t</c> argument of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="index"> The argument index. </param>
        /// <param name="sampler"> The <see cref="ComputeSampler"/> that is passed as the argument. </param>
        /// <param name="track"> Specify whether the kernel should prevent garbage collection of the <paramref name="sampler"/> until <see cref="ComputeKernel"/> execution. This is useful if the application code doesn't refer to <paramref name="sampler"/> after this call or forces a <c>GC.Collect()</c> between this call and the <see cref="ComputeKernel"/> execution. </param>
        /// <remarks> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetSamplerArgument(int index, ComputeSampler sampler, bool track)
        {
            if (track) tracker[index] = sampler;

            SetValueArgument<IntPtr>(index, sampler.Handle);
        }

        /// <summary>
        /// Sets a value argument of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <typeparam name="T"> The type of the argument. </typeparam>
        /// <param name="index"> The argument index. </param>
        /// <param name="data"> The data that is passed as the argument value. </param>
        /// <remarks> Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n-1, where n is the total number of arguments declared by the kernel. </remarks>
        public void SetValueArgument<T>(int index, T data) where T : struct
        {
            GCHandle gcHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                SetArgument(
                    index,
                    new IntPtr(Marshal.SizeOf(typeof(T))),
                    gcHandle.AddrOfPinnedObject());
            }
            finally
            {
                gcHandle.Free();
            }
        }

        /// <summary>
        /// Gets the string representation of the <see cref="ComputeKernel"/>.
        /// </summary>
        /// <returns> The string representation of the <see cref="ComputeKernel"/>. </returns>
        public override string ToString()
        {
            return "ComputeKernel" + base.ToString();
        }

        #endregion

        #region Internal methods

        internal void ReferenceArguments()
        {
            foreach (ComputeResource resource in tracker.Values)
                GC.KeepAlive(resource);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Releases the associated OpenCL object.
        /// </summary>
        /// <param name="manual"> Specifies the operation mode of this method. </param>
        protected override void Dispose(bool manual)
        {
            if (Handle != IntPtr.Zero)
            {
                Trace.WriteLine("Disposing " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
                CL10.ReleaseKernel(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}