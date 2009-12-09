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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK.Compute.CL10;


namespace Cloo
{
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
        public ComputeBuffer( ComputeContext context, MemFlags flags, long count )
        {
            this.contxt = context;
            this.memflags = flags;
            this.count = count;
            byteSize = count * Marshal.SizeOf( typeof( T ) );
            ErrorCode error = ErrorCode.Success;
            Handle = CL.CreateBuffer( context.Handle, flags, new IntPtr( byteSize ), IntPtr.Zero, out error );
            ComputeException.ThrowIfError( error );
        }

        /// <summary>
        /// Creates a new memory object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create the memory.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information such as the memory area that should be used to allocate the memory object and how it will be used.</param>
        /// <param name="data">The elements this buffer will contain.</param>
        public ComputeBuffer( ComputeContext context, MemFlags flags, T[] data )
        {        
            this.contxt = context;
            this.memflags = flags;
            byteSize = data.Length * Marshal.SizeOf( typeof( T ) );
            count = data.Length;

            ErrorCode error = ErrorCode.Success;
            unsafe
            {
                GCHandle dataPtr = GCHandle.Alloc( data, GCHandleType.Pinned );
                try
                {
                    Handle = CL.CreateBuffer(
                        context.Handle,
                        flags,
                        new IntPtr( byteSize ),
                        dataPtr.AddrOfPinnedObject(),
                        out error );
                }
                finally
                {
                    dataPtr.Free();
                }
            }
            ComputeException.ThrowIfError( error );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get a list of supported ComputeBuffer formats available on the context.
        /// </summary>
        /// <param name="context">A valid OpenCL context.</param>
        /// <param name="flags">Restrict the list of formats to these flags.</param>
        /// <returns>A list of supported ComputeBuffer formats.</returns>
        public static ICollection<ImageFormat> GetSupportedFormats( ComputeContext context, MemFlags flags )
        {
            return GetSupportedFormats( context, flags, MemObjectType.MemObjectBuffer );
        }
        
        public override string ToString()
        {
            return "ComputeBuffer" + base.ToString();
        }

        #endregion
    }
}