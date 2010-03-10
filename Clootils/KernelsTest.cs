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
using System.Collections.Generic;
using Cloo;

namespace Clootils
{
    public class KernelsTest: AbstractTest
    {
        /* nVidia
        string kernelSources = @"
    kernel void k01(          float     num ) {}
//  kernel void k02(          float *   num ) {}
    kernel void k03(          image3d_t img ) {}
    kernel void k04(          sampler_t smp ) {}

//  kernel void k05( constant float     num ) {}
    kernel void k06( constant float *   num ) {}
//  kernel void k07( constant image3d_t img ) {}
//  kernel void k08( constant sampler_t smp ) {}

//  kernel void k09( global   float     num ) {}
    kernel void k10( global   float *   num ) {}
    kernel void k11( global   image3d_t img ) {}
//  kernel void k12( global   sampler_t smp ) {}

//  kernel void k13( local    float     num ) {}
    kernel void k14( local    float *   num ) {}
//  kernel void k15( local    image3d_t img ) {}
//  kernel void k16( local    sampler_t smp ) {}
";
         */

        //ATi
        string kernelSources = @"
    kernel void k01(          float     num ) {}
//  kernel void k02(          float *   num ) {}
//  kernel void k03(          image3d_t img ) {}
    kernel void k04(          sampler_t smp ) {}

//  kernel void k05( constant float     num ) {}
    kernel void k06( constant float *   num ) {}
//  kernel void k07( constant image3d_t img ) {}
//  kernel void k08( constant sampler_t smp ) {}

//  kernel void k09( global   float     num ) {}
    kernel void k10( global   float *   num ) {}
//  kernel void k11( global   image3d_t img ) {}
//  kernel void k12( global   sampler_t smp ) {}

//  kernel void k13( local    float     num ) {}
    kernel void k14( local    float *   num ) {}
//  kernel void k15( local    image3d_t img ) {}
//  kernel void k16( local    sampler_t smp ) {}
";

        public KernelsTest()
            : base( "Kernels Test" )
        { }

        protected override void RunInternal()
        {            
            ComputeContextPropertyList pd = new ComputeContextPropertyList( ComputePlatform.Platforms[ 0 ] );
            ComputeContext context = new ComputeContext( ComputeDeviceTypes.Default, pd, null, IntPtr.Zero );

            ComputeProgram program = new ComputeProgram( context, new string[]{ kernelSources } );
            program.Build( null, null, notify, IntPtr.Zero );
            Console.WriteLine( "Program successfully built." );

            List<ComputeKernel> kernels = new List<ComputeKernel>( program.CreateAllKernels() );
            Console.WriteLine( "Kernels successfully created." );            
        }

        private void notify( IntPtr programHandle, IntPtr userDataPtr )
        {
            Console.WriteLine( "Program build notification." );
        }
    }
}