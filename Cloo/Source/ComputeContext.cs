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
    using System.Runtime.InteropServices;
    using Cloo.Bindings;

    public class ComputeContext: ComputeResource
    {
        #region Fields

        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly ComputeContextPropertyList properties;

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
        public ComputeContextPropertyList Properties
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
        /// <param name="notify">A callback function that can be registered by the application. This callback function will be used by the OpenCL implementation to report information on errors that occur in this context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. If notify is null, no callback function is registered.</param>
        public ComputeContext( ICollection<ComputeDevice> devices, ComputeContextPropertyList properties, ComputeContextNotifier notify, IntPtr notifyDataPtr )
        {
            IntPtr[] deviceHandles = Tools.ExtractHandles( devices );
            IntPtr[] propertiesList = ( properties != null ) ? properties.ToIntPtrArray() : null;
            IntPtr notifyFuncPtr = ( notify != null ) ? Marshal.GetFunctionPointerForDelegate( notify ) : IntPtr.Zero;

            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                fixed( IntPtr* propertiesPtr = propertiesList )
                fixed( IntPtr* deviceHandlesPtr = deviceHandles )
                    Handle = CL10.CreateContext( 
                        propertiesPtr, 
                        devices.Count, 
                        deviceHandlesPtr, 
                        notifyFuncPtr, 
                        notifyDataPtr, 
                        out error );
                ComputeException.ThrowOnError( error );
            }

            this.properties = properties;
            this.devices = GetDevices();
        }

        /// <summary>
        /// Creates an OpenCL context from a device type that identifies the specific device(s) to use.
        /// </summary>
        /// <param name="deviceType">A bit-field that identifies the Type of device to associate with this context.</param>
        /// <param name="properties">A descriptor of this context properties.</param>
        /// <param name="notify">A callback function that can be registered by the application. This callback function will be used by the OpenCL implementation to report information on errors that occur in this context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. If notify is null, no callback function is registered.</param>
        /// <param name="notifyDataPtr">Passed as the userDataPtr argument when notify is called. userDataPtr can be IntPtr.Zero.</param>
        public ComputeContext( ComputeDeviceTypes deviceType, ComputeContextPropertyList properties, ComputeContextNotifier notify, IntPtr notifyDataPtr )
        {
            IntPtr[] propertiesList = ( properties != null ) ? properties.ToIntPtrArray() : null;
            IntPtr notifyFuncPtr = ( notify != null ) ? Marshal.GetFunctionPointerForDelegate( notify ) : IntPtr.Zero;

            unsafe
            {
                ComputeErrorCode error = ComputeErrorCode.Success;
                fixed( IntPtr* propertiesPtr = propertiesList )
                    Handle = CL10.CreateContextFromType( 
                        propertiesPtr, 
                        deviceType, 
                        notifyFuncPtr, 
                        notifyDataPtr, 
                        out error );
                ComputeException.ThrowOnError( error );
            }

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
                CL10.ReleaseContext( Handle );
                Handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Private methods

        private ReadOnlyCollection<ComputeDevice> GetDevices()
        {
            List<IntPtr> validDeviceHandles = new List<IntPtr>( GetArrayInfo<ComputeContextInfo, IntPtr>( ComputeContextInfo.Devices, CL10.GetContextInfo ) );
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
    }

    /// <summary>
    /// A callback function that can be registered by the application. This callback function will be used by the OpenCL implementation to report information on errors that occur in this context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe.
    /// </summary>
    /// <param name="errorInfo">An error string.</param>
    /// <param name="clDataPtr">A pointer to binary data that is returned by the OpenCL implementation that can be used to log additional information helpful in debugging the error.</param>
    /// <param name="clDataSize">The size of the binary data that is returned by the OpenCL.</param>
    /// <param name="userDataPtr">A pointer to user supplied data.</param>
    [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
    public delegate void ComputeContextNotifier( String errorInfo, IntPtr clDataPtr, IntPtr clDataSize, IntPtr userDataPtr );
}