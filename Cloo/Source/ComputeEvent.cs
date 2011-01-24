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
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL event.
    /// </summary>
    /// <remarks> An event encapsulates the status of an operation such as a command. It can be used to synchronize operations in a context. </remarks>
    /// <seealso cref="ComputeUserEvent"/>
    /// <seealso cref="ComputeCommandQueue"/>
    /// <seealso cref="ComputeContext"/>
    public class ComputeEvent : ComputeEventBase
    {
        #region Fields

        private GCHandle gcHandle;
        private Array array;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="ComputeCommandQueue"/> associated with the <see cref="ComputeEvent"/>.
        /// </summary>
        /// <value> The <see cref="ComputeCommandQueue"/> associated with the <see cref="ComputeEvent"/>. </value>
        public ComputeCommandQueue CommandQueue { get; private set; }

        #endregion

        #region Constructors

        internal ComputeEvent(IntPtr handle, ComputeCommandQueue queue)
        {
            unsafe
            {
                Handle = handle;
                CommandQueue = queue;
                Type = (ComputeCommandType)GetInfo<ComputeEventInfo, uint>(
                    ComputeEventInfo.CommandType, CL10.GetEventInfo);
                Context = queue.Context;

                if (CommandQueue.Device.Version == new Version(1, 1))
                    HookNotifier();

                Completed += new ComputeCommandStatusChanged(ComputeEvent_Fired);
                Aborted += new ComputeCommandStatusChanged(ComputeEvent_Fired);
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        #endregion

        #region Internal methods

        internal void FreeTracks()
        {
            if (gcHandle.IsAllocated && gcHandle.Target != null)
            {
                gcHandle.Free();
                array = null;
            }
        }

        internal void Track(GCHandle handle)
        {
            gcHandle = handle;
        }

        internal void Track(Array array)
        {
            this.array = array;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Releases the associated OpenCL object.
        /// </summary>
        /// <param name="manual"> Specifies the operation mode of this method. </param>
        protected override void Dispose(bool manual)
        {
            FreeTracks();
            base.Dispose(manual);
        }

        #endregion

        #region Private methods

        private void ComputeEvent_Fired(object sender, EventArgs e)
        {
            lock (CommandQueue.events)
            {
#if DEBUG
                if (CommandQueue.events.IndexOf(this) < 0) 
                    throw new Exception("Shouldn't reach here.");
#endif
                CommandQueue.events.Remove(this);
                Dispose();
            }
        }

        #endregion
    }
}