using System.Reflection;
using System.Text;

using Castle.Core.Internal;
using Castle.Core.Logging;
using Castle.DynamicProxy;

using MethodInvocationLoggingSample.Core.Logging;

namespace MethodInvocationLoggingSample.Core.Container.Interceptors
{
    public class InvocationLoggingInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public InvocationLoggingInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!invocation.TargetType.HasAttribute<InvocationLoggingAttribute>() && !invocation.MethodInvocationTarget.HasAttribute<InvocationLoggingAttribute>())
            {
                invocation.Proceed();

                return;
            }

            LogInvocationEntry(invocation);

            invocation.Proceed();

            LogInvocationExit(invocation);
        }

        private void LogInvocationEntry(IInvocation invocation)
        {
            StringBuilder logMessageBuilder = new StringBuilder($"{invocation.Method}");

            if (invocation.Arguments.Length > 0)
            {
                logMessageBuilder.AppendLine("\n(");

                ParameterInfo[] parameters = invocation.Method.GetParameters();
                for (int i = 0; i < invocation.Arguments.Length; i++)
                {

                    logMessageBuilder.AppendLine($"{parameters[i].Name}: {invocation.Arguments[i]}");
                }

                logMessageBuilder.Append(")");
            }

            logger.Info(logMessageBuilder.ToString());
        }

        private void LogInvocationExit(IInvocation invocation)
        {
            if (invocation.Method.ReturnType != typeof (void))
            {
                logger.Info($"{invocation.Method} return value is: {invocation.ReturnValue ?? "null"}");
            }
            else
            {
                logger.Info($"{invocation.Method} has no return type");
            }
        }
    }
}