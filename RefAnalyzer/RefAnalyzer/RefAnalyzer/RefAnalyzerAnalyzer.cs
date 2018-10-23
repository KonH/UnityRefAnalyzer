using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RefAnalyzer {
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class RefAnalyzerAnalyzer : DiagnosticAnalyzer {
		public const string DiagnosticId = "RefAnalyzer";

		// You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
		// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
		private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
		private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
		private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
		private const string Category = "Naming";

		private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

		public override void Initialize(AnalysisContext context) {
			// TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
			// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
			context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
		}

		static string ClassName = "TestComponent";
		static string MethodName = "OnClick";

		private static void AnalyzeSymbol(SymbolAnalysisContext context) {
			// TODO: Replace the following code with your own analysis, generating Diagnostic objects for any issues you find
			var methodSymbol = (IMethodSymbol)context.Symbol;

			var ownerType = methodSymbol.ContainingType;
			//if ( !ownerType.AllInterfaces.Any(type => type.Name == "Component") ) {
			//	return;
			//}
			// Find just those named type symbols with names containing lowercase letters.
			if ( (ownerType.Name == ClassName) && (methodSymbol.Name == MethodName) ) {
				// For all such symbols, produce a diagnostic.
				var diagnostic = Diagnostic.Create(Rule, methodSymbol.Locations[0], methodSymbol.Name);

				context.ReportDiagnostic(diagnostic);
			}
		}
	}
}
