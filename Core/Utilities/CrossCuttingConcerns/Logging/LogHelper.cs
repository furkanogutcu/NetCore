using System.Collections.Generic;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;

namespace Core.Utilities.CrossCuttingConcerns.Logging
{
    public static class LogHelper
    {
        public static T GetLogDetail<T>(IInvocation invocation)
        where T : LogDetail, new()
        {
            T logDetail = new T
            {
                FullName = $"{invocation.TargetType.FullName}.{invocation.Method.Name}",
                Parameters = GetLogParameters(invocation)
            };

            return logDetail;
        }

        public static List<LogParameter> GetLogParameters(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            return logParameters;
        }
    }
}
