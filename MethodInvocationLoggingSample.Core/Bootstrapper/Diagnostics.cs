using System.Text;

using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;

namespace MethodInvocationLoggingSample.Core.Bootstrapper
{
    internal static class Diagnostics
    {
        public static string Diagnose(IWindsorContainer container)
        {
            IHandler[] potentiallyMisconfiguredHandlers = Diagnostics.GetPotentiallyMisconfigured(container);

            StringBuilder diagnosisReportBuilder = new StringBuilder();
            DependencyInspector diagnosisInspector = new DependencyInspector(diagnosisReportBuilder);

            bool isAnyPotentiallyMisconfiguredHandler = false;
            foreach (IHandler handler in potentiallyMisconfiguredHandlers)
            {
                isAnyPotentiallyMisconfiguredHandler = true;

                IExposeDependencyInfo potentiallyMisconfiguredHandler = (IExposeDependencyInfo)handler;
                potentiallyMisconfiguredHandler.ObtainDependencyDetails(diagnosisInspector);
            }

            return isAnyPotentiallyMisconfiguredHandler ? diagnosisReportBuilder.ToString() : null;
        }

        public static IHandler[] GetPotentiallyMisconfigured(IWindsorContainer container)
        {
            IDiagnosticsHost host = (IDiagnosticsHost)container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey);
            IPotentiallyMisconfiguredComponentsDiagnostic diagnostics = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            return diagnostics.Inspect();
        }
    }
}