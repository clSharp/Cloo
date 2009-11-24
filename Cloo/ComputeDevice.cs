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
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeDevice: ComputeObject
    {
        #region Fields

        public readonly long AddressBits;
        public readonly bool Available;
        public readonly bool CompilerAvailable;
        public readonly string DriverVersion;
        public readonly bool EndianLittle;
        public readonly bool ErrorCorrectionSupport;
        public readonly DeviceExecCapabilitiesFlags ExecutionCapabilities;
        public readonly ReadOnlyCollection<string> Extensions;
        public readonly long GlobalMemCachelineSize;
        public readonly ulong GlobalMemCacheSize;
        public readonly DeviceMemCacheType GlobalMemCacheType;
        public readonly ulong GlobalMemSize;
        public readonly bool ImageSupport;
        public readonly IntPtr Image2DMaxHeight;
        public readonly IntPtr Image2DMaxWidth;
        public readonly IntPtr Image3DMaxDepth;
        public readonly IntPtr Image3DMaxHeight;
        public readonly IntPtr Image3DMaxWidth;
        public readonly ulong LocalMemSize;
        public readonly DeviceLocalMemType LocalMemType;
        public readonly long MaxClockFrequency;
        public readonly long MaxComputeUnits;
        public readonly long MaxConstantArgs;
        public readonly ulong MaxConstantBufferSize;
        public readonly ulong MaxMemAllocSize;
        public readonly IntPtr MaxParameterSize;
        public readonly long MaxReadImageArgs;
        public readonly long MaxSamplers;
        public readonly IntPtr MaxWorkGroupSize;
        public readonly long MaxWorkItemDimensions;
        public readonly ReadOnlyCollection<IntPtr> MaxWorkItemSizes;
        public readonly long MaxWriteImageArgs;
        public readonly long MemBaseAddrAlign;
        public readonly long MinDataTypeAlignSize;
        public readonly string Name;
        public readonly ComputePlatform Platform;
        public readonly long PreferredVectorWidthChar;
        public readonly long PreferredVectorWidthDouble;
        public readonly long PreferredVectorWidthFloat;
        public readonly long PreferredVectorWidthInt;
        public readonly long PreferredVectorWidthLong;
        public readonly long PreferredVectorWidthShort;
        public readonly string Profile;
        public readonly IntPtr ProfilingTimerResolution;
        public readonly CommandQueueFlags QueueProperties;
        public readonly DeviceFpConfigFlags SingleFPConfig;
        public readonly DeviceTypeFlags Type;
        public readonly string Vendor;
        public readonly long VendorId;
        public readonly string Version;

        #endregion

        internal ComputeDevice( ComputePlatform platform, IntPtr handle )
        {
            Handle = handle;
            
            AddressBits                 = GetInfo<long, uint>( DeviceInfo.DeviceAddressBits );
            Available                   = GetBoolInfo( DeviceInfo.DeviceAvailable );
            CompilerAvailable           = GetBoolInfo( DeviceInfo.DeviceCompilerAvailable );
            DriverVersion               = GetStringInfo( DeviceInfo.DriverVersion );
            EndianLittle                = GetBoolInfo( DeviceInfo.DeviceEndianLittle );
            ErrorCorrectionSupport      = GetBoolInfo( DeviceInfo.DeviceErrorCorrectionSupport );
            ExecutionCapabilities       = ( DeviceExecCapabilitiesFlags )GetInfo<long>( DeviceInfo.DeviceExecutionCapabilities );
            Extensions                  = new ReadOnlyCollection<string>( GetStringInfo( DeviceInfo.DeviceExtensions ).Split( ' ' ) );
            GlobalMemCachelineSize      = GetInfo<long, uint>( DeviceInfo.DeviceGlobalMemCachelineSize );
            GlobalMemCacheSize          = GetInfo<ulong>( DeviceInfo.DeviceGlobalMemCacheSize );
            GlobalMemCacheType          = ( DeviceMemCacheType )GetInfo<long>( DeviceInfo.DeviceGlobalMemCacheType );
            GlobalMemSize               = GetInfo<ulong>( DeviceInfo.DeviceGlobalMemSize );
            Image2DMaxHeight            = GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxHeight );
            Image2DMaxWidth             = GetInfo<IntPtr>( DeviceInfo.DeviceImage2dMaxWidth );
            Image3DMaxDepth             = GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxDepth );
            Image3DMaxHeight            = GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxHeight );
            Image3DMaxWidth             = GetInfo<IntPtr>( DeviceInfo.DeviceImage3dMaxWidth );
            ImageSupport                = GetBoolInfo( DeviceInfo.DeviceImageSupport );
            LocalMemSize                = GetInfo<ulong>( DeviceInfo.DeviceLocalMemSize );
            LocalMemType                = ( DeviceLocalMemType )GetInfo<long>( DeviceInfo.DeviceLocalMemType );
            MaxClockFrequency           = GetInfo<long, uint>( DeviceInfo.DeviceMaxClockFrequency );
            MaxComputeUnits             = GetInfo<long, uint>( DeviceInfo.DeviceMaxComputeUnits );
            MaxConstantArgs             = GetInfo<long, uint>( DeviceInfo.DeviceMaxConstantArgs );
            MaxConstantBufferSize       = GetInfo<ulong>( DeviceInfo.DeviceMaxConstantBufferSize );
            MaxMemAllocSize             = GetInfo<ulong>( DeviceInfo.DeviceMaxMemAllocSize );
            MaxParameterSize            = GetInfo<IntPtr>( DeviceInfo.DeviceMaxParameterSize );
            MaxReadImageArgs            = GetInfo<long, uint>( DeviceInfo.DeviceMaxReadImageArgs );
            MaxSamplers                 = GetInfo<long, uint>( DeviceInfo.DeviceMaxSamplers );
            MaxWorkGroupSize            = GetInfo<IntPtr>( DeviceInfo.DeviceMaxWorkGroupSize );
            MaxWorkItemDimensions       = GetInfo<long, uint>( DeviceInfo.DeviceMaxWorkItemDimensions );
            MaxWorkItemSizes            = new ReadOnlyCollection<IntPtr>( GetArrayInfo<DeviceInfo, IntPtr>( DeviceInfo.DeviceMaxWorkItemSizes, CL.GetDeviceInfo ) );
            MaxWriteImageArgs           = GetInfo<long, uint>( DeviceInfo.DeviceMaxWriteImageArgs );
            MemBaseAddrAlign            = GetInfo<long, uint>( DeviceInfo.DeviceMemBaseAddrAlign );
            MinDataTypeAlignSize        = GetInfo<long, uint>( DeviceInfo.DeviceMinDataTypeAlignSize );
            Name                        = GetStringInfo( DeviceInfo.DeviceName );
            this.Platform               = platform;
            PreferredVectorWidthChar    = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthChar );
            PreferredVectorWidthDouble  = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthDouble );
            PreferredVectorWidthFloat   = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthFloat );
            PreferredVectorWidthInt     = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthInt );
            PreferredVectorWidthLong    = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthLong );
            PreferredVectorWidthShort   = GetInfo<long, uint>( DeviceInfo.DevicePreferredVectorWidthShort );
            Profile                     = GetStringInfo( DeviceInfo.DeviceProfile );
            ProfilingTimerResolution    = GetInfo<IntPtr>( DeviceInfo.DeviceProfilingTimerResolution );
            QueueProperties             = ( CommandQueueFlags )GetInfo<long>( DeviceInfo.DeviceQueueProperties );
            SingleFPConfig              = ( DeviceFpConfigFlags )GetInfo<long>( DeviceInfo.DeviceSingleFpConfig );
            Type                        = ( DeviceTypeFlags )GetInfo<long>( DeviceInfo.DeviceType );
            Vendor                      = GetStringInfo( DeviceInfo.DeviceVendor );
            VendorId                    = GetInfo<long, uint>( DeviceInfo.DeviceVendorId );
            Version                     = GetStringInfo( DeviceInfo.DeviceVersion );
        }

        public override string ToString()
        {
            return "ComputeDevice" + base.ToString();
        }

        private bool GetBoolInfo( DeviceInfo paramName )
        {
            return GetBoolInfo<DeviceInfo>( paramName, CL.GetDeviceInfo );
        }

        private ReturnType GetInfo<ReturnType>( DeviceInfo paramName ) where ReturnType: struct
        {
            return GetInfo<DeviceInfo, ReturnType, ReturnType>( paramName, CL.GetDeviceInfo );
        }

        private ReturnType GetInfo<ReturnType, NativeType>( DeviceInfo paramName )
            where ReturnType: struct
            where NativeType: struct
        {
            return GetInfo<DeviceInfo, ReturnType, NativeType>( paramName, CL.GetDeviceInfo );
        }

        private string GetStringInfo( DeviceInfo paramName )
        {
            return GetStringInfo<DeviceInfo>( paramName, CL.GetDeviceInfo );
        }
    }
}
