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

    public class ComputeEventCollection: ICollection<ComputeEvent>
    {
        #region Fields
        
        private readonly Collection<ComputeEvent> events;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ComputeEventCollection class that is empty.
        /// </summary>
        public ComputeEventCollection()
        {
            events = new Collection<ComputeEvent>();
        }

        /// <summary>
        /// Initializes a new instance of the ComputeEventCollection class as a wrapper for the specified list.
        /// </summary>
        /// <param name="events">The list that is wrapped by the new collection.</param>
        public ComputeEventCollection( IList<ComputeEvent> events )
        {
            events = new Collection<ComputeEvent>( events );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Waits on the host thread for events contained in this collection to complete.
        /// </summary>
        public void Wait()
        {
            unsafe
            {
                fixed( IntPtr* eventHandlesPtr = Clootils.ExtractHandles( events ) )
                {
                    ComputeErrorCode error = CL10.WaitForEvents( events.Count, eventHandlesPtr );
                    ComputeException.ThrowOnError( error );
                }
            }
        }

        #endregion

        #region ICollection<ComputeEvent> Members

        public void Add( ComputeEvent item )
        {
            events.Add( item );
        }

        public void Clear()
        {
            events.Clear();
        }

        public bool Contains( ComputeEvent item )
        {
            return events.Contains( item );
        }

        public void CopyTo( ComputeEvent[] array, int arrayIndex )
        {
            events.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get { return events.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove( ComputeEvent item )
        {
            return events.Remove( item );
        }

        #endregion

        #region IEnumerable<ComputeEvent> Members

        public IEnumerator<ComputeEvent> GetEnumerator()
        {
            return ( ( IEnumerable<ComputeEvent> )events ).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ( ( IEnumerable )events ).GetEnumerator();
        }

        #endregion
    }
}