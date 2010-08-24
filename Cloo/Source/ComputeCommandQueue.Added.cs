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

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination) where T : struct
        { Copy(source, destination, 0, 0, source.Count, null); }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, long sourceOffset, long destinationOffset, long count) where T : struct
        { Copy(source, destination, sourceOffset, destinationOffset, count, null); }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region) where T : struct
        { Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), 0, 0, 0, 0, null); }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region) where T : struct
        { Copy(source, destination, sourceOffset, destinationOffset, region, 0, 0, 0, 0, null); }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch) where T : struct
        { Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, null); }

        public void CopyBuffer<T>(ComputeBufferBase<T> source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch) where T : struct
        { Copy(source, destination, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, null); }

        #endregion

        #region CopyBufferToImage

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage destination) where T : struct
        { Copy(source, destination, 0, new SysIntX3(), new SysIntX3(destination.Width, destination.Height, destination.Depth), null); }

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage2D destination, long sourceOffset, SysIntX2 destinationOffset, SysIntX2 destinationRegion) where T : struct
        { Copy(source, destination, sourceOffset, new SysIntX3(destinationOffset, 0), new SysIntX3(destinationRegion, 1), null); }

        public void CopyBufferToImage<T>(ComputeBufferBase<T> source, ComputeImage3D destination, long sourceOffset, SysIntX3 destinationOffset, SysIntX3 destinationRegion) where T : struct
        { Copy(source, destination, sourceOffset, destinationOffset, destinationRegion, null); }

        #endregion

        #region CopyImage

        public void CopyImage(ComputeImage source, ComputeImage destination)
        { Copy(source, destination, new SysIntX3(), new SysIntX3(), new SysIntX3(source.Width, source.Height, (source.Depth == 0 || destination.Depth == 0) ? 1 : source.Depth), null); }

        public void CopyImage(ComputeImage2D source, ComputeImage2D destination, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region)
        { Copy(source, destination, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), null); }

        public void CopyImage(ComputeImage2D source, ComputeImage3D destination, SysIntX2 sourceOffset, SysIntX3 destinationOffset, SysIntX2 region)
        { Copy(source, destination, new SysIntX3(sourceOffset, 0), destinationOffset, new SysIntX3(region, 1), null); }

        public void CopyImage(ComputeImage3D source, ComputeImage2D destination, SysIntX3 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region)
        { Copy(source, destination, sourceOffset, new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), null); }

        public void CopyImage(ComputeImage3D source, ComputeImage3D destination, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region)
        { Copy(source, destination, sourceOffset, destinationOffset, region, null); }

        #endregion

        #region CopyImageToBuffer

        public void CopyImageToBuffer<T>(ComputeImage source, ComputeBufferBase<T> destination) where T : struct
        { Copy(source, destination, new SysIntX3(), 0, new SysIntX3(source.Width, source.Height, (source.Depth == 0) ? 1 : source.Depth), null); }

        public void CopyImageToBuffer<T>(ComputeImage2D source, ComputeBufferBase<T> destination, SysIntX2 sourceOffset, long destinationOffset, SysIntX2 sourceRegion) where T : struct
        { Copy(source, destination, new SysIntX3(sourceOffset, 0), destinationOffset, new SysIntX3(sourceRegion, 1), null); }

        public void CopyImageToBuffer<T>(ComputeImage3D source, ComputeBufferBase<T> destination, SysIntX3 sourceOffset, long destinationOffset, SysIntX3 sourceRegion) where T : struct
        { Copy(source, destination, sourceOffset, destinationOffset, sourceRegion, null); }

        #endregion

        #region ReadFromBuffer

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[] destination, bool blocking) where T : struct
        { ReadFromBuffer(source, ref destination, blocking, 0, 0, source.Count); }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[] destination, bool blocking, long sourceOffset, long destinationOffset, long count) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset + count];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr destinationOffsetPtr = Marshal.UnsafeAddrOfPinnedArrayElement(destination, (int)destinationOffset);
            
            if (blocking)
            {
                Read(source, blocking, sourceOffset, count, destinationOffsetPtr, null);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Read(source, blocking, sourceOffset, count, destinationOffsetPtr, evlist);
                ((ComputeEvent)evlist[0]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[,] destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region) where T : struct
        { ReadFromBuffer(source, ref destination, blocking, sourceOffset, destinationOffset, region, 0, 0); }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[, ,] destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region) where T : struct
        { ReadFromBuffer(source, ref destination, blocking, sourceOffset, destinationOffset, region, 0, 0, 0, 0); }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[,] destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset.Y.ToInt64() + region.Y.ToInt64(), destinationOffset.X.ToInt64() + region.X.ToInt64()];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);

            if (blocking)
            {
                Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, destinationGCHandle.AddrOfPinnedObject(), null);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Read(source, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, destinationGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[0]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        public void ReadFromBuffer<T>(ComputeBufferBase<T> source, ref T[, ,] destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch) where T : struct
        {
            if (destination == null) destination = new T[destinationOffset.Z.ToInt64() + region.Z.ToInt64(), destinationOffset.Y.ToInt64() + region.Y.ToInt64(), destinationOffset.X.ToInt64() + region.X.ToInt64()];
            GCHandle destinationGCHandle = GCHandle.Alloc(destination, GCHandleType.Pinned);

            if (blocking)
            {
                Read(source, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, destinationGCHandle.AddrOfPinnedObject(), null);
                destinationGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Read(source, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, destinationGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[0]).Track(destinationGCHandle);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        #endregion

        #region ReadFromImage

        public void ReadFromImage(ComputeImage source, ref IntPtr destination, bool blocking)
        { throw new NotImplementedException(); }

        public void ReadFromImage(ComputeImage2D source, out IntPtr destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 region)
        { throw new NotImplementedException(); }

        public void ReadFromImage(ComputeImage3D source, out IntPtr destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 region)
        { throw new NotImplementedException(); }

        public void ReadFromImage(ComputeImage2D source, out IntPtr destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 region, long sourceRowPitch)
        { throw new NotImplementedException(); }

        public void ReadFromImage(ComputeImage3D source, out IntPtr destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 region, long sourceRowPitch, long sourceSlicePitch)
        { throw new NotImplementedException(); }

        #endregion

        #region WriteToBuffer

        public void WriteToBuffer<T>(T[] source, ComputeBufferBase<T> destination, bool blocking) where T : struct
        { WriteToBuffer(source, destination, blocking, 0, 0, destination.Count); }

        public void WriteToBuffer<T>(T[] source, ComputeBufferBase<T> destination, bool blocking, long sourceOffset, long destinationOffset, long count) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr sourceOffsetPtr = Marshal.UnsafeAddrOfPinnedArrayElement(source, (int)sourceOffset);

            if (blocking)
            {
                Write(destination, blocking, destinationOffset, count, sourceOffsetPtr, null);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Write(destination, blocking, destinationOffset, count, sourceOffsetPtr, evlist);
                ((ComputeEvent)evlist[0]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[0]).Track(source);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        public void WriteToBuffer<T>(T[,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region) where T : struct
        { WriteToBuffer(source, destination, blocking, sourceOffset, destinationOffset, region, 0, 0); }

        public void WriteToBuffer<T>(T[, ,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region) where T : struct
        { WriteToBuffer(source, destination, blocking, sourceOffset, destinationOffset, region, 0, 0, 0, 0); }

        public void WriteToBuffer<T>(T[,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX2 sourceOffset, SysIntX2 destinationOffset, SysIntX2 region, long sourceRowPitch, long destinationRowPitch) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);

            if (blocking)
            {
                Write(destination, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, sourceGCHandle.AddrOfPinnedObject(), null);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Write(destination, blocking, new SysIntX3(sourceOffset, 0), new SysIntX3(destinationOffset, 0), new SysIntX3(region, 1), sourceRowPitch, 0, destinationRowPitch, 0, sourceGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[0]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[0]).Track(source);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        public void WriteToBuffer<T>(T[, ,] source, ComputeBufferBase<T> destination, bool blocking, SysIntX3 sourceOffset, SysIntX3 destinationOffset, SysIntX3 region, long sourceRowPitch, long destinationRowPitch, long sourceSlicePitch, long destinationSlicePitch) where T : struct
        {
            GCHandle sourceGCHandle = GCHandle.Alloc(source, GCHandleType.Pinned);

            if (blocking)
            {
                Write(destination, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, sourceGCHandle.AddrOfPinnedObject(), null);
                sourceGCHandle.Free();
            }
            else
            {
                IList<ComputeEventBase> evlist = new List<ComputeEventBase>();
                Write(destination, blocking, sourceOffset, destinationOffset, region, sourceRowPitch, sourceSlicePitch, destinationRowPitch, destinationSlicePitch, sourceGCHandle.AddrOfPinnedObject(), evlist);
                ((ComputeEvent)evlist[0]).Track(sourceGCHandle);
                ((ComputeEvent)evlist[0]).Track(source);
                this.events.Add((ComputeEvent)evlist[0]);
            }
        }

        #endregion

        #region WriteToImage

        public void WriteToImage(IntPtr source, ComputeImage destination, bool blocking)
        { throw new NotImplementedException(); }
        
        public void WriteToImage(IntPtr source, ComputeImage2D destination, bool blocking, SysIntX2 destinationOffset, SysIntX2 region)
        { throw new NotImplementedException(); }
        
        public void WriteToImage(IntPtr source, ComputeImage3D destination, bool blocking, SysIntX3 destinationOffset, SysIntX3 region)
        { throw new NotImplementedException(); }
        
        public void WriteToImage(IntPtr source, ComputeImage2D destination, bool blocking, SysIntX2 destinationOffset, SysIntX2 region, long destinationRowPitch)
        { throw new NotImplementedException(); }

        public void WriteToImage(IntPtr source, ComputeImage3D destination, bool blocking, SysIntX3 destinationOffset, SysIntX3 region, long destinationRowPitch, long destinationSlicePitch)
        { throw new NotImplementedException(); }

        #endregion
    }
}