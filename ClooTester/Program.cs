using System;
using System.Runtime.InteropServices;
using Cloo;
using OpenTK.Compute.CL10;

namespace ClooTester
{
    class Program
    {
        /*
        void FloatAdd()
        {
            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, null, null );
            ComputeProgram program = new ComputeProgram( context, vectorAdd );
            program.Build( null, null, null, IntPtr.Zero );
            ComputeKernel kernel = program.CreateKernel( "floatAdd" );            
            float a = 3;
            float b = 2;
            //float c = 0;
            //GCHandle gcHandle = GCHandle.Alloc( c, GCHandleType.Pinned );
            ComputeBuffer<float> c = new ComputeBuffer<float>( context, MemFlags.MemWriteOnly, 1 );
            kernel.SetValueArg( 0, a );
            kernel.SetValueArg( 1, b );
            //kernel.SetValueArg( 2, gcHandle.AddrOfPinnedObject() );
            kernel.SetMemoryArg( 2, c );
            ComputeJobQueue queue = new ComputeJobQueue( context, context.Devices[ 0 ], ( CommandQueueFlags )0 );
            queue.Execute( kernel, new int[] { 1 }, null, null );
            float[] result = queue.Read( c, true, 0, 1, null );
        }
        */

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new InfoPrinter().Run();
            new BinaryPrinter().Run();
            new MemoryMapper().Run();
            new VectorAdd().Run();

            Console.ReadKey();
        }
    }
}
