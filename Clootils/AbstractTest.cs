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

namespace Clootils
{
    public abstract class AbstractTest
    {
        private readonly string name;
        
        protected AbstractTest( string name )
        {
            this.name = name;
        }

        public void Run()
        {
            StartRun();
            try
            {
                RunInternal();
            }
            catch( Exception e )
            {
                Console.WriteLine( e.ToString() );
            }
            EndRun();
        }

        protected void StartRun()
        {
            Console.WriteLine( "\n------------------| Start {0} |------------------", name );
        }

        protected abstract void RunInternal();

        protected void EndRun()
        {
            Console.WriteLine( "-------------------| End {0} |-------------------\n", name );
        }
    }
}