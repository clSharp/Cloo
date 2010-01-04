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

using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenTK.Cloo.CL10;

namespace Cloo
{
    public class ComputeDevice: ComputeObject
    {
        #region Fields

        private readonly long addressBits;
        private readonly bool available;
        private readonly bool compilerAvailable;
        private readonly string driverVersion;
        private readonly bool endianLittle;
        private readonly bool errorCorrectionSupport;
        private readonly DeviceExecCapabilitiesFlags executionCapabilities;
        private readonly ReadOnlyCollection<string> extensions;
        private readonly long globalMemCachelineSize;
        private readonly long globalMemCacheSize;
        private readonly DeviceMemCacheType globalMemCacheType;
        private readonly long globalMemSize;
        private readonly bool imageSupport;
        private readonly long image2DMaxHeight;
        private readonly long image2DMaxWidth;
        private readonly long image3DMaxDepth;
        private readonly long image3DMaxHeight;
        private readonly long image3DMaxWidth;
        private readonly long localMemSize;
        private readonly DeviceLocalMemType localMemType;
        private readonly long maxClockFrequency;
        private readonly long maxComputeUnits;
        private readonly long maxConstArgs;
        private readonly long maxConstBufferSize;
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
        private readonly CommandQueueFlags queueProperties;
        private readonly DeviceFpConfigFlags singleFPConfig;
        private readonly DeviceTypeFlags type;
        private readonly string vendor;
        private readonly long vendorId;
        private readonly string version;

        #endregion

        #region Properties

        /// <summary>
        /// The default compute device address space size specified as an unsigned integer value in bits. Currently supported values are 32 or 64 bits.
        /// </summary>
        public long AddressBits
        {
            get
            {
                return addressBits;
            }
        }

        /// <summary>
        /// Is true if the device is available and false if the device is not available.
        /// </summary>
        public bool Available
        {
            get
            {
                return available;
            }
        }

        /// <summary>
        /// Is false if the implementation does not have a compiler available to compile the program source. Is true if the compiler is available. This can be false for the embededed platform profile only.
        /// </summary>
        public bool CompilerAvailable
        {
            get
            {
                return compilerAvailable;
            }
        }
        
        /// <summary>
        /// OpenCL software driver version string in the form major_number.minor_number.
        /// </summary>
        public string DriverVersion
        {
            get
            {
                return driverVersion;
            }
        }

        /// <summary>
        /// Is true if the OpenCL device is a little endian device and false otherwise.
        /// </summary>
        public bool EndianLittle
        {
            get
            {
                return endianLittle;
            }
        }

        /// <summary>
        /// Is true if the device implements error correction for the memories, caches, registers etc. in the device. Is false if the device does not implement error correction. This can be a requirement for certain clients of OpenCL.
        /// </summary>
        public bool ErrorCorrectionSupport
        {
            get
            {
                return errorCorrectionSupport;
            }
        }

        /// <summary>
        /// Describes the execution capabilities of the device. This is a bit-field that describes one or more of the following values: ExecKernel - The OpenCL device can execute OpenCL kernels. ExecNativeKernel - The OpenCL device can execute native kernels. The mandated minimum capability is ExecKernel.
        /// </summary>
        public DeviceExecCapabilitiesFlags ExecutionCapabilities
        {
            get
            {
                return executionCapabilities;
            }
        }

        /// <summary>
        /// Returns a collection of extension names.
        /// </summary>
        public ReadOnlyCollection<string> Extensions
        {
            get
            {
                return extensions;
            }
        }

        /// <summary>
        /// Size of global memory cache line in bytes.
        /// </summary>
        public long GlobalMemoryCacheLineSize
        {
            get
            {
                return globalMemCachelineSize;
            }
        }

        /// <summary>
        /// Size of global memory cache in bytes.
        /// </summary>
        public long GlobalMemoryCacheSize
        {
            get
            {
                return globalMemCacheSize;
            }
        }

        /// <summary>
        /// Type of global memory cache supported. Valid values are: None, ReadOnlyCache and ReadWriteCache.
        /// </summary>
        public DeviceMemCacheType GlobalMemoryCacheType
        {
            get
            {
                return globalMemCacheType;
            }
        }

        /// <summary>
        /// Size of global device memory in bytes.
        /// </summary>
        public long GlobalMemorySize
        {
            get
            {
                return globalMemSize;
            }
        }

        /// <summary>
        /// Max height of 2D image in pixels. The minimum value is 8192 if ImageSupport is true.
        /// </summary>
        public long Image2DMaxHeight
        {
            get
            {
                return image2DMaxHeight;
            }
        }

