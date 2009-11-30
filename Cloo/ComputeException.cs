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
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeException: Exception
    {
        readonly ErrorCode code;

        public ErrorCode ErrorCode
        {
            get { return code; }
        }

        public ComputeException( ErrorCode code )
        {
            this.code = code;
        }
    }


    public class DeviceNotFoundComputeException: ComputeException
    { public DeviceNotFoundComputeException() : base( ErrorCode.DeviceNotFound ) { } }

    public class DeviceNotAvailableComputeException: ComputeException
    { public DeviceNotAvailableComputeException() : base( ErrorCode.DeviceNotAvailable ) { } }

    public class CompilerNotAvailableComputeException: ComputeException
    { public CompilerNotAvailableComputeException() : base( ErrorCode.CompilerNotAvailable ) { } }

    public class MemoryAllocationComputeException: ComputeException
    { public MemoryAllocationComputeException() : base( ErrorCode.MemObjectAllocationFailure ) { } }

    public class OutOfResourcesComputeException: ComputeException
    { public OutOfResourcesComputeException() : base( ErrorCode.OutOfResources ) { } }

    public class OutOfHostMemoryComputeException: ComputeException
    { public OutOfHostMemoryComputeException() : base( ErrorCode.OutOfHostMemory ) { } }

    public class ProfilingInfoNotAvailableComputeException: ComputeException
    { public ProfilingInfoNotAvailableComputeException() : base( ErrorCode.ProfilingInfoNotAvailable ) { } }

    public class MemoryCopyOverlapComputeException: ComputeException
    { public MemoryCopyOverlapComputeException() : base( ErrorCode.MemCopyOverlap ) { } }

    public class ImageFormatMismatchComputeException: ComputeException
    { public ImageFormatMismatchComputeException() : base( ErrorCode.ImageFormatMismatch ) { } }

    public class ImageFormatNotSupportedComputeException: ComputeException
    { public ImageFormatNotSupportedComputeException() : base( ErrorCode.ImageFormatNotSupported ) { } }

    public class BuildProgramFailureComputeException: ComputeException
    { public BuildProgramFailureComputeException() : base( ErrorCode.BuildProgramFailure ) { } }

    public class MapFailureComputeException: ComputeException
    { public MapFailureComputeException() : base( ErrorCode.MapFailure ) { } }

    public class InvalidValueComputeException: ComputeException
    { public InvalidValueComputeException() : base( ErrorCode.InvalidValue ) { } }

    public class InvalidDeviceTypeComputeException: ComputeException
    { public InvalidDeviceTypeComputeException() : base( ErrorCode.InvalidDevice ) { } }

    public class InvalidPlatformComputeException: ComputeException
    { public InvalidPlatformComputeException() : base( ErrorCode.InvalidPlatform ) { } }

    public class InvalidDeviceComputeException: ComputeException
    { public InvalidDeviceComputeException() : base( ErrorCode.InvalidDevice ) { } }

    public class InvalidContextComputeException: ComputeException
    { public InvalidContextComputeException() : base( ErrorCode.InvalidContext ) { } }

    public class InvalidQueuePropertiesComputeException: ComputeException
    { public InvalidQueuePropertiesComputeException() : base( ErrorCode.InvalidQueueProperties ) { } }

    public class InvalidJobQueueComputeException: ComputeException
    { public InvalidJobQueueComputeException() : base( ErrorCode.InvalidCommandQueue ) { } }

    public class InvalidHostPointerComputeException: ComputeException
    { public InvalidHostPointerComputeException() : base( ErrorCode.InvalidHostPtr ) { } }

    public class InvalidMemoryObjectComputeException: ComputeException
    { public InvalidMemoryObjectComputeException() : base( ErrorCode.InvalidMemObject ) { } }

    public class InvalidImageFormatDescriptorComputeException: ComputeException
    { public InvalidImageFormatDescriptorComputeException() : base( ErrorCode.InvalidImageFormatDescriptor ) { } }

    public class InvalidImageSizeComputeException: ComputeException
    { public InvalidImageSizeComputeException() : base( ErrorCode.InvalidImageSize ) { } }

    public class InvalidSamplerComputeException: ComputeException
    { public InvalidSamplerComputeException() : base( ErrorCode.InvalidSampler ) { } }

    public class InvalidBinaryComputeException: ComputeException
    { public InvalidBinaryComputeException() : base( ErrorCode.InvalidBinary ) { } }

    public class InvalidBuildOptionsComputeException: ComputeException
    { public InvalidBuildOptionsComputeException() : base( ErrorCode.InvalidBuildOptions ) { } }

    public class InvalidProgramComputeException: ComputeException
    { public InvalidProgramComputeException() : base( ErrorCode.InvalidProgram ) { } }

    public class InvalidProgramExecutableComputeException: ComputeException
    { public InvalidProgramExecutableComputeException() : base( ErrorCode.InvalidProgramExecutable ) { } }

    public class InvalidKernelNameComputeException: ComputeException
    { public InvalidKernelNameComputeException() : base( ErrorCode.InvalidKernelName ) { } }

    public class InvalidKernelDefinitionComputeException: ComputeException
    { public InvalidKernelDefinitionComputeException() : base( ErrorCode.InvalidKernelDefinition ) { } }

    public class InvalidKernelComputeException: ComputeException
    { public InvalidKernelComputeException() : base( ErrorCode.InvalidKernel ) { } }

    public class InvalidArgumentIndexComputeException: ComputeException
    { public InvalidArgumentIndexComputeException() : base( ErrorCode.InvalidArgIndex ) { } }

    public class InvalidArgumentValueComputeException: ComputeException
    { public InvalidArgumentValueComputeException() : base( ErrorCode.InvalidArgValue ) { } }

    public class InvalidArgumentSizeComputeException: ComputeException
    { public InvalidArgumentSizeComputeException() : base( ErrorCode.InvalidArgSize ) { } }

    public class InvalidKernelArgumentsComputeException: ComputeException
    { public InvalidKernelArgumentsComputeException() : base( ErrorCode.InvalidKernelArgs ) { } }

    public class InvalidWorkDimensionsComputeException: ComputeException
    { public InvalidWorkDimensionsComputeException() : base( ErrorCode.InvalidWorkDimension ) { } }

    public class InvalidWorkGroupSizeComputeException: ComputeException
    { public InvalidWorkGroupSizeComputeException() : base( ErrorCode.InvalidWorkGroupSize ) { } }

    public class InvalidWorkItemSizeComputeException: ComputeException
    { public InvalidWorkItemSizeComputeException() : base( ErrorCode.InvalidWorkItemSize ) { } }

    public class InvalidGlobalOffsetComputeException: ComputeException
    { public InvalidGlobalOffsetComputeException() : base( ErrorCode.InvalidGlobalOffset ) { } }

    public class InvalidEventWaitListComputeException: ComputeException
    { public InvalidEventWaitListComputeException() : base( ErrorCode.InvalidEventWaitList ) { } }

    public class InvalidEventComputeException: ComputeException
    { public InvalidEventComputeException() : base( ErrorCode.InvalidEvent ) { } }

    public class InvalidOperationComputeException: ComputeException
    { public InvalidOperationComputeException() : base( ErrorCode.InvalidOperation ) { } }

    public class InvalidGraphicsObjectComputeException: ComputeException
    { public InvalidGraphicsObjectComputeException() : base( ErrorCode.InvalidGlObject ) { } }

    public class InvalidBufferSizeComputeException: ComputeException
    { public InvalidBufferSizeComputeException() : base( ErrorCode.InvalidBufferSize ) { } }

    public class InvalidMipLevelComputeException: ComputeException
    { public InvalidMipLevelComputeException() : base( ErrorCode.InvalidMipLevel ) { } }
}