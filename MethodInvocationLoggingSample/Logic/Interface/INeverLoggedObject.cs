using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Logic
{
    public interface INeverLoggedObject : ILoggedObject
    {
        void NotLoggedMethod();
    }
}