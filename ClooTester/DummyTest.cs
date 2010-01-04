using Cloo;
using OpenTK.Cloo.CL10;
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

            int count = 100000;
            ComputeBuffer<float> a = new ComputeBuffer<float>( context, 0, count );

            List<ComputeEvent> events = new List<ComputeEvent>();
            queue.Write( a, false, 0, count, new float[ count ], events );
            float[] read = queue.Read( a, false, 0, count, events );
            
            queue.Wait( events );
        }
    }
}