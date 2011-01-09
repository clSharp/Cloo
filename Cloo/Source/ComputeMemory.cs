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
    using System.Diagnostics;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL memory object.
    /// </summary>
    /// <remarks> A memory object is a handle to a region of global memory. </remarks>
    /// <seealso cref="ComputeBuffer{T}"/>
    /// <seealso cref="ComputeImage"/>
    public abstract class ComputeMemory : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ComputeMemoryFlags flags;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <c>ComputeContext</c> of the <c>ComputeMemory</c>.
        /// </summary>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the <c>ComputeMemoryFlags</c> of the <c>ComputeMemory</c>.
        /// </summary>
        public ComputeMemoryFlags Flags { get { return flags; } }

        /// <summary>
        /// Gets or sets (protected) the size in bytes of the <c>ComputeMemory</c>.
        /// </summary>
        public long Size { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        protected ComputeMemory(ComputeContext context, ComputeMemoryFlags flags)
        {
            this.context = context;
            this.flags = flags;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Releases the associated OpenCL object.
        /// </summary>
        /// <param name="manual"></param>
        protected override void Dispose(bool manual)
        {
            if (Handle != IntPtr.Zero)
            {
                Trace.WriteLine("Disposing " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
                CL10.ReleaseMemObject(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}