using System.Collections.Generic;
using Cloo;

namespace ClooTester
{
    public class DummyTest: AbstractTester
    {
        public DummyTest()
            : base( "Dummy Test" )
        { }

        protected override void RunInternal()
        {
            ComputePlatform currentPlatform = ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." );
            ComputeContextProperties pd = new ComputeContextProperties( currentPlatform );
            ComputeContext context = new ComputeContext( ComputeDeviceTypeFlags.Default, pd, null );
            ComputeCommandQueue queue = new ComputeCommandQueue( context, context.Devices[ 0 ], 0 );
            
            List<ComputeEvent> events = new List<ComputeEvent>();
            
            int count = 1000000;
            ComputeBuffer<float> buffA = new ComputeBuffer<float>( context, ComputeMemoryFlags.ReadOnly, new float[ count ] );
            queue.Write( buffA, false, 0, count, new float[ count ], events );            
        }
    }
}