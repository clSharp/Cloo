namespace Cloo
{
    using System;
    using System.Runtime.InteropServices;
    using OpenTK.Compute.CL10;
    using System.Security;

    internal class Overrides
    {
        const string filename = "opencl.dll";

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clCreateContext", ExactSpelling = true )]
        internal extern static unsafe IntPtr CreateContext( IntPtr* properties, UInt32 num_devices, IntPtr* devices, IntPtr pfn_notify, IntPtr user_data, [OutAttribute] Int32* errcode_ret );

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clCreateContextFromType", ExactSpelling = true )]
        internal extern static unsafe IntPtr CreateContextFromType( IntPtr* properties, DeviceTypeFlags device_type, IntPtr pfn_notify, IntPtr user_data, [OutAttribute] Int32* errcode_ret );

        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyBufferToImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyBufferToImage( IntPtr command_queue, IntPtr src_buffer, IntPtr dst_image, IntPtr src_offset, IntPtr* dst_origin, IntPtr* region, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyImage( IntPtr command_queue, IntPtr src_image, IntPtr dst_image, IntPtr* src_origin, IntPtr* dst_origin, IntPtr* region, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueCopyImageToBuffer", ExactSpelling = true )]
        internal extern static unsafe int EnqueueCopyImageToBuffer( IntPtr command_queue, IntPtr src_image, IntPtr dst_buffer, IntPtr* src_origin, IntPtr* region, IntPtr dst_offset, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueMapImage", ExactSpelling = true )]
        internal extern static unsafe IntPtr EnqueueMapImage( IntPtr command_queue, IntPtr image, bool blocking_map, MapFlags map_flags, IntPtr* origin, IntPtr* region, IntPtr* image_row_pitch, IntPtr* image_slice_pitch, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event, [OutAttribute] int* errcode_ret );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueReadImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueReadImage( IntPtr command_queue, IntPtr image, bool blocking_read, IntPtr* origin, IntPtr* region, IntPtr row_pitch, IntPtr slice_pitch, IntPtr ptr, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event );
        
        [SuppressUnmanagedCodeSecurity()]
        [DllImport( filename, EntryPoint = "clEnqueueWriteImage", ExactSpelling = true )]
        internal extern static unsafe int EnqueueWriteImage( IntPtr command_queue, IntPtr image, bool blocking_write, IntPtr* origin, IntPtr* region, IntPtr input_row_pitch, IntPtr input_slice_pitch, IntPtr ptr, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* @event );        
    }
}