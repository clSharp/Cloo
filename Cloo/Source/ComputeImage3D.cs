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
    using System.Collections.ObjectModel;
    using Cloo.Bindings;

    public class ComputeImage3D: ComputeImage
    {
        #region Constructors

        /// <summary>
        /// Creates a new 3D image.
        /// </summary>
        /// <param name="context">A valid OpenCL context on which the image object is to be created.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information about the image.</param>
        /// <param name="format">A structure that describes the format properties of the image.</param>
        /// <param name="width">Width of the image in pixels.</param>
        /// <param name="height">Height of the image in pixels.</param>
        /// <param name="depth">Depth of the image in pixels.</param>
        /// <param name="rowPitch">The scan-line pitch in bytes.</param>
        /// <param name="slicePitch">The size in bytes of each 2D slice in the 3D image.</param>
        /// <param name="data">The image data that may be already allocated by the application.</param>
        public ComputeImage3D( ComputeContext context, ComputeMemoryFlags flags, ComputeImageFormat format, int width, int height, int depth, long rowPitch, long slicePitch, IntPtr data )
            : base( context, flags )
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateImage3D(
                    context.Handle,
                    flags,
                    &format,
                    new IntPtr( width ),
                    new IntPtr( height ),
                    new IntPtr( depth ),
                    new IntPtr( rowPitch ),
                    new IntPtr( slicePitch ),
                    data,
                    out error );
                ComputeException.ThrowOnError( error );
            }

            Init();
        }

        private ComputeImage3D( IntPtr handle, ComputeContext context, ComputeMemoryFlags flags )
            : base( context, flags )
        {
            Handle = handle;

            Init();
        }

        #endregion

        #region Public methods

        public static ComputeImage3D CreateFromGLTexture3D( ComputeContext context, ComputeMemoryFlags flags, int textureTarget, int mipLevel, int textureId )
        {
            IntPtr image = IntPtr.Zero;
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                image = CL10.CreateFromGLTexture3D(
                    context.Handle,
                    flags,
                    textureTarget,
                    mipLevel,
                    textureId,
                    out error );
                ComputeException.ThrowOnError( error );
            }
            return new ComputeImage3D( image, context, flags );
        }

        /// <summary>
        /// Gets a collection of supported 3D image formats with the given context.
        /// </summary>
        /// <param name="context">A valid OpenCL context on which the image object(s) will be created.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information about the image object(s) that will be created.</param>
        public static ICollection<ComputeImageFormat> GetSupportedFormats( ComputeContext context, ComputeMemoryFlags flags )
        {
            return GetSupportedFormats( context, flags, ComputeMemoryType.Image3D );
        }

        #endregion
    }
}