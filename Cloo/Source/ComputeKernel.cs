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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    public class ComputeKernel: ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly string functionName;
        private readonly ComputeProgram program;
        private readonly Dictionary<int, ComputeResource> tracker;

        #endregion

        #region Properties

        /// <summary>
        /// The ComputeContext associated with the kernel.
        /// </summary>
        public ComputeContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// The kernel function name.
        /// </summary>
        public string FunctionName
        {
            get
            {
                return functionName;
            }
        }

        /// <summary>
        /// The ComputeProgram associated with kernel.
        /// </summary>
        public ComputeProgram Program
        {
            get
            {
                return program;
            }
        }

        #endregion

        #region Constructors

        internal ComputeKernel( IntPtr handle, ComputeProgram program )
        {
            Handle = handle;
            context = program.Context;            
            functionName = GetStringInfo<ComputeKernelInfo>( ComputeKernelInfo.FunctionName, CL10.GetKernelInfo );            
            this.program = program;
            tracker = new Dictionary<int, ComputeResource>();
        }

        internal ComputeKernel( string functionName, ComputeProgram program )
        {
            ComputeErrorCode error = ComputeErrorCode.Success;
            Handle = CL10.CreateKernel( 
                program.Handle, 
                functionName, 
                out error );
            ComputeException.ThrowOnError( error );
            context = program.Context;
            this.functionName = functionName;
            this.program = program;
            tracker = new Dictionary<int, ComputeResource>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the amount of local memory in bytes used by the kernel.
        /// </summary>
        public long GetLocalMemorySize( ComputeDevice device )
        {
            return ( long )GetInfo<ComputeKernelWorkGroupInfo, ulong>(
                device, ComputeKernelWorkGroupInfo.LocalMemorySize, CL10.GetKernelWorkGroupInfo );
        }

        /// <summary>
        /// The compile work-group size specified by the __attribute__((reqd_work_group_size(X, Y, Z))) qualifier. If the above qualifier is not specified (0, 0, 0) is returned.
        /// </summary>
        public long[] GetCompileWorkGroupSize( ComputeDevice device )
        {
            return Tools.ConvertArray(
                GetArrayInfo<ComputeKernelWorkGroupInfo, IntPtr>(
                    device, ComputeKernelWorkGroupInfo.CompileWorkGroupSize, CL10.GetKernelWorkGroupInfo ) );
        }

        /// <summary>
        /// The maximum work-group size that can be used to execute the kernel on the specified device.
        /// </summary>
        public long GetWorkGroupSize( ComputeDevice device )
        {
            return ( long )GetInfo<ComputeKernelWorkGroupInfo, IntPtr>(
                    device, ComputeKernelWorkGroupInfo.WorkGroupSize, CL10.GetKernelWorkGroupInfo );
        }

        /// <summary>
        /// Sets the value of a specific argument of the kernel.
        /// </summary>
        /// <param name="index">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="dataSize">Specifies the size of the argument value in bytes.</param>
        /// <param name="dataAddr">A pointer to data that should be used as the argument value for argument specified by index.</param>
        public void SetArgument( int index, IntPtr dataSize, IntPtr dataAddr )
        {
            ComputeErrorCode error = CL10.SetKernelArg(
                Handle,
                index,
                dataSize,
                dataAddr );
            ComputeException.ThrowOnError( error );
        }

        /// <summary>
        /// Set the argument value for a specific argument of a kernel.
        /// </summary>
        public void SetMemoryArgument( int index, ComputeMemory memObj )
        {
            SetMemoryArgument( index, memObj, true );
        }

        /// <summary>
        /// Set the argument value for a specific argument of a kernel.
        /// </summary>
        /// <param name="index">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="memObj">The memory object that is passed as the argument to the kernel.</param>
        /// <param name="track">Specify whether the kernel should prevent garbage collection of this memory object before kernel execution. This is useful if the application code doesn't refer to this memory object after this call.</param>
        public void SetMemoryArgument( int index, ComputeMemory memObj, bool track )
        {
            if( track ) tracker[ index ] = memObj;

            SetValueArgument<IntPtr>( index, memObj.Handle );
        }

        /// <summary>
        /// Sets the specified kernel argument.
        /// </summary>
        public void SetSamplerArgument( int index, ComputeSampler sampler )
        {
            SetSamplerArgument( index, sampler, true );
        }

        /// <summary>
        /// Sets the argument value for a specific argument of a kernel.
        /// </summary>
        /// <param name="index">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="sampler">The sampler object that is passed as the argument to the kernel.</param>
        /// <param name="track">Specify whether the kernel should prevent garbage collection of this sampler object before kernel execution. This is useful if the application code doesn't refer to this sampler object after this call.</param>
        public void SetSamplerArgument( int index, ComputeSampler sampler, bool track )
        {
            if( track ) tracker[ index ] = sampler;

            SetValueArgument<IntPtr>( index, sampler.Handle );
        }

        /// <summary>
        /// Sets the argument value for a specific argument of a kernel.
        /// </summary>
        /// <typeparam name="T">The type of the argument value.</typeparam>
        /// <param name="index">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="data">The data that is passed as the argument value to the kernel.</param>
        public void SetValueArgument<T>( int index, T data ) where T : struct
        {
            GCHandle gcHandle = GCHandle.Alloc( data, GCHandleType.Pinned );            
            try
            {
                SetArgument( 
                    index,
                    new IntPtr( Marshal.SizeOf( typeof( T ) ) ),
                    gcHandle.AddrOfPinnedObject() );
            }
            finally
            {
                gcHandle.Free();
            }            
        }

        /// <summary>
        /// Gets a string representation of this kernel.
        /// </summary>
        public override string ToString()
        {
            return "ComputeKernel" + base.ToString();
        }

        #endregion

        #region Internal methods

        internal void ReferenceArguments()
        {
            foreach( ComputeResource resource in tracker.Values )
                GC.KeepAlive( resource );
        }

        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( Handle != IntPtr.Zero )
            {
                CL10.ReleaseKernel( Handle );
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}