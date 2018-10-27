using System.IO;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RefAnalyzer.Data;
using RefAnalyzer.Extensions;

namespace RefAnalyzer {
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class RefAnalyzer : DiagnosticAnalyzer {
		public const string DiagnosticId = "RefAnalyzer";

		// You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
		// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
		static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
		static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
		static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
		const string Category = "Unity";

		static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

		RefData  _data;
		RefCache _cache;

		public override void Initialize(AnalysisContext context) {
			context.RegisterCompilationStartAction(AnalyzeCompilation);
			context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
		}

		void AnalyzeCompilation(CompilationStartAnalysisContext context) {
			if ( context == null ) {
				throw new System.ArgumentNullException(nameof(context));
			}
			var solution = context.GetSolution();
			if ( solution != null ) {
				var filePath = solution.FilePath;
				var directoryPath = Path.GetDirectoryName(filePath);
				var jsonFilePath = Path.Combine(directoryPath, "refs.json");
				var refDataLoader = new RefDataLoader(jsonFilePath);
				var contents = refDataLoader.Load();
				var importer = new RefDataImporter(contents);
				_data = importer.Import();
				_cache = new RefCache(_data);
			}
		}
		
		void AnalyzeSymbol(SymbolAnalysisContext context) {
			if ( _data == null ) {
				return;
			}
			var methodSymbol = (IMethodSymbol)context.Symbol;
			var ownerType = methodSymbol.ContainingType;
			var refs = _cache.GetRefs(ownerType.Name, methodSymbol.Name);
			if ( refs != null ) {
				var diagnostic = Diagnostic.Create(Rule, methodSymbol.Locations[0], methodSymbol.Name);
				context.ReportDiagnostic(diagnostic);
			}
		}
	}
}
