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
        /// Gets the <c>ComputeContext</c> of the <c>ComputeSampler</c>.
        /// </summary>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the <c>ComputeImageAddressing</c> mode of the <c>ComputeSampler</c>.
        /// </summary>
        public ComputeImageAddressing Addressing { get { return addressing; } }

        /// <summary>
        /// Gets the <c>ComputeImageFiltering</c> mode of the <c>ComputeSampler</c>.
        /// </summary>
        public ComputeImageFiltering Filtering { get { return filtering; } }

        /// <summary>
        /// Gets the state of usage of normalized x, y and z coordinates when accessing a <c>ComputeImage</c> in a <c>ComputeKernel</c> through the <c>ComputeSampler</c>.
        /// </summary>
        public bool NormalizedCoords { get { return normalizedCoords; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeSampler</c>.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c>. </param>
        /// <param name="normalizedCoords"> The usage state of normalized coordinates when accessing a <c>ComputeImage</c> in a <c>ComputeKernel</c>. </param>
        /// <param name="addressing"> The <c>ComputeImageAddressing</c> mode of the <c>ComputeSampler</c>. Specifies how out-of-range image coordinates are handled while reading. </param>
        /// <param name="filtering"> The <c>ComputeImageFiltering</c> mode of the <c>ComputeSampler</c>. Specifies the type of filter that must be applied when reading data from an image. </param>
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
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the string representation of the <c>ComputeSampler</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeSampler</c>. </returns>
        public override string ToString()
        {
            return "ComputeSampler" + base.ToString();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manual"></param>
        protected override void Dispose(bool manual)
        {
            // free native resources
            if (Handle != IntPtr.Zero)
            {
                CL10.ReleaseSampler(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}