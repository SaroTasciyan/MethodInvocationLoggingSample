using System;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    public interface IPartiallyLoggedObject : ILoggedObject
    {
        int LoggedMethodWithDecoration(Guid id, string data);
        void NotLoggedMethodWithoutDecoration();
        void NotLoggedMethodDespiteDecoration();
    }
}