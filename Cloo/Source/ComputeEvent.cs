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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL event.
    /// </summary>
    /// <remarks> An event object encapsulates the status of an operation such as a command. It can be used to synchronize operations in a context. </remarks>
    /// <seealso cref="ComputeCommandQueue"/>
    /// <seealso cref="ComputeContext"/>
    public class ComputeEvent : ComputeResource
    {
        #region Fields

        private readonly ComputeCommandQueue commandQueue;
        private readonly ComputeCommandType commandType;
        private GCHandle gcHandle;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <c>ComputeCommandQueue</c> associated with the <c>ComputeEvent</c>.
        /// </summary>
        public ComputeCommandQueue CommandQueue
        {
            get
            {
                return commandQueue;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeCommandType</c> associated with the <c>ComputeEvent</c>.
        /// </summary>
        public ComputeCommandType CommandType
        {
            get
            {
                return commandType;
            }
        }

        /// <summary>
        /// Gets a 64-bit value that describes the current <c>ComputeEvent.CommandQueue.Device</c> time counter in nanoseconds when the command identified by the <c>ComputeEvent</c> has finished execution.
        /// </summary>
        public long CommandFinishTime
        {
            get
            {
                unsafe
                {
                    return GetInfo<ComputeCommandProfilingInfo, long>(
                        ComputeCommandProfilingInfo.Ended, CL10.GetEventProfilingInfo);
                }
            }
        }

        /// <summary>
        /// Gets a 64-bit value that describes the current <c>ComputeEvent.CommandQueue.Device</c> time counter in nanoseconds when the command identified by the <c>ComputeEvent</c> is enqueued in the <c>ComputeEvent.CommandQueue</c> by the host.
        /// </summary>
        public long CommandEnqueueTime
        {
            get
            {
                unsafe
                {
                    return (long)GetInfo<ComputeCommandProfilingInfo, ulong>(
                        ComputeCommandProfilingInfo.Queued, CL10.GetEventProfilingInfo);
                }
            }
        }

        /// <summary>
        /// Gets a 64-bit value that describes the current <c>ComputeEvent.CommandQueue.Device</c> time counter in nanoseconds when the command identified by the <c>ComputeEvent</c> starts execution.
        /// </summary>
        public long CommandStartTime
        {
            get
            {
                unsafe
                {
                    return (long)GetInfo<ComputeCommandProfilingInfo, ulong>(
                        ComputeCommandProfilingInfo.Started, CL10.GetEventProfilingInfo);
                }
            }
        }

        /// <summary>
        /// Gets a 64-bit value that describes the current <c>ComputeEvent.CommandQueue.Device</c> time counter in nanoseconds when the command identified by the <c>ComputeEvent</c> that has been enqueued is submitted by the host to the <c>ComputeEvent.CommandQueue.Device</c>.
        /// </summary>
        public long CommandSubmitTime
        {
            get
            {
                unsafe
                {
                    return (long)GetInfo<ComputeCommandProfilingInfo, ulong>(
                        ComputeCommandProfilingInfo.Submitted, CL10.GetEventProfilingInfo);
                }
            }
        }

        /// <summary>
        /// Gets the execution status of the command identified by the <c>ComputeEvent</c>.
        /// </summary>
        public ComputeCommandExecutionStatus ExecutionStatus
        {
            get
            {
                unsafe
                {
                    return (ComputeCommandExecutionStatus)GetInfo<ComputeEventInfo, int>(
                        ComputeEventInfo.ExecutionStatus, CL10.GetEventInfo);
                }
            }
        }

        #endregion

        #region Constructors

        internal ComputeEvent(IntPtr handle, ComputeCommandQueue queue)
        {
            unsafe
            {
                Handle = handle;
                commandQueue = queue;
                commandType = (ComputeCommandType)GetInfo<ComputeEventInfo, uint>(
                    ComputeEventInfo.CommandType, CL10.GetEventInfo);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the string representation of the <c>ComputeEvent</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeEvent</c>. </returns>
        public override string ToString()
        {
            return "ComputeEvent" + base.ToString();
        }

        #endregion

        #region Internal methods

        internal void FreeGCHandle()
        {
            if (!gcHandle.IsAllocated && gcHandle.Target == null) return;
            gcHandle.Free();
        }

        internal void TrackGCHandle(GCHandle handle)
        {
            gcHandle = handle;
        }

        #endregion

        #region Protected methods

        protected override void Dispose(bool manual)
        {
            if (Handle != IntPtr.Zero)
            {
                CL10.ReleaseEvent(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}