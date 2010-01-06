using Cloo;
using OpenTK.Compute.CL10;
using System;
using System.Collections.Generic;
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
            ComputeContext.PropertiesDescriptor pd = new ComputeContext.PropertiesDescriptor( currentPlatform );
            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, pd, null );
            ComputeCommandQueue queue = new ComputeCommandQueue( context, context.Devices[ 0 ], 0 );
            
            List<ComputeEvent> events = new List<ComputeEvent>();
            
            int count = 1000000;
            ComputeBuffer<float> buffA = new ComputeBuffer<float>( context, MemFlags.MemReadOnly, new float[ count ] );
            queue.Write( buffA, false, 0, count, new float[ count ], events );            
        }
    }
}