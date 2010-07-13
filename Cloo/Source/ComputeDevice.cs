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
    using System.Collections.ObjectModel;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL device.
    /// </summary>
    /// <remarks> A device is a collection of compute units. A command-queue is used to queue commands to a device. Examples of commands include executing kernels, or reading and writing memory objects. OpenCL devices typically correspond to a GPU, a multi-core CPU, and other processors such as DSPs and the Cell/B.E. processor. </remarks>
    /// <seealso cref="ComputeCommandQueue"/>
    /// <seealso cref="ComputeKernel"/>
    /// <seealso cref="ComputeMemory"/>
    /// <seealso cref="ComputePlatform"/>
    public class ComputeDevice : ComputeObject
    {
        #region Fields

        private readonly long addressBits;
        private readonly bool available;
        private readonly bool compilerAvailable;
        private readonly string driverVersion;
        private readonly bool endianLittle;
        private readonly bool errorCorrectionSupport;
        private readonly ComputeDeviceExecutionCapabilities executionCapabilities;
        private readonly ReadOnlyCollection<string> extensions;
        private readonly long globalMemoryCachelineSize;
        private readonly long globalMemoryCacheSize;
        private readonly ComputeDeviceMemoryCacheType globalMemoryCacheType;
        private readonly long globalMemorySize;
        private readonly bool imageSupport;
        private readonly long image2DMaxHeight;
        private readonly long image2DMaxWidth;
        private readonly long image3DMaxDepth;
        private readonly long image3DMaxHeight;
        private readonly long image3DMaxWidth;
        private readonly long localMemorySize;
        private readonly ComputeDeviceLocalMemoryType localMemoryType;
        private readonly long maxClockFrequency;
        private readonly long maxComputeUnits;
        private readonly long maxConstantArguments;
        private readonly long maxConstantBufferSize;
        private readonly long maxMemAllocSize;
        private readonly long maxParameterSize;
        private readonly long maxReadImageArgs;
        private readonly long maxSamplers;
        private readonly long maxWorkGroupSize;
        private readonly long maxWorkItemDimensions;
        private readonly ReadOnlyCollection<long> maxWorkItemSizes;
        private readonly long maxWriteImageArgs;
        private readonly long memBaseAddrAlign;
        private readonly long minDataTypeAlignSize;
        private readonly string name;
        private readonly ComputePlatform platform;
        private readonly long preferredVectorWidthChar;
        private readonly long preferredVectorWidthDouble;
        private readonly long preferredVectorWidthFloat;
        private readonly long preferredVectorWidthInt;
        private readonly long preferredVectorWidthLong;
        private readonly long preferredVectorWidthShort;
        private readonly string profile;
        private readonly long profilingTimerResolution;
        private readonly ComputeCommandQueueFlags queueProperties;
        private readonly ComputeDeviceSingleCapabilities singleCapabilities;
        private readonly ComputeDeviceTypes type;
        private readonly string vendor;
        private readonly long vendorId;
        private readonly string version;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default <c>ComputeDevice</c> address space size in bits.
        /// </summary>
        /// <remarks> Currently supported values are 32 or 64 bits. </remarks>
        public long AddressBits
        {
            get
            {
                return addressBits;
            }
        }

        /// <summary>
        /// Gets the availability state of the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> Is true if the <c>ComputeDevice</c> is available and false if the <c>ComputeDevice</c> is not available. </remarks>
        public bool Available
        {
            get
            {
                return available;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeCommandQueueFlags</c> supported by the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeCommandQueueFlags CommandQueueFlags
        {
            get
            {
                return queueProperties;
            }
        }

        /// <summary>
        /// Gets the availability state of the OpenCL compiler of the <c>ComputeDevice.Platform</c>.
        /// </summary>
        /// <remarks> Is false if the implementation does not have a compiler available to compile the program source. Is true if the compiler is available. This can be false for the embededed platform profile only. </remarks>
        public bool CompilerAvailable
        {
            get
            {
                return compilerAvailable;
            }
        }

        /// <summary>
        /// Gets the OpenCL software driver version string of the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The version string is in the form <c>major_number.minor_number</c>. </remarks>
        public string DriverVersion
        {
            get
            {
                return driverVersion;
            }
        }

        /// <summary>
        /// Gets the endianness of the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> Is true if the <c>ComputeDevice</c> is a little endian device and false otherwise. </remarks>
        public bool EndianLittle
        {
            get
            {
                return endianLittle;
            }
        }

        /// <summary>
        /// Gets the error correction support state of the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> Is true if the <c>ComputeDevice</c> implements error correction for the memories, caches, registers etc. Is false if the <c>ComputeDevice</c> does not implement error correction. This can be a requirement for certain clients of OpenCL. </remarks>
        public bool ErrorCorrectionSupport
        {
            get
            {
                return errorCorrectionSupport;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDeviceExecutionCapabilities</c> of the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeDeviceExecutionCapabilities ExecutionCapabilities
        {
            get
            {
                return executionCapabilities;
            }
        }

        /// <summary>
        /// Gets a read-only collection of names of extensions that the <c>ComputeDevice</c> supports.
        /// </summary>
        public ReadOnlyCollection<string> Extensions
        {
            get
            {
                return extensions;
            }
        }

        /// <summary>
        /// Gets the size of the global <c>ComputeDevice</c> memory cache line in bytes.
        /// </summary>
        public long GlobalMemoryCacheLineSize
        {
            get
            {
                return globalMemoryCachelineSize;
            }
        }

        /// <summary>
        /// Gets the size of the global <c>ComputeDevice</c> memory cache in bytes.
        /// </summary>
        public long GlobalMemoryCacheSize
        {
            get
            {
                return globalMemoryCacheSize;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDeviceMemoryCacheType</c> of the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeDeviceMemoryCacheType GlobalMemoryCacheType
        {
            get
            {
                return globalMemoryCacheType;
            }
        }

        /// <summary>
        /// Gets the size of the global <c>ComputeDevice</c> memory in bytes.
        /// </summary>
        public long GlobalMemorySize
        {
            get
            {
                return globalMemorySize;
            }
        }

        /// <summary>
        /// Maximum height of 2D images that the <c>ComputeDevice</c> supports in pixels.
        /// </summary>
        /// <remarks> The minimum value is 8192 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long Image2DMaxHeight
        {
            get
            {
                return image2DMaxHeight;
            }
        }

        /// <summary>
        /// Maximum width of 2D image that the <c>ComputeDevice</c> supports in pixels.
        /// </summary>
        /// <remarks> The minimum value is 8192 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long Image2DMaxWidth
        {
            get
            {
                return image2DMaxWidth;
            }
        }

        /// <summary>
        /// Maximum depth of 3D image the <c>ComputeDevice</c> supports in pixels.
        /// </summary>
        /// <remarks> The minimum value is 2048 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long Image3DMaxDepth
        {
            get
            {
                return image3DMaxDepth;
            }
        }

        /// <summary>
        /// Max height of 3D image the <c>ComputeDevice</c> supports in pixels.
        /// </summary>
        /// <remarks> The minimum value is 2048 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long Image3DMaxHeight
        {
            get
            {
                return image3DMaxHeight;
            }
        }

        /// <summary>
        /// Max width of 3D image the <c>ComputeDevice</c> supports in pixels.
        /// </summary>
        /// <remarks> The minimum value is 2048 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long Image3DMaxWidth
        {
            get
            {
                return image3DMaxWidth;
            }
        }

        /// <summary>
        /// Gets the state of image support of the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> Is true if <c>ComputeImage</c>s are supported by the <c>ComputeDevice</c> and false otherwise. </remarks>
        public bool ImageSupport
        {
            get
            {
                return imageSupport;
            }
        }

        /// <summary>
        /// Gets the size of local memory are of the <c>ComputeDevice</c> in bytes.
        /// </summary>
        /// <remarks> The minimum value is 16 KB. </remarks>
        public long LocalMemorySize
        {
            get
            {
                return localMemorySize;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDeviceLocalMemoryType</c> that is supported on the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeDeviceLocalMemoryType LocalMemoryType
        {
            get
            {
                return localMemoryType;
            }
        }

        /// <summary>
        /// Gets the maximum configured clock frequency of the <c>ComputeDevice</c> in MHz.
        /// </summary>
        public long MaxClockFrequency
        {
            get
            {
                return maxClockFrequency;
            }
        }

        /// <summary>
        /// Gets the number of parallel compute cores on the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The minimum value is 1. </remarks>
        public long MaxComputeUnits
        {
            get
            {
                return maxComputeUnits;
            }
        }

        /// <summary>
        /// Gets the maximum number of arguments declared with the __constant qualifier in a <c>ComputeKernel</c> executing in the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The minimum value is 8. </remarks>
        public long MaxConstantArguments
        {
            get
            {
                return maxConstantArguments;
            }
        }

        /// <summary>
        /// Gets the maximum size in bytes of a constant buffer allocation in the <c>ComputeDevice</c> memory.
        /// </summary>
        /// <remarks>  The minimum value is 64 KB. </remarks>
        public long MaxConstantBufferSize
        {
            get
            {
                return maxConstantBufferSize;
            }
        }

        /// <summary>
        /// Gets the maximum size of memory object allocation in the <c>ComputeDevice</c> memory in bytes.
        /// </summary>
        /// <remarks> The minimum value is <c>max(ComputeDevice.GlobalMemorySize/4, 128*1024*1024)</c>. </remarks>
        public long MaxMemoryAllocationSize
        {
            get
            {
                return maxMemAllocSize;
            }
        }

        /// <summary>
        /// Gets the maximum size in bytes of the arguments that can be passed to a <c>ComputeKernel</c> executing in the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The minimum value is 256. </remarks>
        public long MaxParameterSize
        {
            get
            {
                return maxParameterSize;
            }
        }

        /// <summary>
        /// Gets the maximum number of simultaneous <c>ComputeImage</c>s that can be read by a <c>ComputeKernel</c> executing in the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The minimum value is 128 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long MaxReadImageArguments
        {
            get
            {
                return maxReadImageArgs;
            }
        }

        /// <summary>
        /// Gets the maximum number of <c>ComputeSampler</c>s that can be used in a <c>ComputeKernel</c>.
        /// </summary>
        /// <remarks> The minimum value is 16 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long MaxSamplers
        {
            get
            {
                return maxSamplers;
            }
        }

        /// <summary>
        /// Gets the maximum number of work-items in a work-group executing a <c>ComputeKernel</c> in a <c>ComputeDevice</c> using the data parallel execution model.
        /// </summary>
        /// <remarks> The minimum value is 1. </remarks>
        public long MaxWorkGroupSize
        {
            get
            {
                return maxWorkGroupSize;
            }
        }

        /// <summary>
        /// Gets the maximum number of dimensions that specify the global and local work-item IDs used by the data parallel execution model.
        /// </summary>
        /// <remarks> The minimum value is 3. </remarks>
        public long MaxWorkItemDimensions
        {
            get
            {
                return maxWorkItemDimensions;
            }
        }

        /// <summary>
        /// Gets the maximum number of work-items that can be specified in each dimension of the <paramref name="globalWorkSize"/> argument of <c>ComputeCommandQueue.Execute</c>.
        /// </summary>
        public ReadOnlyCollection<long> MaxWorkItemSizes
        {
            get
            {
                return maxWorkItemSizes;
            }
        }

        /// <summary>
        /// Gets the maximum number of simultaneous <c>ComputeImage</c>s that can be written to by a <c>ComputeKernel</c> executing in the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> The minimum value is 8 if <c>ComputeDevice.ImageSupport</c> is true. </remarks>
        public long MaxWriteImageArguments
        {
            get
            {
                return maxWriteImageArgs;
            }
        }

        /// <summary>
        /// Gets the alignment in bits of the base address of any <c>ComputeMemory</c> allocated in the <c>ComputeDevice</c> memory.
        /// </summary>
        public long MemoryBaseAddressAlignment
        {
            get
            {
                return memBaseAddrAlign;
            }
        }

        /// <summary>
        /// Gets the smallest alignment in bytes which can be used for any data type allocated in the <c>ComputeDevice</c> memory.
        /// </summary>
        public long MinDataTypeAlignmentSize
        {
            get
            {
                return minDataTypeAlignSize;
            }
        }

        /// <summary>
        /// Gets the name of the <c>ComputeDevice</c>.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the <c>ComputePlatform</c> associated with the <c>ComputeDevice</c>.
        /// </summary>
        public ComputePlatform Platform
        {
            get
            {
                return platform;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>double</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthDouble
        {
            get
            {
                return preferredVectorWidthDouble;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>float</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthFloat
        {
            get
            {
                return preferredVectorWidthFloat;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>char</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthChar
        {
            get
            {
                return preferredVectorWidthChar;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>int</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthInt
        {
            get
            {
                return preferredVectorWidthInt;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>long</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthLong
        {
            get
            {
                return preferredVectorWidthLong;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c>'s preferred native vector width size for vector of <c>short</c>s.
        /// </summary>
        /// <remarks> The vector width is defined as the number of scalar elements that can be stored in the vector. </remarks>
        public long PreferredVectorWidthShort
        {
            get
            {
                return preferredVectorWidthShort;
            }
        }

        /// <summary>
        /// Gets the OpenCL profile name supported by the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> 
        /// The profile name returned can be one of the following strings:
        /// <list type="bullets">
        /// <item>
        ///     <term> FULL_PROFILE </term>
        ///     <description> The <c>ComputeDevice</c> supports the OpenCL specification (functionality defined as part of the core specification and does not require any extensions to be supported). </description>
        /// </item>
        /// <item>
        ///     <term> EMBEDDED_PROFILE </term>
        ///     <description> The <c>ComputeDevice</c> supports the OpenCL embedded profile. </description>
        /// </item>
        /// </list>
        /// </remarks>
        public string Profile
        {
            get
            {
                return profile;
            }
        }

        /// <summary>
        /// Gets the resolution of the <c>ComputeDevice</c> timer in nanoseconds.
        /// </summary>
        public long ProfilingTimerResolution
        {
            get
            {
                return profilingTimerResolution;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDeviceSingleCapabilities</c> of the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeDeviceSingleCapabilities SingleCapabilites
        {
            get
            {
                return singleCapabilities;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDeviceTypes</c> of the <c>ComputeDevice</c>.
        /// </summary>
        public ComputeDeviceTypes Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the <c>ComputeDevice</c> vendor name string.
        /// </summary>
        public string Vendor
        {
            get
            {
                return vendor;
            }
        }

        /// <summary>
        /// Gets a unique <c>ComputeDevice</c> vendor identifier.
        /// </summary>
        /// <remarks> An example of a unique device identifier could be the PCIe ID. </remarks>
        public long VendorId
        {
            get
            {
                return vendorId;
            }
        }

        /// <summary>
        /// Gets the OpenCL version supported by the <c>ComputeDevice</c>.
        /// </summary>
        /// <remarks> This version string has the following format: <c>OpenCL[space][major_version].[minor_version][space][vendor-specific information]</c> </remarks>
        public string Version
        {
            get
            {
                return version;
            }
        }

        #endregion

        #region Constructors

        internal ComputeDevice(ComputePlatform platform, IntPtr handle)
        {
            unsafe
            {
                Handle = handle;

                addressBits = GetInfo<uint>(ComputeDeviceInfo.AddressBits);
                available = GetBoolInfo(ComputeDeviceInfo.Available);
                compilerAvailable = GetBoolInfo(ComputeDeviceInfo.CompilerAvailable);
                driverVersion = GetStringInfo(ComputeDeviceInfo.DriverVersion);
                endianLittle = GetBoolInfo(ComputeDeviceInfo.EndianLittle);
                errorCorrectionSupport = GetBoolInfo(ComputeDeviceInfo.ErrorCorrectionSupport);
                executionCapabilities = (ComputeDeviceExecutionCapabilities)GetInfo<long>(ComputeDeviceInfo.ExecutionCapabilities);

                string extensionString = GetStringInfo<ComputeDeviceInfo>(ComputeDeviceInfo.Extensions, CL10.GetDeviceInfo);
                extensions = new ReadOnlyCollection<string>(extensionString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                globalMemoryCachelineSize = GetInfo<uint>(ComputeDeviceInfo.GlobalMemoryCachelineSize);
                globalMemoryCacheSize = (long)GetInfo<ulong>(ComputeDeviceInfo.GlobalMemoryCacheSize);
                globalMemoryCacheType = (ComputeDeviceMemoryCacheType)GetInfo<long>(ComputeDeviceInfo.GlobalMemoryCacheType);
                globalMemorySize = (long)GetInfo<ulong>(ComputeDeviceInfo.GlobalMemorySize);
                image2DMaxHeight = (long)GetInfo<IntPtr>(ComputeDeviceInfo.Image2DMaxHeight);
                image2DMaxWidth = (long)GetInfo<IntPtr>(ComputeDeviceInfo.Image2DMaxWidth);
                image3DMaxDepth = (long)GetInfo<IntPtr>(ComputeDeviceInfo.Image3DMaxDepth);
                image3DMaxHeight = (long)GetInfo<IntPtr>(ComputeDeviceInfo.Image3DMaxHeight);
                image3DMaxWidth = (long)GetInfo<IntPtr>(ComputeDeviceInfo.Image3DMaxWidth);
                imageSupport = GetBoolInfo(ComputeDeviceInfo.ImageSupport);
                localMemorySize = (long)GetInfo<ulong>(ComputeDeviceInfo.LocalMemorySize);
                localMemoryType = (ComputeDeviceLocalMemoryType)GetInfo<long>(ComputeDeviceInfo.LocalMemoryType);
                maxClockFrequency = GetInfo<uint>(ComputeDeviceInfo.MaxClockFrequency);
                maxComputeUnits = GetInfo<uint>(ComputeDeviceInfo.MaxComputeUnits);
                maxConstantArguments = GetInfo<uint>(ComputeDeviceInfo.MaxConstantArguments);
                maxConstantBufferSize = (long)GetInfo<ulong>(ComputeDeviceInfo.MaxConstantBufferSize);
                maxMemAllocSize = (long)GetInfo<ulong>(ComputeDeviceInfo.MaxMemoryAllocationSize);
                maxParameterSize = (long)GetInfo<IntPtr>(ComputeDeviceInfo.MaxParameterSize);
                maxReadImageArgs = GetInfo<uint>(ComputeDeviceInfo.MaxReadImageArguments);
                maxSamplers = GetInfo<uint>(ComputeDeviceInfo.MaxSamplers);
                maxWorkGroupSize = (long)GetInfo<IntPtr>(ComputeDeviceInfo.MaxWorkGroupSize);
                maxWorkItemDimensions = GetInfo<uint>(ComputeDeviceInfo.MaxWorkItemDimensions);
                maxWorkItemSizes = new ReadOnlyCollection<long>(Tools.ConvertArray(GetArrayInfo<ComputeDeviceInfo, IntPtr>(ComputeDeviceInfo.MaxWorkItemSizes, CL10.GetDeviceInfo)));
                maxWriteImageArgs = GetInfo<uint>(ComputeDeviceInfo.MaxWriteImageArguments);
                memBaseAddrAlign = GetInfo<uint>(ComputeDeviceInfo.MemoryBaseAddressAlignment);
                minDataTypeAlignSize = GetInfo<uint>(ComputeDeviceInfo.MinDataTypeAlignmentSize);
                name = GetStringInfo(ComputeDeviceInfo.Name);
                this.platform = platform;
                preferredVectorWidthChar = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthChar);
                preferredVectorWidthDouble = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthDouble);
                preferredVectorWidthFloat = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthFloat);
                preferredVectorWidthInt = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthInt);
                preferredVectorWidthLong = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthLong);
                preferredVectorWidthShort = GetInfo<uint>(ComputeDeviceInfo.PreferredVectorWidthShort);
                profile = GetStringInfo(ComputeDeviceInfo.Profile);
                profilingTimerResolution = (long)GetInfo<IntPtr>(ComputeDeviceInfo.ProfilingTimerResolution);
                queueProperties = (ComputeCommandQueueFlags)GetInfo<long>(ComputeDeviceInfo.CommandQueueProperties);
                singleCapabilities = (ComputeDeviceSingleCapabilities)GetInfo<long>(ComputeDeviceInfo.SingleFPConfig);
                type = (ComputeDeviceTypes)GetInfo<long>(ComputeDeviceInfo.Type);
                vendor = GetStringInfo(ComputeDeviceInfo.Vendor);
                vendorId = GetInfo<uint>(ComputeDeviceInfo.VendorId);
                version = GetStringInfo(ComputeDeviceInfo.Version);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the string representation of the <c>ComputeDevice</c>.
        /// </summary>
        /// <returns>The string representation of the <c>ComputeDevice</c>.</returns>
        public override string ToString()
        {
            return "ComputeDevice(" + Name + ")";
        }

        #endregion

        #region Private methods

        private bool GetBoolInfo(ComputeDeviceInfo paramName)
        {
            unsafe
            {
                return GetBoolInfo<ComputeDeviceInfo>(paramName, CL10.GetDeviceInfo);
            }
        }

        private NativeType GetInfo<NativeType>(ComputeDeviceInfo paramName)
            where NativeType : struct
        {
            unsafe
            {
                return GetInfo<ComputeDeviceInfo, NativeType>(paramName, CL10.GetDeviceInfo);
            }
        }

        private string GetStringInfo(ComputeDeviceInfo paramName)
        {
            unsafe
            {
                return GetStringInfo<ComputeDeviceInfo>(paramName, CL10.GetDeviceInfo);
            }
        }

        #endregion
    }
}