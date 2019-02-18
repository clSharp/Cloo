using System;
using System.IO;
using System.Linq;
using Cloo;

namespace Clootils
{
    class GeneratePrimesExample : IExample
    {
        ComputeProgram program;

        string clProgramSource = @"
        kernel void GetIfPrime(global int* message)
    {
        int index = get_global_id(0);
        int tmp = message[index];
        int upperl = (int)sqrt((float)tmp);
        for (int i = 2; i <= upperl; i++)
        {
            if (tmp % i == 0)
            {
                message[index] = 0;
                return;
            }
        }
    }";

        public string Name
        {
            get { return "Generate prime numbers"; }
        }

        public string Description
        {
            get { return "Demonstrates how to generate prime numbers using the GPU"; }
        }

        public void Run(ComputeContext context, TextWriter log)
        {
            try
            {
                // Create the arrays and fill them with random data.
                var count = 1000000;
                var numbers = Enumerable.Range(2, count).ToArray();
                
                // Create the input buffers and fill them with data from the arrays.
                // Access modifiers should match those in a kernel.
                // CopyHostPointer means the buffer should be filled with the data provided in the last argument.
                ComputeBuffer<int> numbersBuffer = new ComputeBuffer<int>(context, ComputeMemoryFlags.ReadOnly | ComputeMemoryFlags.CopyHostPointer, numbers);
                
                // Create and build the opencl program.
                program = new ComputeProgram(context, clProgramSource);
                program.Build(null, null, null, IntPtr.Zero);

                // Create the kernel function and set its arguments.
                ComputeKernel kernel = program.CreateKernel("GetIfPrime");
                kernel.SetMemoryArgument(0, numbersBuffer);
                
                // Create the event wait list. An event list is not really needed for this example but it is important to see how it works.
                // Note that events (like everything else) consume OpenCL resources and creating a lot of them may slow down execution.
                // For this reason their use should be avoided if possible.
                ComputeEventList eventList = new ComputeEventList();

                // Create the command queue. This is used to control kernel execution and manage read/write/copy operations.
                ComputeCommandQueue commands = new ComputeCommandQueue(context, context.Devices[0], ComputeCommandQueueFlags.None);

                // Execute the kernel "count" times. After this call returns, "eventList" will contain an event associated with this command.
                // If eventList == null or typeof(eventList) == ReadOnlyCollection<ComputeEventBase>, a new event will not be created.
                commands.Execute(kernel, null, new long[] { count }, null, eventList);

                // Read back the results. If the command-queue has out-of-order execution enabled (default is off), ReadFromBuffer 
                // will not execute until any previous events in eventList (in our case only eventList[0]) are marked as complete 
                // by OpenCL. By default the command-queue will execute the commands in the same order as they are issued from the host.
                // eventList will contain two events after this method returns.
                var primes = new int[numbers.Length];
                commands.ReadFromBuffer(numbersBuffer, ref primes, false, eventList);

                // A blocking "ReadFromBuffer" (if 3rd argument is true) will wait for itself and any previous commands
                // in the command queue or eventList to finish execution. Otherwise an explicit wait for all the opencl commands 
                // to finish has to be issued before "arrC" can be used. 
                // This explicit synchronization can be achieved in two ways:

                // 1) Wait for the events in the list to finish,
                //eventList.Wait();

                // 2) Or simply use
                commands.Finish();

                // Print the results to a log/console.
                log.WriteLine("Calculate tons of primes - up to " + count + ": " + string.Join(", ", primes.Take(30).Where(n => n != 0).Where(n => n != 0))
                              + ", ... ," + string.Join(", ", primes.Skip(primes.Length - 30).Where(n => n != 0))); 

                // cleanup commands
                commands.Dispose();

                // cleanup events
                foreach (ComputeEventBase eventBase in eventList)
                {
                    eventBase.Dispose();
                }
                eventList.Clear();

                // cleanup kernel
                kernel.Dispose();

                // cleanup program
                program.Dispose();

                // cleanup buffers
                numbersBuffer.Dispose();
            }
            catch (Exception e)
            {
                log.WriteLine(e.ToString());
            }
        }
    }
}