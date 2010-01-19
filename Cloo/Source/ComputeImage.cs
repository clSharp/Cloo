#region License

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

#endregion

namespace Cloo
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Cloo.Bindings;

    public abstract class ComputeImage: ComputeMemory
    {
        #region Constructors

        protected ComputeImage( ComputeContext context, ComputeMemoryFlags flags )
            : base( context, flags )
        { }

        #endregion

        #region Protected methods

        protected static ICollection<ComputeImageFormat> GetSupportedFormats( ComputeContext context, ComputeMemoryFlags flags, ComputeMemoryType type )
        {
            int formatCountRet = 0;
            ComputeErrorCode error;
            unsafe
            {
                error = CL10.GetSupportedImageFormats( context.Handle, flags, type, 0, null, &formatCountRet );
                ComputeException.ThrowOnError( error );
            }

            ComputeImageFormat[] formats = new ComputeImageFormat[ formatCountRet ];
            unsafe
            {
                fixed( ComputeImageFormat* formatsPtr = formats )
                {
                    error = CL10.GetSupportedImageFormats( context.Handle, flags, type, formatCountRet, formatsPtr, null );
                    ComputeException.ThrowOnError( error );
                }
            }            

            return new Collection<ComputeImageFormat>( formats );
        }

        #endregion
    }
}