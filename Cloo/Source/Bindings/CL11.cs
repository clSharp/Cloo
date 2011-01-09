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

    /// <summary>
    /// Contains bindings to the OpenCL 1.1 functions.
    /// </summary>
    /// <remarks> See the OpenCL specification for documentation regarding these functions. </remarks>
    public class CL11 : CL10
    {
        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateSubBuffer")]
        public extern static unsafe IntPtr CreateSubBuffer(
            IntPtr buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            /* const void * */ IntPtr buffer_create_info,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetMemObjectDestructorCallback")]
        public extern static unsafe ComputeErrorCode SetMemObjectDestructorCallback( 
            IntPtr memobj, 
            /* void (CL_CALLBACK * pfn_notify)(cl_mem memobj, void* user_data)*/ IntPtr pfn_notify, 
            /* void * */ IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clCreateUserEvent")]
        public extern static unsafe IntPtr CreateUserEvent(
            IntPtr context,
            ComputeErrorCode* errcode_ret);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetUserEventStatus")]
        public extern static unsafe ComputeErrorCode SetUserEventStatus(
            IntPtr @event,
            Int32 execution_status);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clSetEventCallback")]
        public extern static unsafe ComputeErrorCode SetEventCallback(
            IntPtr @event,
            Int32 command_exec_callback_type,
            /* void (CL_CALLBACK * pfn_notify)(cl_event, cl_int, void *) */ ComputeEventCallback pfn_notify,
            /* void * */ IntPtr user_data);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueReadBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueReadBufferRect(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_read,
            SysIntX3* buffer_offset,
            SysIntX3* host_offset,
            SysIntX3* region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueWriteBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueWriteBufferRect(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_write,
            SysIntX3* buffer_offset,
            SysIntX3* host_offset,
            SysIntX3* region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [DllImport(libName, EntryPoint = "clEnqueueCopyBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueCopyBufferRect(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_buffer,
            SysIntX3* src_origin,
            SysIntX3* dst_origin,
            SysIntX3* region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            Int32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        /// <summary>
        /// See the OpenCL specification.
        /// </summary>
        [CLSCompliant(false)]
        [Obsolete("This function has been deprecated in OpenCL 1.1.")]
        new public static unsafe ComputeErrorCode
        SetCommandQueueProperty(
            IntPtr command_queue,
            ComputeCommandQueueFlags properties,
            ComputeBoolean enable,
            ComputeCommandQueueFlags* old_properties)
        {
            throw new NotSupportedException("This function has been deprecated in OpenCL 1.1.");
        }
    }
}