using System;
using System.Text;
using Cloo;
using OpenTK.Compute.CL10;
using ClooTester;

namespace ClooTester
{
    class BinaryPrinter: AbstractTester
    {
        public BinaryPrinter()
            : base( "Program binary" )
        { }

        protected override void RunInternal()
        {
            ComputeContext.PropertiesDescriptor pd = new ComputeContext.PropertiesDescriptor( ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." ) );
            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, pd, null );
            ComputeProgram program = new ComputeProgram( context, vectorAddKernel );
            program.Build( null, null, null, IntPtr.Zero );
            Encoding encoding = new ASCIIEncoding();
            Console.WriteLine( encoding.GetString( program.Binaries[ 0 ] ) );
        }
    }
}