        /// <summary>
        /// Max width of 2D image in pixels. The minimum value is 8192 if ImageSupport is true.
        /// </summary>
        public long Image2DMaxWidth
        {
            get
            {
                return image2DMaxWidth;
            }
        }

        /// <summary>
        /// Max depth of 3D image in pixels. The minimum value is 2048 if ImageSupport is true.
        /// </summary>
        public long Image3DMaxDepth
        {
            get
            {
                return image3DMaxDepth;
            }
        }

        /// <summary>
        /// Max height of 3D image in pixels. The minimum value is 2048 if ImageSupport is true.
        /// </summary>
        public long Image3DMaxHeight
        {
            get
            {
                return image3DMaxHeight;
            }
        }

        /// <summary>
        /// Max width of 3D image in pixels. The minimum value is 2048 if ImageSupport is true.
        /// </summary>
        public long Image3DMaxWidth
        {
            get
            {
                return image3DMaxWidth;
            }
        }

        /// <summary>
        /// Is true if images are supported by the OpenCL device and false otherwise.
        /// </summary>
        public bool ImageSupport
        {
            get
            {
                return imageSupport;
            }
        }

        /// <summary>
        /// Size of local memory arena in bytes. The minimum value is 16 KB.
        /// </summary>
        public long LocalMemorySize
        {
            get
            {
                return localMemSize;
            }
        }

        /// <summary>
        /// Type of local memory supported. This can be set to Local implying dedicated local memory storage such as SRAM, or Global.
        /// </summary>
        public DeviceLocalMemType LocalMemoryType
        {
            get
            {
                return localMemType;
            }
        }

        /// <summary>
        /// Maximum configured clock frequency of the device in MHz.
        /// </summary>
        public long MaxClockFrequency
        {
            get
            {
                return maxClockFrequency;
            }
        }

        /// <summary>
        /// The number of parallel compute cores on the OpenCL device. The minimum value is 1.
        /// </summary>
        public long MaxComputeUnits
        {
            get
            {
                return maxComputeUnits;
            }
        }

        /// <summary>
        /// Max number of arguments declared with the __constant qualifier in a kernel. The minimum value is 8.
        /// </summary>
        public long MaxConstantArgs
        {
            get
            {
                return maxConstArgs;
            }
        }

        /// <summary>
        /// Max size in bytes of a constant buffer allocation. The minimum value is 64 KB.
        /// </summary>
        public long MaxConstantBufferSize
        {
            get
            {
                return maxConstBufferSize;
            }
        }

        /// <summary>
        /// Max size of memory object allocation in bytes. The minimum value is max(GlobalMemorySize/4, 128*1024*1024).
        /// </summary>
        public long MaxMemoryAllocSize
        {
            get
            {
                return maxMemAllocSize;
            }
        }

        /// <summary>
        /// Max size in bytes of the arguments that can be passed to a kernel. The minimum value is 256.
        /// </summary>
        public long MaxParameterSize
        {
            get
            {
                return maxParameterSize;
            }
        }

        /// <summary>
        /// Max number of simultaneous image objects that can be read by a kernel. The minimum value is 128 if ImageSupport is true.
        /// </summary>
        public long MaxReadImageArgs
        {
            get
            {
                return maxReadImageArgs;
            }
        }

        /// <summary>
        /// Maximum number of samplers that can be used in a kernel. The minimum value is 16 if ImageSupport is true.
        /// </summary>
        public long MaxSamplers
        {
            get
            {
                return maxSamplers;
            }
        }

        /// <summary>
        /// Maximum number of work-items in a work-group executing a kernel using the data parallel execution model. The minimum value is 1.
        /// </summary>
        public long MaxWorkGroupSize
        {
            get
            {
                return maxWorkGroupSize;
            }
        }

        /// <summary>
        /// Maximum dimensions that specify the global and local work-item IDs used by the data parallel execution model. The minimum value is 3.
        /// </summary>
        public long MaxWorkItemDimensions
        {
            get
            {
                return maxWorkItemDimensions;
            }
        }

        /// <summary>
        /// Maximum number of work-items that can be specified in each dimension of the globalWorkSize to ComputeCommandQueue.Execute(...).
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<long> MaxWorkItemSizes
        {
            get
            {
                return maxWorkItemSizes;
            }
        }

        /// <summary>
        /// Max number of simultaneous image objects that can be written to by a kernel. The minimum value is 8 if  ImageSupport is true.
        /// </summary>
        public long MaxWriteImageArgs
        {
            get
            {
                return maxWriteImageArgs;
            }
        }

