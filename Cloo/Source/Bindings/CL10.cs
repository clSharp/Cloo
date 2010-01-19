#region License

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

#endregion

namespace Cloo.Bindings
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class CL10
    {
        const string dll = "opencl.dll";

        // $Revision: 9283 $ on $Date: 2009-10-14 10:18:57 -0700 (Wed, 14 Oct 2009) $ 

        // Platform API
        [DllImport( dll, EntryPoint = "clGetPlatformIDs" )]
        internal extern static unsafe ComputeErrorCode
        GetPlatformIDs(
            Int32 num_entries,
            IntPtr* platforms,
            Int32* num_platforms );

        [DllImport( dll, EntryPoint = "clGetPlatformInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetPlatformInfo(
            IntPtr platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Device APIs
        [DllImport( dll, EntryPoint = "clGetDeviceIDs" )]
        internal extern static unsafe ComputeErrorCode
        GetDeviceIDs(
            IntPtr platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            IntPtr* devices,
            Int32* num_devices );

        [DllImport( dll, EntryPoint = "clGetDeviceInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetDeviceInfo(
            IntPtr device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Context APIs
        [DllImport( dll, EntryPoint = "clCreateContext" )]
        internal extern static unsafe IntPtr
        CreateContext(
            /* const */ IntPtr* properties,
            Int32 num_devices,
            /* const */ IntPtr* devices,
            /* void (*pfn_notify)(const char *, const IntPtr, IntPtr, IntPtr) */ IntPtr pfn_notify,
            /* void* */ IntPtr user_data,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateContextFromType" )]
        internal extern static unsafe IntPtr
        CreateContextFromType(
            /* const */ IntPtr* properties,
            ComputeDeviceTypes device_type,
            /* void (*pfn_notify)(const char *, const IntPtr, IntPtr, IntPtr) */ IntPtr pfn_notify,
            /* void* */ IntPtr user_data,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clRetainContext" )]
        internal extern static unsafe ComputeErrorCode
        RetainContext( IntPtr context );

        [DllImport( dll, EntryPoint = "clReleaseContext" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseContext( IntPtr context );

        [DllImport( dll, EntryPoint = "clGetContextInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetContextInfo(
            IntPtr context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Command Queue APIs
        [DllImport( dll, EntryPoint = "clCreateCommandQueue" )]
        internal extern static unsafe IntPtr
        CreateCommandQueue(
            IntPtr context,
            IntPtr device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clRetainCommandQueue" )]
        internal extern static unsafe ComputeErrorCode
        RetainCommandQueue( IntPtr command_queue );

        [DllImport( dll, EntryPoint = "clReleaseCommandQueue" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseCommandQueue( IntPtr command_queue );

        [DllImport( dll, EntryPoint = "clGetCommandQueueInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetCommandQueueInfo(
            IntPtr command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clSetCommandQueueProperty" )]
        internal extern static unsafe ComputeErrorCode
        SetCommandQueueProperty(
            IntPtr command_queue,
            ComputeCommandQueueFlags properties,
            ComputeBoolean enable,
            out ComputeCommandQueueFlags old_properties );

        // Memory Object APIs
        [DllImport( dll, EntryPoint = "clCreateBuffer" )]
        internal extern static unsafe IntPtr
        CreateBuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            IntPtr size,
            /* void* */ IntPtr host_ptr,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateImage2D" )]
        internal extern static unsafe IntPtr
        CreateImage2D(
            IntPtr context,
            ComputeMemoryFlags flags,
            /* const */ ComputeImageFormat* image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            /* void* */ IntPtr host_ptr,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateImage2D" )]
        internal extern static unsafe IntPtr
        CreateImage3D(
            IntPtr context,
            ComputeMemoryFlags flags,
            /* const */ ComputeImageFormat* image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            /* void* */ IntPtr host_ptr,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clRetainMemObject" )]
        internal extern static unsafe ComputeErrorCode
        RetainMemObject( IntPtr memobj );

        [DllImport( dll, EntryPoint = "clReleaseMemObject" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseMemObject( IntPtr memobj );

        [DllImport( dll, EntryPoint = "clGetSupportedImageFormats" )]
        internal extern static unsafe ComputeErrorCode
        GetSupportedImageFormats(
            IntPtr context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            ComputeImageFormat* image_formats,
            Int32* num_image_formats );

        [DllImport( dll, EntryPoint = "clGetMemObjectInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetMemObjectInfo(
            IntPtr memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clGetImageInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetImageInfo(
            IntPtr image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Sampler APIs
        [DllImport( dll, EntryPoint = "clCreateSampler" )]
        internal extern static unsafe IntPtr
        CreateSampler(
            IntPtr context,
            ComputeBoolean normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clRetainSampler" )]
        internal extern static unsafe ComputeErrorCode
        RetainSampler( IntPtr sampler );

        [DllImport( dll, EntryPoint = "clReleaseSampler" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseSampler( IntPtr sampler );

        [DllImport( dll, EntryPoint = "clGetSamplerInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetSamplerInfo(
            IntPtr sampler,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Program Object APIs
        [DllImport( dll, EntryPoint = "clCreateProgramWithSource" )]
        internal extern static unsafe IntPtr
        CreateProgramWithSource(
            IntPtr context,
            Int32 count,
            /* const */ String[] strings,
            /* const */ IntPtr* lengths,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateProgramWithBinary" )]
        internal extern static unsafe IntPtr
        CreateProgramWithBinary(
            IntPtr context,
            Int32 num_devices,
            /* const */ IntPtr* device_list,
            /* const */ IntPtr* lengths,
            /* const */ Byte** binaries,
            Int32* binary_status,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clRetainProgram" )]
        internal extern static unsafe ComputeErrorCode
        RetainProgram( IntPtr program );

        [DllImport( dll, EntryPoint = "clReleaseProgram" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseProgram( IntPtr program );

        [DllImport( dll, EntryPoint = "clBuildProgram" )]
        internal extern static unsafe ComputeErrorCode
        BuildProgram(
            IntPtr program,
            Int32 num_devices,
            /* const */ IntPtr* device_list,
            /* const */ String options,
            /* void (*pfn_notify)(cl_program program , IntPtr user_data ) */ IntPtr pfn_notify,
            /* void* */ IntPtr user_data );

        [DllImport( dll, EntryPoint = "clUnloadCompiler" )]
        internal extern static unsafe ComputeErrorCode
        UnloadCompiler();

        [DllImport( dll, EntryPoint = "clGetProgramInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetProgramInfo(
            IntPtr program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clGetProgramBuildInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetProgramBuildInfo(
            IntPtr program,
            IntPtr device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Kernel Object APIs
        [DllImport( dll, EntryPoint = "clCreateKernel" )]
        internal extern static unsafe IntPtr
        CreateKernel(
            IntPtr program,
            /* const */ String kernel_name,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateKernelsInProgram" )]
        internal extern static unsafe ComputeErrorCode
        CreateKernelsInProgram(
            IntPtr program,
            Int32 num_kernels,
            IntPtr* kernels,
            Int32* num_kernels_ret );

        [DllImport( dll, EntryPoint = "clRetainKernel" )]
        internal extern static unsafe ComputeErrorCode
        RetainKernel( IntPtr kernel );

        [DllImport( dll, EntryPoint = "clReleaseKernel" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseKernel( IntPtr kernel );

        [DllImport( dll, EntryPoint = "clSetKernelArg" )]
        internal extern static unsafe ComputeErrorCode
        SetKernelArg(
            IntPtr kernel,
            Int32 arg_index,
            IntPtr arg_size,
            /* const void* */ IntPtr arg_value );

        [DllImport( dll, EntryPoint = "clGetKernelInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetKernelInfo(
            IntPtr kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clGetKernelWorkGroupInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetKernelWorkGroupInfo(
            IntPtr kernel,
            IntPtr device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Event Object APIs
        [DllImport( dll, EntryPoint = "clWaitForEvents" )]
        internal extern static unsafe ComputeErrorCode
        WaitForEvents(
            Int32 num_events,
            /* const */ IntPtr* event_list );

        [DllImport( dll, EntryPoint = "clGetEventInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetEventInfo(
            IntPtr @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clRetainEvent" )]
        internal extern static unsafe ComputeErrorCode
        RetainEvent( IntPtr @event );

        [DllImport( dll, EntryPoint = "clReleaseEvent" )]
        internal extern static unsafe ComputeErrorCode
        ReleaseEvent( IntPtr @event );

        // Profiling APIs
        [DllImport( dll, EntryPoint = "clGetEventProfilingInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetEventProfilingInfo(
            IntPtr @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            out IntPtr param_value_size_ret );

        // Flush and Finish APIs
        [DllImport( dll, EntryPoint = "clFlush" )]
        internal extern static unsafe ComputeErrorCode
        Flush( IntPtr command_queue );

        [DllImport( dll, EntryPoint = "clFinish" )]
        internal extern static unsafe ComputeErrorCode
        Finish( IntPtr command_queue );

        // Enqueued Commands APIs
        [DllImport( dll, EntryPoint = "clEnqueueReadBuffer" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueReadBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_read,
            IntPtr offset,
            IntPtr cb,
            /* void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueWriteBuffer" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueWriteBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_write,
            IntPtr offset,
            IntPtr cb,
            /* const void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueCopyBuffer" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueCopyBuffer(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueReadImage" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueReadImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_read,
            /* const */ IntPtr* origin,
            /* const */ IntPtr* region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            /* void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueWriteImage" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueWriteImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_write,
            /* const */ IntPtr* origin,
            /* const */ IntPtr* region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            /* const void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueCopyImage" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueCopyImage(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_image,
            /* const */ IntPtr* src_origin,
            /* const */ IntPtr* dst_origin,
            /* const */ IntPtr* region,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueCopyImageToBuffer" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueCopyImageToBuffer(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_buffer,
            /* const */ IntPtr* src_origin,
            /* const */ IntPtr* region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueCopyBufferToImage" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueCopyBufferToImage(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_image,
            IntPtr src_offset,
            /* const */ IntPtr* dst_origin,
            /* const */ IntPtr* region,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueMapBuffer" )]
        internal extern static unsafe /* void* */ IntPtr
        EnqueueMapBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clEnqueueMapImage" )]
        internal extern static unsafe /* void* */ IntPtr
        EnqueueMapImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            /* const */ IntPtr* origin,
            /* const */ IntPtr* region,
            IntPtr* image_row_pitch,
            IntPtr* image_slice_pitch,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clEnqueueUnmapMemObject" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueUnmapMemObject(
            IntPtr command_queue,
            IntPtr memobj,
            /* void* */ IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueNDRangeKernel" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueNDRangeKernel(
            IntPtr command_queue,
            IntPtr kernel,
            Int32 work_dim,
            /* const */ IntPtr* global_work_offset,
            /* const */ IntPtr* global_work_size,
            /* const */ IntPtr* local_work_size,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueTask" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueTask(
            IntPtr command_queue,
            IntPtr kernel,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueNativeKernel" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueNativeKernel(
            IntPtr command_queue,
            /* void (*user_func)(IntPtr) */ IntPtr user_func,
            /* void* */ IntPtr args,
            IntPtr cb_args,
            Int32 num_mem_objects,
            /* const */ IntPtr* mem_list,
            /* const void* */ IntPtr* args_mem_loc,
            Int32 num_events_in_wait_list,
            /* const */ IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueMarker" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueMarker(
            IntPtr command_queue,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueWaitForEvents" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueWaitForEvents(
            IntPtr command_queue,
            Int32 num_events,
            /* const */ IntPtr* event_list );

        [DllImport( dll, EntryPoint = "clEnqueueBarrier" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueBarrier( IntPtr command_queue );

        // Extension function access
        //
        // Returns the extension function address for the given function name,
        // or NULL if a valid function can not be found.  The ient must
        // check to make sure the address is not NULL, before using or 
        // calling the returned function address.
        //
        [DllImport( dll, EntryPoint = "clGetExtensionFunctionAddress" )]
        internal extern static unsafe /* void* */ IntPtr
        GetExtensionFunctionAddress(/* const */ String func_name );

        /**************************************************************************************/
        // CL/GL Sharing API

        [DllImport( dll, EntryPoint = "clCreateFromGLBuffer" )]
        internal extern static unsafe IntPtr
        CreateFromGLBuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateFromGLTexture2D" )]
        internal extern static unsafe IntPtr
        CreateFromGLTexture2D(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateFromGLTexture3D" )]
        internal extern static unsafe IntPtr
        CreateFromGLTexture3D(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clCreateFromGLRenderbuffer" )]
        internal extern static unsafe IntPtr
        CreateFromGLRenderbuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            out ComputeErrorCode errcode_ret );

        [DllImport( dll, EntryPoint = "clGetGLObjectInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetGLObjectInfo(
            IntPtr memobj,
            ComputeGLObjectType* gl_object_type,
            Int32* gl_object_name );

        [DllImport( dll, EntryPoint = "clGetGLTextureInfo" )]
        internal extern static unsafe ComputeErrorCode
        GetGLTextureInfo(
            IntPtr memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret );

        [DllImport( dll, EntryPoint = "clEnqueueAcquireGLObjects" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueAcquireGLObjects(
            IntPtr command_queue,
            Int32 num_objects,
            IntPtr* mem_objects,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            out IntPtr newEvent );

        [DllImport( dll, EntryPoint = "clEnqueueReleaseGLObjects" )]
        internal extern static unsafe ComputeErrorCode
        EnqueueReleaseGLObjects(
            IntPtr command_queue,
            Int32 num_objects,
            IntPtr* mem_objects,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            out IntPtr newEvent );
    }
}