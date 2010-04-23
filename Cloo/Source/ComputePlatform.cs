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
    using System.Reflection;

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
        /// A list of devices available on this platform.
        /// </summary>
        public ReadOnlyCollection<ComputeDevice> Devices
        {
            get
            {
                return devices;
            }
        }

        /// <summary>
        /// A list of extension names supported by the platform.
        /// </summary>
        public ReadOnlyCollection<string> Extensions
        {
            get
            {
                return extensions;
            }
        }

        /// <summary>
        /// Platform name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// A list of available platforms.
        /// </summary>
        public static ReadOnlyCollection<ComputePlatform> Platforms
        {
            get
            {
                if (platforms == null)
                    Initialize();

                return platforms;
            }
        }

        /// <summary>
        /// The profile name supported by this platform.
        /// </summary>
        public string Profile
        {
            get
            {
                return profile;
            }
        }

        /// <summary>
        /// Platform vendor.
        /// </summary>
        public string Vendor
        {
            get
            {
                return vendor;
            }
        }

        /// <summary>
        /// The OpenCL version supported by this platform.
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }
        }

        #endregion

        #region Constructors

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
        /// Gets a platform of a matching handle.
        /// </summary>
        /// <param name="handle">The handle of the queried platform.</param>
        public static ComputePlatform GetByHandle(IntPtr handle)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Handle == handle)
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets a platform of a matching name.
        /// </summary>
        /// <param name="platformName">The name of the queried platform.</param>
        public static ComputePlatform GetByName(string platformName)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Name.Equals(platformName))
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets a platform of a matching vendor.
        /// </summary>
        /// <param name="platformVendor">The vendor of the queried platform.</param>
        public static ComputePlatform GetByVendor(string platformVendor)
        {
            foreach (ComputePlatform platform in Platforms)
                if (platform.Vendor.Equals(platformVendor))
                    return platform;

            return null;
        }

        /// <summary>
        /// Retrieves all the available platforms and their devices.
        /// </summary>
        private static void Initialize()
        {
            if (platforms != null) return;

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

        /// <summary>
        /// Queries the available devices on this platform.
        /// </summary>
        /// <returns>The list of devices available on this platform.</returns>
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
        /// Gets a string representation of this platform.
        /// </summary>
        public override string ToString()
        {
            return "ComputePlatform" + base.ToString();
        }

        #endregion
    }
}