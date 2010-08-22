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

    /// <summary>
    /// Represents an OpenCL buffer.
    /// </summary>
    /// <typeparam name="T"> The type of the elements of the <c>ComputeBuffer</c>. <typeparamref name="T"/> is restricted to value types and <c>struct</c>s containing such types. </typeparam>
    /// <remarks> A memory object that stores a linear collection of bytes. Buffer objects are accessible using a pointer in a kernel executing on a device. </remarks>
    /// <seealso cref="ComputeDevice"/>
    /// <seealso cref="ComputeKernel"/>
    /// <seealso cref="ComputeMemory"/>
    public class ComputeBuffer<T> : ComputeBufferBase<T> where T : struct
    {
        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c> used to create the <c>ComputeBuffer</c>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <c>ComputeBuffer</c>. </param>
        /// <param name="count"> The number of elements of the <c>ComputeBuffer</c>. </param>
        public ComputeBuffer(ComputeContext context, ComputeMemoryFlags flags, long count)
            : this(context, flags, count, IntPtr.Zero)
        { }

        /// <summary>
        /// Creates a new <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c> used to create the <c>ComputeBuffer</c>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <c>ComputeBuffer</c>. </param>
        /// <param name="count"> The number of elements of the <c>ComputeBuffer</c>. </param>
        /// <param name="dataPtr"> A pointer to the data for the <c>ComputeBuffer</c>. </param>
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
        /// Creates a new <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c> used to create the <c>ComputeBuffer</c>. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <c>ComputeBuffer</c>. </param>
        /// <param name="data"> The data for the <c>ComputeBuffer</c>. </param>
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
        /// Creates a new <c>ComputeBuffer</c> from an existing OpenGL buffer object.
        /// </summary>
        /// <typeparam name="T"> The type of the elements of the <c>ComputeBuffer</c>. It is recommended, though not required, that <typeparamref name="T"/> matches the type of the elements in the OpenGL buffer. </typeparam>
        /// <param name="context"> A <c>ComputeContext</c> with enabled CL/GL sharing. </param>
        /// <param name="flags"> A bit-field that is used to specify usage information about the <c>ComputeBuffer</c>. Only <c>ComputeMemoryFlags.ReadOnly</c>, <c>ComputeMemoryFlags.WriteOnly</c> and <c>ComputeMemoryFlags.ReadWrite</c> are allowed. </param>
        /// <param name="bufferId"> The OpenGL buffer object id to use for the creation of the <c>ComputeBuffer</c>. </param>
        /// <returns> The created <c>ComputeBuffer</c>. </returns>
        public static ComputeBuffer<T> CreateFromGLBuffer<T>(ComputeContext context, ComputeMemoryFlags flags, int bufferId) where T : struct
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                IntPtr handle = CL10.CreateFromGLBuffer(context.Handle, flags, bufferId, &error);
                ComputeException.ThrowOnError(error);

                return new ComputeBuffer<T>(handle, context, flags);
            }
        }

        /// <summary>
        /// Gets the string representation of the <c>ComputeBuffer</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeBuffer</c>. </returns>
        public override string ToString()
        {
            return "ComputeBuffer" + base.ToString();
        }

        #endregion
    }
}