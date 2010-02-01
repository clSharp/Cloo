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

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    public class ComputeProgram: ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly string[] source;
        private ReadOnlyCollection<byte[]> binaries;
        private string buildOptions;

        #endregion

        #region Properties

        /// <summary>
        /// Return the program binaries for all devices associated with program. he bits returned can be an implementation-specific intermediate representation (a.k.a. IR) or device specific executable bits or both. The decision on which information is returned in the binary is up to the OpenCL implementation.
        /// </summary>
        public ReadOnlyCollection<byte[]> Binaries
        {
            get
            {
                return binaries;
            }
        }

        /// <summary>
        /// Return the build options specified by the options argument when the program is created.
        /// </summary>
        public string BuildOptions
        {
            get
            {
                return buildOptions;
            }
        }

        /// <summary>
        /// Return the context specified when the program object is created
        /// </summary>
        public ComputeContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Return the list of devices associated with the program object. This can be the devices associated with context on which the program object has been created or can be a subset of devices that are specified when a progam object is created from binaries.
        /// </summary>
        public ReadOnlyCollection<ComputeDevice> Devices
        {
            get
            {
                return devices;
            }
        }

        /// <summary>
        /// Return the program source code specified when creating the program. null if program was created from binaries.
        /// </summary>
        public ReadOnlyCollection<string> Source
        {
            get
            {
                return new ReadOnlyCollection<string>( source );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a program for a context, using the source code specified. The devices associated with the program are the devices associated with context.
        /// </summary>
        /// <param name="context">A valid OpenCL context.</param>
        /// <param name="source">The source code for this program.</param>        
        public ComputeProgram( ComputeContext context, string source )
            : this( context, new string[] { source } )
        { }

        /// <summary>
        /// Creates a program for a context, using the source code specified. The devices associated with the program are the devices associated with context.
        /// </summary>
        /// <param name="context">A valid OpenCL context.</param>
        /// <param name="source">The source code for this program.</param>
        public ComputeProgram( ComputeContext context, string[] source )
        {
            IntPtr[] lengths = new IntPtr[ source.Length ];
            for( int i = 0; i < source.Length; i++ )
                lengths[i] = new IntPtr( source[i].Length );

            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                fixed( IntPtr* lengthsPtr = lengths )
                    handle = CL10.CreateProgramWithSource(
                        context.Handle,
                        source.Length,
                        source,                    
                        lengthsPtr,
                        out error );
                ComputeException.ThrowOnError( error );
            }

            this.context = context;
            this.devices = context.Devices;
            this.source = source;
        }

        /// <summary>
        /// Creates a program for a context, using the binaries specified.
        /// </summary>
        /// <param name="context">A valid OpenCL context.</param>
        /// <param name="binaries">The binaries to be assigned to the devices.</param>
        /// <param name="devices">A subset of the devices associated with the context or null for all the devices associated with the context.</param>
        public ComputeProgram( ComputeContext context, IList<byte[]> binaries, IList<ComputeDevice> devices )
        {
            int count = binaries.Count;

            IntPtr[] deviceHandles = ( devices != null ) ?
                Tools.ExtractHandles( devices ) :
                Tools.ExtractHandles( context.Devices );

            IntPtr[] binariesPtrs = new IntPtr[ count ];
            IntPtr[] binariesLengths = new IntPtr[ count ];
            int[] binariesStats = new int[ count ];
            ComputeErrorCode error = ComputeErrorCode.Success;
            GCHandle binariesPtrGCHandle = new GCHandle();
            GCHandle[] binariesGCHandles = new GCHandle[ count ];

            try
            {
                binariesPtrGCHandle = GCHandle.Alloc( binariesPtrs, GCHandleType.Pinned );
                for( int i = 0; i < count; i++ )
                {
                    binariesGCHandles[ i ] = GCHandle.Alloc( binaries[ i ], GCHandleType.Pinned );
                    binariesPtrs[ i ] = binariesGCHandles[ i ].AddrOfPinnedObject();
                    binariesLengths[ i ] = new IntPtr( binaries[ i ].Length );
                }

                unsafe
                {
                    byte** binariesPtr = ( byte** )binariesPtrGCHandle.AddrOfPinnedObject();
                    fixed( IntPtr* binariesLengthsPtr = binariesLengths )
                    fixed( IntPtr* deviceHandlesPtr = deviceHandles )
                    fixed( int* binaryStatusPtr = binariesStats )
                        handle = CL10.CreateProgramWithBinary(
                            context.Handle,
                            count,
                            deviceHandlesPtr,
                            binariesLengthsPtr,
                            binariesPtr,
                            binaryStatusPtr,
                            out error );
                }
            }
            finally
            {
                binariesPtrGCHandle.Free();
                for( int i = 0; i < count; i++ )
                    binariesGCHandles[ i ].Free();
            }

            ComputeException.ThrowOnError( error );

            this.binaries = new ReadOnlyCollection<byte[]>( binaries );
            this.context = context;
            this.devices = new ReadOnlyCollection<ComputeDevice>(
                ( devices != null ) ? devices : context.Devices );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Builds (compiles and links) a program executable from the program source or binary for all the devices or some specific devices in the OpenCL context associated with program.
        /// </summary>
        /// <param name="devices">A list of devices associated with program. If the list is null, the program executable is built for all devices associated with program for which a source or a binary has been loaded.</param>
        /// <param name="options">A set of options for the OpenCL compiler.</param>
        /// <param name="notify">A notification routine. The notification routine is a callback function that an application can register and which will be called when the program executable has been built (successfully or unsuccessfully). If notify is not null, ComputeProgram.Build does not need to wait for the build to complete and can return immediately. If notify is null, ComputeProgram.Build does not return until the build has completed. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe.</param>
        /// <param name="notifyDataPtr">Passed as an argument when notify is called. notifyDataPtr can be IntPtr.Zero. </param>
        public void Build( ICollection<ComputeDevice> devices, string options, ComputeProgramBuildNotifier notify, IntPtr notifyDataPtr )
        {
            IntPtr[] deviceHandles = Tools.ExtractHandles( devices );
            buildOptions = ( options != null ) ? options : "" ;
            IntPtr notifyPtr = ( notify != null ) ? Marshal.GetFunctionPointerForDelegate( notify ) : IntPtr.Zero;

            unsafe
            {
                ComputeErrorCode error;
                fixed( IntPtr* deviceHandlesPtr = deviceHandles )
                error = CL10.BuildProgram(
                    Handle,
                    ( devices != null ) ? devices.Count : 0,
                    ( devices != null ) ? deviceHandlesPtr : null,
                    buildOptions,
                    notifyPtr,
                    notifyDataPtr );
                ComputeException.ThrowOnError( error );
            }

            binaries = GetBinaries();            
        }

        /// <summary>
        /// Creates kernel objects for all kernel functions in program. Kernel objects are not created for any __kernel functions in program that do not have the same function definition across all devices for which a program executable has been successfully built.
        /// </summary>
        public ICollection<ComputeKernel> CreateAllKernels()
        {
            ICollection<ComputeKernel> kernels = new Collection<ComputeKernel>();
            int kernelsCount = 0;
            IntPtr[] kernelHandles;

            unsafe
            {
                ComputeErrorCode error = CL10.CreateKernelsInProgram( Handle, 0, null, &kernelsCount );
                ComputeException.ThrowOnError( error );

                kernelHandles = new IntPtr[ kernelsCount ];
                fixed( IntPtr* kernelHandlesPtr = kernelHandles )
                    error = CL10.CreateKernelsInProgram( 
                        Handle, 
                        kernelsCount, 
                        kernelHandlesPtr, 
                        null );
                ComputeException.ThrowOnError( error );
            }

            for( int i = 0; i < kernelsCount; i++ )
                kernels.Add( new ComputeKernel( kernelHandles[ i ], this ) );

            return kernels;
        }

        /// <summary>
        /// Creates a kernel object for the kernel function specified by the function name.
        /// </summary>
        public ComputeKernel CreateKernel( string functionName )
        {
            return new ComputeKernel( functionName, this );
        }

        /// <summary>
        /// Gets the build log of program for the specified device.
        /// </summary>
        public string GetBuildLog( ComputeDevice device )
        {
            return GetStringInfo<ComputeProgramBuildInfo>(
                device,
                ComputeProgramBuildInfo.BuildLog,
                CL10.GetProgramBuildInfo );
        }

        /// <summary>
        /// Gets the build status of program for the specified device.
        /// </summary>
        public ComputeProgramBuildStatus GetBuildStatus( ComputeDevice device )
        {
            return ( ComputeProgramBuildStatus )GetInfo<ComputeProgramBuildInfo, uint>(
                device,
                ComputeProgramBuildInfo.Status,
                CL10.GetProgramBuildInfo );
        }

        /// <summary>
        /// Gets a string representation of this program.
        /// </summary>
        public override string ToString()
        {
            return "ComputeProgram" + base.ToString();
        }

        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( manual )
            {
                //dispose managed resources
            }

            if( Handle != IntPtr.Zero )
            {
                CL10.ReleaseProgram( Handle );
                handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Private methods

        private ReadOnlyCollection<byte[]> GetBinaries()
        {
            IntPtr[] binaryLengths = GetArrayInfo<ComputeProgramInfo, IntPtr>(
                ComputeProgramInfo.BinarySizes, CL10.GetProgramInfo );

            GCHandle[] binariesGCHandles = new GCHandle[ binaryLengths.Length ];
            IntPtr[] binariesPtrs = new IntPtr[ binaryLengths.Length ];
            IList<byte[]> binaries = new List<byte[]>();

            try
            {
                for( int i = 0; i < binaryLengths.Length; i++ )
                {
                    byte[] binary = new byte[ binaryLengths[ i ].ToInt64() ];
                    binariesGCHandles[ i ] = GCHandle.Alloc( binary, GCHandleType.Pinned );
                    binariesPtrs[ i ] = binariesGCHandles[ i ].AddrOfPinnedObject();
                    binaries.Add( binary );
                }

                unsafe
                {
                    IntPtr ret;
                    ComputeErrorCode error = ( int )ComputeErrorCode.Success;
                    error = CL10.GetProgramInfo(
                        Handle,
                        ComputeProgramInfo.Binaries,
                        new IntPtr( binariesPtrs.Length * IntPtr.Size ),
                        Marshal.UnsafeAddrOfPinnedArrayElement( binariesPtrs, 0 ),
                        out ret );
                    ComputeException.ThrowOnError( error );
                }
            }
            finally
            {
                for( int i = 0; i < binaryLengths.Length; i++ )
                    binariesGCHandles[ i ].Free();
            }

            return new ReadOnlyCollection<byte[]>( binaries );
        }

        #endregion
    }

    [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
    public delegate void ComputeProgramBuildNotifier( IntPtr programHandle, IntPtr userDataPtr );
}