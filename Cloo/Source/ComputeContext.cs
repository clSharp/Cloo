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

namespace Cloo
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Threading;
    using Cloo.Bindings;

    /// <summary>
    /// Represents an OpenCL context.
    /// </summary>
    /// <remarks> The environment within which the kernels execute and the domain in which synchronization and memory management is defined. </remarks>
    /// <br/>
    /// <example> 
    /// This example shows how to create a <see cref="ComputeContext"/> that is able to share data with an OpenGL context in a Microsoft Windows OS:
    /// <code>
    /// [DllImport("opengl32.dll")]
    /// extern static IntPtr wglGetCurrentDC();
    /// // ...
    /// IntPtr deviceContextHandle = wglGetCurrentDC();
    /// ComputePlatform platform = ComputePlatform.GetByName(nameOfPlatformCapableOfCLGLInterop);
    /// ComputeContextProperty p1 = new ComputeContextProperty(ComputeContextPropertyName.Platform, platform.Handle);
    /// ComputeContextProperty p2 = new ComputeContextProperty(ComputeContextPropertyName.CL_GL_CONTEXT_KHR, openGLContextHandle);
    /// ComputeContextProperty p3 = new ComputeContextProperty(ComputeContextPropertyName.CL_WGL_HDC_KHR, deviceContextHandle);
    /// ComputeContextPropertyList cpl = new ComputeContextPropertyList(new ComputeContextProperty[] { p1, p2, p3 });
    /// ComputeContext context = new ComputeContext(ComputeDeviceTypes.Gpu, cpl, null, IntPtr.Zero);
    /// // Create a shared OpenCL/OpenGL buffer.
    /// // The generic type should match the type of data that the buffer contains.
    /// // glBufferId is an existent OpenGL buffer identifier.
    /// <![CDATA[ComputeBuffer<float> clglBuffer = ComputeBuffer.CreateFromGLBuffer<float>(context, ComputeMemoryFlags.ReadWrite, glBufferId);]]>
    /// </code>
    /// </example>
    /// <seealso cref="ComputeDevice"/>
    /// <seealso cref="ComputePlatform"/>
    public class ComputeContext : ComputeResource
    {
        #region Fields

        private readonly ReadOnlyCollection<ComputeDevice> devices;
        private readonly ComputePlatform platform;
        private readonly ComputeContextPropertyList properties;
        private ComputeContextNotifier callback;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a read-only collection of the <see cref="ComputeDevice"/>s of the <see cref="ComputeContext"/>.
        /// </summary>
        /// <value> A read-only collection of the <see cref="ComputeDevice"/>s of the <see cref="ComputeContext"/>. </value>
        public ReadOnlyCollection<ComputeDevice> Devices { get { return devices; } }

        /// <summary>
        /// Gets the <see cref="ComputePlatform"/> of the <see cref="ComputeContext"/>.
        /// </summary>
        /// <value> The <see cref="ComputePlatform"/> of the <see cref="ComputeContext"/>. </value>
        public ComputePlatform Platform { get { return platform; } }

        /// <summary>
        /// Gets a collection of <see cref="ComputeContextProperty"/>s of the <see cref="ComputeContext"/>.
        /// </summary>
        /// <value> A collection of <see cref="ComputeContextProperty"/>s of the <see cref="ComputeContext"/>. </value>
        public ComputeContextPropertyList Properties { get { return properties; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="ComputeContext"/> on a collection of <see cref="ComputeDevice"/>s.
        /// </summary>
        /// <param name="devices"> A collection of <see cref="ComputeDevice"/>s to associate with the <see cref="ComputeContext"/>. </param>
        /// <param name="properties"> A <see cref="ComputeContextPropertyList"/> of the <see cref="ComputeContext"/>. </param>
        /// <param name="notify"> A delegate instance that refers to a notification routine. This routine is a callback function that will be used by the OpenCL implementation to report information on errors that occur in the <see cref="ComputeContext"/>. The callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe and that the delegate instance doesn't get collected by the Garbage Collector until <see cref="ComputeContext"/> is disposed. If <paramref name="notify"/> is <c>null</c>, no callback function is registered. </param>
        /// <param name="notifyDataPtr"> Optional user data that will be passed to <paramref name="notify"/>. </param>
        public ComputeContext(ICollection<ComputeDevice> devices, ComputeContextPropertyList properties, ComputeContextNotifier notify, IntPtr notifyDataPtr)
        {
            unsafe
            {
                IntPtr[] deviceHandles = Tools.ExtractHandles(devices);
                IntPtr[] propertyArray = (properties != null) ? properties.ToIntPtrArray() : null;
                callback = notify;

                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateContext(propertyArray, devices.Count, deviceHandles, notify, notifyDataPtr, &error);
                ComputeException.ThrowOnError(error);

                this.properties = properties;
                ComputeContextProperty platformProperty = properties.GetByName(ComputeContextPropertyName.Platform);
                this.platform = ComputePlatform.GetByHandle(platformProperty.Value);
                this.devices = GetDevices();
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        /// <summary>
        /// Creates a new <see cref="ComputeContext"/> on all the <see cref="ComputeDevice"/>s that match the specified <see cref="ComputeDeviceTypes"/>.
        /// </summary>
        /// <param name="deviceType"> A bit-field that identifies the type of <see cref="ComputeDevice"/> to associate with the <see cref="ComputeContext"/>. </param>
        /// <param name="properties"> A <see cref="ComputeContextPropertyList"/> of the <see cref="ComputeContext"/>. </param>
        /// <param name="notify"> A delegate instance that refers to a notification routine. This routine is a callback function that will be used by the OpenCL implementation to report information on errors that occur in the <see cref="ComputeContext"/>. The callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe and that the delegate instance doesn't get collected by the Garbage Collector until <see cref="ComputeContext"/> is disposed. If <paramref name="notify"/> is <c>null</c>, no callback function is registered. </param>
        /// <param name="userDataPtr"> Optional user data that will be passed to <paramref name="notify"/>. </param>
        public ComputeContext(ComputeDeviceTypes deviceType, ComputeContextPropertyList properties, ComputeContextNotifier notify, IntPtr userDataPtr)
        {
            unsafe
            {
                IntPtr[] propertyArray = (properties != null) ? properties.ToIntPtrArray() : null;
                callback = notify;

                ComputeErrorCode error = ComputeErrorCode.Success;
                Handle = CL10.CreateContextFromType(propertyArray, deviceType, notify, userDataPtr, &error);
                ComputeException.ThrowOnError(error);

                this.properties = properties;
                ComputeContextProperty platformProperty = properties.GetByName(ComputeContextPropertyName.Platform);
                this.platform = ComputePlatform.GetByHandle(platformProperty.Value);
                this.devices = GetDevices();
            }

            Trace.WriteLine("Created " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the string representation of the <see cref="ComputeContext"/>.
        /// </summary>
        /// <returns> The string representation of the <see cref="ComputeContext"/>. </returns>
        public override string ToString()
        {
            return "ComputeContext" + base.ToString();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Releases the associated OpenCL object.
        /// </summary>
        /// <param name="manual"> Specifies the operation mode of this method. </param>
        protected override void Dispose(bool manual)
        {
            if (manual)
            {
                //free managed resources
            }

            // free native resources
            if (Handle != IntPtr.Zero)
            {
                Trace.WriteLine("Disposing " + this + " in Thread(" + Thread.CurrentThread.ManagedThreadId + ").");
                CL10.ReleaseContext(Handle);
                Handle = IntPtr.Zero;
            }
        }

        #endregion

        #region Private methods

        private ReadOnlyCollection<ComputeDevice> GetDevices()
        {
            unsafe
            {
                List<IntPtr> validDeviceHandles = new List<IntPtr>(GetArrayInfo<ComputeContextInfo, IntPtr>(ComputeContextInfo.Devices, CL10.GetContextInfo));
                List<ComputeDevice> validDevices = new List<ComputeDevice>();
                foreach (ComputePlatform platform in ComputePlatform.Platforms)
                {
                    foreach (ComputeDevice device in platform.Devices)
                        if (validDeviceHandles.Contains(device.Handle))
                            validDevices.Add(device);
                }
                return new ReadOnlyCollection<ComputeDevice>(validDevices);
            }
        }

        #endregion
    }

    /// <summary>
    /// A callback function that can be registered by the application to report information on errors that occur in the <see cref="ComputeContext"/>.
    /// </summary>
    /// <param name="errorInfo"> An error string. </param>
    /// <param name="clDataPtr"> A pointer to binary data that is returned by the OpenCL implementation that can be used to log additional information helpful in debugging the error.</param>
    /// <param name="clDataSize"> The size of the binary data that is returned by the OpenCL. </param>
    /// <param name="userDataPtr"> The pointer to the optional user data specified in <paramref name="userDataPtr"/> argument of <see cref="ComputeContext"/> constructor. </param>
    /// <remarks> This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. </remarks>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)] //Cdecl calling convention may cause problems on ATI Stream 2.2+
    public delegate void ComputeContextNotifier(String errorInfo, IntPtr clDataPtr, IntPtr clDataSize, IntPtr userDataPtr);
}