using Topshelf;

using MethodInvocationLoggingSample.Core.Service;

namespace MethodInvocationLoggingSample.Bootstrapper
{
    using Core.Bootstrapper;

    internal class ApplicationBootstrapper : Bootstrapper
    {
        private const string Description = "Does nothing besides logging called methods providing enter (argument) and exit (return value) information";
        private const string DisplayName = "Method Invocation Logging Sample Service";
        private const string ServiceName = "MethodInvocationLoggingSampleService";

        public ApplicationBootstrapper() : base(DiagnosticsOptions.Output & DiagnosticsOptions.Throw) { }

        public override void Bootstrap()
        {
            base.Bootstrap();

            Host host = HostFactory.New(configurator =>
            {
                configurator.Service<IService>(service =>
                {
                    service.ConstructUsing(_ => Resolve<IService>());
                    service.WhenStarted(x => x.Start());
                    service.WhenStopped(x =>
                    {
                        x.Stop();

                        Release(x);
                        DisposeContainer();
                    });
                });
                configurator.RunAsLocalSystem();

                configurator.SetDescription(Description);
                configurator.SetDisplayName(DisplayName);
                configurator.SetServiceName(ServiceName);
            });

            host.Run();
        }
    }
}