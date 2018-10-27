using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefAnalyzer.Data;
using RefAnalyzer.Test.Helpers;
using DiagnosticVerifier = RefAnalyzer.Test.Verifiers.DiagnosticVerifier;

namespace RefAnalyzer.Test {
	[TestClass]
	public class UnitTest : DiagnosticVerifier {
		
		[TestMethod]
		public void NoDiagnosticsExpectedToShowUp() {
			var test = @"";
			VerifyCSharpDiagnostic(test);
		}

		[TestMethod]
		public void IsDiagnosticExpected() {
			var data = new RefData();
			data.AddScene("scene").AddNode(new RefNode("srcPath", "srcType", "srcProp", "tgPath", "TestClass", "TestMethod"));
			var test = @"
class TestClass {
	public void TestMethod() {
	}
}
			";
			var expected = new DiagnosticResult {
				Id = "RefAnalyzer",
				Message = "Method 'TestMethod' is referenced via Inspector.",
				Severity = DiagnosticSeverity.Warning,
				Locations = new[] { new DiagnosticResultLocation("Test0.cs", 3, 14) }
			};
			VerifyCSharpDiagnostic(new RefAnalyzer(data), test, expected);
		}

		protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() {
			return new RefAnalyzer();
		}
	}
}
