using Cloo;
using System;
using OpenTK.Compute.CL10;

namespace ClooTester
{
    class VectorAdd: AbstractTester
    {
        public VectorAdd()
            : base( "VectorAdd" )
        { }

        protected override void RunInternal()
        {
            ComputeContext.PropertiesDescriptor pd = new ComputeContext.PropertiesDescriptor( ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." ) );

            ComputeContext context = new ComputeContext( ComputeDeviceTypeFlags.Default, pd, null );
            ComputeProgram program = new ComputeProgram( context, vectorAddKernel );
            program.Build( null, null, null, IntPtr.Zero );
            ComputeKernel kernel = program.CreateKernel( "vectorAdd" );

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

            ComputeBuffer<float> a = new ComputeBuffer<float>( context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPtr, arrA );
            ComputeBuffer<float> b = new ComputeBuffer<float>( context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPtr, arrB );
            ComputeBuffer<float> c = new ComputeBuffer<float>( context, ComputeMemoryFlags.WriteOnly, arrC.Length );

            kernel.SetMemoryArg( 0, a );
            kernel.SetMemoryArg( 1, b );
            kernel.SetMemoryArg( 2, c );

            ComputeCommandQueue queue = new ComputeCommandQueue( context, context.Devices[ 0 ], ComputeCommandQueueFlags.Profiling );
            queue.Execute( kernel, null, new long[] { count }, null, null );

            arrC = queue.Read( c, true, 0, count, null );
            for( int i = 0; i < count; i++ )
            {
                Console.WriteLine( "{0} + {1} = {2}", arrA[ i ], arrB[ i ], arrC[ i ] );
            }
        }
    }
}