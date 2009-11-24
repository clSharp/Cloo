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
        private ComputeJobQueue commandQueue;
        private CommandType commandType;

        public ComputeJobQueue CommandQueue
        {
            get
            {
                return commandQueue;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return commandType; 
            }
        }

        public int ExecutionStatus
        {
            get
            {
                return GetInfo<EventInfo, int, int>(
                    EventInfo.EventCommandExecutionStatus, CL.GetEventInfo );
            }
        }

        public ulong CmdFinishedTimestamp
        {
            get
            {
                return GetInfo<ProfilingInfo, ulong, ulong>(
                    ProfilingInfo.ProfilingCommandEnd, CL.GetEventProfilingInfo );
            }
        }

        public ulong CmdQueuedTimestamp
        {
            get
            {
                return GetInfo<ProfilingInfo, ulong, ulong>(
                    ProfilingInfo.ProfilingCommandQueued, CL.GetEventProfilingInfo );
            }
        }

        public ulong CmdStartedTimestamp
        {
            get
            {
                return GetInfo<ProfilingInfo, ulong, ulong>(
                    ProfilingInfo.ProfilingCommandStart, CL.GetEventProfilingInfo );
            }
        }

        public ulong CmdSubmittedTimestamp
        {
            get
            {
                return GetInfo<ProfilingInfo, ulong, ulong>(
                    ProfilingInfo.ProfilingCommandSubmit, CL.GetEventProfilingInfo );
            }
        }

        internal ComputeEvent( IntPtr handle, ComputeJobQueue queue )
        {
            Handle = handle;
            commandQueue = queue;
            commandType = ( CommandType )GetInfo<EventInfo, long, uint>( 
                EventInfo.EventCommandType, CL.GetEventInfo );
        }

        public override string ToString()
        {
            return "ComputeEvent" + base.ToString();
        }

        /// <summary>
        /// Waits on the host thread for commands identified by event objects in the list to complete.
        /// </summary>
        public static void WaitFor( ICollection<ComputeEvent> events )
        {
            IntPtr[] eventHandles = ExtractHandles( events );
            
            int error = CL.WaitForEvents( eventHandles.Length, eventHandles );
            ComputeTools.CheckError( error );
        }

        protected override void Dispose( bool manual )
        {
            if( Handle != IntPtr.Zero )
            {
                CL.ReleaseEvent( Handle );
                Handle = IntPtr.Zero;
            }
        }
    }
}
