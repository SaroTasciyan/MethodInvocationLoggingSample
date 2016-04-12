using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using MethodInvocationLoggingSample.Core.Container.Interceptors;

namespace MethodInvocationLoggingSample.Core.Container.Installers
{
    public class InterceptorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<InvocationLoggingInterceptor>());
        }
    }
}