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
        #region Fields

        private readonly ErrorCode code;

        #endregion

        #region Properties

        public ErrorCode ErrorCode
        {
            get { return code; }
        }
        
        #endregion

        #region Constructors

        public ComputeException( ErrorCode code )
        {
            this.code = code;
        }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void ThrowIfError( int errorCode )
        {
            ThrowIfError( ( ErrorCode )errorCode );
        }

        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void ThrowIfError( ErrorCode errorCode )
        {
            switch( errorCode )
            {
                case ErrorCode.Success:
                    return;
                
                case ErrorCode.DeviceNotFound:
                    throw new DeviceNotFoundComputeException();
                
                case ErrorCode.DeviceNotAvailable: 
                    throw new DeviceNotAvailableComputeException();
                
                case ErrorCode.CompilerNotAvailable: 
                    throw new CompilerNotAvailableComputeException();
                
                case ErrorCode.MemObjectAllocationFailure: 
                    throw new MemoryAllocationComputeException();
                
                case ErrorCode.OutOfResources: 
                    throw new OutOfResourcesComputeException();

                case ErrorCode.OutOfHostMemory: 
                    throw new OutOfHostMemoryComputeException();

                case ErrorCode.ProfilingInfoNotAvailable: 
                    throw new ProfilingInfoNotAvailableComputeException();

                case ErrorCode.MemCopyOverlap: 
                    throw new MemoryCopyOverlapComputeException();

                case ErrorCode.ImageFormatMismatch: 
                    throw new ImageFormatMismatchComputeException();

                case ErrorCode.ImageFormatNotSupported: 
                    throw new ImageFormatNotSupportedComputeException();

                case ErrorCode.BuildProgramFailure: 
                    throw new BuildProgramFailureComputeException();

                case ErrorCode.MapFailure: 
                    throw new MapFailureComputeException();

                case ErrorCode.InvalidValue: 
                    throw new InvalidValueComputeException();

                case ErrorCode.InvalidDeviceType: 
                    throw new InvalidDeviceTypeComputeException();

                case ErrorCode.InvalidPlatform: 
                    throw new InvalidPlatformComputeException();

                case ErrorCode.InvalidDevice: 
                    throw new InvalidDeviceComputeException();

                case ErrorCode.InvalidContext: 
                    throw new InvalidContextComputeException();

                case ErrorCode.InvalidQueueProperties: 
                    throw new InvalidQueuePropertiesComputeException();

                case ErrorCode.InvalidCommandQueue: 
                    throw new InvalidJobQueueComputeException();

                case ErrorCode.InvalidHostPtr: 
                    throw new InvalidHostPointerComputeException();

                case ErrorCode.InvalidMemObject: 
                    throw new InvalidMemoryObjectComputeException();

                case ErrorCode.InvalidImageFormatDescriptor: 
                    throw new InvalidImageFormatDescriptorComputeException();

                case ErrorCode.InvalidImageSize: 
                    throw new InvalidImageSizeComputeException();

                case ErrorCode.InvalidSampler: 
                    throw new InvalidSamplerComputeException();

                case ErrorCode.InvalidBinary: 
                    throw new InvalidBinaryComputeException();

                case ErrorCode.InvalidBuildOptions: 
                    throw new InvalidBuildOptionsComputeException();

                case ErrorCode.InvalidProgram: 
                    throw new InvalidProgramComputeException();

                case ErrorCode.InvalidProgramExecutable: 
                    throw new InvalidProgramExecutableComputeException();

                case ErrorCode.InvalidKernelName: 
                    throw new InvalidKernelNameComputeException();

                case ErrorCode.InvalidKernelDefinition: 
                    throw new InvalidKernelDefinitionComputeException();

                case ErrorCode.InvalidKernel: 
                    throw new InvalidKernelComputeException();

                case ErrorCode.InvalidArgIndex: 
                    throw new InvalidArgumentIndexComputeException();

                case ErrorCode.InvalidArgValue: 
                    throw new InvalidArgumentValueComputeException();

                case ErrorCode.InvalidArgSize: 
                    throw new InvalidArgumentSizeComputeException();

                case ErrorCode.InvalidKernelArgs: 
                    throw new InvalidKernelArgumentsComputeException();

                case ErrorCode.InvalidWorkDimension: 
                    throw new InvalidWorkDimensionsComputeException();

                case ErrorCode.InvalidWorkGroupSize: 
                    throw new InvalidWorkGroupSizeComputeException();

                case ErrorCode.InvalidWorkItemSize: 
                    throw new InvalidWorkItemSizeComputeException();

                case ErrorCode.InvalidGlobalOffset: 
                    throw new InvalidGlobalOffsetComputeException();

                case ErrorCode.InvalidEventWaitList: 
                    throw new InvalidEventWaitListComputeException();

                case ErrorCode.InvalidEvent: 
                    throw new InvalidEventComputeException();

                case ErrorCode.InvalidOperation: 
                    throw new InvalidOperationComputeException();

                case ErrorCode.InvalidGlObject: 
                    throw new InvalidGraphicsObjectComputeException();

                case ErrorCode.InvalidBufferSize: 
                    throw new InvalidBufferSizeComputeException();

                case ErrorCode.InvalidMipLevel: 
                    throw new InvalidMipLevelComputeException();
                
                default:
                    throw new ComputeException( errorCode );            
            }
        }

        #endregion
    }

    #region Exception classes

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
    { public InvalidDeviceTypeComputeException() : base( ErrorCode.InvalidDeviceType ) { } }

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

    #endregion
}