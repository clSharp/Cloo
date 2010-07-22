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

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL platform.
    /// </summary>
    /// <remarks> The host plus a collection of devices managed by the OpenCL framework that allow an application to share resources and execute kernels on devices in the platform. </remarks>
    /// <seealso cref="ComputeDevice"/>
    /// <seealso cref="ComputeKernel"/>
    /// <seealso cref="ComputeResource"/>
    public class ComputePlatform : ComputeObject
    {
        #region Fields

        private ReadOnlyCollection<ComputeDevice> devices;
        private readonly ReadOnlyCollection<string> extensions;
        private readonly string name;
        private static ReadOnlyCollection<ComputePlatform> platforms;
        private readonly string profile;
        private readonly string vendor;
        private readonly string version;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only collection of <c>ComputeDevice</c>s available on the <c>ComputePlatform</c>.
        /// </summary>
        public ReadOnlyCollection<ComputeDevice> Devices { get { return devices; } }

        /// <summary>
        /// Gets a read-only collection of extension names supported by the <c>ComputePlatform</c>.
        /// </summary>
        public ReadOnlyCollection<string> Extensions { get { return extensions; } }

        /// <summary>
        /// Gets the <c>ComputePlatform</c> name.
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Gets a read-only collection of available <c>ComputePlatform</c>s.
        /// </summary>
        public static ReadOnlyCollection<ComputePlatform> Platforms { get { return platforms; } }

        /// <summary>
        /// Gets the name of the profile supported by the <c>ComputePlatform</c>.
        /// </summary>
        public string Profile { get { return profile; } }

        /// <summary>
        /// Gets the <c>ComputePlatform</c> vendor.
        /// </summary>
        public string Vendor { get { return vendor; } }

        /// <summary>
        /// Gets the OpenCL version supported by the <c>ComputePlatform</c>.
        /// </summary>
        public string Version { get { return version; } }

        #endregion

        #region Constructors

        static ComputePlatform()
        {
            unsafe
            {
                IntPtr[] handles;
                int handlesLength = 0;
                ComputeErrorCode error = CL10.GetPlatformIDs(0, null, &handlesLength);
                ComputeException.ThrowOnError(error);
                handles = new IntPtr[handlesLength];

                fixed (IntPtr* handlesPtr = handles)
                {
                    error = CL10.GetPlatformIDs(handlesLength, handlesPtr, null);
                    ComputeException.ThrowOnError(error);
                }

                List<ComputePlatform> platformList = new List<ComputePlatform>(handlesLength);
                foreach (IntPtr handle in handles)
                    platformList.Add(new ComputePlatform(handle));

                platforms = platformList.AsReadOnly();
            }
        }

        private ComputePlatform(IntPtr handle)
        {
            unsafe
            {
                Handle = handle;                

                string extensionString = GetStringInfo<ComputePlatformInfo>(ComputePlatformInfo.Extensions, CL10.GetPlatformInfo);
                extensions = new ReadOnlyCollection<string>(extensionString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

                name = GetStringInfo<ComputePlatformInfo>(ComputePlatformInfo.Name, CL10.GetPlatformInfo);
                profile = GetStringInfo<ComputePlatformInfo>(ComputePlatformInfo.Profile, CL10.GetPlatformInfo);
                vendor = GetStringInfo<ComputePlatformInfo>(ComputePlatformInfo.Vendor, CL10.GetPlatformInfo);
                version = GetStringInfo<ComputePlatformInfo>(ComputePlatformInfo.Version, CL10.GetPlatformInfo);
                QueryDevices();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a <c>ComputePlatform</c> of a specified handle.
        /// </summary>
        /// <param name="handle"> The handle of the queried <c>ComputePlatform</c>. </param>
        /// <returns> The <c>ComputePlatform</c> of the matching handle or <c>null</c> if none matches. </returns>
        public static ComputePlatform GetByHandle(IntPtr handle)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Handle == handle)
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets the first matching <c>ComputePlatform</c> of a specified name.
        /// </summary>
        /// <param name="platformName"> The name of the queried <c>ComputePlatform</c>. </param>
        /// <returns> The first <c>ComputePlatform</c> of the specified name or <c>null</c> if none matches. </returns>
        public static ComputePlatform GetByName(string platformName)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Name.Equals(platformName))
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets the first matching <c>ComputePlatform</c> of a specified vendor.
        /// </summary>
        /// <param name="platformVendor"> The vendor of the queried <c>ComputePlatform</c>. </param>
        /// <returns> The first <c>ComputePlatform</c> of the specified vendor or <c>null</c> if none matches. </returns>
        public static ComputePlatform GetByVendor(string platformVendor)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Vendor.Equals(platformVendor))
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets a read-only collection of available <c>ComputeDevice</c>s on the <c>ComputePlatform</c>.
        /// </summary>
        /// <returns> A read-only collection of the available <c>ComputeDevice</c>s on the <c>ComputePlatform</c>. </returns>
        /// <remarks> This method resets the <c>ComputePlatform.Devices</c>. This is useful if one or more of them become unavailable (<c>ComputeDevice.Available</c> is <c>false</c>) after a <c>ComputeContext</c> and <c>ComputeCommandQueue</c>s that use the <c>ComputeDevice</c> have been created and commands have been queued to them. Further calls will trigger an <c>OutOfResourcesComputeException</c> until this method is executed. You will also need to recreate any <c>ComputeResource</c> that was created on the no longer available <c>ComputeDevice</c>. </remarks>
        public ReadOnlyCollection<ComputeDevice> QueryDevices()
        {
            unsafe
            {
                int handlesLength = 0;
                ComputeErrorCode error = CL10.GetDeviceIDs(Handle, ComputeDeviceTypes.All, 0, null, &handlesLength);
                ComputeException.ThrowOnError(error);

                IntPtr[] handles = new IntPtr[handlesLength];
                fixed (IntPtr* devicesPtr = handles)
                {
                    error = CL10.GetDeviceIDs(Handle, ComputeDeviceTypes.All, handlesLength, devicesPtr, null);
                    ComputeException.ThrowOnError(error);
                }

                ComputeDevice[] devices = new ComputeDevice[handlesLength];
                for (int i = 0; i < handlesLength; i++)
                    devices[i] = new ComputeDevice(this, handles[i]);

                this.devices = new ReadOnlyCollection<ComputeDevice>(devices);

                return this.devices;
            }
        }

        /// <summary>
        /// Gets the string representation of the <c>ComputePlatform</c>.
        /// </summary>
        /// <returns> The string representation of the <c>ComputePlatform</c>. </returns>
        public override string ToString()
        {
            return "ComputePlatform(" + Name + ")";
        }

        #endregion
    }
}