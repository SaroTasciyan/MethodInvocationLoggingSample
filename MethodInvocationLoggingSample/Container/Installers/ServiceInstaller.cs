using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using MethodInvocationLoggingSample.Core.Service;

namespace MethodInvocationLoggingSample.Container.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IService>().ImplementedBy<Service>().LifestylePerThread());
        }
    }
}