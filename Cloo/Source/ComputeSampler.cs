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
    /// Represents an OpenCL sampler.
    /// </summary>
    /// <remarks> An object that describes how to sample an image when the image is read in the kernel. The image read functions take a sampler as an argument. The sampler specifies the image addressing-mode i.e. how out-of-range image coordinates are handled, the filtering mode, and whether the input image coordinate is a normalized or unnormalized value. </remarks>
    /// <seealso cref="ComputeImage"/>
    public class ComputeSampler : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ComputeImageAddressing addressing;
        private readonly ComputeImageFiltering filtering;
        private readonly bool normalizedCoords;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="ComputeContext"/> of the <see cref="ComputeSampler"/>.
        /// </summary>
        /// <value> The <see cref="ComputeContext"/> of the <see cref="ComputeSampler"/>. </value>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the <see cref="ComputeImageAddressing"/> mode of the <see cref="ComputeSampler"/>.
        /// </summary>
        /// <value> The <see cref="ComputeImageAddressing"/> mode of the <see cref="ComputeSampler"/>. </value>
        public ComputeImageAddressing Addressing { get { return addressing; } }

        /// <summary>
        /// Gets the <see cref="ComputeImageFiltering"/> mode of the <see cref="ComputeSampler"/>.
        /// </summary>
        /// <value> The <see cref="ComputeImageFiltering"/> mode of the <see cref="ComputeSampler"/>. </value>
        public ComputeImageFiltering Filtering { get { return filtering; } }

        /// <summary>
        /// Gets the state of usage of normalized x, y and z coordinates when accessing a <see cref="ComputeImage"/> in a <see cref="ComputeKernel"/> through the <see cref="ComputeSampler"/>.
        /// </summary>
        /// <value> The state of usage of normalized x, y and z coordinates when accessing a <see cref="ComputeImage"/> in a <see cref="ComputeKernel"/> through the <see cref="ComputeSampler"/>. </value>
        public bool NormalizedCoords { get { return normalizedCoords; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeSampler"/>.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/>. </param>
        /// <param name="normalizedCoords"> The usage state of normalized coordinates when accessing a <see cref="ComputeImage"/> in a <see cref="ComputeKernel"/>. </param>
        /// <param name="addressing"> The <see cref="ComputeImageAddressing"/> mode of the <see cref="ComputeSampler"/>. Specifies how out-of-range image coordinates are handled while reading. </param>
        /// <param name="filtering"> The <see cref="ComputeImageFiltering"/> mode of the <see cref="ComputeSampler"/>. Specifies the type of filter that must be applied when reading data from an image. </param>
        public ComputeSampler(ComputeContext context, bool normalizedCoords, ComputeImageAddressing addressing, ComputeImageFiltering filtering)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateSampler(
                    context.Handle,
                    (normalizedCoords) ? ComputeBoolean.True : ComputeBoolean.False,
                    addressing,
                    filtering,
                    &error);
                ComputeException.ThrowOnError(error);
                this.addressing = addressing;
                this.context = context;
                this.filtering = filtering;
                this.normalizedCoords = normalizedCoords;
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the string representation of the <see cref="ComputeSampler"/>.
        /// </summary>
        /// <returns> The string representation of the <see cref="ComputeSampler"/>. </returns>
        public override string ToString()
        {
            return "ComputeSampler" + base.ToString();
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
                CL10.ReleaseSampler(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}