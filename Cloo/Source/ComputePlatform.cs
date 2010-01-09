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

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using OpenTK.Compute.CL10;

    public class ComputePlatform : ComputeObject
    {
        #region Fields
        
        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly ReadOnlyCollection<string> extensions;
        private readonly string name;
        private static readonly ReadOnlyCollection<ComputePlatform> platforms;
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

        static ComputePlatform()
        {
            IntPtr[] handles;
            int handlesLength = 0;
            unsafe
            {
                int error = CL.GetPlatformIDs( 0, null, &handlesLength );
                ComputeException.ThrowIfError( error );
                handles = new IntPtr[ handlesLength ];

                fixed( IntPtr* handlesPtr = handles )
                { error = CL.GetPlatformIDs( handlesLength, handlesPtr, null ); }
                ComputeException.ThrowIfError( error );
            }

            List<ComputePlatform> platformList = new List<ComputePlatform>( handlesLength );
            foreach( IntPtr handle in handles )
                platformList.Add( new ComputePlatform( handle ) );

            platforms = platformList.AsReadOnly();
        }

        private ComputePlatform( IntPtr handle )
        {
            Handle = handle;
            devices = new ReadOnlyCollection<ComputeDevice>( GetDevices() );
            
            string extensionString = GetStringInfo<PlatformInfo>( PlatformInfo.PlatformExtensions, CL.GetPlatformInfo );
            extensions = new ReadOnlyCollection<string>( extensionString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ) );
            
            name = GetStringInfo<PlatformInfo>( PlatformInfo.PlatformName, CL.GetPlatformInfo );
            profile = GetStringInfo<PlatformInfo>( PlatformInfo.PlatformProfile, CL.GetPlatformInfo );
            vendor = GetStringInfo<PlatformInfo>( PlatformInfo.PlatformVendor, CL.GetPlatformInfo );
            version = GetStringInfo<PlatformInfo>( PlatformInfo.PlatformVersion, CL.GetPlatformInfo );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a platform of a matching name.
        /// </summary>
        /// <param name="platformName">The name of the queried platform.</param>
        public static ComputePlatform GetByName( string platformName )
        {
            foreach( ComputePlatform platform in Platforms )
                if( platform.Name.Equals( platformName ) )
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets a platform of a matching vendor.
        /// </summary>
        /// <param name="platformVendor">The vendor of the queried platform.</param>
        public static ComputePlatform GetByVendor( string platformVendor )
        {
            foreach( ComputePlatform platform in Platforms )
                if( platform.Vendor.Equals( platformVendor ) )
                    return platform;

            return null;
        }

        /// <summary>
        /// Gets a string representation of this platform.
        /// </summary>
        public override string ToString()
        {
            return "ComputePlatform" + base.ToString();
        }

        #endregion

        #region Private methods

        private ComputeDevice[] GetDevices()
        {
            IntPtr[] handles;
            uint handlesLength = 0;
            unsafe
            {
                int error = CL.GetDeviceIDs( Handle, DeviceTypeFlags.DeviceTypeAll, 0, null, &handlesLength );
                ComputeException.ThrowIfError( error );

                handles = new IntPtr[ handlesLength ];
                fixed( IntPtr* devicesPtr = handles )
                    error = CL.GetDeviceIDs( Handle, DeviceTypeFlags.DeviceTypeAll, handlesLength, devicesPtr, null );
                ComputeException.ThrowIfError( error );
            }

            ComputeDevice[] devices = new ComputeDevice[ handlesLength ];
            for( int i = 0; i < handlesLength; i++ )
                devices[ i ] = new ComputeDevice( this, handles[ i ] );

            return devices;
        }

        #endregion
    }
}