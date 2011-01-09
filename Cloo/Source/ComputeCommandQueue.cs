#region License

/*

Copyright (c) 2009 - 2011 Fatjon Sakiqi

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
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL command queue.
    /// </summary>
    /// <remarks> A command queue is an object that holds commands that will be executed on a specific device. The command queue is created on a specific device in a context. Commands to a command queue are queued in-order but may be executed in-order or out-of-order. </remarks>
    /// <seealso cref="ComputeContext"/>
    /// <seealso cref="ComputeDevice"/>
    public partial class ComputeCommandQueue : ComputeResource
    {
        #region Fields

        private readonly ComputeContext context;
        private readonly ComputeDevice device;
        private bool outOfOrderExec;
        private bool profiling;
        internal IList<ComputeEvent> events;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="ComputeContext"/> of the <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <value> The <see cref="ComputeContext"/> of the <see cref="ComputeCommandQueue"/>. </value>
        public ComputeContext Context { get { return context; } }

        /// <summary>
        /// Gets the <see cref="ComputeDevice"/> of the <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <value> The <see cref="ComputeDevice"/> of the <see cref="ComputeCommandQueue"/>. </value>
        public ComputeDevice Device { get { return device; } }

        /// <summary>
        /// Gets the out-of-order execution mode of the commands in the <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <value> Is <c>true</c> if <see cref="ComputeCommandQueue"/> has out-of-order execution mode enabled and <c>false</c> otherwise. </value>
        public bool OutOfOrderExecution { get { return outOfOrderExec; } }

        /// <summary>
        /// Gets the profiling mode of the commands in the <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <value> Is <c>true</c> if <see cref="ComputeCommandQueue"/> has profiling enabled and <c>false</c> otherwise. </value>
        public bool Profiling { get { return profiling; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <param name="context"> A <see cref="ComputeContext"/>. </param>
        /// <param name="device"> A <see cref="ComputeDevice"/> associated with the <paramref name="context"/>. It can either be one of <see cref="ComputeContext.Devices"/> or have the same <see cref="ComputeDeviceTypes"/> as the <paramref name="device"/> specified when the <paramref name="context"/> is created. </param>
        /// <param name="properties"> The properties for the <see cref="ComputeCommandQueue"/>. </param>
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

                events = new List<ComputeEvent>();
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enqueues a command to acquire a collection of <see cref="ComputeMemory"/>s that have been previously created from OpenGL objects.
        /// </summary>
        /// <typeparam name="CLMemType"> The type of <paramref name="memObjs"/>. </typeparam>
        /// <param name="memObjs"> A collection of OpenCL memory objects that correspond to OpenGL objects. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void AcquireGLObjects<CLMemType>(ICollection<CLMemType> memObjs, ICollection<ComputeEventBase> events)
            where CLMemType: ComputeMemory
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
                    error = CL10.EnqueueAcquireGLObjects(Handle, memObjHandles.Length, memObjHandlesPtr, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
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
        /// Enqueues a command to copy data between buffers.
        /// </summary>
        /// <typeparam name="T"> The type of data in the buffers. </typeparam>
        /// <param name="source"> The buffer to copy from. </param>
        /// <param name="destination"> The buffer to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The number of elements to copy. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new event identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, long sourceOffset, long destinationOffset, long region, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBuffer(Handle, source.Handle, destination.Handle, new IntPtr(sourceOffset * sizeofT), new IntPtr(destinationOffset * sizeofT), new IntPtr(region * sizeofT), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to copy a 2D or 3D region of elements between two buffers.
        /// </summary>
        /// <typeparam name="T"> The type of data in the buffers. </typeparam>
        /// <param name="source"> The buffer to copy from. </param>
        /// <param name="destination"> The buffer to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> offset in elements where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> offset in elements where writing starts. </param>
        /// <param name="region"> The region of the elements to copy. </param>
        /// <param name="sourceRowPitch"> The size of the source buffer row in bytes. If set to zero then <paramref name="sourceRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="sourceSlicePitch"> The size of the source buffer 2D slice in bytes. If set to zero then <paramref name="sourceSlicePitch"/> equals <c>region.Y * sizeof(T) * sourceRowPitch</c>. </param>
        /// <param name="destinationRowPitch"> The size of the destination buffer row in bytes. If set to zero then <paramref name="destinationRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="destinationSlicePitch"> The size of the destination buffer 2D slice in bytes. If set to zero then <paramref name="destinationSlicePitch"/> equals <c>region.Y * sizeof(T) * destinationRowPitch</c>. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> Requires OpenCL 1.1. </remarks>
        public void Copy<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long sourceSlicePitch, long destinationRowPitch, long destinationSlicePitch, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                sourceOffset.X = new IntPtr(sourceOffset.X.ToInt64() * sizeofT);
                destinationOffset.X = new IntPtr(destinationOffset.X.ToInt64() * sizeofT);
                region.X = new IntPtr(region.X.ToInt64() * sizeofT);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL11.EnqueueCopyBufferRect(this.Handle, source.Handle, destination.Handle, &(sourceOffset), &(destinationOffset), &(region), new IntPtr(sourceRowPitch), new IntPtr(sourceSlicePitch), new IntPtr(destinationRowPitch), new IntPtr(destinationSlicePitch), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to copy data from buffer to <see cref="ComputeImage"/>.
        /// </summary>
        /// <typeparam name="T"> The type of data in <paramref name="source"/>. </typeparam>
        /// <param name="source"> The buffer to copy from. </param>
        /// <param name="destination"> The image to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeBufferBase<T> source, ComputeImage destination, long sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyBufferToImage(Handle, source.Handle, destination.Handle, new IntPtr(sourceOffset * sizeofT), &(destinationOffset.X), &(region.X), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to copy data from <see cref="ComputeImage"/> to buffer.
        /// </summary>
        /// <param name="source"> The image to copy from. </param>
        /// <param name="destination"> The buffer to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void Copy<T>(ComputeImage source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, long destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyImageToBuffer(Handle, source.Handle, destination.Handle, &(sourceOffset.X), &(region.X), new IntPtr(destinationOffset * sizeofT), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to copy data between <see cref="ComputeImage"/>s.
        /// </summary>
        /// <param name="source"> The <see cref="ComputeImage"/> to copy from. </param>
        /// <param name="destination"> The <see cref="ComputeImage"/> to copy to. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to copy. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void Copy(ComputeImage source, ComputeImage destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueCopyImage(Handle, source.Handle, destination.Handle, &(sourceOffset.X), &(destinationOffset.X), &(region.X), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to execute a single <see cref="ComputeKernel"/>.
        /// </summary>
        /// <param name="kernel"> The <see cref="ComputeKernel"/> to execute. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void ExecuteTask(ComputeKernel kernel, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueTask(Handle, kernel.Handle, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                kernel.ReferenceArguments();

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }
        
        /* TODO: Driven execution
         * Executes a range of kernels depending on the type and dimensions of one of the kernel arguments.
         * This argument, which must be of type <see cref="ComputeMemory"/> is called the driving argument and its index is the drivingArgIndex.
        public void Execute(ComputeKernel kernel, long drivingArgIndex)
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// Enqueues a command to execute a range of <see cref="ComputeKernel"/>s in parallel.
        /// </summary>
        /// <param name="kernel"> The <see cref="ComputeKernel"/> to execute. </param>
        /// <param name="globalWorkOffset"> An array of values that describe the offset used to calculate the global ID of a work-item instead of having the global IDs always start at offset (0, 0,... 0). </param>
        /// <param name="globalWorkSize"> An array of values that describe the number of global work-items in dimensions that will execute the kernel function. The total number of global work-items is computed as global_work_size[0] *...* global_work_size[work_dim - 1]. </param>
        /// <param name="localWorkSize"> An array of values that describe the number of work-items that make up a work-group (also referred to as the size of the work-group) that will execute the <paramref name="kernel"/>. The total number of work-items in a work-group is computed as local_work_size[0] *... * local_work_size[work_dim - 1]. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void Execute(ComputeKernel kernel, long[] globalWorkOffset, long[] globalWorkSize, long[] localWorkSize, ICollection<ComputeEventBase> events)
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
                    ComputeErrorCode error = CL10.EnqueueNDRangeKernel(Handle, kernel.Handle, globalWorkSize.Length, globalWorkOffsetPtr, globalWorkSizePtr, localWorkSizePtr, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                kernel.ReferenceArguments();

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Blocks until all previously enqueued commands are issued to the <see cref="ComputeCommandQueue.Device"/> and have completed.
        /// </summary>
        public void Finish()
        {
            ComputeErrorCode error = CL10.Finish(Handle);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Issues all previously enqueued commands to the <see cref="ComputeCommandQueue.Device"/>.
        /// </summary>
        /// <remarks> This method only guarantees that all previously enqueued commands get issued to the OpenCL device. There is no guarantee that they will be complete after this method returns. </remarks>
        public void Flush()
        {
            ComputeErrorCode error = CL10.Flush(Handle);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// Enqueues a command to map a part of a buffer into the host address space.
        /// </summary>
        /// <param name="buffer"> The buffer to map. </param>
        /// <param name="blocking">  The mode of operation of this call. </param>
        /// <param name="flags"> A list of properties for the mapping mode. </param>
        /// <param name="offset"> The <paramref name="buffer"/> offset in elements where mapping starts. </param>
        /// <param name="region"> The number of elements to map. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public IntPtr Map<T>(ComputeBufferBase<T> buffer, bool blocking, ComputeMemoryMappingFlags flags, long offset, long region, ICollection<ComputeEventBase> events) where T : struct
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
                    mappedPtr = CL10.EnqueueMapBuffer(Handle, buffer.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, flags, new IntPtr(offset * sizeofT), new IntPtr(region * sizeofT), eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null, &error);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));

                return mappedPtr;
            }
        }

        /// <summary>
        /// Enqueues a command to map a part of a <see cref="ComputeImage"/> into the host address space.
        /// </summary>
        /// <param name="image"> The <see cref="ComputeImage"/> to map. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="flags"> A list of properties for the mapping mode. </param>
        /// <param name="offset"> The <paramref name="image"/> position where reading starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to map. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public IntPtr Map(ComputeImage image, bool blocking, ComputeMemoryMappingFlags flags, SysIntX3 offset, SysIntX3 region, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;
                IntPtr mappedPtr;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = ComputeErrorCode.Success;
                    mappedPtr = CL10.EnqueueMapImage(Handle, image.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, flags, &(offset.X), &(region.X), null, null, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null, &error);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));

                return mappedPtr;
            }
        }

        /// <summary>
        /// Enqueues a command to read data from a buffer.
        /// </summary>
        /// <param name="source"> The buffer to read from. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="offset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="region"> The number of elements to read. </param>
        /// <param name="destination"> A pointer to a preallocated memory area to read the data into. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public void Read<T>(ComputeBufferBase<T> source, bool blocking, long offset, long region, IntPtr destination, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReadBuffer(Handle, source.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, new IntPtr(offset * sizeofT), new IntPtr(region * sizeofT), destination, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to read a 2D or 3D region of elements from a buffer.
        /// </summary>
        /// <typeparam name="T"> The type of the elements of the buffer. </typeparam>
        /// <param name="source"> The buffer to read from. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The region of the elements to copy. </param>
        /// <param name="sourceRowPitch"> The size of the source buffer row in bytes. If set to zero then <paramref name="sourceRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="sourceSlicePitch"> The size of the source buffer 2D slice in bytes. If set to zero then <paramref name="sourceSlicePitch"/> equals <c>region.Y * sizeof(T) * sourceRowPitch</c>. </param>
        /// <param name="destinationRowPitch"> The size of the destination buffer row in bytes. If set to zero then <paramref name="destinationRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="destinationSlicePitch"> The size of the destination buffer 2D slice in bytes. If set to zero then <paramref name="destinationSlicePitch"/> equals <c>region.Y * sizeof(T) * destinationRowPitch</c>. </param>
        /// <param name="destination"> A pointer to a preallocated memory area to read the data into. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> Requires OpenCL 1.1. </remarks>
        private void Read<T>(ComputeBufferBase<T> source, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long sourceSlicePitch, long destinationRowPitch, long destinationSlicePitch, IntPtr destination, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                sourceOffset.X = new IntPtr(sourceOffset.X.ToInt64() * sizeofT);
                destinationOffset.X = new IntPtr(destinationOffset.X.ToInt64() * sizeofT);
                region.X = new IntPtr(region.X.ToInt64() * sizeofT);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL11.EnqueueReadBufferRect(this.Handle, source.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, &(sourceOffset), &(destinationOffset), &(region), new IntPtr(sourceRowPitch), new IntPtr(sourceSlicePitch), new IntPtr(destinationRowPitch), new IntPtr(destinationSlicePitch), destination, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to read data from a <see cref="ComputeImage"/>.
        /// </summary>
        /// <param name="source"> The <see cref="ComputeImage"/> to read from. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="offset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to read. </param>
        /// <param name="rowPitch"> The <see cref="ComputeImage.RowPitch"/> of <paramref name="source"/> or 0. </param>
        /// <param name="slicePitch"> The <see cref="ComputeImage.SlicePitch"/> of <paramref name="source"/> or 0. </param>
        /// <param name="destination"> A pointer to a preallocated memory area to read the data into. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public void Read(ComputeImage source, bool blocking, SysIntX3 offset, SysIntX3 region, long rowPitch, long slicePitch, IntPtr destination, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReadImage(Handle, source.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, &(offset.X), &(region.X), new IntPtr(rowPitch), new IntPtr(slicePitch), destination, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to release <see cref="ComputeMemory"/>s that have been created from OpenGL objects.
        /// </summary>
        /// <typeparam name="CLMemType"> The type of <paramref name="memObjs"/>. </typeparam>
        /// <param name="memObjs"> A collection of <see cref="ComputeMemory"/>s that correspond to OpenGL memory objects. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void ReleaseGLObjects<CLMemType>(ICollection<CLMemType> memObjs, ICollection<ComputeEventBase> events) where CLMemType: ComputeMemory
        {
            unsafe
            {
                IntPtr[] memObjHandles = Tools.ExtractHandles(memObjs);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* memoryObjectsPtr = memObjHandles)
                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueReleaseGLObjects(Handle, memObjHandles.Length, memoryObjectsPtr, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Gets the string representation of the <see cref="ComputeCommandQueue"/>.
        /// </summary>
        /// <returns> The string representation of the <see cref="ComputeCommandQueue"/>. </returns>
        public override string ToString()
        {
            return "ComputeCommandQueue" + base.ToString();
        }

        /// <summary>
        /// Enqueues a command to unmap a buffer or a <see cref="ComputeImage"/> from the host address space.
        /// </summary>
        /// <param name="memory"> The <see cref="ComputeMemory"/>. </param>
        /// <param name="mappedPtr"> The host address returned by a previous call to <see cref="ComputeCommandQueue.Map"/>. This pointer is <c>IntPtr.Zero</c> after this method returns. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        public void Unmap(ComputeMemory memory, ref IntPtr mappedPtr, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueUnmapMemObject(Handle, memory.Handle, mappedPtr, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                mappedPtr = IntPtr.Zero;

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a wait command for a collection of <see cref="ComputeEvent"/>s to complete before any future commands queued in the <see cref="ComputeCommandQueue"/> are executed.
        /// </summary>
        /// <param name="events"> The <see cref="ComputeEvent"/>s that this command will wait for. </param>
        public void Wait(ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWaitForEvents(Handle, eventHandles.Length, eventHandlesPtr);
                    ComputeException.ThrowOnError(error);
                }
            }
        }

        /// <summary>
        /// Enqueues a command to write data to a buffer.
        /// </summary>
        /// <param name="destination"> The buffer to write to. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="region"> The number of elements to write. </param>
        /// <param name="source"> The data written to the buffer. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public void Write<T>(ComputeBufferBase<T> destination, bool blocking, long destinationOffset, long region, IntPtr source, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWriteBuffer(Handle, destination.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, new IntPtr(destinationOffset * sizeofT), new IntPtr(region * sizeofT), source, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to write a 2D or 3D region of elements to a buffer.
        /// </summary>
        /// <typeparam name="T"> The type of the elements of the buffer. </typeparam>
        /// <param name="destination"> The buffer to write to. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="destinationOffset"> The <paramref name="destination"/> position where writing starts. </param>
        /// <param name="sourceOffset"> The <paramref name="source"/> position where reading starts. </param>
        /// <param name="region"> The region of the elements to copy. </param>
        /// <param name="destinationRowPitch"> The size of the destination buffer row in bytes. If set to zero then <paramref name="destinationRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="destinationSlicePitch"> The size of the destination buffer 2D slice in bytes. If set to zero then <paramref name="destinationSlicePitch"/> equals <c>region.Y * sizeof(T) * destinationRowPitch</c>. </param>
        /// <param name="sourceRowPitch"> The size of the memory area row in bytes. If set to zero then <paramref name="sourceRowPitch"/> equals <c>region.X * sizeof(T)</c>. </param>
        /// <param name="sourceSlicePitch"> The size of the memory area 2D slice in bytes. If set to zero then <paramref name="sourceSlicePitch"/> equals <c>region.Y * sizeof(T) * sourceRowPitch</c>. </param>
        /// <param name="source"> The data written to the buffer. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> Requires OpenCL 1.1. </remarks>
        private void Write<T>(ComputeBufferBase<T> destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long destinationRowPitch, long destinationSlicePitch, long sourceRowPitch, long sourceSlicePitch, IntPtr source, ICollection<ComputeEventBase> events) where T : struct
        {
            unsafe
            {
                int sizeofT = Marshal.SizeOf(typeof(T));
                sourceOffset.X = new IntPtr(sourceOffset.X.ToInt64() * sizeofT);
                destinationOffset.X = new IntPtr(destinationOffset.X.ToInt64() * sizeofT);
                region.X = new IntPtr(region.X.ToInt64() * sizeofT);
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL11.EnqueueWriteBufferRect(this.Handle, destination.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, &(destinationOffset), &(sourceOffset), &(region), new IntPtr(destinationRowPitch), new IntPtr(destinationSlicePitch), new IntPtr(sourceRowPitch), new IntPtr(sourceSlicePitch), source, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        /// <summary>
        /// Enqueues a command to write data to a <see cref="ComputeImage"/>.
        /// </summary>
        /// <param name="destination"> The <see cref="ComputeImage"/> to write to. </param>
        /// <param name="blocking"> Specify whether this a blocking or non-blocking command. </param>
        /// <param name="destinationOffset"> The (x, y, z) offset in pixels where writing starts. </param>
        /// <param name="region"> The region (width, height, depth) in pixels to write. </param>
        /// <param name="rowPitch"> The <see cref="ComputeImage.RowPitch"/> of <paramref name="destination"/> or 0. </param>
        /// <param name="slicePitch"> The <see cref="ComputeImage.SlicePitch"/> of <paramref name="destination"/> or 0. </param>
        /// <param name="source"> The content written to the <see cref="ComputeImage"/>. </param>
        /// <param name="events"> A collection of events that need to complete before this particular command can be executed. If <paramref name="events"/> is not <c>null</c> a new <see cref="ComputeEvent"/> identifying this command is attached to the end of the collection. </param>
        /// <remarks> If <paramref name="blocking"/> is <c>true</c> this method will not return until the command completes. If <paramref name="blocking"/> is <c>false</c> this method will return immediately after the command is enqueued. </remarks>
        public void Write(ComputeImage destination, bool blocking, SysIntX3 destinationOffset, SysIntX3 region, long rowPitch, long slicePitch, IntPtr source, ICollection<ComputeEventBase> events)
        {
            unsafe
            {
                IntPtr[] eventHandles = Tools.ExtractHandles(events);
                IntPtr newEventHandle = IntPtr.Zero;

                fixed (IntPtr* eventHandlesPtr = eventHandles)
                {
                    ComputeErrorCode error = CL10.EnqueueWriteImage(Handle, destination.Handle, (blocking) ? ComputeBoolean.True : ComputeBoolean.False, &(destinationOffset.X), &(region.X), new IntPtr(rowPitch), new IntPtr(slicePitch), source, eventHandles.Length, eventHandlesPtr, (events != null) ? &newEventHandle : null);
                    ComputeException.ThrowOnError(error);
                }

                if (events != null && !events.IsReadOnly)
                    events.Add(new ComputeEvent(newEventHandle, this));
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Releases the associated OpenCL object.
        /// </summary>
        /// <param name="manual"></param>
        protected override void Dispose(bool manual)
        {
            if (manual)
            {
                //free managed resources
            }

            // free native resources
            if (Handle != IntPtr.Zero)
            {
                Trace.WriteLine("Disposing " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
                CL10.ReleaseCommandQueue(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}