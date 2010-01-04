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
using OpenTK.Cloo.CL10;

namespace Cloo
{
    public class ComputeImage3D: ComputeMemory
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
        /// <param name="slicePitch">The count in bytes of each 2D slice in the 3D image.</param>
        /// <param name="data">The image data that may be already allocated by the application.</param>
        public ComputeImage3D( ComputeContext context, MemFlags flags, ImageFormat format, int width, int height, int depth, int rowPitch, int slicePitch, IntPtr data )
            : base( context, flags )
        {
            int error = ( int )ErrorCode.Success;
            unsafe
            {
                Handle = CL.CreateImage3D( context.Handle, flags, &format, ( IntPtr )width, ( IntPtr )height, ( IntPtr )depth, ( IntPtr )rowPitch, ( IntPtr )slicePitch, data, &error );
            }
            ComputeException.ThrowIfError( error );

            byteCount = ( long )GetInfo<MemInfo, IntPtr>( MemInfo.MemSize, CL.GetMemObjectInfo );
        }

        protected ComputeImage3D( ComputeContext context, MemFlags flags )
            : base( context, flags )
        { }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a collection of supported 3D image formats with the given context.
        /// </summary>
        /// <param name="context">A valid OpenCL context on which the image object(s) will be created.</param>
        /// <param name="flags">A bit-field that is used to specify allocation and usage information about the image object(s) that will be created.</param>
        public static ICollection<ImageFormat> GetSupportedFormats( ComputeContext context, MemFlags flags )
        {
            return GetSupportedFormats( context, flags, MemObjectType.MemObjectImage3d );
        }

        #endregion
    }
}
