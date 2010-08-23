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
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public partial class ComputeCommandQueue
    {
        #region Obsolete

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Copy<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, 0, 0, source.Count, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        public void Copy<T>(ComputeBufferBase<T> source, ComputeImage destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, 0, new long[] { 0, 0, 0 }, new long[] { destination.Width, destination.Height, destination.Depth }, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Copy<T>(ComputeImage source, ComputeBufferBase<T> destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, new long[] { 0, 0, 0 }, 0, new long[] { source.Width, source.Height, source.Depth }, events);

        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Copy(ComputeImage source, ComputeImage destination, ICollection<ComputeEventBase> events)
        {
            long[] offset = new long[] { 0, 0, 0 };
            Copy(source, destination, offset, offset, new long[] { source.Width, source.Height, source.Depth }, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public IntPtr Map<T>(ComputeBufferBase<T> buffer, bool blocking, ComputeMemoryMappingFlags flags, ICollection<ComputeEventBase> events) where T : struct
        {
            return Map(buffer, blocking, flags, 0, buffer.Count, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public IntPtr Map(ComputeImage image, bool blocking, ComputeMemoryMappingFlags flags, ICollection<ComputeEventBase> events)
        {
            return Map(image, blocking, flags, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public T[] Read<T>(ComputeBufferBase<T> buffer, ICollection<ComputeEventBase> events) where T : struct
        {
            return Read(buffer, 0, buffer.Count, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public T[] Read<T>(ComputeBufferBase<T> buffer, long offset, long count, ICollection<ComputeEventBase> events) where T : struct
        {
            T[] data = new T[count];
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try { Read(buffer, true, offset, count, dataHandle.AddrOfPinnedObject(), events); }
            finally { dataHandle.Free(); }
            return data;
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Read(ComputeImage image, bool blocking, IntPtr destination, ICollection<ComputeEventBase> events)
        {
            Read(image, blocking, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, image.RowPitch, image.SlicePitch, destination, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Read(ComputeImage image, bool blocking, long[] offset, long[] region, IntPtr destination, ICollection<ComputeEventBase> events)
        {
            Read(image, blocking, offset, region, image.RowPitch, image.SlicePitch, destination, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Write<T>(ComputeBufferBase<T> buffer, T[] source, ICollection<ComputeEventBase> events) where T : struct
        {
            Write(buffer, 0, source.Length, source, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Write<T>(ComputeBufferBase<T> buffer, long offset, long count, T[] source, ICollection<ComputeEventBase> events) where T : struct
        {
            GCHandle dataHandle = GCHandle.Alloc(source, GCHandleType.Pinned);
            try { Write(buffer, true, offset, count, dataHandle.AddrOfPinnedObject(), events); }
            finally { dataHandle.Free(); }
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Write(ComputeImage image, bool blocking, IntPtr source, ICollection<ComputeEventBase> events)
        {
            Write(image, blocking, new long[] { 0, 0, 0 }, new long[] { image.Width, image.Height, image.Depth }, source, events);
        }

        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete]
        public void Write(ComputeImage destination, bool blocking, long[] offset, long[] region, IntPtr source, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, offset, region, destination.RowPitch, destination.SlicePitch, source, events);
        }

        #endregion
    }
}