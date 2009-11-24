using System;
using Cloo;

namespace ClooTester
{
    class InfoPrinter: AbstractTester
    {
        public InfoPrinter()
            : base( "OpenCL platform info" )
        { }

        public override void Run()
        {
            StartRun();

            foreach( ComputePlatform platform in ComputePlatform.Platforms )
            {
                Console.WriteLine(
                    "name:     " + platform.Name + "\n" +
                    "version:  " + platform.Version + "\n" +
                    "profile:  " + platform.Profile + "\n" +
                    "vendor:   " + platform.Vendor + "\n" +
                    "extensions:" );
                foreach( string extension in platform.Extensions )
                    Console.WriteLine( " + " + extension );

                Console.WriteLine( "\ndevices:" );
                foreach( ComputeDevice device in platform.Devices )
                {
                    Console.WriteLine(
                        "\tname:    " + device.Name + "\n" +
                        "\tdriver:  " + device.DriverVersion + "\n" +
                        "\tvendor:  " + device.Vendor );
                    Console.WriteLine( "\textensions:" );
                    foreach( string extension in device.Extensions )
                        Console.WriteLine( "\t + " + extension );
                }
            }

            EndRun();
        }
    }
}
