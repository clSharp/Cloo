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
        ErrorCode code;

        public ComputeException( ErrorCode code )
        {
            this.code = code;
        }
    }
    
    /*
    public class DeviceNotFoundComputeException: ComputeException { }
    public class DeviceNotAvailableComputeException: ComputeException { }
    public class CompilerNotAvailableComputeException: ComputeException { }
    public class BufferAllocationComputeException: ComputeException { }
    public class OutOfResourcesComputeException: ComputeException { }
    public class OutOfHostMemoryComputeException: ComputeException { }
    public class ProfilingInfoNotAvailableComputeException: ComputeException { }
    public class MemoryCopyOverlapComputeException: ComputeException { }
    public class ImageFormatMismatchComputeException: ComputeException { }
    public class ImageFormatNotSupportedComputeException: ComputeException { }
    public class BuildProgramFailureComputeException: ComputeException { }
    public class MapFailureComputeException: ComputeException { }
    public class InvalidValueComputeException: ComputeException { }
    public class InvalidDeviceTypeComputeException: ComputeException { }
    public class InvalidPlatformComputeException: ComputeException { }
    public class InvalidDeviceComputeException: ComputeException { }
    public class InvalidContextComputeException: ComputeException { }
    public class InvalidQueuePropertiesComputeException: ComputeException { }
    public class InvalidCommandQueueComputeException: ComputeException { }
    public class InvalidHostPointerComputeException: ComputeException { }
    public class InvalidBufferComputeException: ComputeException { }
    public class InvalidImageFormatDescriptorComputeException: ComputeException { }
    public class InvalidImageSizeComputeException: ComputeException { }
    public class InvalidSamplerComputeException: ComputeException { }
    public class InvalidBinaryComputeException: ComputeException { }
    public class InvalidBuildOptionsComputeException: ComputeException { }
    public class InvalidProgramComputeException: ComputeException { }
    public class InvalidProgramExecutableComputeException: ComputeException { }
    public class InvalidKernelNameComputeException: ComputeException { }
    public class InvalidKernelDefinitionComputeException: ComputeException { }
    public class InvalidKernelComputeException: ComputeException { }
    public class InvalidArgumentIndexComputeException: ComputeException { }
    public class InvalidArgumentValueComputeException: ComputeException { }
    public class InvalidArgumentSizeComputeException: ComputeException { }
    public class InvalidKernelArgumentsComputeException: ComputeException { }
    public class InvalidWorkDimensionsComputeException: ComputeException { }
    public class InvalidWorkGroupSizeComputeException: ComputeException { }
    public class InvalidWorkItemSizeComputeException: ComputeException { }
    public class InvalidGlobalOffsetComputeException: ComputeException { }
    public class InvalidEventWaitListComputeException: ComputeException { }
    public class InvalidEventComputeException: ComputeException { }
    public class InvalidOperationComputeException: ComputeException { }
    public class InvalidGraphicsObjectComputeException: ComputeException { }
    public class InvalidBufferSizeComputeException: ComputeException { }
    public class InvalidMipLevelComputeException: ComputeException { }
    */
}