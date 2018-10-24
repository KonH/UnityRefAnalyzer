using System;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RefAnalyzer.Extensions {
	public static class AnalysisContextExtensions {
		public static Solution GetSolution(this CompilationStartAnalysisContext context) {
			if ( context == null ) {
				throw new ArgumentNullException(nameof(context));
			}
			return context.Options.GetPrivateFieldValue<Solution>();
		}

		public static T GetPrivateFieldValue<T>(this object obj) {
			if ( obj == null ) {
				throw new ArgumentNullException(nameof(obj));
			}

			foreach ( var pi in obj.GetType().GetRuntimeFields() ) {
				if ( pi.FieldType == typeof(T) ) {
					return (T)pi.GetValue(obj);
				}
			}
			return default(T);
		}
	}

}
