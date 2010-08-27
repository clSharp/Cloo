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
        #region CopyBuffer

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, 0, 0, source.Count, events);
        }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, long sourceOffset, long destinationOffset, long count, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, destinationOffset, count, events);
        }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), 0, 0, 0, 0, events);
        }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, destinationOffset, region, 0, 0, 0, 0, events);
        }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, events);
        }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, events);
        }

        #endregion

        #region CopyBufferToImage

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, 0, new SysIntX3(), new SysIntX3(destination.Width, destination.Height, destination.Depth), events);
        }

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage2D destination, long sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), events);
        }

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage3D destination, long sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, destinationOffset, region, events);
        }

        #endregion

        #region CopyImage

        public void CopyImage(ComputeImage source, ComputeImage destination, ICollection<ComputeEventBase> events)
        {
            Copy(source, destination, new SysIntX3(), new SysIntX3(), new SysIntX3(source.Width, source.Height, (source.Depth == 0 || destination.Depth == 0) ? 1 : source.Depth), events);
        }

        public void CopyImage(ComputeImage2D source, ComputeImage2D destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events)
        {
            Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), events);
        }

        public void CopyImage(ComputeImage2D source, ComputeImage3D destination, SysIntX2 sourceOffset, SysIntX3 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events)
        {
            Copy(source, destination, new SysIntX3(sourceOffset, 0), destinationOffset, new SysIntX3(region, 1), events);
        }

        public void CopyImage(ComputeImage3D source, ComputeImage2D destination, SysIntX3 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events)
        {
            Copy(source, destination, sourceOffset, new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), null);
        }

        public void CopyImage(ComputeImage3D source, ComputeImage3D destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events)
        {
            Copy(source, destination, sourceOffset, destinationOffset, region, events);
        }

        #endregion

        #region CopyImageToBuffer

        public void CopyImageToBuffer<T>(ComputeImage source, ComputeBufferBase<T> destination, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, new SysIntX3(), 0, new SysIntX3(source.Width, source.Height, (source.Depth == 0) ? 1 : source.Depth), events);
        }

        public void CopyImageToBuffer<T>(ComputeImage2D source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, long destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, new SysIntX3(sourceOffset, 0), destinationOffset, new SysIntX3(region, 1), events);
        }

        public void CopyImageToBuffer<T>(ComputeImage3D source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, long destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            Copy(source, destination, sourceOffset, destinationOffset, region, events);
        }

        #endregion

        #region ReadFromBuffer

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[] destination, bool blocking, ICollection<ComputeEventBase> events) where T : struct
        {
            ReadFromBuffer(source, ref destination, blocking, 0, 0, source.Count, events);
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[] destination, bool blocking, long sourceOffset, long destinationOffset, long count, ICollection<ComputeEventBase> events) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset + count];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destinationOffsetPtr = Marshal.UnsafeAddrOfPinnedArrayElement(destination, (int)destinationOffset);
            
            if (blocking)
            {
                Read(source, blocking, sourceOffset, count, destinationOffsetPtr, events);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Read(source, blocking, sourceOffset, count, destinationOffsetPtr, evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[,] destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events) where T : struct
        {
            ReadFromBuffer(source, ref destination, blocking, sourceOffset, destinationOffset, region, 0, 0, events);
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[, ,] destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            ReadFromBuffer(source, ref destination, blocking, sourceOffset, destinationOffset, region, 0, 0, 0, 0, events);
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[,] destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch, ICollection<ComputeEventBase> events) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset.Y.ToInt64() + region.Y.ToInt64(), destinationOffset.X.ToInt64() + region.X.ToInt64()];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);

            if (blocking)
            {
                Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, destinationGCHandle.AddrOfPinnedObject(), events);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, destinationGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[, ,] destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch, ICollection<ComputeEventBase> events) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset.Z.ToInt64() + region.Z.ToInt64(), destinationOffset.Y.ToInt64() + region.Y.ToInt64(), destinationOffset.X.ToInt64() + region.X.ToInt64()];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);

            if (blocking)
            {
                Read(source, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, destinationGCHandle.AddrOfPinnedObject(), events);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Read(source, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, destinationGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        #endregion

        #region ReadFromImage

        public void ReadFromImage(ComputeImage source, ref IntPtr destination, bool blocking, ICollection<ComputeEventBase> events)
        {
            Read(source, blocking, new SysIntX3(), new SysIntX3(source.Width, source.Height, source.Depth), 0, 0, destination, events);
        }

        public void ReadFromImage(ComputeImage2D source, ref IntPtr destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 region, ICollection<ComputeEventBase> events)
        {
            Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(region, 1), 0, 0, destination, events);
        }

        public void ReadFromImage(ComputeImage3D source, ref IntPtr destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 region, ICollection<ComputeEventBase> events)
        {
            Read(source, blocking, sourceOffset, region, 0, 0, destination, events);
        }

        public void ReadFromImage(ComputeImage2D source, ref IntPtr destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 region, long sourceRowPitch, ICollection<ComputeEventBase> events)
        {
            Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destination, events);
        }

        public void ReadFromImage(ComputeImage3D source, ref IntPtr destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 region, long sourceRowPitch, long sourceSlicePitch, ICollection<ComputeEventBase> events)
        {
            Read(source, blocking, sourceOffset, region, sourceRowPitch, sourceSlicePitch, destination, events);
        }

        #endregion

        #region WriteToBuffer

        public void WriteToBuffer<T>(T[] source, ComputeBufferBase<T> destination, bool blocking, ICollection<ComputeEventBase> events) where T : struct
        {
            WriteToBuffer(source, destination, blocking, 0, 0, destination.Count, events);
        }

        public void WriteToBuffer<T>(T[] source, ComputeBufferBase<T> destination, bool blocking, long sourceOffset, long destinationOffset, long count, ICollection<ComputeEventBase> events) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr sourceOffsetPtr = Marshal.UnsafeAddrOfPinnedArrayElement(source, (int)sourceOffset);

            if (blocking)
            {
                Write(destination, blocking, destinationOffset, count, sourceOffsetPtr, events);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Write(destination, blocking, destinationOffset, count, sourceOffsetPtr, evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(source);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        public void WriteToBuffer<T>(T[,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events) where T : struct
        {
            WriteToBuffer(source, destination, blocking, sourceOffset, destinationOffset, region, 0, 0, events);
        }

        public void WriteToBuffer<T>(T[, ,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events) where T : struct
        {
            WriteToBuffer(source, destination, blocking, sourceOffset, destinationOffset, region, 0, 0, 0, 0, events);
        }

        public void WriteToBuffer<T>(T[,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch, ICollection<ComputeEventBase> events) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);

            if (blocking)
            {
                Write(destination, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, sourceGCHandle.AddrOfPinnedObject(), events);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Write(destination, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, sourceGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(source);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        public void WriteToBuffer<T>(T[, ,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch, ICollection<ComputeEventBase> events) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);

            if (blocking)
            {
                Write(destination, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, sourceGCHandle.AddrOfPinnedObject(), events);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = (events != null) ? (List<ComputeEventBase>)events : new List<ComputeEventBase>();
                Write(destination, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, sourceGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[evlist.Count - 1]).Track(source);
                this.events.Add((ComputeEvent)evlist[evlist.Count - 1]);
            }
        }

        #endregion

        #region WriteToImage

        public void WriteToImage(IntPtr source, ComputeImage destination, bool blocking, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, new SysIntX3(), new SysIntX3(destination.Width, destination.Height, destination.Depth), 0, 0, source, events);
        }

        public void WriteToImage(IntPtr source, ComputeImage2D destination, bool blocking, SysIntX2 destinationOffset, SysIntX2 region, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), 0, 0, source, events);
        }

        public void WriteToImage(IntPtr source, ComputeImage3D destination, bool blocking, SysIntX3 destinationOffset, SysIntX3 region, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, destinationOffset, region, 0, 0, source, events);
        }

        public void WriteToImage(IntPtr source, ComputeImage2D destination, bool blocking, SysIntX2 destinationOffset, SysIntX2 region, long destinationRowPitch, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), destinationRowPitch, 0, source, events);
        }

        public void WriteToImage(IntPtr source, ComputeImage3D destination, bool blocking, SysIntX3 destinationOffset, SysIntX3 region, long destinationRowPitch, long destinationSlicePitch, ICollection<ComputeEventBase> events)
        {
            Write(destination, blocking, destinationOffset, region, destinationRowPitch, destinationSlicePitch, source, events);
        }

        #endregion
    }
}