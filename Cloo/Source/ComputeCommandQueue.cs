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

/* 
 * Should investigate:
 * There may be a problem related to some asynchronous operations.
 * The GC handles may be released before such operations complete.
 *
 * Possible fix:
 * Store these handles in the ComputeEvent accompanying the command.
 * They will be released through ComputeEvent.Dispose().
 */

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    public class ComputeCommandQueue: ComputeResource
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
            ComputeErrorCode error = ComputeErrorCode.Success;
            Handle = CL10.CreateCommandQueue( context.Handle, device.Handle, properties, out error );
            ComputeException.ThrowOnError( error );
            this.device = device;
            this.context = context;
            outOfOrderExec = ( ( long )( properties & ComputeCommandQueueFlags.OutOfOrderExecution ) != 0 );
            profiling = ( ( long )( properties & ComputeCommandQueueFlags.Profiling ) != 0 );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Acquire OpenCL memory objects that have been created from OpenGL objects.
        /// </summary>
        /// <param name="memObjs">A list of CL memory objects that correspond to GL objects.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void AcquireGLObjects( ICollection<ComputeMemory> memObjs, ICollection<ComputeEvent> events )
        {
            IntPtr[] memObjHandles = Tools.ExtractHandles( memObjs );
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* memObjHandlesPtr = memObjHandles )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = ( int )ComputeErrorCode.Success;
                    error = CL10.EnqueueAcquireGLObjects(
                        Handle,
                        memObjHandles.Length,
                        memObjHandlesPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a barrier. This barrier ensures that all queued commands have finished execution before the next batch of commands can begin execution.
        /// </summary>
        public void AddBarrier()
        {
            ComputeErrorCode error = CL10.EnqueueBarrier( Handle );
            ComputeException.ThrowOnError( error );
        }

        /// <summary>
        /// Enqueues a marker.
        /// </summary>
        public ComputeEvent AddMarker()
        {
            IntPtr newEventHandle = IntPtr.Zero;
            unsafe
            {
                ComputeErrorCode error = CL10.EnqueueMarker( Handle, &newEventHandle );
                ComputeException.ThrowOnError( error );
            }
            return new ComputeEvent( newEventHandle, this );
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
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr( sourceOffset * sizeofT ),
                        new IntPtr( destinationOffset * sizeofT ),
                        new IntPtr( count * sizeofT ),
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to copy.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy<T>( ComputeBuffer<T> source, ComputeImage destination, long sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {                
                fixed( IntPtr* destinationOffsetPtr = Tools.ConvertArray( destinationOffset ) )
                fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBufferToImage(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr( sourceOffset * sizeofT ),
                        destinationOffsetPtr,
                        regionPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to copy.</param>
        /// <param name="destinationOffset">The destination offset in elements where writing starts.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy<T>( ComputeImage source, ComputeBuffer<T> destination, long[] sourceOffset, long destinationOffset, long[] region, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                fixed( IntPtr* sourceOffsetPtr = Tools.ConvertArray( sourceOffset ) )
                fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueCopyImageToBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        sourceOffsetPtr,
                        regionPtr,
                        new IntPtr( destinationOffset * sizeofT ),
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to copy.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Copy( ComputeImage source, ComputeImage destination, long[] sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* sourceOffsetPtr = Tools.ConvertArray( sourceOffset ) )
                fixed( IntPtr* destinationOffsetPtr = Tools.ConvertArray( destinationOffset ) )
                fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueCopyImage(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        sourceOffsetPtr,
                        destinationOffsetPtr,
                        regionPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to execute a single kernel.
        /// </summary>
        /// <param name="kernel">The kernel to execute.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Execute( ComputeKernel kernel, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueTask(
                        Handle,
                        kernel.Handle,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
        }

        /// <summary>
        /// Enqueues a command to execute a range of kernels.
        /// </summary>
        /// <param name="kernel">The kernel to execute.</param>
        /// <param name="globalWorkOffset">Can be used to specify an array of values that describe the offset used to calculate the global ID of a work-item instead of having the global IDs always start at offset (0, 0,... 0).</param>
        /// <param name="globalWorkSize">An array of values that describe the number of global work-items in dimensions that will execute the kernel function. The total number of global work-items is computed as global_work_size[0] *...* global_work_size[work_dim - 1].</param>
        /// <param name="localWorkSize">An array of values that describe the number of work-items that make up a work-group (also referred to as the size of the work-group) that will execute the kernel specified by kernel. The total number of work-items in a work-group is computed as local_work_size[0] *... * local_work_size[work_dim - 1].</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Execute( ComputeKernel kernel, long[] globalWorkOffset, long[] globalWorkSize, long[] localWorkSize, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* globalWorkOffsetPtr = Tools.ConvertArray( globalWorkOffset ) )
                fixed( IntPtr* globalWorkSizePtr = Tools.ConvertArray( globalWorkSize ) )
                fixed( IntPtr* localWorkSizePtr = Tools.ConvertArray( localWorkSize ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueNDRangeKernel(
                        Handle,
                        kernel.Handle,
                        globalWorkSize.Length,
                        globalWorkOffsetPtr,
                        globalWorkSizePtr,
                        localWorkSizePtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
            ComputeErrorCode error = CL10.Finish( Handle );
            ComputeException.ThrowOnError( error );
        }

        /// <summary>
        /// Issues all previously queued OpenCL commands in this queue to the associated device. This method only guarantees that all queued commands get issued to the appropriate device. There is no guarantee that they will be complete after this method returns.
        /// </summary>
        public void Flush()
        {
            ComputeErrorCode error = CL10.Flush( Handle );
            ComputeException.ThrowOnError( error );
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
        public IntPtr Map<T>( ComputeBuffer<T> buffer, bool blocking, ComputeMemoryMappingFlags flags, long offset, long count, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;
            IntPtr mappedPtr = IntPtr.Zero;
            int sizeofT = Marshal.SizeOf( typeof( T ) );

            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    mappedPtr = CL10.EnqueueMapBuffer(
                        Handle,
                        buffer.Handle,
                        ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                        flags,
                        new IntPtr( offset * sizeofT ),
                        new IntPtr( count * sizeofT ),
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null,
                        out error );
                    ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to map.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public IntPtr Map( ComputeImage image, bool blocking, ComputeMemoryMappingFlags flags, long[] offset, long[] region, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;
            IntPtr mappedPtr;

            unsafe
            {
                fixed( IntPtr* offsetPtr = Tools.ConvertArray( offset ) )
                fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    mappedPtr = CL10.EnqueueMapImage(
                        Handle,
                        image.Handle,
                        ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                        flags,
                        offsetPtr,
                        regionPtr,
                        null,
                        null,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null,
                        out error );
                    ComputeException.ThrowOnError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );

            return mappedPtr;
        }

        /// <summary>
        /// Enqueues a command to read data from a buffer.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="blocking">Indicates if this operation is blocking or non-blocking.</param>
        /// <param name="offset">The offset in elements where reading starts.</param>
        /// <param name="count">The number of elements to read.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public T[] Read<T>( ComputeBuffer<T> buffer, bool blocking, long offset, long count, ICollection<ComputeEvent> events ) where T: struct
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            int sizeofT = Marshal.SizeOf( typeof( T ) );            
            T[] readData = new T[ count ];
            GCHandle gcHandle = GCHandle.Alloc( readData, GCHandleType.Pinned );

            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                try
                {
                    fixed( IntPtr* eventHandlesPtr = eventHandles )
                        error = CL10.EnqueueReadBuffer(
                            Handle,
                            buffer.Handle,
                            ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                            new IntPtr( offset * sizeofT ),
                            new IntPtr( count * sizeofT ),
                            gcHandle.AddrOfPinnedObject(),
                            eventHandles.Length,
                            eventHandlesPtr,
                            ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to read.</param>
        /// <param name="rowPitch">The length of each row in bytes. This value must be greater than or equal to the pixel size in bytes * width.</param>
        /// <param name="slicePitch">Size in bytes of the 2D slice of the 3D region of a 3D image being read. This must be 0 if image is a 2D image. This value must be greater than or equal to rowPitch * height.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public byte[] Read( ComputeImage image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            byte[] readData = new byte[ region[ 2 ] * slicePitch + region[ 1 ] * rowPitch + region[ 0 ] * image.PixelSize ];
            GCHandle gcHandle = GCHandle.Alloc( readData, GCHandleType.Pinned );

            unsafe
            {
                try
                {
                    fixed( IntPtr* offsetPtr = Tools.ConvertArray( offset ) )
                    fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                    fixed( IntPtr* eventHandlesPtr = eventHandles )
                    {
                        ComputeErrorCode error = CL10.EnqueueReadImage(
                            Handle,
                            image.Handle,
                            ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                            offsetPtr,
                            regionPtr,
                            new IntPtr( rowPitch ),
                            new IntPtr( slicePitch ),
                            gcHandle.AddrOfPinnedObject(),
                            eventHandles.Length,
                            eventHandlesPtr,
                            ( events != null ) ? &newEventHandle : null );
                        ComputeException.ThrowOnError( error );
                    }
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
        /// Release OpenCL memory objects that have been created from OpenGL objects.
        /// </summary>
        /// <param name="memObjs">A collection of CL memory objects that correspond to GL objects.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void ReleaseGLObjects( ICollection<ComputeMemory> memObjs, ICollection<ComputeEvent> events )
        {
            IntPtr[] memObjHandles = Tools.ExtractHandles( memObjs );
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* memoryObjectsPtr = memObjHandles )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = ( int )ComputeErrorCode.Success;
                    error = CL10.EnqueueReleaseGLObjects(
                        Handle,
                        memObjHandles.Length,
                        memoryObjectsPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
                }
            }

            if( events != null )
                events.Add( new ComputeEvent( newEventHandle, this ) );
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
        /// <param name="memory">A valid memory object.</param>
        /// <param name="mappedPtr">The host address returned by a previous call to Map( ComputeMemory, ... ).</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Unmap( ComputeMemory memory, ref IntPtr mappedPtr, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                ComputeErrorCode error = ( int )ComputeErrorCode.Success;
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                    error = CL10.EnqueueUnmapMemObject(
                        Handle,
                        memory.Handle,
                        mappedPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                ComputeException.ThrowOnError( error );
            }

            mappedPtr = IntPtr.Zero;
        }

        /// <summary>
        /// Enqueues a wait for a list of events to complete before any future commands queued in the command-queue are executed.
        /// </summary>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Wait( ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );

            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.WaitForEvents( eventHandles.Length, eventHandlesPtr );
                    ComputeException.ThrowOnError( error );
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
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                ComputeErrorCode error = ( int )ComputeErrorCode.Success;
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                    error = CL10.EnqueueWriteBuffer(
                            Handle,
                            buffer.Handle,
                            ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                            new IntPtr( offset * sizeofT ),
                            new IntPtr( count * sizeofT ),
                            Marshal.UnsafeAddrOfPinnedArrayElement( data, 0 ),
                            eventHandles.Length,
                            eventHandlesPtr,
                            ( events != null ) ? &newEventHandle : null );
                ComputeException.ThrowOnError( error );
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
        /// <param name="region">The region (width, height, depth) in pixels to write.</param>
        /// <param name="rowPitch">The length of image scan-line in bytes.</param>
        /// <param name="slicePitch">The count in bytes of the 2D slice of the 3D image.</param>
        /// <param name="data">The content written to the image.</param>
        /// <param name="events">Specify events that need to complete before this particular command can be executed. If events is not null a new event identifying this command is attached to the end of the list.</param>
        public void Write( ComputeImage image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, IntPtr data, ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = Tools.ExtractHandles( events );
            IntPtr newEventHandle = IntPtr.Zero;

            unsafe
            {
                fixed( IntPtr* offsetPtr = Tools.ConvertArray( offset ) )
                fixed( IntPtr* regionPtr = Tools.ConvertArray( region ) )
                fixed( IntPtr* eventHandlesPtr = eventHandles )
                {
                    ComputeErrorCode error = CL10.EnqueueWriteImage(
                        Handle,
                        image.Handle,
                        ( blocking ) ? ComputeBoolean.True : ComputeBoolean.False,
                        offsetPtr,
                        regionPtr,
                        new IntPtr( rowPitch ),
                        new IntPtr( slicePitch ),
                        data,
                        eventHandles.Length,
                        eventHandlesPtr,
                        ( events != null ) ? &newEventHandle : null );
                    ComputeException.ThrowOnError( error );
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
                CL10.ReleaseCommandQueue( Handle );
                Handle = IntPtr.Zero;
            }
        }
        
        #endregion

        #region Private methods

        private void SetProperty( ComputeCommandQueueFlags flags, bool enable )
        {
            ComputeCommandQueueFlags oldProperties = 0;
            ComputeErrorCode error = ( int )ComputeErrorCode.Success;
            error = CL10.SetCommandQueueProperty( 
                Handle,
                flags,
                ( enable ) ? ComputeBoolean.True : ComputeBoolean.False,
                out oldProperties );
            ComputeException.ThrowOnError( error );
        }
        
        #endregion
    }
}