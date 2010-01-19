using System;
using System.Collections.Generic;
using Cloo;
using Cloo.Bindings;

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
            ComputeContextPropertyList pd = new ComputeContextPropertyList( currentPlatform );
            ComputeContext context = new ComputeContext( ComputeDeviceTypes.Default, pd, null, IntPtr.Zero );
            unsafe
            {
                Clx.GetGLContextInfoKHR( null, ( ComputeGLContextInfo )0, IntPtr.Zero, IntPtr.Zero, ( IntPtr* )IntPtr.Zero );
            }
        }
    }
}