/*

Copyright (c) 2009 Fatjon Sakiqi

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

/* 
 * Should investigate:
 * There may be a problem related to asynchronous operations.
 * The GC Handles may be released before such operations complete.
 */

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using OpenTK.Compute.CL10;

    public class ComputeCommandQueue : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ComputeDevice device;
        private bool outOfOrderExec;
        private bool profiling;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context specified when the command-queue is created.
        /// </summary>
        public ComputeContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Gets the device specified when the command-queue is created.
        /// </summary>
        public ComputeDevice Device
        {
            get
            {
                return device;
            }
        }

        /// <summary>
        /// Gets or sets the execution order of commands in the command-queue.
        /// </summary>
        public bool OutOfOrderExecution
        {
            get
            {
                return outOfOrderExec;
            }
            set
            {
                SetProperty( ComputeCommandQueueFlags.OutOfOrderExecution, value );
                outOfOrderExec = value;
            }
        }

        /// <summary>
        /// Enable or disable profiling of commands in the command-queue.
        /// </summary>
        public bool Profiling
        {
            get
            {
                return profiling;
            }
            set
            {
                SetProperty( ComputeCommandQueueFlags.Profiling, value );
                profiling = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new command-queue.
        /// </summary>
        /// <param name="context">Must be a valid OpenCL context.</param>
        /// <param name="device">Must be a device associated with context. It can either be in the list of devices or have the same Type as the device specified when the contex is created.</param>
        /// <param name="properties">A list of properties for the command-queue.</param>
        public ComputeCommandQueue( ComputeContext context, ComputeDevice device, ComputeCommandQueueFlags properties )
        {
            ErrorCode error = ErrorCode.Success;
            Handle = CL.CreateCommandQueue( context.Handle, device.Handle, ( CommandQueueFlags )properties, out error );
            ComputeException.ThrowIfError( error );
            this.device = device;
            this.context = context;
            outOfOrderExec = ( ( long )( properties & ComputeCommandQueueFlags.OutOfOrderExecution ) != 0 );
            profiling = ( ( long )( properties & ComputeCommandQueueFlags.Profiling ) != 0 );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enqueues a barrier. This barrier ensures that all queued commands have finished execution before the next batch of commands can begin execution.
        /// </summary>
        public void Barrier()
        {
            int error = CL.EnqueueBarrier( Handle );
            ComputeException.ThrowIfError( error );
        }

        /// <summary>
        /// Enqueues a command to copy data between buffers.
        /// </summary>
        /// <param name="source">The buffer to copy from.</param>
        /// <param name="destination">The buffer to copy to.</param>
        /// <param name="sourceOffset">The source offset in elements where reading starts.</param>
        /// <param name="destinationOffset">The destination offset in elements where writing starts.</param>
        /// <param name="count">The number of elements to copy.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy<T>( ComputeBuffer<T> source, ComputeBuffer<T> destination, long sourceOffset, long destinationOffset, long count, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = CL.EnqueueCopyBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr( sourceOffset * sizeofT ),
                        new IntPtr( destinationOffset * sizeofT ),
                        new IntPtr( count * sizeofT ),
                        eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to copy data from buffer to image.
        /// </summary>
        /// <param name="source">The buffer to copy from.</param>
        /// <param name="destination">The image to copy to.</param>
        /// <param name="sourceOffset">The source offset in elements where reading starts.</param>
        /// <param name="destinationOffset">The destination (x, y, z) offset in pixels where writing starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to copy.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy<T>( ComputeBuffer<T> source, ComputeImage3D destination, long sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events ) where T: struct
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {                
                fixed( IntPtr* destinationOffsetPtr = ComputeTools.ConvertArray( destinationOffset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = DllImports.EnqueueCopyBufferToImage(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr( sourceOffset * sizeofT ),
                        destinationOffsetPtr,
                        regionPtr,
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }                
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to copy data from image to buffer.
        /// </summary>
        /// <param name="source">The image to copy from.</param>
        /// <param name="destination">The buffer to copy to.</param>
        /// <param name="sourceOffset">The source (x, y, z) offset in pixels where reading starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to copy.</param>
        /// <param name="destinationOffset">The destination offset in elements where writing starts.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy<T>( ComputeImage3D source, ComputeBuffer<T> destination, long[] sourceOffset, long destinationOffset, long[] region, ICollection<ComputeEvent> events ) where T: struct
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                fixed( IntPtr* sourceOffsetPtr = ComputeTools.ConvertArray( sourceOffset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = DllImports.EnqueueCopyImageToBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        sourceOffsetPtr,
                        regionPtr,
                        new IntPtr( destinationOffset * sizeofT ),
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to copy data between images.
        /// </summary>
        /// <param name="source">The image to copy from.</param>
        /// <param name="destination">The image to copy to.</param>
        /// <param name="sourceOffset">The source (x, y, z) offset in pixels where reading starts.</param>
        /// <param name="destinationOffset">The destination (x, y, z) offset in pixels where writing starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to copy.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy( ComputeImage3D source, ComputeImage3D destination, long[] sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events )
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* sourceOffsetPtr = ComputeTools.ConvertArray( sourceOffset ) )
                fixed( IntPtr* destinationOffsetPtr = ComputeTools.ConvertArray( destinationOffset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = DllImports.EnqueueCopyImage(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        sourceOffsetPtr,
                        destinationOffsetPtr,
                        regionPtr,
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to execute a single kernel.
        /// </summary>
        public void Execute( ComputeKernel kernel, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = CL.EnqueueTask(
                        Handle,
                        kernel.Handle,
                        eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to execute a range of kernels.
        /// </summary>
        public void Execute( ComputeKernel kernel, long[] globalWorkOffset, long[] globalWorkSize, long[] localWorkSize, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* globalWorkOffsetPtr = ComputeTools.ConvertArray( globalWorkOffset ) )
                fixed( IntPtr* globalWorkSizePtr = ComputeTools.ConvertArray( globalWorkSize ) )
                fixed( IntPtr* localWorkSizePtr = ComputeTools.ConvertArray( localWorkSize ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = CL.EnqueueNDRangeKernel(
                        Handle,
                        kernel.Handle,
                        globalWorkSize.Length,
                        globalWorkOffsetPtr,
                        globalWorkSizePtr,
                        localWorkSizePtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
            }
            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Blocks until all previously queued OpenCL commands in this queue are issued to the associated device and have completed.
        /// </summary>
        public void Finish()
        {
            int error = CL.Finish( Handle );
            ComputeException.ThrowIfError( error );
        }

        /// <summary>
        /// Issues all previously queued OpenCL commands in this queue to the associated device. This method only guarantees that all queued commands get issued to the appropriate device. There is no guarantee that they will be complete after this method returns.
        /// </summary>
        public void Flush()
        {
            int error = CL.Finish( Handle );
            ComputeException.ThrowIfError( error );
        }

        /// <summary>
        /// Enqueues a command to map a part of a buffer into the host address space.
        /// </summary>
        /// <param name="buffer">The buffer to map.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="flags">A list of properties for the mapping mode.</param>
        /// <param name="offset">The source offset in elements where mapping starts.</param>
        /// <param name="count">The number of elements to map.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        /// <returns>The mapped area.</returns>
        public IntPtr Map<T>( ComputeBuffer<T> buffer, bool blocking, ComputeMemoryMapFlags flags, long offset, long count, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
            IntPtr mappedPtr = IntPtr.Zero;
            int error = ( int )ErrorCode.Success;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    mappedPtr = CL.EnqueueMapBuffer(
                        Handle,
                        buffer.Handle,
                        blocking,
                        ( MapFlags )flags,
                        new IntPtr( offset * sizeofT ),
                        new IntPtr( count * sizeofT ),
                        eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle,
                        &error );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );

            return mappedPtr;
        }

        /// <summary>
        /// Enqueues a command to map a part of an image into the host address space.
        /// </summary>
        /// <param name="image">The image to map.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="flags">A list of properties for the mapping mode.</param>
        /// <param name="offset">The source (x, y, z) offset in pixels where mapping starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to map.</param>
        /// <param name="rowPitch">Returns the length of image scan-line in bytes.</param>
        /// <param name="slicePitch">Returns the count in bytes of the 2D slice of the 3D image.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public IntPtr Map( ComputeImage3D image, bool blocking, ComputeMemoryMapFlags flags, long[] offset, long[] region, out long rowPitch, out long slicePitch, ICollection<ComputeEvent> events )
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
            IntPtr mappedPtr, rowPitchPtr, slicePitchPtr;

            unsafe
            {
                fixed( IntPtr* offsetPtr = ComputeTools.ConvertArray( offset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = (int)ErrorCode.Success;

                    mappedPtr = DllImports.EnqueueMapImage(
                        Handle,
                        image.Handle,
                        blocking,
                        ( MapFlags )flags,
                        offsetPtr,
                        regionPtr,
                        &rowPitchPtr,
                        &slicePitchPtr,
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle,
                        &error );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );

            rowPitch = rowPitchPtr.ToInt64();
            slicePitch = slicePitchPtr.ToInt64();

            return mappedPtr;
        }

        /// <summary>
        /// Enqueues a marker.
        /// </summary>
        public ComputeEvent Marker()
        {
            IntPtr eventHandle = IntPtr.Zero;
            int error = CL.EnqueueMarker( Handle, ref eventHandle );
            ComputeException.ThrowIfError( error );

            return new ComputeEvent( eventHandle, this );
        }

        /// <summary>
        /// Enqueues a command to read data from a buffer.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="offset">The offset in elements where reading starts.</param>
        /// <param name="count">The number of elements to read.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        /// <returns>The content read from the buffer.</returns>
        public T[] Read<T>( ComputeBuffer<T> buffer, bool blocking, long offset, long count, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            int sizeofT = Marshal.SizeOf( typeof( T ) );            
            T[] readData = new T[ buffer.Count ];
            GCHandle gcHandle = GCHandle.Alloc( readData, GCHandleType.Pinned );

            unsafe
            {
                int error = ( int )ErrorCode.Success;
                try
                {
                    fixed( IntPtr* eventHandlesPtr = eventHandles )
                        error = CL.EnqueueReadBuffer(
                            Handle,
                            buffer.Handle,
                            blocking,
                            new IntPtr( offset * sizeofT ),
                            new IntPtr( count * sizeofT ),
                            gcHandle.AddrOfPinnedObject(),
                            eventHandles.Length,
                            eventHandlesPtr,
                            &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
                finally
                {
                    gcHandle.Free();
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );

            return readData;
        }

        /// <summary>
        /// Enqueues a command to read data from an image.
        /// </summary>
        /// <param name="image">The image to read from.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="offset">The (x, y, z) offset in pixels where reading starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to read.</param>
        /// <param name="rowPitch">The length of image scan-line in bytes.</param>
        /// <param name="slicePitch">The count in bytes of the 2D slice of the 3D image.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        /// <returns>A pointer to the image data.</returns>
        public IntPtr Read( ComputeImage3D image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, ICollection<ComputeEvent> events )
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;
                        
            byte[] imageBits = new byte[ region[ 2 ] * slicePitch * region[ 1 ] * rowPitch * region[ 0 ] ];
            IntPtr readData = Marshal.UnsafeAddrOfPinnedArrayElement( imageBits, 0 );

            unsafe
            {
                fixed( IntPtr* offsetPtr = ComputeTools.ConvertArray( offset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = DllImports.EnqueueReadImage(
                        Handle,
                        image.Handle,
                        blocking,
                        offsetPtr,
                        regionPtr,
                        new IntPtr( rowPitch ),
                        new IntPtr( slicePitch ),
                        readData,
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }                
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );

            return readData;
        }

        /// <summary>
        /// Gets a string representation of this queue.
        /// </summary>
        public override string ToString()
        {
            return "ComputeQueue" + base.ToString();
        }

        /// <summary>
        /// Enqueues a command to unmap a buffer or an image from the host address space.
        /// </summary>
        public void Unmap( ComputeMemory memory, ref IntPtr mappedPtr, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                int error = ( int )ErrorCode.Success;
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                    error = CL.EnqueueUnmapMemObject(
                        Handle,
                        memory.Handle,
                        mappedPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                ComputeException.ThrowIfError( error );
            }

            mappedPtr = IntPtr.Zero;
        }

        /// <summary>
        /// Enqueues a wait for a list of events to complete before any future commands queued in the command-queue are executed.
        /// </summary>
        public void Wait( ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = CL.WaitForEvents( eventHandles.Length, eventHandlesPtr );
                    ComputeException.ThrowIfError( error );
                }
            }
        }

        /// <summary>
        /// Enqueues a command to write data to a buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="offset">The offset in elements where writing starts.</param>
        /// <param name="count">The number of elements to write.</param>
        /// <param name="data">The content written to the buffer.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Write<T>( ComputeBuffer<T> buffer, bool blocking, long offset, long count, T[] data, ICollection<ComputeEvent> events ) where T: struct
        {
            int sizeofT = Marshal.SizeOf( typeof( T ) );
            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                int error = ( int )ErrorCode.Success;
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                    error = CL.EnqueueWriteBuffer(
                            Handle,
                            buffer.Handle,
                            blocking,
                            new IntPtr( offset * sizeofT ),
                            new IntPtr( count * sizeofT ),
                            data,
                            eventHandles.Length,
                            eventHandlesPtr,
                            &newEventHandle );
                ComputeException.ThrowIfError( error );
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to write data to an image.
        /// </summary>
        /// <param name="image">The image to write to.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="offset">The (x, y, z) offset in pixels where writing starts.</param>
        /// <param name="region">The region (width, height, depth) count in pixels to write.</param>
        /// <param name="rowPitch">The length of image scan-line in bytes.</param>
        /// <param name="slicePitch">The count in bytes of the 2D slice of the 3D image.</param>
        /// <param name="data">The content written to the image.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Write( ComputeImage3D image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, IntPtr data, ICollection<ComputeEvent> events )
        {
            throw new NotImplementedException();

            IntPtr[] eventHandles = ( events != null ) ? ExtractHandles( events ) : new IntPtr[ 0 ];
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* offsetPtr = ComputeTools.ConvertArray( offset ) )
                fixed( IntPtr* regionPtr = ComputeTools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    int error = DllImports.EnqueueWriteImage(
                        Handle,
                        image.Handle,
                        blocking,
                        offsetPtr,
                        regionPtr,
                        new IntPtr( rowPitch ),
                        new IntPtr( slicePitch ),
                        data,
                        ( uint )eventHandles.Length,
                        eventHandlesPtr,
                        &newEventHandle );
                    ComputeException.ThrowIfError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( manual )
            {
                //free managed resources
            }

            // free native resources
            if( Handle != IntPtr.Zero )
            {
                CL.ReleaseCommandQueue( Handle );
                Handle = IntPtr.Zero;
            }
        }
        
        #endregion

        #region Private methods

        private void SetProperty( ComputeCommandQueueFlags flags, bool enable )
        {
            int error = ( int )ErrorCode.Success;
            error = CL.SetCommandQueueProperty( Handle, ( CommandQueueFlags )flags, enable, ( CommandQueueFlags[] )null );
            ComputeException.ThrowIfError( error );
        }
        
        #endregion
    }
}