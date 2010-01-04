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

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;
    using OpenTK.Cloo.CL10;

    public class ComputeContext: ComputeResource
    {
        #region Fields

        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly PropertiesDescriptor properties;

        #endregion

        #region Properties

        /// <summary>
        /// The devices associated with this context.
        /// </summary>
        public ReadOnlyCollection<ComputeDevice> Devices
        {
            get
            {
                return devices;
            }
        }

        /// <summary>
        /// The properties of the context as specified on context creation.
        /// </summary>
        public PropertiesDescriptor Properties
        {
            get
            {
                return properties;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an OpenCL context with one or more associated devices.
        /// </summary>
        /// <param name="devices">A list of devices to associate with this context.</param>
        /// <param name="properties">A descriptor of this context properties.</param>
        /// <param name="notify">A descriptor specifying the callback function and the callback user data.</param>
        public ComputeContext( ICollection<ComputeDevice> devices, PropertiesDescriptor properties, NotifyDescriptor notify )
        {
            IntPtr[] deviceHandles = ComputeObject.ExtractHandles( devices );

            IntPtr[] propertiesList = ( properties != null ) ? properties.PropertiesList : null;
            NotifyDescriptor notifyDescr = ( notify != null ) ? notify : new NotifyDescriptor( null, IntPtr.Zero );
            int error;
            unsafe
            {
                fixed( IntPtr* propertiesPtr = propertiesList )
                fixed( IntPtr* deviceHandlesPtr = deviceHandles )
                    Handle = Overrides.CreateContext( propertiesPtr, ( uint )devices.Count, deviceHandlesPtr, notifyDescr.funcPtr, notifyDescr.dataPtr, &error );
            }
            ComputeException.ThrowIfError( error );
            this.properties = properties;
            this.devices = GetDevices();
        }

        /// <summary>
        /// Creates an OpenCL context from a device Type that identifies the specific device(s) to use.
        /// </summary>
        /// <param name="deviceType">A bit-field that identifies the Type of device to associate with this context.</param>
        /// <param name="properties">A descriptor of this context properties.</param>
        /// <param name="notify">A descriptor specifying the callback function and the callback user data.</param>
        public ComputeContext( DeviceTypeFlags deviceType, PropertiesDescriptor properties, NotifyDescriptor notify )
        {
            IntPtr[] propertiesList = ( properties != null ) ? properties.PropertiesList : null;
            NotifyDescriptor notifyDescr = ( notify != null ) ? notify : new NotifyDescriptor( null, IntPtr.Zero );
            int error = ( int )ErrorCode.Success;
            unsafe
            {
                fixed( IntPtr* propertiesPtr = propertiesList )
                    Handle = Overrides.CreateContextFromType( propertiesPtr, deviceType, notifyDescr.funcPtr, notifyDescr.dataPtr, &error );
            }
            ComputeException.ThrowIfError( error );
            this.properties = properties;
            this.devices = GetDevices();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a string representation of this context.
        /// </summary>
        public override string ToString()
        {
            return "ComputeContext" + base.ToString();
        }
        
        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( manual )
            {
                //free managed resources
            }

            // free native resources
            if( Handle != IntPtr.Zero )
            {
                CL.ReleaseContext( Handle );
                Handle = IntPtr.Zero;                
            }
        }

        #endregion

        #region Private methods

        private ReadOnlyCollection<ComputeDevice> GetDevices()
        {
            List<IntPtr> validDeviceHandles = new List<IntPtr>( GetArrayInfo<ContextInfo, IntPtr>( ContextInfo.ContextDevices, CL.GetContextInfo ) );
            List<ComputeDevice> validDevices = new List<ComputeDevice>();
            foreach( ComputePlatform platform in ComputePlatform.Platforms )
            {
                foreach( ComputeDevice device in platform.Devices )
                    if( validDeviceHandles.Contains( device.Handle ) )
                        validDevices.Add( device );
            }
            return new ReadOnlyCollection<ComputeDevice>( validDevices );
        }

        #endregion

        #region Delegates

        public delegate void NotifyDelegate( string errorInfo, IntPtr clData, IntPtr dataSize, IntPtr userData );

        #endregion

        #region Inner classes

        public class NotifyDescriptor
        {
            internal readonly IntPtr funcPtr;
            internal readonly IntPtr dataPtr;

            public NotifyDescriptor( NotifyDelegate notifyDelegate, IntPtr notifyData )
            {
                funcPtr = ( notifyDelegate != null ) ? Marshal.GetFunctionPointerForDelegate( notifyDelegate ) : IntPtr.Zero;
                this.dataPtr = notifyData;
            }
        }

        public class PropertiesDescriptor
        {
            readonly ComputePlatform platform;
            
            internal IntPtr[] PropertiesList
            {
                get
                {
                    List<IntPtr> propertiesList = new List<IntPtr>();
                    if( platform != null )
                    {
                        propertiesList.Add( new IntPtr( ( int )ContextProperties.ContextPlatform ) );
                        propertiesList.Add( platform.Handle );
                    }
                    propertiesList.Add( IntPtr.Zero );
                    return propertiesList.ToArray();
                }
            }

            public PropertiesDescriptor( ComputePlatform platform )
            {
                this.platform = platform;
            }
        }

        #endregion
    }
}