using System.IO;
using System.Linq;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using RefAnalyzer.Data;

namespace RefAnalyzer {
	[DiagnosticAnalyzer(LanguageNames.CSharp)]
	public class RefAnalyzer : DiagnosticAnalyzer {
		const string DiagnosticId = "RefAnalyzer";
		const string Category     = "Unity";

		// You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
		// See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
		static readonly LocalizableString    Title         = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
		static readonly LocalizableString    MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
		static readonly DiagnosticDescriptor Rule          = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true);

		public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

		RefData  _data;
		RefCache _cache;

		public RefAnalyzer() {}

		public RefAnalyzer(RefData data) {
			_data = data;
			_cache = new RefCache(_data);
		}

		public override void Initialize(AnalysisContext context) {
			context.RegisterSyntaxTreeAction(AnalyzeTree);
			context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
		}

		void AnalyzeTree(SyntaxTreeAnalysisContext context) {
			if ( _data != null ) {
				// skip loading, if already have data
				return;
			}
			var filePath = context.Tree.FilePath;
			if ( string.IsNullOrWhiteSpace(filePath) ) {
				return;
			}
			var curDir = Path.GetDirectoryName(filePath);
			while ( true ) {
				var expectedPath = Path.Combine(curDir, "refs.json");
				if ( TryLoadDataFrom(expectedPath) ) {
					return;
				}
				if ( string.IsNullOrWhiteSpace(curDir) ) {
					return;
				}
				var parentDir = Directory.GetParent(curDir);
				if ( parentDir != null ) {
					curDir = parentDir.FullName;
				} else {
					return;
				}
			}
		}

		bool TryLoadDataFrom(string jsonFilePath) {
			if ( !File.Exists(jsonFilePath) ) {
				return false;
			}
			var refDataLoader = new RefDataLoader(jsonFilePath);
			var contents      = refDataLoader.Load();
			var importer      = new RefDataImporter(contents);
			_data  = importer.Import();
			_cache = new RefCache(_data);
			return true;
		}
		
		void AnalyzeSymbol(SymbolAnalysisContext context) {
			if ( _data == null ) {
				return;
			}
			var methodSymbol = (IMethodSymbol)context.Symbol;
			var ownerType = methodSymbol.ContainingType;
			var refs = _cache.GetRefs(ownerType.Name, methodSymbol.Name);
			if ( refs != null ) {
				var pathes = string.Join(
					",",
					refs.Select(
						r => $"{r.ScenePath} ({r.SourcePath}.{r.SourceProperty} => {r.TargetPath})"
					)
				);
				var desc = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources), pathes);
				var concreteDescriptor = new DiagnosticDescriptor(
					Rule.Id, Rule.Title, Rule.MessageFormat, Rule.Category, Rule.DefaultSeverity, isEnabledByDefault: Rule.IsEnabledByDefault,
					description: desc
				);
				var diagnostic = Diagnostic.Create(concreteDescriptor, methodSymbol.Locations[0], methodSymbol.Name);
				context.ReportDiagnostic(diagnostic);
			}
		}
	}
}
