using Castle.MicroKernel.Registration;
using Castle.Windsor;

using MethodInvocationLoggingSample.Core.Configuration;

using Windsor = Castle.MicroKernel.SubSystems.Configuration;

namespace MethodInvocationLoggingSample.Core.Container.Installers
{
    public class ConfigurationStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Windsor.IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationStore>().ImplementedBy<ConfigurationStore>().LifestyleSingleton());
        }
    }
}