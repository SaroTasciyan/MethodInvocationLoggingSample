using System;

namespace MethodInvocationLoggingSample.Core.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)] // TODO # Properties ?
    public class InvocationLoggingAttribute : Attribute
    {
    }
}