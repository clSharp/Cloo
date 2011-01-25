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
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.Globalization;

    /// <summary>
    /// Contains various helper methods.
    /// </summary>
    public class Tools
    {
        #region Public methods

        /*
        /// <summary>
        /// Attempts to convert a PixelFormat to a <see cref="ComputeImageFormat"/>.
        /// </summary>
        /// <param name="format"> The format to convert. </param>
        /// <returns> A <see cref="ComputeImageFormat"/> that matches the specified argument. </returns>
        /// <remarks> Note that only <c>Alpha</c>, <c>Format16bppRgb555</c>, <c>Format16bppRgb565</c> and <c>Format32bppArgb</c> input values are currently supported. </remarks>        
        public static ComputeImageFormat ConvertImageFormat(PixelFormat format)
        {
            switch(format)
            {
                case PixelFormat.Alpha:
                    return new ComputeImageFormat(ComputeImageChannelOrder.A, ComputeImageChannelType.UnsignedInt8);
                case PixelFormat.Format16bppRgb555:
                    return new ComputeImageFormat(ComputeImageChannelOrder.Rgb, ComputeImageChannelType.UNormShort555);
                case PixelFormat.Format16bppRgb565:
                    return new ComputeImageFormat(ComputeImageChannelOrder.Rgb, ComputeImageChannelType.UNormShort565);
                case PixelFormat.Format32bppArgb:
                    return new ComputeImageFormat(ComputeImageChannelOrder.Argb, ComputeImageChannelType.UnsignedInt8);
                default: throw new ArgumentException("Pixel format not supported.");
            }
        }
        */

        /// <summary>
        /// Parses an OpenCL version string.
        /// </summary>
        /// <param name="versionString"> The version string to parse. Must be in the format: <c>Additional substrings[space][major_version].[minor_version][space]Additional substrings</c>. </param>
        /// <param name="substringIndex"> The index of the substring that specifies the OpenCL version. </param>
        /// <returns> A <c>Version</c> instance containing the major and minor version from <paramref name="versionString"/>. </returns>
        public static Version ParseVersionString(String versionString, int substringIndex)
        {
            string[] verstring = versionString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Version(verstring[substringIndex]);
        }

        #endregion

        #region Internal methods

        internal static IntPtr[] ConvertArray(long[] array)
        {
            if (array == null) return null;

            NumberFormatInfo nfi = new NumberFormatInfo();

            IntPtr[] result = new IntPtr[array.Length];
            for (long i = 0; i < array.Length; i++)
                result[i] = new IntPtr(array[i]);
            return result;
        }

        internal static long[] ConvertArray(IntPtr[] array)
        {
            if (array == null) return null;

            NumberFormatInfo nfi = new NumberFormatInfo();

            long[] result = new long[array.Length];
            for (long i = 0; i < array.Length; i++)
                result[i] = array[i].ToInt64();
            return result;
        }

        internal static IntPtr[] ExtractHandles<T>(ICollection<T> computeObjects) where T : ComputeObject
        {
            if (computeObjects == null)
                return new IntPtr[0];

            IntPtr[] result = new IntPtr[computeObjects.Count];
            int i = 0;
            foreach (T computeObj in computeObjects)
            {
                result[i] = computeObj.Handle;
                i++;
            }
            return result;
        }

        #endregion
    }
}