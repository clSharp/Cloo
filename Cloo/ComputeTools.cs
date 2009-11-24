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
using System.Globalization;
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeTools
    {
        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void CheckError( int errorCode )
        {            
            CheckError( ( ErrorCode )errorCode );
        }

        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void CheckError( ErrorCode errorCode )
        {
            switch( errorCode )
            {
                case ErrorCode.Success:
                    return;
                default:
                    throw new ComputeException( errorCode );
            }
        }

        internal static IntPtr[] ConvertArray( int[] array )
        {
            if( array == null ) return null;

            NumberFormatInfo nfi = new NumberFormatInfo();

            IntPtr[] result = new IntPtr[ array.Length ];
            for( int i = 0; i < array.Length; i++ )
                result[ i ] = new IntPtr( array[ i ] );
            return result;
        }
    }
}
