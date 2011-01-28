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
    [SuppressUnmanagedCodeSecurity]
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
        public extern static ComputeErrorCode
        GetPlatformIDs(
            Int32 num_entries,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            out Int32 num_platforms);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetPlatformInfo")]
        public extern static ComputeErrorCode
        GetPlatformInfo(
            IntPtr platform,
            ComputePlatformInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetDeviceIDs")]
        public extern static ComputeErrorCode
        GetDeviceIDs(
            IntPtr platform,
            ComputeDeviceTypes device_type,
            Int32 num_entries,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            out Int32 num_devices);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetDeviceInfo")]
        public extern static ComputeErrorCode
        GetDeviceInfo(
            IntPtr device,
            ComputeDeviceInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateContext")]
        public extern static IntPtr
        CreateContext(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateContextFromType")]
        public extern static IntPtr
        CreateContextFromType(
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] properties,
            ComputeDeviceTypes device_type,
            ComputeContextNotifier pfn_notify,
            IntPtr user_data,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainContext")]
        public extern static ComputeErrorCode
        RetainContext(IntPtr context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseContext")]
        public extern static ComputeErrorCode
        ReleaseContext(IntPtr context);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetContextInfo")]
        public extern static ComputeErrorCode
        GetContextInfo(
            IntPtr context,
            ComputeContextInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateCommandQueue")]
        public extern static IntPtr
        CreateCommandQueue(
            IntPtr context,
            IntPtr device,
            ComputeCommandQueueFlags properties,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainCommandQueue")]
        public extern static ComputeErrorCode
        RetainCommandQueue(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseCommandQueue")]
        public extern static ComputeErrorCode
        ReleaseCommandQueue(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetCommandQueueInfo")]
        public extern static ComputeErrorCode
        GetCommandQueueInfo(
            IntPtr command_queue,
            ComputeCommandQueueInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetCommandQueueProperty")]
        public extern static ComputeErrorCode
        SetCommandQueueProperty(
            IntPtr command_queue,
            ComputeCommandQueueFlags properties,
            ComputeBoolean enable,
            out ComputeCommandQueueFlags old_properties);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateBuffer")]
        public extern static IntPtr
        CreateBuffer(
            IntPtr context,
            ComputeMemoryFlags flags,
            IntPtr size,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateImage2D")]
        public extern static IntPtr
        CreateImage2D(
            IntPtr context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_row_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateImage3D")]
        public extern static IntPtr
        CreateImage3D(
            IntPtr context,
            ComputeMemoryFlags flags,
            ref ComputeImageFormat image_format,
            IntPtr image_width,
            IntPtr image_height,
            IntPtr image_depth,
            IntPtr image_row_pitch,
            IntPtr image_slice_pitch,
            IntPtr host_ptr,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainMemObject")]
        public extern static ComputeErrorCode
        RetainMemObject(IntPtr memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseMemObject")]
        public extern static ComputeErrorCode
        ReleaseMemObject(IntPtr memobj);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetSupportedImageFormats")]
        public extern static ComputeErrorCode
        GetSupportedImageFormats(
            IntPtr context,
            ComputeMemoryFlags flags,
            ComputeMemoryType image_type,
            Int32 num_entries,
            [MarshalAs(UnmanagedType.LPArray)] ComputeImageFormat[] image_formats,
            out Int32 num_image_formats);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetMemObjectInfo")]
        public extern static ComputeErrorCode
        GetMemObjectInfo(
            IntPtr memobj,
            ComputeMemoryInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetImageInfo")]
        public extern static ComputeErrorCode
        GetImageInfo(
            IntPtr image,
            ComputeImageInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateSampler")]
        public extern static IntPtr
        CreateSampler(
            IntPtr context,
            ComputeBoolean normalized_coords,
            ComputeImageAddressing addressing_mode,
            ComputeImageFiltering filter_mode,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainSampler")]
        public extern static ComputeErrorCode
        RetainSampler(IntPtr sampler);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseSampler")]
        public extern static ComputeErrorCode
        ReleaseSampler(IntPtr sampler);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetSamplerInfo")]
        public extern static ComputeErrorCode
        GetSamplerInfo(
            IntPtr sampler,
            ComputeSamplerInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateProgramWithSource")]
        public extern static IntPtr
        CreateProgramWithSource(
            IntPtr context,
            Int32 count,
            String[] strings,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateProgramWithBinary")]
        public extern static IntPtr
        CreateProgramWithBinary(
            IntPtr context,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] device_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] lengths,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] binaries,
            [MarshalAs(UnmanagedType.LPArray)] Int32[] binary_status,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainProgram")]
        public extern static ComputeErrorCode
        RetainProgram(IntPtr program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseProgram")]
        public extern static ComputeErrorCode
        ReleaseProgram(IntPtr program);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clBuildProgram")]
        public extern static ComputeErrorCode
        BuildProgram(
            IntPtr program,
            Int32 num_devices,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] device_list,
            String options,
            ComputeProgramBuildNotifier pfn_notify,
            IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clUnloadCompiler")]
        public extern static ComputeErrorCode
        UnloadCompiler();

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetProgramInfo")]
        public extern static ComputeErrorCode
        GetProgramInfo(
            IntPtr program,
            ComputeProgramInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetProgramBuildInfo")]
        public extern static ComputeErrorCode
        GetProgramBuildInfo(
            IntPtr program,
            IntPtr device,
            ComputeProgramBuildInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateKernel")]
        public extern static IntPtr
        CreateKernel(
            IntPtr program,
            String kernel_name,
            out ComputeErrorCode errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateKernelsInProgram")]
        public extern static ComputeErrorCode
        CreateKernelsInProgram(
            IntPtr program,
            Int32 num_kernels,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] kernels,
            out Int32 num_kernels_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainKernel")]
        public extern static ComputeErrorCode
        RetainKernel(IntPtr kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseKernel")]
        public extern static ComputeErrorCode
        ReleaseKernel(IntPtr kernel);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetKernelArg")]
        public extern static ComputeErrorCode
        SetKernelArg(
            IntPtr kernel,
            Int32 arg_index,
            IntPtr arg_size,
            IntPtr arg_value);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetKernelInfo")]
        public extern static ComputeErrorCode
        GetKernelInfo(
            IntPtr kernel,
            ComputeKernelInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetKernelWorkGroupInfo")]
        public extern static ComputeErrorCode
        GetKernelWorkGroupInfo(
            IntPtr kernel,
            IntPtr device,
            ComputeKernelWorkGroupInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clWaitForEvents")]
        public extern static ComputeErrorCode
        WaitForEvents(
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetEventInfo")]
        public extern static ComputeErrorCode
        GetEventInfo(
            IntPtr @event,
            ComputeEventInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clRetainEvent")]
        public extern static ComputeErrorCode
        RetainEvent(IntPtr @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clReleaseEvent")]
        public extern static ComputeErrorCode
        ReleaseEvent(IntPtr @event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetEventProfilingInfo")]
        public extern static ComputeErrorCode
        GetEventProfilingInfo(
            IntPtr @event,
            ComputeCommandProfilingInfo param_name,
            IntPtr param_value_size,
            IntPtr param_value,
            out IntPtr param_value_size_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clFlush")]
        public extern static ComputeErrorCode
        Flush(IntPtr command_queue);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clFinish")]
        public extern static ComputeErrorCode
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
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            ref SysIntX3 origin,
            ref SysIntX3 region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            ref SysIntX3 src_origin,
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            ref SysIntX3 src_origin,
            ref SysIntX3 region,
            IntPtr dst_offset,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            ref SysIntX3 dst_origin,
            ref SysIntX3 region,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMapBuffer")]
        public extern static unsafe IntPtr
        EnqueueMapBuffer(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            IntPtr offset,
            IntPtr cb,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] IntPtr[] new_event,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMapImage")]
        public extern static unsafe IntPtr
        EnqueueMapImage(
            IntPtr command_queue,
            IntPtr image,
            ComputeBoolean blocking_map,
            ComputeMemoryMappingFlags map_flags,
            ref SysIntX3 origin,
            ref SysIntX3 region,
            out IntPtr image_row_pitch,
            out IntPtr image_slice_pitch,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] IntPtr[] new_event,
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
            IntPtr mapped_ptr,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueNativeKernel")]
        public extern static unsafe ComputeErrorCode
        EnqueueNativeKernel(
            IntPtr command_queue,
            IntPtr user_func,
            IntPtr args,
            IntPtr cb_args,
            Int32 num_mem_objects,
            IntPtr* mem_list,
            IntPtr* args_mem_loc,
            Int32 num_events_in_wait_list,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueMarker")]
        public extern static unsafe ComputeErrorCode
        EnqueueMarker(
            IntPtr command_queue,
            out IntPtr new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueWaitForEvents")]
        public extern static unsafe ComputeErrorCode
        EnqueueWaitForEvents(
            IntPtr command_queue,
            Int32 num_events,
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_list);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueBarrier")]
        public extern static unsafe ComputeErrorCode
        EnqueueBarrier(IntPtr command_queue);

        
        /// <summary>
        /// Gets the extension function address for the given function name,
        /// or NULL if a valid function can not be found. The client must
        /// check to make sure the address is not NULL, before using or 
        /// calling the returned function address.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clGetExtensionFunctionAddress")]
        public extern static unsafe IntPtr
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
            out IntPtr param_value_size_ret);

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
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);

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
            [MarshalAs(UnmanagedType.LPArray)] IntPtr[] event_wait_list,
            [MarshalAs(UnmanagedType.LPArray, SizeConst=1)] IntPtr[] new_event);
    }
}