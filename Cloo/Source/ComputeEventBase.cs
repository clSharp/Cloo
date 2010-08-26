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
    /// Represents the parent type to any Cloo event types.
    /// </summary>
    /// <seealso cref="ComputeEvent"/>
    /// <seealso cref="ComputeUserEvent"/>
    public class ComputeEventBase : ComputeResource
    {
        #region Fields

        protected ComputeEventCallbackRaw notifier;

        #endregion

        #region Events

        /// <summary>
        /// Occurrs when <c>ComputeEventBase.CommandExecutionStatus</c> changes to <c>ComputeCommandExecutionStatus.Complete</c>.
        /// </summary>
        public event ComputeEventCallback Completed;

        /// <summary>
        /// Occurrs when the operation that generated the event is abnormally terminated.
        /// </summary>
        public event ComputeEventCallback Terminated;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <c>ComputeCommandType</c> associated with the event.
        /// </summary>
        public ComputeCommandType CommandType { get; protected set; }

        /// <summary>
        /// Gets a 64-bit value that describes the device time counter in nanoseconds when the event's command has finished execution.
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
        /// Gets a 64-bit value that describes the device time counter in nanoseconds when the event's command is enqueued in the <c>ComputeCommandQueue</c> by the host.
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
        /// Gets the execution status of the associated command.
        /// </summary>
        /// <remarks> Is negative if the command's execution was abnormally terminated. </remarks>
        public ComputeCommandExecutionStatus CommandExecutionStatus
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

        /// <summary>
        /// Gets a 64-bit value that describes the device time counter in nanoseconds when the associated command starts execution.
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
        /// Gets a 64-bit value that describes the device time counter in nanoseconds when the associated command that has been enqueued is submitted by the host to the device.
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
        /// Gets the <c>ComputeContext</c> associated with the event.
        /// </summary>
        public ComputeContext Context { get; protected set; }

        #endregion
        
        #region Public methods

        /// <summary>
        /// Gets the string representation of the <c>ComputeEventBase</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputeEventBase</c>. </returns>
        public override string ToString()
        {
            return "ComputeEvent" + base.ToString();
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

        protected void Notify(IntPtr eventHandle, int cmdExecStatusOrErr, IntPtr userData)
        {
            switch (cmdExecStatusOrErr)
            {
                case (int)ComputeCommandExecutionStatus.Complete:
                    if (Completed != null)
                        Completed(this, new EventArgs());
                    break;
                default:
                    if (Terminated != null)
                        Terminated(this, new EventArgs());
                    break;
            }
        }

        #endregion
    }

    //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void ComputeEventCallbackRaw(IntPtr eventHandle, int cmdExecStatusOrErr, IntPtr userData);

    //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void ComputeEventCallback(object sender, EventArgs e);
}