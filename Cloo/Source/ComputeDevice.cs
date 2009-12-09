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
using OpenTK.Compute.CL10;

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
        private readonly ulong globalMemCacheSize;
        private readonly DeviceMemCacheType globalMemCacheType;
        private readonly ulong globalMemSize;
        private readonly bool imageSupport;
        private readonly long image2DMaxHeight;
        private readonly long image2DMaxWidth;
        private readonly long image3DMaxDepth;
        private readonly long image3DMaxHeight;
        private readonly long image3DMaxWidth;
        private readonly ulong localMemSize;
        private readonly DeviceLocalMemType localMemType;
        private readonly long maxClockFrequency;
        private readonly long maxComputeUnits;
        private readonly long maxConstArgs;
        private readonly ulong maxConstBufferSize;
        private readonly ulong maxMemAllocSize;
        private readonly long maxParameterSize;
        private readonly long maxReadImageArgs;
        private readonly long maxSamplers;
        private readonly long maxWorkGroupSize;
        private readonly long maxWorkItemDimensions;
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<long> maxWorkItemSizes;
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

        public long AddressBits
        {
            get
            {
                return addressBits;
            }
        }

        public bool Available
        {
            get
            {
                return available;
            }
        }

        public bool CompilerAvailable
        {
            get
            {
                return compilerAvailable;
            }
        }

        public string DriverVersion
        {
            get
            {
                return driverVersion;
            }
        }

        public bool EndianLittle
        {
            get
            {
                return endianLittle;
            }
        }

        public bool ErrorCorrectionSupport
        {
            get
            {
                return errorCorrectionSupport;
            }
        }

        public DeviceExecCapabilitiesFlags ExecutionCapabilities
        {
            get
            {
                return executionCapabilities;
            }
        }

        public ReadOnlyCollection<string> Extensions
        {
            get
            {
                return extensions;
            }
        }

        public long GlobalMemoryCacheLineSize
        {
            get
            {
                return globalMemCachelineSize;
            }
        }

        public ulong GlobalMemoryCacheSize
        {
            get
            {
                return globalMemCacheSize;
            }
        }

        public DeviceMemCacheType GlobalMemoryCacheType
        {
            get
            {
                return globalMemCacheType;
            }
        }

        public ulong GlobalMemorySize
        {
            get
            {
                return globalMemSize;
            }
        }

        public long Image2DMaxHeight
        {
            get
            {
                return image2DMaxHeight;
            }
        }

        public long Image2DMaxWidth
        {
            get
            {
                return image2DMaxWidth;
            }
        }

        public long Image3DMaxDepth
        {
            get
            {
                return image3DMaxDepth;
            }
        }

        public long Image3DMaxHeight
        {
            get
            {
                return image3DMaxHeight;
            }
        }

        public long Image3DMaxWidth
        {
            get
            {
                return image3DMaxWidth;
            }
        }

        public bool ImageSupport
        {
            get
            {
                return imageSupport;
            }
        }

        public ulong LocalMemorySize
        {
            get
            {
                return localMemSize;
            }
        }

        public DeviceLocalMemType LocalMemoryType
        {
            get
            {
                return localMemType;
            }
        }

        public long MaxClockFrequency
        {
            get
            {
                return maxClockFrequency;
            }
        }

        public long MaxComputeUnits
        {
            get
            {
                return maxComputeUnits;
            }
        }

        public long MaxConstArgs
        {
            get
            {
                return maxConstArgs;
            }
        }

        public ulong MaxConstBufferSize
        {
            get
            {
                return maxConstBufferSize;
            }
        }

        public ulong MaxMemoryAllocSize
        {
            get
            {
                return maxMemAllocSize;
            }
        }

        public long MaxParameterSize
        {
            get
            {
                return maxParameterSize;
            }
        }

        public long MaxReadImageArgs
        {
            get
            {
                return maxReadImageArgs;
            }
        }

        public long MaxSamplers
        {
            get
            {
                return maxSamplers;
            }
        }

        public long MaxWorkGroupSize
        {
            get
            {
                return maxWorkGroupSize;
            }
        }

        public long MaxWorkItemDimensions
        {
            get
            {
                return maxWorkItemDimensions;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<long> MaxWorkItemSizes
        {
            get
            {
                return maxWorkItemSizes;
            }
        }

        public long MaxWriteImageArgs
        {
            get
            {
                return maxWriteImageArgs;
            }
        }

        public long MemoryBaseAddressAlignment
        {
            get
            {
                return memBaseAddrAlign;
            }
        }

        public long MinDataTypeAlignmentSize
        {
            get
            {
                return minDataTypeAlignSize;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public ComputePlatform Platform
        {
            get
            {
                return platform;
            }
        }

        public long PreferredVectorWidthDouble
        {
            get
            {
                return preferredVectorWidthDouble;
            }
        }

        public long PreferredVectorWidthFloat
        {
            get
            {
                return preferredVectorWidthFloat;
            }
        }

        public long PreferredVectorWidthChar
        {
            get
            {
                return preferredVectorWidthChar;
            }
        }

        public long PreferredVectorWidthInt
        {
            get
            {
                return preferredVectorWidthInt;
            }
        }

        public long PreferredVectorWidthLong
        {
            get
            {
                return preferredVectorWidthLong;
            }
        }

        public long PreferredVectorWidthShort
        {
            get
            {
                return preferredVectorWidthShort;
            }
        }

        public string Profile
        {
            get
            {
                return profile;
            }
        }

        public long ProfilingTimerResolution
        {
            get
            {
                return profilingTimerResolution;
            }
        }

        public CommandQueueFlags QueueProperties
        {
            get
            {
                return queueProperties;
            }
        }

        public DeviceFpConfigFlags SingleFPConfig
        {
            get
            {
                return singleFPConfig;
            }
        }

        public DeviceTypeFlags Type
        {
            get
            {
                return type;
            }
        }

        public string Vendor
        {
            get
            {
                return vendor;
            }
        }

        public long VendorId
        {
            get
            {
                return vendorId;
            }
        }

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
            extensions                  = new ReadOnlyCollection<string>( Regex.Split( GetStringInfo( DeviceInfo.DeviceExtensions ), "[\\s]+" ) );
            globalMemCachelineSize      = GetInfo<uint>( DeviceInfo.DeviceGlobalMemCachelineSize );
            globalMemCacheSize          = GetInfo<ulong>( DeviceInfo.DeviceGlobalMemCacheSize );
            globalMemCacheType          = ( DeviceMemCacheType )GetInfo<long>( DeviceInfo.DeviceGlobalMemCacheType );
            globalMemSize               = GetInfo<ulong>( DeviceInfo.DeviceGlobalMemSize );
            image2DMaxHeight            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxHeight );
            image2DMaxWidth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxWidth );
            image3DMaxDepth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxDepth );
            image3DMaxHeight            = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxHeight );
            image3DMaxWidth             = ( long )GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxWidth );
            imageSupport                = GetBoolInfo( DeviceInfo.DeviceImageSupport );
            localMemSize                = GetInfo<ulong>( DeviceInfo.DeviceLocalMemSize );
            localMemType                = ( DeviceLocalMemType )GetInfo<long>( DeviceInfo.DeviceLocalMemType );
            maxClockFrequency           = GetInfo<uint>( DeviceInfo.DeviceMaxClockFrequency );
            maxComputeUnits             = GetInfo<uint>( DeviceInfo.DeviceMaxComputeUnits );
            maxConstArgs                = GetInfo<uint>( DeviceInfo.DeviceMaxConstantArgs );
            maxConstBufferSize          = GetInfo<ulong>( DeviceInfo.DeviceMaxConstantBufferSize );
            maxMemAllocSize             = GetInfo<ulong>( DeviceInfo.DeviceMaxMemAllocSize );
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