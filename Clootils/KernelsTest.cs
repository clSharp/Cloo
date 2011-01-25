#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

using System;
using System.Collections.Generic;
using Cloo;
using System.IO;
using System.Runtime.InteropServices;

namespace Clootils
{
    public class KernelsTest : TestBase
    {
        static ComputeProgram program;
        static TextWriter log;

        static string kernelSources = @"
    kernel void k1(           float     num ) {}
  //kernel void k2(           sampler_t smp ) {}       // Causes havoc in Nvidia's drivers. This is, however, a valid kernel signature.
  //kernel void k3( read_only image2d_t dem ) {}       // The same.
  //kernel void k4( constant  float *   num ) {}       // Causes InvalidBinary if drivers == 64bit and application == 32 bit.
    kernel void k5( global    float *   num ) {}
    kernel void k6( local     float *   num ) {}
";

        public static void Run(TextWriter log, ComputeContext context)
        {
            StartTest(log, "Kernels test");
            
            try
            {
                KernelsTest.log = log;

                program = new ComputeProgram(context, kernelSources);
                program.Build(null, null, null, IntPtr.Zero);
                log.WriteLine("Program successfully built.");
                program.CreateAllKernels();
                log.WriteLine("Kernels successfully created.");
            }
            catch (Exception e)
            {
                log.WriteLine(e.ToString());
            }

            EndTest(log, "Kernels test");
        }
    }
}