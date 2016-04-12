using System;

using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using MethodInvocationLoggingSample.Core.Configuration;

namespace MethodInvocationLoggingSample.Core.Bootstrapper
{
    public abstract class Bootstrapper
    {
        #region DiagnosticsOptions

        [Flags]
        protected enum DiagnosticsOptions : byte
        {
            Output = 0x01,
            Log = 0x02,
            Throw = 0x04
        }

        #endregion DiagnosticsOptions

        private const string WindsorInstallerPathKey = "WindsorInstallerPath";

        private readonly IWindsorContainer container;
        protected DiagnosticsOptions? Options { get; }

        protected Bootstrapper(DiagnosticsOptions? options = null)
        {
            container = new WindsorContainer();
            Options = options;
        }

        public virtual void Bootstrap()
        {
            IConfigurationStore configurationStore = new ConfigurationStore();
            string installerPath;
            bool isInstallerPathSpecified = configurationStore.TryGet(WindsorInstallerPathKey, out installerPath);

            IWindsorInstaller windsorInstaller = FromAssembly.InDirectory(new AssemblyFilter(isInstallerPathSpecified ? installerPath : String.Empty));

            container.Install(windsorInstaller);

            if (Options.HasValue)
            {
                string diagnosisReport = Diagnostics.Diagnose(container);

                if (!diagnosisReport.IsNullOrEmpty())
                { HandleDiagnosisReport(diagnosisReport); }
            }
        }

        protected T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        protected void Release<T>(T @object)
        {
            container.Release(@object);
        }

        protected void DisposeContainer()
        {
            container.Dispose();
        }

        private void HandleDiagnosisReport(string diagnosisReport)
        {
            // ReSharper disable once PossibleInvalidOperationException
            DiagnosticsOptions options = Options.Value;
            string output = $"Potentially misconfigured dependencies detected.\n{diagnosisReport}";

            if (options.HasFlag(DiagnosticsOptions.Output))
            {
                Console.WriteLine(output);
            }

            if (options.HasFlag(DiagnosticsOptions.Log))
            {
                //TODO #3 Bootsrapper.HandleDiagnosisReport() option: add logging support. ILogger implementation will be resolved, hence lifycyle management is required
            }

            if (options.HasFlag(DiagnosticsOptions.Throw))
            {
                throw new Exception(output);
            }
        }
    }
}