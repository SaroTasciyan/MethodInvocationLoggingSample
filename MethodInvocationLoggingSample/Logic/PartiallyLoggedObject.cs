using System;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    public class PartiallyLoggedObject : IPartiallyLoggedObject
    {
        [InvocationLogging]
        public int LoggedMethodWithDecoration(Guid id, string data)
        {
            NotLoggedMethodDespiteDecoration();

            return DateTime.Today.Day;
        }

        public void NotLoggedMethodWithoutDecoration()
        {
        }

        [InvocationLogging]
        public void NotLoggedMethodDespiteDecoration() // ReSharper disable once RedundantJumpStatement
        {
            return;
        }
    }
}