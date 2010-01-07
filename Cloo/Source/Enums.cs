#region License

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

#endregion

namespace Cloo
{
    using System;

    public enum ComputeCommandType: int
    {
        NDRangeKernel = 0x11F0,
        Task = 0x11F1,
        NativeKernel = 0x11F2,
        ReadBuffer = 0x11F3,
        WriteBuffer = 0x11F4,
        CopyBuffer = 0x11F5,
        ReadImage = 0x11F6,
        WriteImage = 0x11F7,
        CopyImage = 0x11F8,
        CopyImageToBuffer = 0x11F9,
        CopyBufferToImage = 0x11FA,
        MapBuffer = 0x11FB,
        MapImage = 0x11FC,
        UnmapMemory = 0x11FD,
        Marker = 0x11FE,
        AcquireGLObjects = 0x11FF,
        ReleaseGLObjects = 0x1200
    }

    public enum ComputeCommandQueueFlags: long
    {
        OutOfOrderExecution = ( 1 << 0 ),
        Profiling = ( 1 << 1 )
    }

    public enum ComputeDeviceExecutionFlags: long
    {
        OpenCLKernel = ( 1 << 0 ),
        NativeKernel = ( 1 << 1 )
    }

    public enum ComputeDeviceFPFlags: long
    {
        Denorm = ( 1 << 0 ),
        InfNan = ( 1 << 1 ),
        RoundToNearest = ( 1 << 2 ),
        RoundToZero = ( 1 << 3 ),
        RoundToInf = ( 1 << 4 ),
        Fma = ( 1 << 5 )
    }

    public enum ComputeDeviceLocalMemoryType: int
    {
        Local = 0x1,
        Global = 0x2
    }

    public enum ComputeDeviceMemoryCacheType: int
    {
        None = 0x0,
        ReadOnly = 0x1,
        WriteOnly = 0x2
    }

    public enum ComputeDeviceTypeFlags: long
    {
        Default = ( 1 << 0 ),
        Cpu = ( 1 << 1 ),
        Gpu = ( 1 << 2 ),
        Accelerator = ( 1 << 3 ),
        All = 0xFFFFFFFF
    }

    /*
    public enum ComputeErrorCode: int
    {

    }
    */

    public enum ComputeMemoryFlags: long
    {
        ReadWrite = ( 1 << 0 ),
        WriteOnly = ( 1 << 1 ),
        ReadOnly = ( 1 << 2 ),
        UseHostPtr = ( 1 << 3 ),
        AllocHostPtr = ( 1 << 4 ),
        CopyHostPtr = ( 1 << 5 )
    }

    public enum ComputeMemoryMapFlags: long
    {
        Read = ( 1 << 0 ),
        Write = ( 1 << 1 )
    }

    public enum ComputeMemoryType: int
    {
        Buffer = 0x10F0,
        Image2D = 0x10F1,
        Image3D = 0x10F2
    }

    public enum ComputeSamplerAddressing: int
    {
        None = 0x1130,
        ClampToEdge = 0x1131,
        Clamp = 0x1132,
        Repeat = 0x1133
    }

    public enum ComputeSamplerFiltering: int
    {
        Nearest = 0x1140,
        Linear = 0x1141
    }
}