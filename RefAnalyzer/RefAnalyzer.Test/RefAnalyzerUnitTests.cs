using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper;

namespace RefAnalyzer.Test {
	[TestClass]
	public class UnitTest : DiagnosticVerifier {
		
		[TestMethod]
		public void NoDiagnosticsExpectedToShowUp() {
			var test = @"";
			VerifyCSharpDiagnostic(test);
		}

		protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() {
			return new RefAnalyzer();
		}
	}
}
