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

using System;
using System.Windows.Forms;
using Cloo;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace Clootils
{
    public class Program
    {
        [DllImport( "kernel32" )]
        static extern bool AllocConsole();

        [DllImport( "kernel32.dll" )]
        public static extern bool FreeConsole();

        [STAThread]
        public static void Main()
        {
            bool runningWin32NT = ( Environment.OSVersion.Platform == PlatformID.Win32NT ) ? true : false;
            bool consoleAllocated = false;

            try
            {
                if( runningWin32NT ) consoleAllocated = AllocConsole();
                Application.Run( new MainForm() );                
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString(), "Clootils Error" );
            }
            
            if( runningWin32NT && consoleAllocated ) FreeConsole();
        }
    }
}