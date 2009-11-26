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
    public class ComputeException: Exception { }
    public class DeviceNotFoundException: ComputeException { }
    public class DeviceNotAvailableException: ComputeException { }
    public class CompilerNotAvailableException: ComputeException { }
    public class BufferAllocationException: ComputeException { }
    public class OutOfResourcesException: ComputeException { }
    public class OutOfHostMemoryException: ComputeException { }
    public class ProfilingInfoNotAvailableException: ComputeException { }
    public class MemoryCopyOverlapException: ComputeException { }
    public class ImageFormatMismatchException: ComputeException { }
    public class ImageFormatNotSupportedException: ComputeException { }
    public class BuildProgramFailureException: ComputeException { }
    public class MapFailureException: ComputeException { }
    public class InvalidValueException: ComputeException { }
    public class InvalidDeviceTypeException: ComputeException { }
    public class InvalidPlatformException: ComputeException { }
    public class InvalidDeviceException: ComputeException { }
    public class InvalidContextException: ComputeException { }
    public class InvalidQueuePropertiesException: ComputeException { }
    public class InvalidCommandQueueException: ComputeException { }
    public class InvalidHostPointerException: ComputeException { }
    public class InvalidBufferException: ComputeException { }
    public class InvalidImageFormatDescriptorException: ComputeException { }
    public class InvalidImageSizeException: ComputeException { }
    public class InvalidSamplerException: ComputeException { }
    public class InvalidBinaryException: ComputeException { }
    public class InvalidBuildOptionsException: ComputeException { }
    public class InvalidProgramException: ComputeException { }
    public class InvalidProgramExecutableException: ComputeException { }
    public class InvalidKernelNameException: ComputeException { }
    public class InvalidKernelDefinitionException: ComputeException { }
    public class InvalidKernelException: ComputeException { }
    public class InvalidArgumentIndexException: ComputeException { }
    public class InvalidArgumentValueException: ComputeException { }
    public class InvalidArgumentSizeException: ComputeException { }
    public class InvalidKernelArgumentsException: ComputeException { }
    public class InvalidWorkDimensionsException: ComputeException { }
    public class InvalidWorkGroupSizeException: ComputeException { }
    public class InvalidWorkItemSizeException: ComputeException { }
    public class InvalidGlobalOffsetException: ComputeException { }
    public class InvalidEventWaitListException: ComputeException { }
    public class InvalidEventException: ComputeException { }
    public class InvalidOperationException: ComputeException { }
    public class InvalidGraphicsObjectException: ComputeException { }
    public class InvalidBufferSizeException: ComputeException { }
    public class InvalidMipLevelException: ComputeException { }
    */
}