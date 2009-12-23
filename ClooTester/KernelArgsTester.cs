using System;
using System.Collections.Generic;
using Cloo;
using OpenTK;
using OpenTK.Compute.CL10;

namespace ClooTester
{
    public class KernelArgsTester: AbstractTester
    {
        /* nVidia compiled code
        string argsKernel = @"
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

        //2 3 5 7 8 9 11 12 13 15 16
        string argsKernel = @"
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

        public KernelArgsTester()
            : base( "Kernel args test" )
        {
        }

        protected override void RunInternal()
        {
            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, null, null );
            ComputeBuffer<Vector4> result = new ComputeBuffer<Vector4>( context, MemFlags.MemReadWrite, 1 );

            ComputeProgram program = new ComputeProgram( context, argsKernel );
            program.Build( null, null, null, IntPtr.Zero );
            Console.WriteLine( "Program successfully built." );

            List<ComputeKernel> kernels = new List<ComputeKernel>( program.CreateAllKernels() );
            Console.WriteLine( "Kernels successfully created." );
        }
    }
}