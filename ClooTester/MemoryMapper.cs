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

        protected override void RunInternal()
        {
            ComputeContextProperties pd = new ComputeContextProperties( ComputePlatform.GetByVendor( "Advanced Micro Devices, Inc." ) );

            ComputeContext context = new ComputeContext( ComputeDeviceTypeFlags.Default, pd, null );
            ComputeCommandQueue queue = new ComputeCommandQueue( context, context.Devices[ 0 ], ComputeCommandQueueFlags.Profiling );

            Console.WriteLine( "Original content:" );

            Random rand = new Random();
            int count = 6;
            long[] bufferContent = new long[ count ];
            for( int i = 0; i < count; i++ )
            {
                bufferContent[ i ] = ( long )( rand.NextDouble() * long.MaxValue );
                Console.WriteLine( "\t" + bufferContent[ i ] );
            }

            ComputeBuffer<long> buffer = new ComputeBuffer<long>( context, ComputeMemoryFlags.CopyHostPtr, bufferContent );
            IntPtr mappedPtr = queue.Map( buffer, true, ComputeMemoryMapFlags.Read, 0, bufferContent.Length, null );

            Console.WriteLine( "Mapped content:" );

            for( int i = 0; i < bufferContent.Length; i++ )
            {
                IntPtr ptr = new IntPtr( mappedPtr.ToInt64() + i * sizeof( long ) );
                Console.WriteLine( "\t" + Marshal.ReadInt64( ptr ) );
            }

            queue.Unmap( buffer, ref mappedPtr, null );
        }
    }
}