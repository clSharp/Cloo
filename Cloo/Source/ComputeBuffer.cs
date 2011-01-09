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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL buffer.
    /// </summary>
    /// <typeparam name="T"> The type of the elements of the <see cref="ComputeBuffer{T}"/>. <typeparamref name="T"/> is restricted to value types and <c>struct</c>s containing such types. </typeparam>
    /// <remarks> A memory object that stores a linear collection of bytes. Buffer objects are accessible using a pointer in a kernel executing on a device. </remarks>
    /// <seealso cref="ComputeDevice"/>
    /// <seealso cref="ComputeKernel"/>
    /// <seealso cref="ComputeMemory"/>
    public class ComputeBuffer<T> : ComputeBufferBase<T> where T : struct
    {
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeBuffer{T}"/>.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/> used to create the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="count"> The number of elements of the <see cref="ComputeBuffer{T}"/>. </param>
        public ComputeBuffer(ComputeContext context, ComputeMemoryFlags flags, long count)
            : this(context, flags, count, IntPtr.Zero)
        { }

        /// <summary>
        /// Creates a new <see cref="ComputeBuffer{T}"/>.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/> used to create the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="count"> The number of elements of the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="dataPtr"> A pointer to the data for the <see cref="ComputeBuffer{T}"/>. </param>
        public ComputeBuffer(ComputeContext context, ComputeMemoryFlags flags, long count, IntPtr dataPtr)
            : base(context, flags)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateBuffer(context.Handle, flags, new IntPtr(Marshal.SizeOf(typeof(T)) * count), dataPtr, &error);
                ComputeException.ThrowOnError(error);

                Init();
            }
        }

        /// <summary>
        /// Creates a new <see cref="ComputeBuffer{T}"/>.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/> used to create the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <see cref="ComputeBuffer{T}"/>. </param>
        /// <param name="data"> The data for the <see cref="ComputeBuffer{T}"/>. </param>
        public ComputeBuffer(ComputeContext context, ComputeMemoryFlags flags, T[] data)
            : base(context, flags)
        {
            unsafe
            {
                GCHandle dataPtr = GCHandle.Alloc(data, GCHandleType.Pinned);
                try
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    Handle = CL10.CreateBuffer(context.Handle, flags, new IntPtr(Marshal.SizeOf(typeof(T)) * data.Length), dataPtr.AddrOfPinnedObject(), &error);
                    ComputeException.ThrowOnError(error);
                }
                finally
                {
                    dataPtr.Free();
                }

                Init();
            }
        }

        private ComputeBuffer(IntPtr handle, ComputeContext context, ComputeMemoryFlags flags)
            : base(context, flags)
        {
            unsafe
            {
                Handle = handle;
                Init();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new <see cref="ComputeBuffer{T}"/> from an existing OpenGL buffer object.
        /// </summary>
        /// <typeparam name="DataType"> The type of the elements of the <see cref="ComputeBuffer{T}"/>. <typeparamref name="T"/> should match the type of the elements in the OpenGL buffer. </typeparam>
        /// <param name="context"> A <see cref="ComputeContext"/> with enabled CL/GL sharing. </param>
        /// <param name="flags"> A bit-field that is used to specify usage information about the <see cref="ComputeBuffer{T}"/>. Only <see cref="ComputeMemoryFlags.ReadOnly"/>, <see cref="ComputeMemoryFlags.WriteOnly"/> and <see cref="ComputeMemoryFlags.ReadWrite"/> are allowed. </param>
        /// <param name="bufferId"> The OpenGL buffer object id to use for the creation of the <see cref="ComputeBuffer{T}"/>. </param>
        /// <returns> The created <see cref="ComputeBuffer{T}"/>. </returns>
        public static ComputeBuffer<DataType> CreateFromGLBuffer<DataType>(ComputeContext context, ComputeMemoryFlags flags, int bufferId) where DataType : struct
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                IntPtr handle = CL10.CreateFromGLBuffer(context.Handle, flags, bufferId, &error);
                ComputeException.ThrowOnError(error);

                return new ComputeBuffer<DataType>(handle, context, flags);
            }
        }

        /// <summary>
        /// Gets the string representation of the <see cref="ComputeBuffer{T}"/>.
        /// </summary>
        /// <returns> The string representation of the <see cref="ComputeBuffer{T}"/>. </returns>
        public override string ToString()
        {
            return "ComputeBuffer{T}" + base.ToString();
        }

        #endregion
    }
}