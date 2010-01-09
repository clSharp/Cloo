using System;
using System.Text;
using Cloo;

namespace ClooTester
{
    class BinaryPrinter: AbstractTester
    {
        public BinaryPrinter()
            : base( "Program binary" )
        { }

        protected override void RunInternal()
        {
            ComputeContextProperties pd = new ComputeContextProperties( ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." ) );
            ComputeContext context = new ComputeContext( ComputeDeviceTypeFlags.Default, pd, null );
            ComputeProgram program = new ComputeProgram( context, vectorAddKernel );
            program.Build( null, null, null, IntPtr.Zero );
            Encoding encoding = new ASCIIEncoding();
            Console.WriteLine( encoding.GetString( program.Binaries[ 0 ] ) );
        }
    }
}