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
using System.Globalization;
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeTools
    {
        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void CheckError( int errorCode )
        {            
            CheckError( ( ErrorCode )errorCode );
        }

        /// <summary>
        /// Checks for an error and throws an exception if such is encountered.
        /// </summary>
        public static void CheckError( ErrorCode errorCode )
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

                case ErrorCode.InvalidDevice: 
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

        internal static IntPtr[] ConvertArray( int[] array )
        {
            if( array == null ) throw null;

            NumberFormatInfo nfi = new NumberFormatInfo();

            IntPtr[] result = new IntPtr[ array.Length ];
            for( int i = 0; i < array.Length; i++ )
                result[ i ] = new IntPtr( array[ i ] );
            return result;
        }
    }
}
