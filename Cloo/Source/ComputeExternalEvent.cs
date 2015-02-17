using Cloo.Bindings;

namespace Cloo
{
    /// <summary>
    /// Represents an event created by an external library
    /// </summary>
    public class ComputeExternalEvent : ComputeEvent
    {
        public ComputeExternalEvent(CLEventHandle handle, ComputeCommandQueue queue)
            : base(handle, queue)
        {
        }
    }
}