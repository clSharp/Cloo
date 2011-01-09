#region License

/*

Copyright (c) 2009 - 2011 Fatjon Sakiqi

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

    /// <summary>
    /// Contains bindings to the OpenCL 1.0 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    public class CL10
    {
        /// <summary>
        /// The name of the library that contains the available OpenCL function points.
        /// </summary>
        protected const string libName = "opencl.dll";

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetPlatformIDs")]
        public extern static unsafe ComputeErrorCode
        GetPlatformIDs(
            Int32 num_entries,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            Int32* num_platforms);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetPlatformInfo")]
        public extern static unsafe ComputeErrorCode
        GetPlatformInfo(
            IntPtr platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetDeviceIDs")]
        public extern static unsafe ComputeErrorCode
        GetDeviceIDs(
            IntPtr platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            Int32* num_devices);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetDeviceInfo")]
        public extern static unsafe ComputeErrorCode
        GetDeviceInfo(
            IntPtr device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateContext")]
        public extern static unsafe IntPtr
        CreateContext(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            /* void (*pfn_notify)(const char *, const IntPtr, IntPtr, IntPtr) */ ComputeContextNotifier pfn_notify,
            /* void* */ IntPtr user_data,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateContextFromType")]
        public extern static unsafe IntPtr
        CreateContextFromType(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            /* void (*pfn_notify)(const char *, const IntPtr, IntPtr, IntPtr) */ ComputeContextNotifier pfn_notify,
            /* void* */ IntPtr user_data,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainContext")]
        public extern static unsafe ComputeErrorCode
        RetainContext(IntPtr context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseContext")]
        public extern static unsafe ComputeErrorCode
        ReleaseContext(IntPtr context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetContextInfo")]
        public extern static unsafe ComputeErrorCode
        GetContextInfo(
            IntPtr context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateCommandQueue")]
        public extern static unsafe IntPtr
        CreateCommandQueue(
            IntPtr context,
            IntPtr device,
            ComputeCommandQueueFlags properties,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainCommandQueue")]
        public extern static unsafe ComputeErrorCode
        RetainCommandQueue(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseCommandQueue")]
        public extern static unsafe ComputeErrorCode
        ReleaseCommandQueue(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetCommandQueueInfo")]
        public extern static unsafe ComputeErrorCode
        GetCommandQueueInfo(
            IntPtr command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetCommandQueueProperty")]
        public extern static unsafe ComputeErrorCode
        SetCommandQueueProperty(
            IntPtr command_queue,
            ComputeCommandQueueFlags properties,
            ComputeBoolean enable,
            ComputeCommandQueueFlags* old_properties);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateBuffer")]
        public extern static unsafe IntPtr
        CreateBuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            IntPtr size,
            /* void* */ IntPtr host_ptr,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateImage2D")]
        public extern static unsafe IntPtr
        CreateImage2D(
            IntPtr context,
            ComputeMemoryFlags flags,
            ComputeImageFormat* image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            /* void* */ IntPtr host_ptr,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateImage3D")]
        public extern static unsafe IntPtr
        CreateImage3D(
            IntPtr context,
            ComputeMemoryFlags flags,
            ComputeImageFormat* image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            /* void* */ IntPtr host_ptr,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainMemObject")]
        public extern static unsafe ComputeErrorCode
        RetainMemObject(IntPtr memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseMemObject")]
        public extern static unsafe ComputeErrorCode
        ReleaseMemObject(IntPtr memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetSupportedImageFormats")]
        public extern static unsafe ComputeErrorCode
        GetSupportedImageFormats(
            IntPtr context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            ComputeImageFormat* image_formats,
            Int32* num_image_formats);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetMemObjectInfo")]
        public extern static unsafe ComputeErrorCode
        GetMemObjectInfo(
            IntPtr memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetImageInfo")]
        public extern static unsafe ComputeErrorCode
        GetImageInfo(
            IntPtr image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateSampler")]
        public extern static unsafe IntPtr
        CreateSampler(
            IntPtr context,
            ComputeBoolean normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainSampler")]
        public extern static unsafe ComputeErrorCode
        RetainSampler(IntPtr sampler);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseSampler")]
        public extern static unsafe ComputeErrorCode
        ReleaseSampler(IntPtr sampler);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetSamplerInfo")]
        public extern static unsafe ComputeErrorCode
        GetSamplerInfo(
            IntPtr sampler,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateProgramWithSource")]
        public extern static unsafe IntPtr
        CreateProgramWithSource(
            IntPtr context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateProgramWithBinary")]
        public extern static unsafe IntPtr
        CreateProgramWithBinary(
            IntPtr context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            Byte** binaries,
            Int32* binary_status,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainProgram")]
        public extern static unsafe ComputeErrorCode
        RetainProgram(IntPtr program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseProgram")]
        public extern static unsafe ComputeErrorCode
        ReleaseProgram(IntPtr program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clBuildProgram")]
        public extern static unsafe ComputeErrorCode
        BuildProgram(
            IntPtr program,
            Int32 num_devices,
            IntPtr* device_list,
            String options,
            /* void (*pfn_notify)(cl_program program , IntPtr user_data ) */ ComputeProgramBuildNotifier pfn_notify,
            /* void* */ IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clUnloadCompiler")]
        public extern static unsafe ComputeErrorCode
        UnloadCompiler();

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetProgramInfo")]
        public extern static unsafe ComputeErrorCode
        GetProgramInfo(
            IntPtr program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetProgramBuildInfo")]
        public extern static unsafe ComputeErrorCode
        GetProgramBuildInfo(
            IntPtr program,
            IntPtr device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateKernel")]
        public extern static unsafe IntPtr
        CreateKernel(
            IntPtr program,
            String kernel_name,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateKernelsInProgram")]
        public extern static unsafe ComputeErrorCode
        CreateKernelsInProgram(
            IntPtr program,
            Int32 num_kernels,
            IntPtr* kernels,
            Int32* num_kernels_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainKernel")]
        public extern static unsafe ComputeErrorCode
        RetainKernel(IntPtr kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseKernel")]
        public extern static unsafe ComputeErrorCode
        ReleaseKernel(IntPtr kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetKernelArg")]
        public extern static unsafe ComputeErrorCode
        SetKernelArg(
            IntPtr kernel,
            Int32 arg_index,
            IntPtr arg_size,
            /* const void* */ IntPtr arg_value);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetKernelInfo")]
        public extern static unsafe ComputeErrorCode
        GetKernelInfo(
            IntPtr kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetKernelWorkGroupInfo")]
        public extern static unsafe ComputeErrorCode
        GetKernelWorkGroupInfo(
            IntPtr kernel,
            IntPtr device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clWaitForEvents")]
        public extern static unsafe ComputeErrorCode
        WaitForEvents(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetEventInfo")]
        public extern static unsafe ComputeErrorCode
        GetEventInfo(
            IntPtr @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainEvent")]
        public extern static unsafe ComputeErrorCode
        RetainEvent(IntPtr @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseEvent")]
        public extern static unsafe ComputeErrorCode
        ReleaseEvent(IntPtr @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetEventProfilingInfo")]
        public extern static unsafe ComputeErrorCode
        GetEventProfilingInfo(
            IntPtr @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            /* void* */ IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clFlush")]
        public extern static unsafe ComputeErrorCode
        Flush(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clFinish")]
        public extern static unsafe ComputeErrorCode
        Finish(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueReadBuffer")]
        public extern static unsafe ComputeErrorCode
        EnqueueReadBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_read,
            IntPtr offset,
            IntPtr cb,
            /* void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueWriteBuffer")]
        public extern static unsafe ComputeErrorCode
        EnqueueWriteBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_write,
            IntPtr offset,
            IntPtr cb,
            /* const void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueCopyBuffer")]
        public extern static unsafe ComputeErrorCode
        EnqueueCopyBuffer(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_buffer,
            IntPtr src_offset,
            IntPtr dst_offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueReadImage")]
        public extern static unsafe ComputeErrorCode
        EnqueueReadImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_read,
            IntPtr* origin,
            IntPtr* region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            /* void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueWriteImage")]
        public extern static unsafe ComputeErrorCode
        EnqueueWriteImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_write,
            IntPtr* origin,
            IntPtr* region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            /* const void* */ IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueCopyImage")]
        public extern static unsafe ComputeErrorCode
        EnqueueCopyImage(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_image,
            IntPtr* src_origin,
            IntPtr* dst_origin,
            IntPtr* region,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueCopyImageToBuffer")]
        public extern static unsafe ComputeErrorCode
        EnqueueCopyImageToBuffer(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_buffer,
            IntPtr* src_origin,
            IntPtr* region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueCopyBufferToImage")]
        public extern static unsafe ComputeErrorCode
        EnqueueCopyBufferToImage(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_image,
            IntPtr src_offset,
            IntPtr* dst_origin,
            IntPtr* region,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMapBuffer")]
        public extern static unsafe /* void* */ IntPtr
        EnqueueMapBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMapImage")]
        public extern static unsafe /* void* */ IntPtr
        EnqueueMapImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr* origin,
            IntPtr* region,
            IntPtr* image_row_pitch,
            IntPtr* image_slice_pitch,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueUnmapMemObject")]
        public extern static unsafe ComputeErrorCode
        EnqueueUnmapMemObject(
            IntPtr command_queue,
            IntPtr memobj,
            /* void* */ IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueNDRangeKernel")]
        public extern static unsafe ComputeErrorCode
        EnqueueNDRangeKernel(
            IntPtr command_queue,
            IntPtr kernel,
            Int32 work_dim,
            IntPtr* global_work_offset,
            IntPtr* global_work_size,
            IntPtr* local_work_size,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueTask")]
        public extern static unsafe ComputeErrorCode
        EnqueueTask(
            IntPtr command_queue,
            IntPtr kernel,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueNativeKernel")]
        public extern static unsafe ComputeErrorCode
        EnqueueNativeKernel(
            IntPtr command_queue,
            /* void (*user_func)(IntPtr) */ IntPtr user_func,
            /* void* */ IntPtr args,
            IntPtr cb_args,
            Int32 num_mem_objects,
            IntPtr* mem_list,
            /* const void* */ IntPtr* args_mem_loc,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMarker")]
        public extern static unsafe ComputeErrorCode
        EnqueueMarker(
            IntPtr command_queue,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueWaitForEvents")]
        public extern static unsafe ComputeErrorCode
        EnqueueWaitForEvents(
            IntPtr command_queue,
            Int32 num_events,
            IntPtr* event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueBarrier")]
        public extern static unsafe ComputeErrorCode
        EnqueueBarrier(IntPtr command_queue);

        
        /// <summary>
        /// Gets the extension function address for the given function name,
        /// or NULL if a valid function can not be found.  The client must
        /// check to make sure the address is not NULL, before using or 
        /// calling the returned function address.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetExtensionFunctionAddress")]
        public extern static unsafe /* void* */ IntPtr
        GetExtensionFunctionAddress(String func_name);

        /**************************************************************************************/
        // CL/GL Sharing API

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateFromGLBuffer")]
        public extern static unsafe IntPtr
        CreateFromGLBuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 bufobj,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateFromGLTexture2D")]
        public extern static unsafe IntPtr
        CreateFromGLTexture2D(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateFromGLTexture3D")]
        public extern static unsafe IntPtr
        CreateFromGLTexture3D(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 target,
            Int32 miplevel,
            Int32 texture,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateFromGLRenderbuffer")]
        public extern static unsafe IntPtr
        CreateFromGLRenderbuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            Int32 renderbuffer,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetGLObjectInfo")]
        public extern static unsafe ComputeErrorCode
        GetGLObjectInfo(
            IntPtr memobj,
            ComputeGLObjectType* gl_object_type,
            Int32* gl_object_name);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetGLTextureInfo")]
        public extern static unsafe ComputeErrorCode
        GetGLTextureInfo(
            IntPtr memobj,
            ComputeGLTextureInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            IntPtr* param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueAcquireGLObjects")]
        public extern static unsafe ComputeErrorCode
        EnqueueAcquireGLObjects(
            IntPtr command_queue,
            Int32 num_objects,
            IntPtr* mem_objects,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueReleaseGLObjects")]
        public extern static unsafe ComputeErrorCode
        EnqueueReleaseGLObjects(
            IntPtr command_queue,
            Int32 num_objects,
            IntPtr* mem_objects,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);
    }
}