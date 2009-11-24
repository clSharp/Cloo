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
using System.Text;
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeImage2D: ComputeImage3D
    {
        public ComputeImage2D( ComputeContext context, MemFlags flags, ImageFormat format, int width, int height, int rowPitch, IntPtr data )
        {
            this.contxt = context;
            this.memflags = flags;

            int error = ( int )ErrorCode.Success;
            unsafe
            {
                Handle = CL.CreateImage2D( context.Handle, flags, &format, ( IntPtr )width, ( IntPtr )height, ( IntPtr )rowPitch, data, &error );
            }
            ComputeTools.CheckError( error );
        }

        public new static ICollection<ImageFormat> GetSupportedFormats( ComputeContext context, MemFlags flags )
        {
            return GetSupportedFormats( context, flags, MemObjectType.MemObjectImage2d );
        }
    }    
}
