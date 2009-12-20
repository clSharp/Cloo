using System;
using System.Runtime.InteropServices;
using Cloo;
using OpenTK.Compute.CL10;

namespace ClooTester
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new BinaryPrinter().Run();
            new MemoryMapper().Run();
            new VectorAdd().Run();
            new TriangleIntersector().Run();
            new KernelArgsTester().Run();

            Console.ReadKey();
        }
    }
}
