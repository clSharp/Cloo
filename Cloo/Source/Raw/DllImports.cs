#region License

/*

Copyright (c) 2009 The Open Toolkit Library

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

namespace Cloo
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using OpenTK.Compute.CL10;

    internal class DllImports
    {
        const string filename = "opencl.dll";

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clCreateContext", ExactSpelling = true )]
        internal extern static unsafe IntPtr CreateContext( 
            IntPtr* properties,
            UInt32 num_devices,
            IntPtr* devices,
            IntPtr pfn_notify,
            IntPtr user_data,
            [Out] Int32* errcode_ret );

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clCreateContextFromType", ExactSpelling = true )]
        internal extern static unsafe IntPtr CreateContextFromType(
            IntPtr* properties,
            DeviceTypeFlags device_type,
            IntPtr pfn_notify,
            IntPtr user_data,
            [Out] Int32* errcode_ret );

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyBufferToImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyBufferToImage(
            IntPtr command_queue,
            IntPtr src_buffer,
            IntPtr dst_image,
            IntPtr src_offset,
            IntPtr* dst_origin,
            IntPtr* region,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            [Out] IntPtr* event_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyImage(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_image,
            IntPtr* src_origin,
            IntPtr* dst_origin,
            IntPtr* region,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            [Out] IntPtr* event_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyImageToBuffer", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyImageToBuffer(
            IntPtr command_queue,
            IntPtr src_image,
            IntPtr dst_buffer,
            IntPtr* src_origin,
            IntPtr* region,
            IntPtr dst_offset,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            [Out] IntPtr* event_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueMapImage", ExactSpelling = true )]
        internal extern static unsafe IntPtr EnqueueMapImage(
            IntPtr command_queue,
            IntPtr image,
            bool blocking_map,
            MapFlags map_flags,
            IntPtr* origin,
            IntPtr* region,
            IntPtr* image_row_pitch,
            IntPtr* image_slice_pitch,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            IntPtr* event_ret,
            [Out] Int32* errcode_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueReadImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueReadImage(
            IntPtr command_queue,
            IntPtr image,
            bool blocking_read,
            IntPtr* origin,
            IntPtr* region,
            IntPtr row_pitch,
            IntPtr slice_pitch,
            IntPtr ptr,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            [Out] IntPtr* event_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueWriteImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueWriteImage(
            IntPtr command_queue,
            IntPtr image,
            bool blocking_write,
            IntPtr* origin,
            IntPtr* region,
            IntPtr input_row_pitch,
            IntPtr input_slice_pitch,
            IntPtr ptr,
            UInt32 num_events_in_wait_list,
            IntPtr* event_wait_list,
            [Out] IntPtr* event_ret );        
    }
}