using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Extensions
{
    public static class AnalysisContextExtensions
    {
        public static Solution GetSolution(this CompilationStartAnalysisContext context)
        {
            Guard.NotNull(context);
            return context.Options.GetPrivateFieldValue<Solution>();
        }

        public static T GetPrivateFieldValue<T>(this object obj)
        {
            Guard.NotNull(obj);

            foreach (var pi in obj.GetType().GetRuntimeFields())
            {
                if (pi.FieldType == typeof(T))
                {
                    return (T) pi.GetValue(obj);
                }
            }

            return default(T);
        }
    }
}