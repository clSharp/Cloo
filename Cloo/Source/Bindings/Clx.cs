using System;
using System.Runtime.InteropServices;
namespace Cloo.Bindings
{
    public enum ComputeGLContextInfo: int
    {
    }

    public class Clx
    {
        public class Delegates
        {
            public unsafe delegate Int32 clGetGLContextInfoKHR(
                IntPtr* properties,
                ComputeGLContextInfo param_name,
                IntPtr param_value_size,
                IntPtr param_value,
                IntPtr* param_value_size_ret );
        }

        public readonly static Delegates.clGetGLContextInfoKHR GetGLContextInfoKHR;

        static Clx()
        {
            GetGLContextInfoKHR = ( Delegates.clGetGLContextInfoKHR )Marshal.GetDelegateForFunctionPointer(
                Imports.GetExtensionFunctionAddress( "clGetGLContextInfoKHR" ),
                typeof( Delegates.clGetGLContextInfoKHR ) );
        }
    }
}