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
        None = 0,
        OutOfOrderExecution = ( 1 << 0 ),
        Profiling = ( 1 << 1 )
    }

    public enum ComputeContextPropertyName: int
    {
        Platform = 0x1084
    }

    public enum ComputeDeviceExecutionCapabilites: long
    {
        OpenCLKernel = ( 1 << 0 ),
        NativeKernel = ( 1 << 1 )
    }

    public enum ComputeDeviceSingleFPCapabilites: long
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

    public enum ComputeDeviceTypes: long
    {
        Default = ( 1 << 0 ),
        Cpu = ( 1 << 1 ),
        Gpu = ( 1 << 2 ),
        Accelerator = ( 1 << 3 ),
        All = 0xFFFFFFFF
    }
        
    public enum ComputeImageChannelOrder: int
    {
        Color = 0x10B0,
        Alpha = 0x10B1,
        ColorColor = 0x10B2,
        ColorAlpha = 0x10B3,
        Rgb = 0x10B4,
        Rgba = 0x10B5,
        Bgra = 0x10B6,
        Argb = 0x10B7,
        Intensity = 0x10B8,
        Luminance = 0x10B9
    }

    public enum ComputeImageChannelType: int
    {
        SNormInt8 = 0x10D0,
        SNormInt16 = 0x10D1,
        UNormInt8 = 0x10D2,
        UNormInt16 = 0x10D3,
        UNormShort565 = 0x10D4,
        UNormShort555 = 0x10D5,
        UNormInt101010 = 0x10D6,
        SignedInt8 = 0x10D7,
        SignedInt16 = 0x10D8,
        SignedInt32 = 0x10D9,
        UnsignedInt8 = 0x10DA,
        UnsignedInt16 = 0x10DB,
        UnsingedInt32 = 0x10DC,
        HalfFloat = 0x10DD,
        Float = 0x10DE,
    }

    public enum ComputeMemoryFlags: long
    {
        ReadWrite = ( 1 << 0 ),
        WriteOnly = ( 1 << 1 ),
        ReadOnly = ( 1 << 2 ),
        UseHostPointer = ( 1 << 3 ),
        AllocateHostPointer = ( 1 << 4 ),
        CopyHostPointer = ( 1 << 5 )
    }

    public enum ComputeMemoryMappingFlags: long
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

    public enum ComputeImageAddressing: int
    {
        None = 0x1130,
        ClampToEdge = 0x1131,
        Clamp = 0x1132,
        Repeat = 0x1133
    }

    public enum ComputeImageFiltering: int
    {
        Nearest = 0x1140,
        Linear = 0x1141
    }
}