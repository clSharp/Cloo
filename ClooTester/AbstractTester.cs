using System;

namespace ClooTester
{
    public abstract class AbstractTester
    {
        private readonly string name;

        protected string vectorAddKernel = @"
            kernel void
            vectorAdd(global const float * a,
                      global const float * b,
                      global       float * c)
            {
                // Vector element index
                int nIndex = get_global_id(0);
                c[nIndex] = a[nIndex] + b[nIndex];
            }";
        
        protected AbstractTester( string name )
        {
            this.name = name;
        }

        protected void StartRun()
        {
            Console.WriteLine( "\n------------------| Start {0} |------------------", name );
        }

        public abstract void Run();

        protected void EndRun()
        {
            Console.WriteLine( "-------------------| End {0} |-------------------\n", name );
        }
    }
}