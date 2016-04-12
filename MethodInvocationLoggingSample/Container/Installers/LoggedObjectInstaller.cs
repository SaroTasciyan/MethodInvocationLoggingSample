using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using MethodInvocationLoggingSample.Core.Container.Interceptors;
using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Container.Installers
{
    public class LoggedObjectInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register
            (
                Classes.FromAssemblyInThisApplication()
                    .BasedOn<ILoggedObject>()
                    .WithService.DefaultInterfaces()
                    .Configure(x => x.Interceptors<InvocationLoggingInterceptor>())
                    .LifestyleTransient()
            );
        }
    }
}