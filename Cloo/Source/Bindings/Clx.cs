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

namespace Cloo.Bindings
{
    using System;
    using System.Runtime.InteropServices;
    
    public class Clx
    {
        internal static class Delegates
        {
            internal unsafe delegate ComputeErrorCode clGetGLContextInfoKHR(
                IntPtr* properties,
                ComputeGLContextInfo param_name,
                IntPtr param_value_size,
                IntPtr param_value,
                IntPtr* param_value_size_ret );
        }

        private readonly Delegates.clGetGLContextInfoKHR clGetGLContextInfoKHR;

        public unsafe ComputeErrorCode GetGLContextInfoKHR(
                IntPtr* properties,
                ComputeGLContextInfo param_name,
                IntPtr param_value_size,
                IntPtr param_value,
                IntPtr* param_value_size_ret )
        {
            return clGetGLContextInfoKHR( properties, param_name, param_value_size, param_value, param_value_size_ret );
        }

        public Clx( ComputePlatform platform )
        {
            if( platform.Extensions.Contains( "cl_khr_gl_sharing" ) )
                clGetGLContextInfoKHR = ( Delegates.clGetGLContextInfoKHR )Marshal.GetDelegateForFunctionPointer(
                    CL10.GetExtensionFunctionAddress( "clGetGLContextInfoKHR" ),
                    typeof( Delegates.clGetGLContextInfoKHR ) );
            else
                clGetGLContextInfoKHR = null;
        }
    }
}