using Cloo;
using OpenTK.Compute.CL10;
namespace ClooTester
{
    public class DummyTest: AbstractTester
    {
        public DummyTest()
            : base( "Dummy Test" )
        { }

        protected override void RunInternal()
        {
            /*
            ComputePlatform currentPlatform = null;
            //find our platform
            foreach( ComputePlatform platform in ComputePlatform.Platforms )
            {
                System.Console.WriteLine( "VendorName = {0}", platform.Vendor );
                if( platform.Name.Equals( "ATI Stream" ) )
                {
                    currentPlatform = platform;
                }
                //break;
            }

            ComputeContext.PropertiesDescriptor pd = new ComputeContext.PropertiesDescriptor( ComputePlatform.Platforms[ 0 ] );

            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, pd, null );*/

            ComputePlatform currentPlatform = null;
            //find our platform
            foreach( ComputePlatform platform in ComputePlatform.Platforms )
            {
                System.Console.WriteLine( "VendorName = {0}", platform.Vendor );
                if( platform.Vendor.Equals( "Advanced Micro Devices, Inc." ) )
                {
                    currentPlatform = platform;
                }
                //break;
            }

            ComputeContext.PropertiesDescriptor pd = new ComputeContext.PropertiesDescriptor( currentPlatform );

            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, pd, null );
        }
    }
}