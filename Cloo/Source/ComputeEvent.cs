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

using System;
using System.Collections.Generic;
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeEvent: ComputeResource
    {
        #region Fields

        private ComputeJobQueue jobQueue;
        private CommandType jobType;

        #endregion

        #region Properties

        public ComputeJobQueue JobQueue
        {
            get
            {
                return jobQueue;
            }
        }

        public CommandType JobType
        {
            get
            {
                return jobType; 
            }
        }

        public int ExecutionStatus
        {
            get
            {
                return GetInfo<EventInfo, int>(
                    EventInfo.EventCommandExecutionStatus, CL.GetEventInfo );
            }
        }

        public long JobFinishedTimestamp
        {
            get
            {
                return ( long )GetInfo<ProfilingInfo, ulong>(
                    ProfilingInfo.ProfilingCommandEnd, CL.GetEventProfilingInfo );
            }
        }

        public long JobQueuedTimestamp
        {
            get
            {
                return ( long )GetInfo<ProfilingInfo, ulong>(
                    ProfilingInfo.ProfilingCommandQueued, CL.GetEventProfilingInfo );
            }
        }

        public long JobStartedTimestamp
        {
            get
            {
                return ( long )GetInfo<ProfilingInfo, ulong>(
                    ProfilingInfo.ProfilingCommandStart, CL.GetEventProfilingInfo );
            }
        }

        public long JobSubmittedTimestamp
        {
            get
            {
                return ( long )GetInfo<ProfilingInfo, ulong>(
                    ProfilingInfo.ProfilingCommandSubmit, CL.GetEventProfilingInfo );
            }
        }

        #endregion

        #region Constructors

        internal ComputeEvent( IntPtr handle, ComputeJobQueue queue )
        {
            Handle = handle;
            jobQueue = queue;
            jobType = ( CommandType )GetInfo<EventInfo, uint>( 
                EventInfo.EventCommandType, CL.GetEventInfo );
        }

        #endregion

        #region Public methods

        public override string ToString()
        {
            return "ComputeEvent" + base.ToString();
        }

        /// <summary>
        /// Waits on the host thread for jobs identified by event objects in the list to complete.
        /// </summary>
        public static void WaitFor( ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ExtractHandles( events );
            
            int error = CL.WaitForEvents( eventHandles.Length, eventHandles );
            ComputeException.ThrowIfError( error );
        }

        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( Handle != IntPtr.Zero )
            {
                CL.ReleaseEvent( Handle );
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}