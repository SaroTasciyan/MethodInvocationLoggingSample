using MethodInvocationLoggingSample.Bootstrapper;

namespace MethodInvocationLoggingSample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            new ApplicationBootstrapper().Bootstrap();
        }
    }
}
