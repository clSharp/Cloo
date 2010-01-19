#region License

/*

Copyright (c) 2009 Fatjon Sakiqi

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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;
    using OpenTK.Compute.CL10;

    public class ComputeBuffer<T>: ComputeMemory where T: struct
    {
        #region Fields

        private long count;

        #endregion

        #region Properties

        /// <summary>
        /// The number of items this buffer contains.
        /// </summary>
        public long Count
        {
            get
            {
                return count; 
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new memory object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create the memory.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the memory object and how it will be used.</param>
        /// <param name="count">The number of elements this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags, long count )
            : base( context, flags )
        {
            this.count = count;
            Size = count * Marshal.SizeOf( typeof( T ) );
            ErrorCode error = ErrorCode.Success;
            unsafe
            {
                Handle = CL.CreateBuffer( 
                    context.Handle, 
                    ( MemFlags )flags,
                    new IntPtr( Size ),
                    IntPtr.Zero,
                    &error );
                ComputeException.ThrowOnError( error );
            }
        }

        /// <summary>
        /// Creates a new memory object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create the memory.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the memory object and how it will be used.</param>
        /// <param name="data">The elements this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags, T[] data )
            : base( context, flags )
        {        
            Size = data.Length * Marshal.SizeOf( typeof( T ) );
            count = data.Length;

            unsafe
            {
                GCHandle dataPtr = GCHandle.Alloc( data, GCHandleType.Pinned );
                try
                {
                    ErrorCode error = ErrorCode.Success;
                    Handle = CL.CreateBuffer(
                        context.Handle,
                        ( MemFlags )flags,
                        new IntPtr( Size ),
                        dataPtr.AddrOfPinnedObject(),
                        &error );
                    ComputeException.ThrowOnError( error );
                }
                finally
                {
                    dataPtr.Free();
                }
            }
        }

        private ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags )
            : base( context, flags )
        { }

        #endregion

        #region Public methods

        public static ComputeBuffer<T> CreateFromGLBuffer<T>( ComputeContext context, ComputeMemoryFlags flags, int bufferId ) where T: struct
        {
            ComputeBuffer<T> buffer = new ComputeBuffer<T>( context, flags );
            unsafe
            {
                int error = ( int )ErrorCode.Success;
                buffer.Handle = Imports.CreateFromGLBuffer( 
                    context.Handle, 
                    flags, 
                    ( uint )bufferId,
                    &error );
                ComputeException.ThrowOnError( error );
            }

            buffer.Size = ( long )buffer.GetInfo<MemInfo, IntPtr>( MemInfo.MemSize, CL.GetMemObjectInfo );            
            buffer.count = buffer.Size / Marshal.SizeOf( typeof( T ) );            
            return buffer;
        }

        /// <summary>
        /// Gets a string representation of this buffer.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ComputeBuffer" + base.ToString();
        }

        #endregion
    }
}