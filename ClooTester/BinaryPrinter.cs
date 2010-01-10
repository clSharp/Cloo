﻿using System;
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
            ComputeContext context = new ComputeContext( ComputeDeviceTypes.Default, pd, null, IntPtr.Zero );
            ComputeProgram program = new ComputeProgram( context, vectorAddKernel );
            program.Build( null, null, null, IntPtr.Zero );
            Encoding encoding = new ASCIIEncoding();
            Console.WriteLine( encoding.GetString( program.Binaries[ 0 ] ) );
        }
    }
}