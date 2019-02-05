using System;
using System.Diagnostics;
using System.Linq;
using Cloo.Extensions;

namespace ClootilsNetCore
{
    class Program
    {
        public static int Main(string[] args)
        {
            const int DataSize = 10000000;
            var sw  = new Stopwatch();

            // OpenCL part
            var deviceNames = ClooExtensions.GetDeviceNames();
            int[] primes;

            for (int deviceId = 0; deviceId < deviceNames.Length; deviceId++)
            {
                primes = Enumerable.Range(2, DataSize).ToArray();

                sw.Restart();
                primes.ClooForEach(IsPrime, null, (i, n, v) => i==deviceId);
                sw.Stop();
                Console.WriteLine($"OpenCL[{deviceId}] ({deviceNames[deviceId]}): {sw.ElapsedMilliseconds}ms");

                Console.WriteLine(string.Join(", ", primes.Where(n => n != 0).Where(n => n != 0).Take(20)));
            }                       
            
            // CPU part
            primes = Enumerable.Range(2, DataSize).ToArray();

            sw.Restart();
            for (int id=0; id<primes.Length; id++)
            {
                var tmp = primes[id];
                int upperl=(int)Math.Sqrt((float)tmp);
                for (int i=2;i<=upperl;i++)
                {
                    if(tmp%i==0)
                    {
                        primes[id]=0;
                        break;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"CPU: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine(string.Join(", ", primes.Where(n => n != 0).Where(n => n != 0).Take(20)));

            Console.Write($"Done, press any key to exit...");
            Console.ReadKey();

            return 0;
        }

        private static string IsPrime =
@"
kernel void GetIfPrime(global int* message)
{
    int index = get_global_id(0);
    int tmp = message[index];
    int upperl=(int)sqrt((float)tmp);
    for(int i=2;i<=upperl;i++)
    {
        if(tmp%i==0)
        {
            message[index]=0;
            return;
        }
    }
}";

    }
}
