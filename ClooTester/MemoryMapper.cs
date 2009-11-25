using System;
using System.Runtime.InteropServices;
using Cloo;
using OpenTK.Compute.CL10;

namespace ClooTester
{
    public class MemoryMapper: AbstractTester
    {
        public MemoryMapper()
            : base( "Memory Object mapping" )
        { }

        public override void Run()
        {
            StartRun();

            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, null, null );
            ComputeJobQueue queue = new ComputeJobQueue( context, context.Devices[ 0 ], ( CommandQueueFlags )0 );

            Console.WriteLine( "Original content:" );

            Random rand = new Random();
            int count = 6;
            long[] bufferContent = new long[ count ];
            for( int i = 0; i < count; i++ )
            {
                bufferContent[ i ] = ( long )( rand.NextDouble() * long.MaxValue );
                Console.WriteLine( "\t" + bufferContent[ i ] );
            }

            ComputeBuffer<long> buffer = new ComputeBuffer<long>( context, MemFlags.MemCopyHostPtr, bufferContent );
            IntPtr mappedPtr = queue.Map( buffer, true, MapFlags.MapRead, 0, bufferContent.Length, null );

            Console.WriteLine( "Mapped content:" );

            for( int i = 0; i < bufferContent.Length; i++ )
            {
                IntPtr ptr = new IntPtr( mappedPtr.ToInt64() + i * sizeof( long ) );
                Console.WriteLine( "\t" + Marshal.ReadInt64( ptr ) );
            }

            queue.Unmap( buffer, ref mappedPtr, null );

            EndRun();
        }
    }
}