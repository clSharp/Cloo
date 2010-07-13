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
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Cloo.Bindings;

    /// <summary>
    /// Represents a list of <c>ComputeEvent</c>s.
    /// </summary>
    /// <seealso cref="ComputeCommandQueue"/>
    /// <seealso cref="ComputeEvent"/>
    public class ComputeEventList : IList<ComputeEvent>
    {
        #region Fields

        private readonly List<ComputeEvent> events;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an empty <c>ComputeEventList</c>.
        /// </summary>
        public ComputeEventList()
        {
            events = new List<ComputeEvent>();
        }

        /// <summary>
        /// Creates a new <c>ComputeEventList</c> from an existing list of <c>ComputeEvent</c>s.
        /// </summary>
        /// <param name="events"> A list of <c>ComputeEvent</c>s. </param>
        public ComputeEventList(IList<ComputeEvent> events)
        {
            events = new Collection<ComputeEvent>(events);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Waits on the host thread for events in this list to complete.
        /// </summary>
        public void Wait()
        {
            unsafe
            {
                fixed (IntPtr* eventHandlesPtr = Tools.ExtractHandles(events))
                {
                    ComputeErrorCode error = CL10.WaitForEvents(events.Count, eventHandlesPtr);
                    ComputeException.ThrowOnError(error);
                }
            }
        }

        #endregion

        #region Internal methods

        internal void FreeGCHandles()
        {
            foreach (ComputeEvent ev in this)
                ev.FreeGCHandle();
        }

        #endregion

        #region IList<ComputeEvent> Members

        public int IndexOf(ComputeEvent item)
        {
            return events.IndexOf(item);
        }

        public void Insert(int index, ComputeEvent item)
        {
            events.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            events.RemoveAt(index);
        }

        public ComputeEvent this[int index]
        {
            get
            {
                return events[index];
            }
            set
            {
                events[index] = value;
            }
        }

        #endregion

        #region ICollection<ComputeEvent> Members

        public void Add(ComputeEvent item)
        {
            events.Add(item);
        }

        public void Clear()
        {
            events.Clear();
        }

        public bool Contains(ComputeEvent item)
        {
            return events.Contains(item);
        }

        public void CopyTo(ComputeEvent[] array, int arrayIndex)
        {
            events.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return events.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ComputeEvent item)
        {
            return events.Remove(item);
        }

        #endregion

        #region IEnumerable<ComputeEvent> Members

        public IEnumerator<ComputeEvent> GetEnumerator()
        {
            return ((IEnumerable<ComputeEvent>)events).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)events).GetEnumerator();
        }

        #endregion
    }
}