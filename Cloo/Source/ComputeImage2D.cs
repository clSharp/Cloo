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
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL 2D image.
    /// </summary>
    /// <seealso cref="ComputeImage"/>
    public class ComputeImage2D : ComputeImage
    {
        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeImage2D</c>.
        /// </summary>
        /// <param name="context"> A valid <c>ComputeContext</c> in which the <c>ComputeImage2D</c> is to be created. </param>
        /// <param name="flags"> A bit-field that is used to specify allocation and usage information about the <c>ComputeImage2D</c>. </param>
        /// <param name="format"> A structure that describes the format properties of the <c>ComputeImage2D</c>. </param>
        /// <param name="width"> The width of the <c>ComputeImage2D</c> in pixels. </param>
        /// <param name="height"> The height of the <c>ComputeImage2D</c> in pixels. </param>
        /// <param name="rowPitch"> The size in bytes of each row of elements of the <c>ComputeImage2D</c>. If left zero, OpenCL will compute the proper value based on <c>ComputeImage.Width</c> and <c>ComputeImage.ElementSize</c>. </param>
        /// <param name="data"> The data to initialize the <c>ComputeImage2D</c>. Can be <c>IntPtr.Zero</c>. </param>
        public ComputeImage2D(ComputeContext context, ComputeMemoryFlags flags, ComputeImageFormat format, int width, int height, long rowPitch, IntPtr data)
            : base(context, flags)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateImage2D(
                    context.Handle,
                    flags,
                    &format,
                    new IntPtr(width),
                    new IntPtr(height),
                    new IntPtr(rowPitch),
                    data,
                    &error);
                ComputeException.ThrowOnError(error);

                Init();
            }
        }

        private ComputeImage2D(IntPtr handle, ComputeContext context, ComputeMemoryFlags flags)
            : base(context, flags)
        {
            Handle = handle;

            Init();
        }

        #endregion

        #region Public methods

        public static ComputeImage2D CreateFromGLRenderbuffer(ComputeContext context, ComputeMemoryFlags flags, int renderbufferId)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                IntPtr image = CL10.CreateFromGLRenderbuffer(
                    context.Handle,
                    flags,
                    renderbufferId,
                    &error);
                ComputeException.ThrowOnError(error);

                return new ComputeImage2D(image, context, flags);
            }
        }

        public static ComputeImage2D CreateFromGLTexture2D(ComputeContext context, ComputeMemoryFlags flags, int textureTarget, int mipLevel, int textureId)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                IntPtr image = CL10.CreateFromGLTexture2D(
                    context.Handle,
                    flags,
                    textureTarget,
                    mipLevel,
                    textureId,
                    &error);
                ComputeException.ThrowOnError(error);

                return new ComputeImage2D(image, context, flags);
            }
        }

        /// <summary>
        /// Gets a collection of supported 2D <c>ComputeImage</c> formats with the given context.
        /// </summary>
        /// <param name="context">A valid OpenCL context on which the <c>ComputeImage</c> object(s) will be created.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information about the <c>ComputeImage</c> object(s) that will be created.</param>
        public static ICollection<ComputeImageFormat> GetSupportedFormats(ComputeContext context, ComputeMemoryFlags flags)
        {
            return GetSupportedFormats(context, flags, ComputeMemoryType.Image2D);
        }

        #endregion
    }
}