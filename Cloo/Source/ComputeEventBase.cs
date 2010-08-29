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

        private ComputeEventCallback statusNotify;

        #endregion

        #region Events

        /// <summary>
        /// Occurrs when the command associated with the event is abnormally terminated.
        /// </summary>
        public event ComputeCommandStatusChanged Aborted;

        /// <summary>
        /// Occurrs when <c>ComputeEventBase.Status</c> changes to <c>ComputeCommandExecutionStatus.Complete</c>.
        /// </summary>
        public event ComputeCommandStatusChanged Completed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <c>ComputeContext</c> associated with the event.
        /// </summary>
        public ComputeContext Context { get; protected set; }

        /// <summary>
        /// Gets a 64-bit value that describes the device time counter in nanoseconds when the event's command has finished execution.
        /// </summary>
        public long FinishTime
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
        public long EnqueueTime
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
        public ComputeCommandExecutionStatus Status
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
        public long StartTime
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
        public long SubmitTime
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
        /// Gets the <c>ComputeCommandType</c> associated with the event.
        /// </summary>
        public ComputeCommandType Type { get; protected set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manual"></param>
        protected override void Dispose(bool manual)
        {
            if (Handle != IntPtr.Zero)
            {
                CL10.ReleaseEvent(Handle);
                Handle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void HookNotifier()
        {
            statusNotify = new ComputeEventCallback(StatusNotify);
            ComputeErrorCode error = CL11.SetEventCallback(Handle, (int)ComputeCommandExecutionStatus.Complete, statusNotify, IntPtr.Zero);
            ComputeException.ThrowOnError(error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evArgs"></param>
        protected virtual void OnCompleted(object sender, ComputeCommandStatusArgs evArgs)
        {
            if (Completed != null)
                Completed(sender, evArgs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evArgs"></param>
        protected virtual void OnAborted(object sender, ComputeCommandStatusArgs evArgs)
        {
            if (Aborted != null)
                Aborted(sender, evArgs);
        }

        #endregion

        #region Private methods

        private void StatusNotify(IntPtr eventHandle, int cmdExecStatusOrErr, IntPtr userData)
        {
            ComputeCommandStatusArgs statusArgs = new ComputeCommandStatusArgs(this, (ComputeCommandExecutionStatus)cmdExecStatusOrErr);
            switch (cmdExecStatusOrErr)
            {
                case (int)ComputeCommandExecutionStatus.Complete: OnCompleted(this, statusArgs); break;
                default: OnAborted(this, statusArgs); break;
            }
        }

        #endregion

        #region Obsolete
#if OBSOLETE
        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public long CommandEnqueueTime { get { return EnqueueTime; } }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public ComputeCommandExecutionStatus CommandExecutionStatus { get { return Status; } }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public long CommandFinishTime { get { return FinishTime; } }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public long CommandStartTime { get { return StartTime; } }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public long CommandSubmitTime { get { return SubmitTime; } }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public ComputeCommandType CommandType { get { return Type; } }
#endif
        #endregion
    }

    /// <summary>
    /// Represents the arguments of a command status change.
    /// </summary>
    public class ComputeCommandStatusArgs : EventArgs
    {
        /// <summary>
        /// Gets the event associated with the command that had its status changed.
        /// </summary>
        public ComputeEventBase Event { get; private set; }

        /// <summary>
        /// Gets the execution status of the command represented by the event.
        /// </summary>
        /// <remarks> Returns a negative integer if the command was abnormally terminated. </remarks>
        public ComputeCommandExecutionStatus Status { get; private set; }

        /// <summary>
        /// Creates a new <c>ComputeCommandStatusArgs</c> instance.
        /// </summary>
        /// <param name="ev"> The event representing the command that had its status changed. </param>
        /// <param name="status"> The status of the command. </param>
        public ComputeCommandStatusArgs(ComputeEventBase ev, ComputeCommandExecutionStatus status)
        {
            Event = ev;
            Status = status;
        }

        /// <summary>
        /// Creates a new <c>ComputeCommandStatusArgs</c> instance.
        /// </summary>
        /// <param name="ev"> The event of the command that had its status changed. </param>
        /// <param name="status"> The status of the command. </param>
        public ComputeCommandStatusArgs(ComputeEventBase ev, int status)
            : this(ev, (ComputeCommandExecutionStatus)status)
        { }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventHandle"></param>
    /// <param name="cmdExecStatusOrErr"></param>
    /// <param name="userData"></param>
    public delegate void ComputeEventCallback(IntPtr eventHandle, int cmdExecStatusOrErr, IntPtr userData);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void ComputeCommandStatusChanged(object sender, ComputeCommandStatusArgs args);
}