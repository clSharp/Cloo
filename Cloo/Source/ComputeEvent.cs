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
    public class ComputeEvent : ComputeEventBase
    {
        #region Properties

        /// <summary>
        /// Gets the <c>ComputeCommandQueue</c> associated with the <c>ComputeEvent</c>.
        /// </summary>
        /// <remarks> Is <c>null</c> for user <c>ComputeEvent</c>s. </remarks>
        public ComputeCommandQueue CommandQueue { get; private set; }

        #endregion

        #region Constructors

        internal ComputeEvent(IntPtr handle, ComputeCommandQueue queue)
        {
            unsafe
            {
                Handle = handle;
                CommandQueue = queue;
                CommandType = (ComputeCommandType)GetInfo<ComputeEventInfo, uint>(
                    ComputeEventInfo.CommandType, CL10.GetEventInfo);
                Context = queue.Context;

                if (Tools.ParseVersionString(CommandQueue.Device.Version) == new Version(1, 1))
                {
                    notifier = Notify;
                    IntPtr notifyPtr = Marshal.GetFunctionPointerForDelegate(notifier);
                    CL11.SetEventCallback(Handle, (int)ComputeCommandExecutionStatus.Complete, notifyPtr, IntPtr.Zero);
                }
            }
        }

        #endregion
    }
}