using System;
using System.Runtime.InteropServices;
using Cloo;

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
            new DummyTest().Run();
            new BinaryPrinter().Run();
            new MemoryMapper().Run();
            new VectorAdd().Run();
            new ImageTest().Run();
            new KernelArgsTester().Run();

            Console.ReadKey();
        }
    }
}
