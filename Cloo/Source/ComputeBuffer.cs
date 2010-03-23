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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    public class ComputeBuffer<T>: ComputeMemory where T: struct
    {
        #region Fields

        private readonly long count;

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
        /// <param name="context">A valid OpenCL context used to create this buffer.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the buffer and how it will be used.</param>
        /// <param name="count">The number of elements this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags, long count )
            : base( context, flags )
        {
            unsafe
            {
                this.count = count;
                Size = count * Marshal.SizeOf( typeof( T ) );

                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateBuffer(
                    context.Handle,
                    flags,
                    new IntPtr( Size ),
                    IntPtr.Zero,
                    &error );
                ComputeException.ThrowOnError( error );
            }
        }

        /// <summary>
        /// Creates a new memory object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create this buffer.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the buffer and how it will be used.</param>
        /// <param name="data">A pointer to the data this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags, IntPtr dataPtr )
            : base( context, flags )
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateBuffer(
                    context.Handle,
                    flags,
                    new IntPtr( Size ),
                    dataPtr,
                    &error );
                ComputeException.ThrowOnError( error );

                Size = ( long )GetInfo<ComputeMemoryInfo, IntPtr>( ComputeMemoryInfo.Size, CL10.GetMemObjectInfo );
                count = Size / Marshal.SizeOf( typeof( T ) );
            }
        }

        /// <summary>
        /// Creates a new memory object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create this buffer.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the buffer and how it will be used.</param>
        /// <param name="data">The elements this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, ComputeMemoryFlags flags, T[] data )
            : base( context, flags )
        {
            unsafe
            {
                count = data.Length;
                Size = count * Marshal.SizeOf( typeof( T ) );

                GCHandle dataPtr = GCHandle.Alloc( data, GCHandleType.Pinned );
                try
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    Handle = CL10.CreateBuffer(
                        context.Handle,
                        flags,
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

        private ComputeBuffer( IntPtr handle, ComputeContext context, ComputeMemoryFlags flags )
            : base( context, flags )
        {
            unsafe
            {
                Handle = handle;

                Size = ( long )GetInfo<ComputeMemoryInfo, IntPtr>( ComputeMemoryInfo.Size, CL10.GetMemObjectInfo );
                count = Size / Marshal.SizeOf( typeof( T ) );
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new buffer from an existing OpenGL buffer.
        /// </summary>
        /// <typeparam name="T">The type of the elements of this buffer.</typeparam>
        /// <param name="context">A compute context with enabled CL/GL sharing.</param>
        /// <param name="flags">A bit field that is used to specify allocation and usage information.</param>
        /// <param name="bufferId">The OpenGL buffer object to create this buffer from.</param>
        public static ComputeBuffer<T> CreateFromGLBuffer<T>( ComputeContext context, ComputeMemoryFlags flags, int bufferId ) where T: struct
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                IntPtr handle = CL10.CreateFromGLBuffer(
                    context.Handle,
                    flags,
                    bufferId,
                    &error );
                ComputeException.ThrowOnError( error );

                return new ComputeBuffer<T>( handle, context, flags );
            }
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