using System;
using System.Collections.Generic;
using Cloo;

namespace ClooTester
{
    public class KernelArgsTester: AbstractTester
    {
        /* nVidia
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

        //ATi
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
            ComputeContextPropertyList cpl = new ComputeContextPropertyList( ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." ) );

            ComputeContext context = new ComputeContext( ComputeDeviceTypes.Default, cpl, null, IntPtr.Zero );

            ComputeProgram program = new ComputeProgram( context, argsKernel );
            program.Build( null, null, new ComputeProgramBuildNotifier( notify ), IntPtr.Zero );
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