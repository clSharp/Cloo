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
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SysIntX3
    {
        #region Fields

        public IntPtr X, Y, Z;

        #endregion

        #region Constructors

        public SysIntX3(SysIntX2 x2, long z)
            : this(x2.X, x2.Y, new IntPtr(z))
        { }

        public SysIntX3(int x, int y, int z)
            : this(new IntPtr(x), new IntPtr(y), new IntPtr(z))
        { }

        public SysIntX3(long x, long y, long z)
            : this(new IntPtr(x), new IntPtr(y), new IntPtr(z))
        { }

        public SysIntX3(IntPtr x, IntPtr y, IntPtr z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        public static SysIntX3 operator *(int scalar, SysIntX3 x3)
        {
            return new SysIntX3(scalar * x3.X.ToInt64(), scalar * x3.Y.ToInt64(), scalar * x3.Z.ToInt64());
        }
    }
}