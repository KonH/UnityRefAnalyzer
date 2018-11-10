using System;

namespace RefAnalyzer.Validation {
	public static class Guard {
		public static void NotNull<T>(T value) {
			if ( value == null ) {
				throw new ArgumentNullException();
			}
		}

		public static void NotNullOrEmpty(string value) {
			NotNull(value);
			NotEmpty(value);
		}

		public static void NotEmpty(string value) {
			if ( string.Empty.Equals(value) ) {
				throw new ArgumentException("Parameter cannot be empty.");
			}
		}

		public static void NotNullOrWhiteSpace(string value) {
			NotNull(value);

			if ( string.IsNullOrWhiteSpace(value) ) {
				throw new ArgumentException("Parameter cannot be white space.");
			}
		}

		public static void IsValid<T>(T value, Func<T, bool> validate, string message) {
			if ( !validate(value) ) {
				throw new ArgumentException(message);
			}
		}
	}
}