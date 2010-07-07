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

    public enum ComputeErrorCode : int
    {
        Success = 0,
        DeviceNotFound = -1,
        DeviceNotAvailable = -2,
        CompilerNotAvailable = -3,
        MemoryObjectAllocationFailure = -4,
        OutOfResources = -5,
        OutOfHostMemory = -6,
        ProfilingInfoNotAvailable = -7,
        MemoryCopyOverlap = -8,
        ImageFormatMismatch = -9,
        ImageFormatNotSupported = -10,
        BuildProgramFailure = -11,
        MapFailure = -12,
        MisalignedSubBufferOffset = -13,
        ExecutionStatusErrorForEventsInWaitList = -14,
        InvalidValue = -30,
        InvalidDeviceType = -31,
        InvalidPlatform = -32,
        InvalidDevice = -33,
        InvalidContext = -34,
        InvalidCommandQueueFlags = -35,
        InvalidCommandQueue = -36,
        InvalidHostPointer = -37,
        InvalidMemoryObject = -38,
        InvalidImageFormatDescriptor = -39,
        InvalidImageSize = -40,
        InvalidSampler = -41,
        InvalidBinary = -42,
        InvalidBuildOptions = -43,
        InvalidProgram = -44,
        InvalidProgramExecutable = -45,
        InvalidKernelName = -46,
        InvalidKernelDefinition = -47,
        InvalidKernel = -48,
        InvalidArgumentIndex = -49,
        InvalidArgumentValue = -50,
        InvalidArgumentSize = -51,
        InvalidKernelArguments = -52,
        InvalidWorkDimension = -53,
        InvalidWorkGroupSize = -54,
        InvalidWorkItemSize = -55,
        InvalidGlobalOffset = -56,
        InvalidEventWaitList = -57,
        InvalidEvent = -58,
        InvalidOperation = -59,
        InvalidGLObject = -60,
        InvalidBufferSize = -61,
        InvalidMipLevel = -62,
        InvalidGlobalWorkSize = -63,
        CL_INVALID_GL_SHAREGROUP_REFERENCE_KHR = -1000,
        CL_PLATFORM_NOT_FOUND_KHR = -1001,
    }

    public enum OpenCLVersion : int
    {
        Version_1_0 = 1,
        Version_1_1 = 1
    }

    public enum ComputeBoolean : int
    {
        False = 0,
        True = 1
    }

    public enum ComputePlatformInfo : int
    {
        Profile = 0x0900,
        Version = 0x0901,
        Name = 0x0902,
        Vendor = 0x0903,
        Extensions = 0x0904,
        CL_PLATFORM_ICD_SUFFIX_KHR = 0x0920,
    }

    [Flags]
    public enum ComputeDeviceTypes : long
    {
        Default = 1 << 0,
        Cpu = 1 << 1,
        Gpu = 1 << 2,
        Accelerator = 1 << 3,
        All = 0xFFFFFFFF
    }

    public enum ComputeDeviceInfo : int
    {
        Type = 0x1000,
        VendorId = 0x1001,
        MaxComputeUnits = 0x1002,
        MaxWorkItemDimensions = 0x1003,
        MaxWorkGroupSize = 0x1004,
        MaxWorkItemSizes = 0x1005,
        PreferredVectorWidthChar = 0x1006,
        PreferredVectorWidthShort = 0x1007,
        PreferredVectorWidthInt = 0x1008,
        PreferredVectorWidthLong = 0x1009,
        PreferredVectorWidthFloat = 0x100A,
        PreferredVectorWidthDouble = 0x100B,
        MaxClockFrequency = 0x100C,
        AddressBits = 0x100D,
        MaxReadImageArguments = 0x100E,
        MaxWriteImageArguments = 0x100F,
        MaxMemoryAllocationSize = 0x1010,
        Image2DMaxWidth = 0x1011,
        Image2DMaxHeight = 0x1012,
        Image3DMaxWidth = 0x1013,
        Image3DMaxHeight = 0x1014,
        Image3DMaxDepth = 0x1015,
        ImageSupport = 0x1016,
        MaxParameterSize = 0x1017,
        MaxSamplers = 0x1018,
        MemoryBaseAddressAlignment = 0x1019,
        MinDataTypeAlignmentSize = 0x101A,
        SingleFPConfig = 0x101B,
        GlobalMemoryCacheType = 0x101C,
        GlobalMemoryCachelineSize = 0x101D,
        GlobalMemoryCacheSize = 0x101E,
        GlobalMemorySize = 0x101F,
        MaxConstantBufferSize = 0x1020,
        MaxConstantArguments = 0x1021,
        LocalMemoryType = 0x1022,
        LocalMemorySize = 0x1023,
        ErrorCorrectionSupport = 0x1024,
        ProfilingTimerResolution = 0x1025,
        EndianLittle = 0x1026,
        Available = 0x1027,
        CompilerAvailable = 0x1028,
        ExecutionCapabilities = 0x1029,
        CommandQueueProperties = 0x102A,
        Name = 0x102B,
        Vendor = 0x102C,
        DriverVersion = 0x102D,
        Profile = 0x102E,
        Version = 0x102F,
        Extensions = 0x1030,
        Platform = 0x1031,
        CL_DEVICE_DOUBLE_FP_CONFIG = 0x1032,
        CL_DEVICE_HALF_FP_CONFIG = 0x1033,
        PreferredVectorWidthHalf = 0x1034,
        HostUnifiedMemory = 0x1035,
        NativeVectorWidthChar = 0x1036,
        NativeVectorWidthShort = 0x1037,
        NativeVectorWidthInt = 0x1038,
        NativeVectorWidthLong = 0x1039,
        NativeVectorWidthFloat = 0x103A,
        NativeVectorWidthDouble = 0x103B,
        NativeVectorWidthHalf = 0x103C,
        OpenCLCVersion = 0x103D
    }

    [Flags]
    public enum ComputeDeviceSingleCapabilities : long
    {
        Denorm = 1 << 0,
        InfNan = 1 << 1,
        RoundToNearest = 1 << 2,
        RoundToZero = 1 << 3,
        RoundToInf = 1 << 4,
        Fma = 1 << 5,
        SoftFloat = 1 << 6
    }

    public enum ComputeDeviceMemoryCacheType : int
    {
        None = 0x0,
        ReadOnlyCache = 0x1,
        ReadWriteCache = 0x2,
    }

    public enum ComputeDeviceLocalMemoryType : int
    {
        Local = 0x1,
        Global = 0x2
    }

    public enum ComputeDeviceExecutionCapabilities : int
    {
        OpenCLKernel = 1 << 0,
        NativeKernel = 1 << 1
    }

    [Flags]
    public enum ComputeCommandQueueFlags : long
    {
        None = 0,
        OutOfOrderExecution = 1 << 0,
        Profiling = 1 << 1
    }

    public enum ComputeContextInfo : int
    {
        ReferenceCount = 0x1080,
        Devices = 0x1081,
        Properties = 0x1082,
        NumDevices = 0x1083,
        Platform = 0x1084,
    }

    public enum ComputeContextPropertyName : int
    {
        Platform = ComputeContextInfo.Platform,
        CL_GL_CONTEXT_KHR = 0x2008,
        CL_EGL_DISPLAY_KHR = 0x2009,
        CL_GLX_DISPLAY_KHR = 0x200A,
        CL_WGL_HDC_KHR = 0x200B,
        CL_CGL_SHAREGROUP_KHR = 0x200C,
    }

    public enum ComputeCommandQueueInfo : int
    {
        Context = 0x1090,
        Device = 0x1091,
        ReferenceCount = 0x1092,
        Properties = 0x1093
    }

    [Flags]
    public enum ComputeMemoryFlags : long
    {
        None = 0,
        ReadWrite = 1 << 0,
        WriteOnly = 1 << 1,
        ReadOnly = 1 << 2,
        UseHostPointer = 1 << 3,
        AllocateHostPointer = 1 << 4,
        CopyHostPointer = 1 << 5
    }

    public enum ComputeImageChannelOrder : int
    {
        R = 0x10B0,
        A = 0x10B1,
        RG = 0x10B2,
        RA = 0x10B3,
        Rgb = 0x10B4,
        Rgba = 0x10B5,
        Bgra = 0x10B6,
        Argb = 0x10B7,
        Intensity = 0x10B8,
        Luminance = 0x10B9,
        RX = 0x10BA,
        Rgx = 0x10BB,
        Rgbx = 0x10BC
    }

    public enum ComputeImageChannelType : int
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
        UnsignedInt32 = 0x10DC,
        HalfFloat = 0x10DD,
        Float = 0x10DE,
    }

    public enum ComputeMemoryType : int
    {
        Buffer = 0x10F0,
        Image2D = 0x10F1,
        Image3D = 0x10F2
    }

    public enum ComputeMemoryInfo : int
    {
        Type = 0x1100,
        Flags = 0x1101,
        Size = 0x1102,
        HostPointer = 0x1103,
        MapppingCount = 0x1104,
        ReferenceCount = 0x1105,
        Context = 0x1106,
        AssociatedMemoryObject = 0x1107,
        Offset = 0x1108
    }

    public enum ComputeImageInfo : int
    {
        Format = 0x1110,
        ElementSize = 0x1111,
        RowPitch = 0x1112,
        SlicePitch = 0x1113,
        Width = 0x1114,
        Height = 0x1115,
        Depth = 0x1116
    }

    public enum ComputeImageAddressing : int
    {
        None = 0x1130,
        ClampToEdge = 0x1131,
        Clamp = 0x1132,
        Repeat = 0x1133,
        MirroredRepeat = 0x1134
    }

    public enum ComputeImageFiltering : int
    {
        Nearest = 0x1140,
        Linear = 0x1141
    }

    public enum ComputeSamplerInfo : int
    {
        ReferenceCount = 0x1150,
        Context = 0x1151,
        NormalizedCoords = 0x1152,
        Addressing = 0x1153,
        Filtering = 0x1154
    }

    [Flags]
    public enum ComputeMemoryMappingFlags : long
    {
        Read = 1 << 0,
        Write = 1 << 1
    }

    public enum ComputeProgramInfo : int
    {
        ReferenceCount = 0x1160,
        Context = 0x1161,
        DeviceCount = 0x1162,
        Devices = 0x1163,
        Source = 0x1164,
        BinarySizes = 0x1165,
        Binaries = 0x1166
    }

    public enum ComputeProgramBuildInfo : int
    {
        Status = 0x1181,
        Options = 0x1182,
        BuildLog = 0x1183
    }

    public enum ComputeProgramBuildStatus : int
    {
        Success = 0,
        None = -1,
        Error = -2,
        InProgress = -3
    }

    public enum ComputeKernelInfo : int
    {
        FunctionName = 0x1190,
        ArgumentCount = 0x1191,
        ReferenceCount = 0x1192,
        Context = 0x1193,
        Program = 0x1194
    }

    public enum ComputeKernelWorkGroupInfo : int
    {
        WorkGroupSize = 0x11B0,
        CompileWorkGroupSize = 0x11B1,
        LocalMemorySize = 0x11B2,
        PreferredWorkGroupSizeMultiple = 0x11B3,
        PrivateMemorySize = 0x11B4
    }

    public enum ComputeEventInfo : int
    {
        CommandQueue = 0x11D0,
        CommandType = 0x11D1,
        ReferenceCount = 0x11D2,
        ExecutionStatus = 0x11D3,
        Context = 0x11D4
    }

    public enum ComputeCommandType : int
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
        ReleaseGLObjects = 0x1200,
        ReadBufferRectangle = 0x1201,
        WriteBufferRectangle = 0x1202,
        CopyBufferRectangle = 0x1203,
        User = 0x1204
    }

    public enum ComputeCommandExecutionStatus : int
    {
        Complete = 0x0,
        Running = 0x1,
        Submitted = 0x2,
        Queued = 0x3
    }

    public enum ComputeBufferCreateType : int
    {
        Region = 0x1220
    }

    public enum ComputeCommandProfilingInfo : int
    {
        Queued = 0x1280,
        Submitted = 0x1281,
        Started = 0x1282,
        Ended = 0x1283
    }

    /**************************************************************************************/
    // CL/GL Sharing API

    public enum ComputeGLObjectType : int
    {
        Buffer = 0x2000,
        Texture2D = 0x2001,
        Texture3D = 0x2002,
        Renderbuffer = 0x2003
    }

    public enum ComputeGLTextureInfo : int
    {
        TextureTarget = 0x2004,
        MipMapLevel = 0x2005
    }

    public enum ComputeGLContextInfo : int
    {
        CL_CURRENT_DEVICE_FOR_GL_CONTEXT_KHR = 0x2006,
        CL_DEVICES_FOR_GL_CONTEXT_KHR = 0x2007
    }
}