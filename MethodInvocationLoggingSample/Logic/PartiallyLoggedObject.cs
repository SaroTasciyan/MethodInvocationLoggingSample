using System;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    public class PartiallyLoggedObject : IPartiallyLoggedObject
    {
        [InvocationLogging]
        public int LoggedMethodWithDecoration(Guid id, string data)
        {
            return DateTime.Today.Day;
        }

        public void NotLoggedMethodWithoutDecoration()
        {
        }
    }
}