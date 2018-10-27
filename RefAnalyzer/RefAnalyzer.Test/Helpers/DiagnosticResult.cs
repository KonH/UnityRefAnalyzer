using Microsoft.CodeAnalysis;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Test.Helpers {
	/// <summary>
	///     Location where the diagnostic appears, as determined by path, line number, and column number.
	/// </summary>
	public struct DiagnosticResultLocation {
		public string Path   { get; }
		public int    Line   { get; }
		public int    Column { get; }

		public DiagnosticResultLocation(string path, int line, int column) {
			Guard.IsValid(line, l => l >= -1, "line must be >= -1");
			Guard.IsValid(column, c => c >= -1, "column must be >= -1");

			Path   = path;
			Line   = line;
			Column = column;
		}
	}

	/// <summary>
	///     Struct that stores information about a Diagnostic appearing in a source
	/// </summary>
	public struct DiagnosticResult {
		public DiagnosticSeverity Severity { get; set; }
		public string             Id       { get; set; }
		public string             Message  { get; set; }

		public string Path   => Locations.Length > 0 ? Locations[0].Path : "";
		public int    Line   => Locations.Length > 0 ? Locations[0].Line : -1;
		public int    Column => Locations.Length > 0 ? Locations[0].Column : -1;

		public DiagnosticResultLocation[] Locations {
			get => _locations ?? (_locations = new DiagnosticResultLocation[] { });
			set => _locations = value;
		}

		DiagnosticResultLocation[] _locations;
	}
}