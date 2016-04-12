using System;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    public interface IAlwaysLoggedObject : ILoggedObject
    {
        DateTime LoggedMethodWithoutDecoration(int id, string data);
    }
}