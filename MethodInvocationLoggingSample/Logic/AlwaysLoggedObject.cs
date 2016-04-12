using System;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    [InvocationLogging]
    public class AlwaysLoggedObject : IAlwaysLoggedObject
    {
        public DateTime LoggedMethodWithoutDecoration(int id, string data)
        {
            return DateTime.Now;
        }
    }
}