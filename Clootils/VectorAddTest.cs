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
using Cloo;

namespace Clootils
{
    class VectorAddTest: AbstractTest
    {
        private string kernelSource = @"
kernel void VectorAdd(
    global read_only float* a,
    global read_only float* b,
    global write_only float* c )
{
    int index = get_global_id(0);
    c[index] = a[index] + b[index];
}
";
        public VectorAddTest()
            : base( "VectorAdd Test" )
        { }

        protected override void RunInternal()
        {
            ComputeContextPropertyList cpl = new ComputeContextPropertyList( ComputePlatform.Platforms[ 0 ] );

            ComputeContext context = new ComputeContext( ComputeDeviceTypes.Default, cpl, null, IntPtr.Zero );
            ComputeProgram program = new ComputeProgram( context, new string[]{ kernelSource } );
            program.Build( null, null, null, IntPtr.Zero );
            ComputeKernel kernel = program.CreateKernel( "VectorAdd" );

            int count = 10;
            float[] arrA = new float[ count ];
            float[] arrB = new float[ count ];
            float[] arrC = new float[ count ];

            Random rand = new Random();

            for( int i = 0; i < count; i++ )
            {
                arrA[ i ] = ( float )( rand.NextDouble() * 100 );
                arrB[ i ] = ( float )( rand.NextDouble() * 100 );
            }

            ComputeBuffer<float> a = new ComputeBuffer<float>( context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, arrA );
            ComputeBuffer<float> b = new ComputeBuffer<float>( context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, arrB );
            ComputeBuffer<float> c = new ComputeBuffer<float>( context, ComputeMemoryFlags.WriteOnly, arrC.Length );

            kernel.SetMemoryArgument( 0, a );
            kernel.SetMemoryArgument( 1, b );
            kernel.SetMemoryArgument( 2, c );

            ComputeCommandQueue queue = new ComputeCommandQueue( context, context.Devices[ 0 ], ComputeCommandQueueFlags.None );
            queue.Execute( kernel, null, new long[] { count }, null, null );

            arrC = queue.Read( c, true, 0, count, null );
            for( int i = 0; i < count; i++ )
            {
                Console.WriteLine( "{0} + {1} = {2}", arrA[ i ], arrB[ i ], arrC[ i ] );
            }
        }
    }
}