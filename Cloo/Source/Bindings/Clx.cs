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
    /// Contains bindings to the OpenCL extension functions.
    /// </summary>
    /// <remarks> See the Khronos OpenCL API extensions registry for documentation regarding these functions. </remarks>
    public class CLx
    {
        private readonly Delegates.clCreateSubDevicesEXT clCreateSubDevicesEXT = null;
        private readonly Delegates.clEnqueueMigrateMemObjectEXT clEnqueueMigrateMemObjectEXT = null;
        private readonly Delegates.clGetGLContextInfoKHR clGetGLContextInfoKHR = null;
        private readonly Delegates.clIcdGetPlatformIDsKHR clIcdGetPlatformIDsKHR = null;
        private readonly Delegates.clReleaseDeviceEXT clReleaseDeviceEXT = null;
        private readonly Delegates.clRetainDeviceEXT clRetainDeviceEXT = null;

        /// <summary> </summary>
        [CLSCompliant(false)]
        public unsafe ComputeErrorCode CreateSubDevicesEXT(IntPtr in_device, cl_device_partition_property_ext* properties, uint num_entries, IntPtr* out_devices, uint* num_devices)
        {
            if (clCreateSubDevicesEXT == null) throw new EntryPointNotFoundException();
            return clCreateSubDevicesEXT(in_device, properties, num_entries, out_devices, num_devices);
        }

        /// <summary> </summary>
        [CLSCompliant(false)]
        public unsafe ComputeErrorCode EnqueueMigrateMemObjectEXT(IntPtr command_queue, uint num_mem_objects, IntPtr* mem_objects, cl_mem_migration_flags_ext flags, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* new_event)
        {
            if (clEnqueueMigrateMemObjectEXT == null) throw new EntryPointNotFoundException();
            return clEnqueueMigrateMemObjectEXT(command_queue, num_mem_objects, mem_objects, flags, num_events_in_wait_list, event_wait_list, new_event);
        }

        /// <summary> </summary>
        [CLSCompliant(false)]
        public unsafe ComputeErrorCode GetGLContextInfoKHR(IntPtr* properties, ComputeGLContextInfo param_name, IntPtr param_value_size, IntPtr param_value, IntPtr* param_value_size_ret)
        {
            if (clGetGLContextInfoKHR == null) throw new EntryPointNotFoundException();
            return clGetGLContextInfoKHR(properties, param_name, param_value_size, param_value, param_value_size_ret);
        }

        /// <summary> </summary>
        //[CLSCompliant(false)]
        //public unsafe ComputeErrorCode IcdGetPlatformIDsKHR(uint num_entries, IntPtr* platforms, uint* num_platforms)
        //{
        //    if (clIcdGetPlatformIDsKHR == null) throw new EntryPointNotFoundException();
        //    return clIcdGetPlatformIDsKHR(num_entries, platforms, num_platforms);
        //}

        /// <summary> </summary>
        [CLSCompliant(false)]
        public unsafe ComputeErrorCode ReleaseDeviceEXT(IntPtr device)
        {
            if (clReleaseDeviceEXT == null) throw new EntryPointNotFoundException();
            return clReleaseDeviceEXT(device);
        }

        /// <summary> </summary>
        [CLSCompliant(false)]
        public unsafe ComputeErrorCode RetainDeviceEXT(IntPtr device)
        {
            if (clRetainDeviceEXT == null) throw new EntryPointNotFoundException();
            return clRetainDeviceEXT(device);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platform"></param>
        public CLx(ComputePlatform platform)
        {
            if (platform.Extensions.Contains("cl_ext_device_fission"))
            {
                clCreateSubDevicesEXT = (Delegates.clCreateSubDevicesEXT)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clCreateSubDevicesEXT"), typeof(Delegates.clCreateSubDevicesEXT));
                clReleaseDeviceEXT = (Delegates.clReleaseDeviceEXT)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clReleaseDeviceEXT"), typeof(Delegates.clReleaseDeviceEXT));
                clRetainDeviceEXT = (Delegates.clRetainDeviceEXT)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clRetainDeviceEXT"), typeof(Delegates.clRetainDeviceEXT));
            }

            if (platform.Extensions.Contains("cl_ext_migrate_memobject")) 
                clEnqueueMigrateMemObjectEXT = (Delegates.clEnqueueMigrateMemObjectEXT)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clEnqueueMigrateMemObjectEXT"), typeof(Delegates.clEnqueueMigrateMemObjectEXT));
            
            if (platform.Extensions.Contains("cl_khr_gl_sharing")) 
                clGetGLContextInfoKHR = (Delegates.clGetGLContextInfoKHR)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clGetGLContextInfoKHR"), typeof(Delegates.clGetGLContextInfoKHR));

            //if (platform.Extensions.Contains("cl_khr_icd"))
            //    clIcdGetPlatformIDsKHR = (Delegates.clIcdGetPlatformIDsKHR)Marshal.GetDelegateForFunctionPointer(CL10.GetExtensionFunctionAddress("clIcdGetPlatformIDsKHR"), typeof(Delegates.clIcdGetPlatformIDsKHR));
        }

        internal static class Delegates
        {
            internal unsafe delegate ComputeErrorCode clCreateSubDevicesEXT(IntPtr in_device, cl_device_partition_property_ext * properties, uint num_entries, IntPtr* out_devices, uint* num_devices);
            internal unsafe delegate ComputeErrorCode clEnqueueMigrateMemObjectEXT(IntPtr command_queue, uint num_mem_objects, IntPtr* mem_objects, cl_mem_migration_flags_ext flags, uint num_events_in_wait_list, IntPtr* event_wait_list, IntPtr* new_event);
            internal unsafe delegate ComputeErrorCode clGetGLContextInfoKHR(IntPtr* properties, ComputeGLContextInfo param_name, IntPtr param_value_size, IntPtr param_value, IntPtr* param_value_size_ret);
            internal unsafe delegate ComputeErrorCode clIcdGetPlatformIDsKHR(uint num_entries, IntPtr* platforms, uint* num_platforms);
            internal unsafe delegate ComputeErrorCode clReleaseDeviceEXT(IntPtr device);
            internal unsafe delegate ComputeErrorCode clRetainDeviceEXT(IntPtr device);
        }
    }
}