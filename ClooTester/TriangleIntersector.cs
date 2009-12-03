using System;
using System.Diagnostics;
using Cloo;
using OpenTK;
using OpenTK.Compute.CL10;
using System.Runtime.InteropServices;

namespace ClooTester
{
    class TriangleIntersector: AbstractTester
    {
        string kernelSource = @"
kernel void intersect(
    float4 dir,
    global float4 * pointA,
    global float4 * pointB,
    global float4 * pointC,
    global float *  hits )
{

int index = get_global_id(0);

hits[index] = 0;
float4 EdgeAB = pointB[index] - pointA[index];
float4 EdgeAC = pointC[index] - pointA[index];
float4 Normal = normalize( cross( EdgeAB, EdgeAC ) );
float dotABAB = dot( EdgeAB, EdgeAB );
float dotABAC = dot( EdgeAB, EdgeAC );
float dotACAC = dot( EdgeAC, EdgeAC );

float Denominator = dotABAC * dotABAC - dotABAB * dotACAC;

if( Denominator == 0 ) return;

// cosine between ray direction and triangle normal
float cosDirNorm = dot( dir, Normal );

// backface culling
if( cosDirNorm >= 0 ) return;

// distance from eye
float distance = dot( pointA[index], Normal ) / cosDirNorm;

// intersection behind camera
if( distance < 0 ) return;

float4 point = distance * dir;

float4 w = point - pointA[index];

float uw = dot( EdgeAB, w );
float vw = dot( EdgeAC, w );

float s = ( dotABAC * vw - dotACAC * uw ) / Denominator;
if( s < 0 ) return;

float t = ( dotABAC * uw - dotABAB * vw ) / Denominator;
if( t < 0 ) return;

if( ( s + t ) > 1 ) return;

//!!!!found intersection!!!!
hits[ index ] = distance;
}
";
        public TriangleIntersector()
            : base( "Triangle intersection" )
        { }

        public override void Run()
        {
            StartRun();

            ComputeContext context = new ComputeContext( DeviceTypeFlags.DeviceTypeDefault, null, null );
            ComputeProgram program = new ComputeProgram( context, kernelSource );

            try
            {
                program.Build( context.Devices, null, null, IntPtr.Zero );
            }
            catch( ComputeException e )
            {
                Console.WriteLine( program.GetBuildLog( context.Devices[ 0 ] ) );
            }

            ComputeKernel kernel = program.CreateKernel( "intersect" );
            ComputeJobQueue queue = new ComputeJobQueue( context, context.Devices[ 0 ], ( CommandQueueFlags )0 );

            Random rand = new Random();

            int count = 65535;
            Vector4[] arrA = new Vector4[ count ];
            Vector4[] arrB = new Vector4[ count ];
            Vector4[] arrC = new Vector4[ count ];

            for( int i = 0; i < count; i++ )
            {
                arrA[ i ] = new Vector4(
                    ( float )rand.NextDouble(),
                    ( float )rand.NextDouble(),
                    -1,
                    0 );

                arrB[ i ] = new Vector4(
                    ( float )rand.NextDouble(),
                    ( float )rand.NextDouble(),
                    -1,
                    0 );

                arrC[ i ] = new Vector4(
                    ( float )rand.NextDouble(),
                    ( float )rand.NextDouble(),
                    -1,
                    0 );
            }

            Vector4 dir = new Vector4( 0.5f, 0.5f, -1, 0 );


            ComputeBuffer<Vector4> pointA = new ComputeBuffer<Vector4>( context, MemFlags.MemReadOnly | MemFlags.MemCopyHostPtr, arrA );
            ComputeBuffer<Vector4> pointB = new ComputeBuffer<Vector4>( context, MemFlags.MemReadOnly | MemFlags.MemCopyHostPtr, arrB );
            ComputeBuffer<Vector4> pointC = new ComputeBuffer<Vector4>( context, MemFlags.MemReadOnly | MemFlags.MemCopyHostPtr, arrC );
            ComputeBuffer<float> hits = new ComputeBuffer<float>( context, MemFlags.MemWriteOnly, count );

            kernel.SetValueArg( 0, dir );
            kernel.SetMemoryArg( 1, pointA );
            kernel.SetMemoryArg( 2, pointB );
            kernel.SetMemoryArg( 3, pointC );
            kernel.SetMemoryArg( 4, hits );

            Stopwatch clTime = new Stopwatch();
            clTime.Start();
            queue.Execute( kernel, new int[] { count }, null, null );
            clTime.Stop();

            float[] clHits = queue.Read( hits, true, 0, count, null );

            Stopwatch csTime = new Stopwatch();
            float[] csHits = new float[ count ];
            csTime.Start();
            for( int i = 0; i < count; i++ )
                intersect( dir, arrA, arrB, arrC, csHits, i );
            csTime.Stop();

            /*
            for( int i = 0; i < count; i++ )
                Console.WriteLine( "{0}, {1}", clHits[ i ], csHits[ i ] );
            */

            Console.WriteLine( "Cloo ticks: {0}, \t\tmilliseconds: {1}", clTime.ElapsedTicks, clTime.ElapsedMilliseconds );
            Console.WriteLine( ".NET ticks: {0}, \t\tmilliseconds: {1}", csTime.ElapsedTicks, csTime.ElapsedMilliseconds );

            EndRun();
        }

        private void intersect(
            Vector4 dir,
            Vector4[] pointA,
            Vector4[] pointB,
            Vector4[] pointC,
            float[] hits,
            int index )
        {

            Vector4 EdgeAB = pointB[ index ] - pointA[ index ];
            Vector4 EdgeAC = pointC[ index ] - pointA[ index ];
            Vector4 Normal = Vector4.Normalize( new Vector4( Vector3.Cross( EdgeAB.Xyz, EdgeAC.Xyz ), 0 ) );
            float dotABAB = Vector4.Dot( EdgeAB, EdgeAB );
            float dotABAC = Vector4.Dot( EdgeAB, EdgeAC );
            float dotACAC = Vector4.Dot( EdgeAC, EdgeAC );

            float Denominator = dotABAC * dotABAC - dotABAB * dotACAC;
            
            if( Denominator == 0 ) return;

            // cosine between ray direction and triangle normal
            float cosDirNorm = Vector4.Dot( dir, Normal );            

            // backface culling
            if( cosDirNorm >= 0 ) return;

            // distance from eye
            float distance = Vector4.Dot( pointA[ index ], Normal ) / cosDirNorm;

            // intersection behind camera
            if( distance < 0 ) return;

            Vector4 point = distance * dir;

            Vector4 w = point - pointA[ index ];

            float uw = Vector4.Dot( EdgeAB, w );
            float vw = Vector4.Dot( EdgeAC, w );

            float s = ( dotABAC * vw - dotACAC * uw ) / Denominator;
            if( s < 0 ) return;

            float t = ( dotABAC * uw - dotABAB * vw ) / Denominator;
            if( t < 0 ) return;

            if( ( s + t ) > 1 ) return;

            //!!!!found intersection!!!!
            hits[ index ] = distance;
        }
    }
}