        /// <summary>
        /// Describes the alignment in bits of the base address of any allocated memory object.
        /// </summary>
        public long MemoryBaseAddressAlign
        {
            get
            {
                return memBaseAddrAlign;
            }
        }

        /// <summary>
        /// The smallest alignment in bytes which can be used for any data type.
        /// </summary>
        public long MinDataTypeAlignSize
        {
            get
            {
                return minDataTypeAlignSize;
            }
        }

        /// <summary>
        /// Device name string.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// The platform associated with this device.
        /// </summary>
        public ComputePlatform Platform
        {
            get
            {
                return platform;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthDouble
        {
            get
            {
                return preferredVectorWidthDouble;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthFloat
        {
            get
            {
                return preferredVectorWidthFloat;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthChar
        {
            get
            {
                return preferredVectorWidthChar;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthInt
        {
            get
            {
                return preferredVectorWidthInt;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthLong
        {
            get
            {
                return preferredVectorWidthLong;
            }
        }

        /// <summary>
        /// Preferred native vector width size for built-in scalar types that can be put into vectors. The vector width is defined as the number of scalar elements that can be stored in the vector.
        /// </summary>
        public long PreferredVectorWidthShort
        {
            get
            {
                return preferredVectorWidthShort;
            }
        }

        /// <summary>
        /// OpenCL profile string. Returns the profile name supported by the device (see note). The profile name returned can be one of the following strings: FULL_PROFILE - if the device supports the OpenCL specification (functionality defined as part of the core specification and does not require any extensions to be supported). EMBEDDED_PROFILE - if the device supports the OpenCL embedded profile.
        /// </summary>
        public string Profile
        {
            get
            {
                return profile;
            }
        }

        /// <summary>
        /// Describes the resolution of device timer. This is measured in nanoseconds.
        /// </summary>
        public long ProfilingTimerResolution
        {
            get
            {
                return profilingTimerResolution;
            }
        }

        /// <summary>
        /// Describes the command-queue properties supported by the device.
        /// </summary>
        public CommandQueueFlags QueueProperties
        {
            get
            {
                return queueProperties;
            }
        }

        /// <summary>
        /// Describes single precision floating-point capability of the device.
        /// </summary>
        public DeviceFpConfigFlags SingleFPConfig
        {
            get
            {
                return singleFPConfig;
            }
        }

        /// <summary>
        /// The OpenCL device type. Currently supported values are one of or a combination of: Cpu, Gpu, Accelerator, or Default.
        /// </summary>
        public DeviceTypeFlags Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Vendor name string.
        /// </summary>
        public string Vendor
        {
            get
            {
                return vendor;
            }
        }

        /// <summary>
        /// A unique device vendor identifier. An example of a unique device identifier could be the PCIe ID.
        /// </summary>
        public long VendorId
        {
            get
            {
                return vendorId;
            }
        }

        /// <summary>
        /// OpenCL version string. Returns the OpenCL version supported by the device. This version string has the following format: OpenCL[space][major_version.minor_version][space][vendor-specific information]
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }
        }

        #endregion

        #region Constructors

        internal ComputeDevice( ComputePlatform platform, IntPtr handle )
        {
            Handle = handle;

            addressBits                 = GetInfo<uint>( DeviceInfo.DeviceAddressBits );
            available                   = GetBoolInfo( DeviceInfo.DeviceAvailable );
            compilerAvailable           = GetBoolInfo( DeviceInfo.DeviceCompilerAvailable );
            driverVersion               = GetStringInfo( DeviceInfo.DriverVersion );
            endianLittle                = GetBoolInfo( DeviceInfo.DeviceEndianLittle );
            errorCorrectionSupport      = GetBoolInfo( DeviceInfo.DeviceErrorCorrectionSupport );
            executionCapabilities       = ( DeviceExecCapabilitiesFlags )GetInfo<long>( DeviceInfo.DeviceExecutionCapabilities );
            
            string extensionString = GetStringInfo<DeviceInfo>( DeviceInfo.DeviceExtensions, CL.GetDeviceInfo );
            if( extensionString.Equals( "" ) )
                extensions = new ReadOnlyCollection<string>( new string[ 0 ] );
            else
                extensions = new ReadOnlyCollection<string>( Regex.Split( extensionString, "[\\s]+" ) );

            globalMemCachelineSize      = GetInfo<uint>( DeviceInfo.DeviceGlobalMemCachelineSize );
            globalMemCacheSize          = ( long )GetInfo<ulong>( DeviceInfo.DeviceGlobalMemCacheSize );
            globalMemCacheType          = ( DeviceMemCacheType )GetInfo<long>( DeviceInfo.DeviceGlobalMemCacheType );
            globalMemSize               = ( long )GetInfo<ulong>( DeviceInfo.DeviceGlobalMemSize );
            image2DMaxHeight            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxHeight );
            image2DMaxWidth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxWidth );
            image3DMaxDepth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxDepth );
            image3DMaxHeight            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxHeight );
            image3DMaxWidth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxWidth );
            imageSupport                = GetBoolInfo( DeviceInfo.DeviceImageSupport );
            localMemSize                = ( long )GetInfo<ulong>( DeviceInfo.DeviceLocalMemSize );
            localMemType                = ( DeviceLocalMemType )GetInfo<long>( DeviceInfo.DeviceLocalMemType );
            maxClockFrequency           = GetInfo<uint>( DeviceInfo.DeviceMaxClockFrequency );
            maxComputeUnits             = GetInfo<uint>( DeviceInfo.DeviceMaxComputeUnits );
            maxConstArgs                = GetInfo<uint>( DeviceInfo.DeviceMaxConstantArgs );
            maxConstBufferSize          = ( long )GetInfo<ulong>( DeviceInfo.DeviceMaxConstantBufferSize );
            maxMemAllocSize             = ( long )GetInfo<ulong>( DeviceInfo.DeviceMaxMemAllocSize );
            maxParameterSize            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceMaxParameterSize );
            maxReadImageArgs            = GetInfo<uint>( DeviceInfo.DeviceMaxReadImageArgs );
            maxSamplers                 = GetInfo<uint>( DeviceInfo.DeviceMaxSamplers );
            maxWorkGroupSize            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceMaxWorkGroupSize );
            maxWorkItemDimensions       = GetInfo<uint>( DeviceInfo.DeviceMaxWorkItemDimensions );
            maxWorkItemSizes            = new ReadOnlyCollection<long>( ComputeTools.ConvertArray( GetArrayInfo<DeviceInfo, IntPtr>( DeviceInfo.DeviceMaxWorkItemSizes, CL.GetDeviceInfo ) ) );
            maxWriteImageArgs           = GetInfo<uint>( DeviceInfo.DeviceMaxWriteImageArgs );
            memBaseAddrAlign            = GetInfo<uint>( DeviceInfo.DeviceMemBaseAddrAlign );
            minDataTypeAlignSize        = GetInfo<uint>( DeviceInfo.DeviceMinDataTypeAlignSize );
            name                        = GetStringInfo( DeviceInfo.DeviceName );
            this.platform               = platform;
            preferredVectorWidthChar    = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthChar );
            preferredVectorWidthDouble  = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthDouble );
            preferredVectorWidthFloat   = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthFloat );
            preferredVectorWidthInt     = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthInt );
            preferredVectorWidthLong    = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthLong );
            preferredVectorWidthShort   = GetInfo<uint>( DeviceInfo.DevicePreferredVectorWidthShort );
            profile                     = GetStringInfo( DeviceInfo.DeviceProfile );
            profilingTimerResolution    = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceProfilingTimerResolution );
            queueProperties             = ( CommandQueueFlags )GetInfo<long>( DeviceInfo.DeviceQueueProperties );
            singleFPConfig              = ( DeviceFpConfigFlags )GetInfo<long>( DeviceInfo.DeviceSingleFpConfig );
            type                        = ( DeviceTypeFlags )GetInfo<long>( DeviceInfo.DeviceType );
            vendor                      = GetStringInfo( DeviceInfo.DeviceVendor );
            vendorId                    = GetInfo<uint>( DeviceInfo.DeviceVendorId );
            version                     = GetStringInfo( DeviceInfo.DeviceVersion );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a string representation of this device.
        /// </summary>
        public override string ToString()
        {
            return "ComputeDevice" + base.ToString();
        }

        #endregion

        #region Private methods

        private bool GetBoolInfo( DeviceInfo paramName )
        {
            return GetBoolInfo<DeviceInfo>( paramName, CL.GetDeviceInfo );
        }

        private NativeType GetInfo<NativeType>( DeviceInfo paramName )
            where NativeType: struct
        {
            return GetInfo<DeviceInfo, NativeType>( paramName, CL.GetDeviceInfo );
        }

        private string GetStringInfo( DeviceInfo paramName )
        {
            return GetStringInfo<DeviceInfo>( paramName, CL.GetDeviceInfo );
        }

        #endregion
    }
}
