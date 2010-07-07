#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

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

    public class CL11 : CL10
    {
        [Obsolete("This function has been deprecated in OpenCL 1.1", true)]
        public new static unsafe ComputeErrorCode
        SetCommandQueueProperty(
            IntPtr command_queue,
            ComputeCommandQueueFlags properties,
            ComputeBoolean enable,
            out ComputeCommandQueueFlags old_properties) 
        {
            throw new NotSupportedException("This function has been deprecated in OpenCL 1.1");
        }

        [DllImport(dll, EntryPoint="clCreateSubBuffer")]
        public extern static unsafe IntPtr CreateSubBuffer(
            IntPtr buffer,
            ComputeMemoryFlags flags,
            ComputeBufferCreateType buffer_create_type,
            /* const void * */ IntPtr buffer_create_info,
            ComputeErrorCode* errcode_ret);

        [DllImport(dll, EntryPoint="clSetMemObjectDestructorCallback")]
        public extern static unsafe ComputeErrorCode SetMemObjectDestructorCallback( 
            IntPtr memobj, 
            /* void (CL_CALLBACK * pfn_notify)(cl_mem memobj, void* user_data)*/ IntPtr pfn_notify, 
            /* void * */ IntPtr user_data);

        [DllImport(dll, EntryPoint = "clCreateUserEvent")]
        public extern static unsafe IntPtr CreateUserEvent(
            IntPtr context,
            ComputeErrorCode* errcode_ret);

        [DllImport(dll, EntryPoint = "clSetUserEventStatus")]
        public extern static unsafe ComputeErrorCode SetUserEventStatus(
            IntPtr @event,
            int execution_status);

        [DllImport(dll, EntryPoint = "clSetEventCallback")]
        public extern static unsafe ComputeErrorCode SetEventCallback(
            IntPtr @event,
            int command_exec_callback_type,
            /* void (CL_CALLBACK * pfn_notify)(cl_event, cl_int, void *) */ IntPtr pfn_notify,
            /* void * */ IntPtr user_data);

        [DllImport(dll, EntryPoint = "clEnqueueReadBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueReadBufferRect(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_read,
            IntPtr* buffer_offset,
            IntPtr* host_offset,
            IntPtr* region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            int num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        [DllImport(dll, EntryPoint = "clEnqueueWriteBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueWriteBufferRect(
            IntPtr command_queue,
            IntPtr buffer,
            ComputeBoolean blocking_write,
            IntPtr* buffer_offset,
            IntPtr* host_offset,
            IntPtr* region,
            IntPtr buffer_row_pitch,
            IntPtr buffer_slice_pitch,
            IntPtr host_row_pitch,
            IntPtr host_slice_pitch,
            IntPtr ptr,
            int num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);

        [DllImport(dll, EntryPoint = "clEnqueueCopyBufferRect")]
        public extern static unsafe ComputeErrorCode EnqueueCopyBufferRect(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_buffer,
            IntPtr* src_origin,
            IntPtr* dst_origin,
            IntPtr* region,
            IntPtr src_row_pitch,
            IntPtr src_slice_pitch,
            IntPtr dst_row_pitch,
            IntPtr dst_slice_pitch,
            int num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* new_event);
    }
}