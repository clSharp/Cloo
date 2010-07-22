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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL command queue.
    /// </summary>
    /// <remarks> An object that holds commands that will be executed on a specific device. The command-queue is created on a specific device in a context. Commands to a command-queue are queued in-order but may be executed in-order or out-of-order. </remarks>
    /// <seealso cref="ComputeContext"/>
    /// <seealso cref="ComputeDevice"/>
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
        /// Gets the <c>ComputeContext</c> of the <c>ComputeCommandQueue</c>.
        /// </summary>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the <c>ComputeDevice</c> of the <c>ComputeCommandQueue</c>.
        /// </summary>
        public ComputeDevice Device { get { return device; } }

        /// <summary>
        /// Gets or sets the out-of-order execution mode of the commands in the <c>ComputeCommandQueue</c>.
        /// </summary>
        public bool OutOfOrderExecution
        {
            get { return outOfOrderExec; }
            set
            {
                SetProperty(ComputeCommandQueueFlags.OutOfOrderExecution, value);
                outOfOrderExec = value;
            }
        }

        /// <summary>
        /// Gets or sets the profiling mode of the commands in the <c>ComputeCommandQueue</c>.
        /// </summary>
        public bool Profiling
        {
            get { return profiling; }
            set
            {
                SetProperty(ComputeCommandQueueFlags.Profiling, value);
                profiling = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <c>ComputeCommandQueue</c>.
        /// </summary>
        /// <param name="context"> A <c>ComputeContext</c>. </param>
        /// <param name="device"> A <c>ComputeDevice</c> associated with the <paramref name="context"/>. It can either be one of <c>ComputeContext.Devices</c> or have the same <c>ComputeDeviceTypes</c> as the <paramref name="device"/> specified when the <paramref name="context"/> is created. </param>
        /// <param name="properties"> The properties for the <c>ComputeCommandQueue</c>. </param>
        public ComputeCommandQueue(ComputeContext context, ComputeDevice device, ComputeCommandQueueFlags properties)
        {
            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateCommandQueue(context.Handle, device.Handle, properties, &error);
                ComputeException.ThrowOnError(error);
                this.device = device;
                this.context = context;
                outOfOrderExec = ((properties & ComputeCommandQueueFlags.OutOfOrderExecution) == ComputeCommandQueueFlags.OutOfOrderExecution);
                profiling = ((properties & ComputeCommandQueueFlags.Profiling) == ComputeCommandQueueFlags.Profiling);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enqueues a command to acquire a collection of <c>ComputeMemory</c>s that have been previously created from OpenGL objects.
        /// </summary>
        /// <param name="memObjs"> A collection of <c>ComputeMemory</c>s that correspond to OpenGL memory objects. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void AcquireGLObjects(ICollection<ComputeMemory> memObjs, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] memObjHandles = Tools.ExtractHandles(memObjs);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* memObjHandlesPtr = memObjHandles)
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    error = CL10.EnqueueAcquireGLObjects(
                        Handle,
                        memObjHandles.Length,
                        memObjHandlesPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a barrier.
        /// </summary>
        /// <remarks> A barrier ensures that all queued commands have finished execution before the next batch of commands can begin execution. </remarks>
        public void AddBarrier()
        {
            ComputeErrorCode error = CL10.EnqueueBarrier(Handle);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Enqueues a marker.
        /// </summary>
        public ComputeEvent AddMarker()
        {
            unsafe
            {
                IntPtr newEventHandle = IntPtr.Zero;
                ComputeErrorCode error = CL10.EnqueueMarker(Handle, &newEventHandle);
                ComputeException.ThrowOnError(error);
                return new ComputeEvent(newEventHandle, this);
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Copy<T>(ComputeBuffer<T> source, ComputeBuffer<T> destination, ICollection<ComputeEvent> events) where T : struct
        {
            Copy(source, destination, 0, 0, source.Count, events);
        }

        /// <summary>
        /// Enqueues a command to copy data between <c>ComputeBuffer</c>s.
        /// </summary>
        /// <param name="source"> The <c>ComputeBuffer</c> to copy from. </param>
        /// <param name="destination"> The <c>ComputeBuffer</c> to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> offset in elements where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> offset in elements where writing starts. </param>
        /// <param name="count"> The number of elements to copy. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeBuffer<T> source, ComputeBuffer<T> destination, long sourceOffset, long destinationOffset, long count, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr(sourceOffset * sizeofT),
                        new IntPtr(destinationOffset * sizeofT),
                        new IntPtr(count * sizeofT),
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Copy<T>(ComputeBuffer<T> source, ComputeImage destination, ICollection<ComputeEvent> events) where T : struct
        {
            Copy(source, destination, 0, new long[] { 0, 0, 0 }, new long[] { destination.Width, destination.Height, destination.Depth }, events);
        }

        /// <summary>
        /// Enqueues a command to copy data from <c>ComputeBuffer</c> to <c>ComputeImage</c>.
        /// </summary>
        /// <param name="source"> The <c>ComputeBuffer</c> to copy from. </param>
        /// <param name="destination"> The <c>ComputeImage</c> to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> offset in elements where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> (x, y, z) offset in pixels where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeBuffer<T> source, ComputeImage destination, long sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* destinationOffsetPtr = Tools.ConvertArray(destinationOffset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBufferToImage(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        new IntPtr(sourceOffset * sizeofT),
                        destinationOffsetPtr,
                        regionPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }


        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Copy<T>(ComputeImage source, ComputeBuffer<T> destination, ICollection<ComputeEvent> events) where T : struct
        {
            Copy(source, destination, new long[] { 0, 0, 0 }, 0, new long[] { source.Width, source.Height, source.Depth }, events);

        }

        /// <summary>
        /// Enqueues a command to copy data from <c>ComputeImage</c> to <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="source"> The <c>ComputeImage</c> to copy from. </param>
        /// <param name="destination"> The <c>ComputeBuffer</c> to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> (x, y, z) offset in pixels where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> offset in elements where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeImage source, ComputeBuffer<T> destination, long[] sourceOffset, long destinationOffset, long[] region, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* sourceOffsetPtr = Tools.ConvertArray(sourceOffset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyImageToBuffer(
                        Handle,
                        source.Handle,
                        destination.Handle,
                        sourceOffsetPtr,
                        regionPtr,
                        new IntPtr(destinationOffset * sizeofT),
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Copy(ComputeImage source, ComputeImage destination, ICollection<ComputeEvent> events)
        {
            long[] offset = new long[] { 0, 0, 0 };
            Copy(source, destination, offset, offset, new long[] { source.Width, source.Height, source.Depth }, events);
        }

        /// <summary>
        /// Enqueues a command to copy data between <c>ComputeImage</c>s.
        /// </summary>
        /// <param name="source"> The <c>ComputeImage</c> to copy from. </param>
        /// <param name="destination"> The <c>ComputeImage</c> to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> (x, y, z) offset in pixels where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> (x, y, z) offset in pixels where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Copy(ComputeImage source, ComputeImage destination, long[] sourceOffset, long[] destinationOffset, long[] region, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* sourceOffsetPtr = Tools.ConvertArray(sourceOffset))
                fixed (IntPtr* destinationOffsetPtr = Tools.ConvertArray(destinationOffset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
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
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to execute a single <c>ComputeKernel</c>.
        /// </summary>
        /// <param name="kernel"> The <c>ComputeKernel</c> to execute. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void ExecuteTask(ComputeKernel kernel, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueTask(
                        Handle,
                        kernel.Handle,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                kernel.ReferenceArguments();

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to parallely execute a range of <c>ComputeKernel</c>s.
        /// </summary>
        /// <param name="kernel"> The <c>ComputeKernel</c> to execute. </param>
        /// <param name="globalWorkOffset"> An array of values that describe the offset used to calculate the global ID of a work-item instead of having the global IDs always start at offset (0, 0,... 0). </param>
        /// <param name="globalWorkSize"> An array of values that describe the number of global work-items in dimensions that will execute the kernel function. The total number of global work-items is computed as global_work_size[0] *...* global_work_size[work_dim - 1]. </param>
        /// <param name="localWorkSize"> An array of values that describe the number of work-items that make up a work-group (also referred to as the size of the work-group) that will execute the <paramref name="kernel"/>. The total number of work-items in a work-group is computed as local_work_size[0] *... * local_work_size[work_dim - 1]. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Execute(ComputeKernel kernel, long[] globalWorkOffset, long[] globalWorkSize, long[] localWorkSize, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* globalWorkOffsetPtr = Tools.ConvertArray(globalWorkOffset))
                fixed (IntPtr* globalWorkSizePtr = Tools.ConvertArray(globalWorkSize))
                fixed (IntPtr* localWorkSizePtr = Tools.ConvertArray(localWorkSize))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
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
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                kernel.ReferenceArguments();

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Blocks until all previously queued OpenCL commands in the <c>ComputeCommandQueue</c> are issued to the <c>ComputeCommandQueue.Device</c> and have completed.
        /// </summary>
        public void Finish()
        {
            ComputeErrorCode error = CL10.Finish(Handle);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Issues all previously queued OpenCL commands in the <c>ComputeCommandQueue</c> to the <c>ComputeCommandQueue.Device</c>.
        /// </summary>
        /// <remarks> This method only guarantees that all queued commands get issued to the OpenCL device. There is no guarantee that they will be complete after this method returns. </remarks>
        public void Flush()
        {
            ComputeErrorCode error = CL10.Flush(Handle);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public IntPtr Map<T>(ComputeBuffer<T> buffer, bool blocking, ComputeMemoryMappingFlags flags, ICollection<ComputeEvent> events) where T : struct
        {
            return Map(buffer, blocking, flags, 0, buffer.Count, events);
        }

        /// <summary>
        /// Enqueues a command to map a part of a <c>ComputeBuffer</c> into the host address space.
        /// </summary>
        /// <param name="buffer"> The <c>ComputeBuffer</c> to map. </param>
        /// <param name="blocking">  The mode of operation of this call. </param>
        /// <param name="flags"> A list of properties for the mapping mode. </param>
        /// <param name="offset"> The <paramref name="source"/> offset in elements where mapping starts. </param>
        /// <param name="count"> The number of elements to map. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public IntPtr Map<T>(ComputeBuffer<T> buffer, bool blocking, ComputeMemoryMappingFlags flags, long offset, long count, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;
                IntPtr mappedPtr = IntPtr.Zero;
                int sizeofT = Marshal.SizeOf(typeof(T));

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    mappedPtr = CL10.EnqueueMapBuffer(
                        Handle,
                        buffer.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        flags,
                        new IntPtr(offset * sizeofT),
                        new IntPtr(count * sizeofT),
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null,
                        &error);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));

                return mappedPtr;
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public IntPtr Map(ComputeImage image, bool blocking, ComputeMemoryMappingFlags flags, ICollection<ComputeEvent> events)
        {
            return Map(image, blocking, flags, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, events);
        }

        /// <summary>
        /// Enqueues a command to map a part of a <c>ComputeImage</c> into the host address space.
        /// </summary>
        /// <param name="image"> The <c>ComputeImage</c> to map. </param>
        /// <param name="blocking"> The mode of operation of this call. </param>
        /// <param name="flags"> A list of properties for the mapping mode. </param>
        /// <param name="offset"> The <paramref name="source"/> (x, y, z) offset in pixels where mapping starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to map. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public IntPtr Map(ComputeImage image, bool blocking, ComputeMemoryMappingFlags flags, long[] offset, long[] region, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;
                IntPtr mappedPtr;

                fixed (IntPtr* offsetPtr = Tools.ConvertArray(offset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    mappedPtr = CL10.EnqueueMapImage(
                        Handle,
                        image.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        flags,
                        offsetPtr,
                        regionPtr,
                        null,
                        null,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null,
                        &error);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));

                return mappedPtr;
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public T[] Read<T>(ComputeBuffer<T> buffer, ICollection<ComputeEvent> events) where T : struct
        {
            return Read(buffer, 0, buffer.Count, events);
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public T[] Read<T>(ComputeBuffer<T> buffer, long offset, long count, ICollection<ComputeEvent> events) where T : struct
        {
            T[] data = new T[count];
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try { Read(buffer, true, offset, count, dataHandle.AddrOfPinnedObject(), events); }
            finally { dataHandle.Free(); }
            return data;
        }

        /// <summary>
        /// Enqueues a command to read data from a <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="buffer"> The buffer to read from. </param>
        /// <param name="blocking"> The mode of operation of this call. </param>
        /// <param name="offset"> The offset in elements where reading starts. </param>
        /// <param name="count"> The number of elements to read. </param>
        /// <param name="data"> A preallocated memory area to read the data into. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public void Read<T>(ComputeBuffer<T> buffer, bool blocking, long offset, long count, IntPtr data, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReadBuffer(
                        Handle,
                        buffer.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        new IntPtr(offset * sizeofT),
                        new IntPtr(count * sizeofT),
                        data,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Read(ComputeImage image, bool blocking, IntPtr data, ICollection<ComputeEvent> events)
        {
            Read(image, blocking, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, image.RowPitch, image.SlicePitch, data, events);
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Read(ComputeImage image, bool blocking, long[] offset, long[] region, IntPtr data, ICollection<ComputeEvent> events)
        {
            Read(image, blocking, offset, region, image.RowPitch, image.SlicePitch, data, events);
        }

        /// <summary>
        /// Enqueues a command to read data from a <c>ComputeImage</c>.
        /// </summary>
        /// <param name="image"> The <c>ComputeImage</c> to read from. </param>
        /// <param name="blocking"> The mode of operation of this call. </param>
        /// <param name="offset"> The (x, y, z) offset in pixels where reading starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to read. </param>
        /// <param name="rowPitch"> The <c>ComputeImage.RowPitch</c> or 0. </param>
        /// <param name="slicePitch"> The <c>ComputeImage.SlicePitch</c> or 0. </param>
        /// <param name="data"> A preallocated memory area to read the data into. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public void Read(ComputeImage image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, IntPtr data, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* offsetPtr = Tools.ConvertArray(offset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReadImage(
                        Handle,
                        image.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        offsetPtr,
                        regionPtr,
                        new IntPtr(rowPitch),
                        new IntPtr(slicePitch),
                        data,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to release <c>ComputeMemory</c>s that have been created from OpenGL objects.
        /// </summary>
        /// <param name="memObjs"> A collection of <c>ComputeMemory</c>s that correspond to OpenGL memory objects. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void ReleaseGLObjects(ICollection<ComputeMemory> memObjs, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] memObjHandles = Tools.ExtractHandles(memObjs);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* memoryObjectsPtr = memObjHandles)
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReleaseGLObjects(
                        Handle,
                        memObjHandles.Length,
                        memoryObjectsPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Gets the string representation of the <c>ComputeCommandQueue</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeCommandQueue</c>. </returns>
        public override string ToString()
        {
            return "ComputeQueue" + base.ToString();
        }

        /// <summary>
        /// Enqueues a command to unmap a <c>ComputeBuffer</c> or a <c>ComputeImage</c> from the host address space.
        /// </summary>
        /// <param name="memory"> The <c>ComputeMemory</c>. </param>
        /// <param name="mappedPtr"> The host address returned by a previous call to <c>ComputeCommandQueue.Map</c>. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        public void Unmap(ComputeMemory memory, ref IntPtr mappedPtr, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueUnmapMemObject(
                        Handle,
                        memory.Handle,
                        mappedPtr,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                mappedPtr = IntPtr.Zero;

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a wait command for a collection of <c>ComputeEvent</c>s to complete before any future commands queued in the <c>ComputeCommandQueue</c> are executed.
        /// </summary>
        /// <param name="events"> The <c>ComputeEvent</c>s that this command will wait for. </param>
        public void Wait(ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWaitForEvents(
                        Handle,
                        eventHandles.Length,
                        eventHandlesPtr);
                    ComputeException.ThrowOnError(error);
                }
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Write<T>(ComputeBuffer<T> buffer, T[] data, ICollection<ComputeEvent> events) where T : struct
        {
            Write(buffer, 0, data.Length, data, events);
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Write<T>(ComputeBuffer<T> buffer, long offset, long count, T[] data, ICollection<ComputeEvent> events) where T : struct
        {
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try { Write(buffer, true, offset, count, dataHandle.AddrOfPinnedObject(), events); }
            finally { dataHandle.Free(); }
        }

        /// <summary>
        /// Enqueues a command to write data to a <c>ComputeBuffer</c>.
        /// </summary>
        /// <param name="buffer"> The buffer to write to. </param>
        /// <param name="blocking"> The mode of operation of this call. </param>
        /// <param name="offset"> The offset in elements where writing starts. </param>
        /// <param name="count"> The number of elements to write. </param>
        /// <param name="data"> The content written to the <c>ComputeBuffer</c>. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public void Write<T>(ComputeBuffer<T> buffer, bool blocking, long offset, long count, IntPtr data, ICollection<ComputeEvent> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWriteBuffer(
                        Handle,
                        buffer.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        new IntPtr(offset * sizeofT),
                        new IntPtr(count * sizeofT),
                        data,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Write(ComputeImage image, bool blocking, IntPtr data, ICollection<ComputeEvent> events)
        {
            Write(image, blocking, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, data, events);
        }

        /// <summary>
        /// Deprecated. Use the full version instead.
        /// </summary>
        [Obsolete("Deprecated. Use the full version instead.", true)]
        public void Write(ComputeImage image, bool blocking, long[] offset, long[] region, IntPtr data, ICollection<ComputeEvent> events)
        {
            Write(image, blocking, offset, region, image.RowPitch, image.SlicePitch, data, events);
        }

        /// <summary>
        /// Enqueues a command to write data to a <c>ComputeImage</c>.
        /// </summary>
        /// <param name="image"> The <c>ComputeImage</c> to write to. </param>
        /// <param name="blocking"> The mode of operation of this call. </param>
        /// <param name="offset"> The (x, y, z) offset in pixels where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to write. </param>
        /// <param name="rowPitch"> The <c>ComputeImage.RowPitch</c> or 0. </param>
        /// <param name="slicePitch"> The <c>ComputeImage.SlicePitch</c> or 0. </param>
        /// <param name="data"> The content written to the <c>ComputeImage</c>. </param>
        /// <param name="events"> A collection of <c>ComputeEvent</c>s that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <c>ComputeEvent</c> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after enqueueing the command in the <c>ComputeCommandQueue</c>. </remarks>
        public void Write(ComputeImage image, bool blocking, long[] offset, long[] region, long rowPitch, long slicePitch, IntPtr data, ICollection<ComputeEvent> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* offsetPtr = Tools.ConvertArray(offset))
                fixed (IntPtr* regionPtr = Tools.ConvertArray(region))
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWriteImage(
                        Handle,
                        image.Handle,
                        (blocking) ? ComputeBoolean.True : ComputeBoolean.False,
                        offsetPtr,
                        regionPtr,
                        new IntPtr(rowPitch),
                        new IntPtr(slicePitch),
                        data,
                        eventHandles.Length,
                        eventHandlesPtr,
                        (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        #endregion

        #region Protected methods

        protected override void Dispose(bool manual)
        {
            if (manual)
            {
                //free managed resources
            }

            // free native resources
            if (Handle != IntPtr.Zero)
            {
                CL10.ReleaseCommandQueue(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Private methods

        private void SetProperty(ComputeCommandQueueFlags flags, bool enable)
        {
            ComputeCommandQueueFlags oldProperties = 0;
            ComputeErrorCode error = ComputeErrorCode.Success;
            error = CL10.SetCommandQueueProperty(
                Handle,
                flags,
                (enable) ? ComputeBoolean.True : ComputeBoolean.False,
                out oldProperties);
            ComputeException.ThrowOnError(error);
        }

        #endregion
    }
}