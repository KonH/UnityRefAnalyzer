using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Test.Validation {
	[TestClass]
	public class GuardTests {
		[TestMethod]
		public void CheckingNullStringWithGuardShouldThrowException() {
			// Given
			string input = null;

			// When
			// Then
			Assert.ThrowsException<ArgumentNullException>(() => { Guard.NotNull(input); });
		}

		[TestMethod]
		public void CheckingCorrectStringWithNullGuardShouldNotThrowException() {
			// Given
			const string exampleInput = "Example input";

			// When
			// Then
			Guard.NotNull(exampleInput);
		}

		[TestMethod]
		public void CheckingCorrectStringWithEmptyGuardShouldNotThrowException() {
			// Given
			const string exampleInput = "Example input";

			// When
			// Then
			Guard.NotEmpty(exampleInput);
		}

		[TestMethod]
		public void CheckingCorrectStringWithNotNullOrWhiteSpaceGuardShouldNotThrowException() {
			// Given
			const string exampleInput = "Example input";

			// When
			// Then
			Guard.NotNullOrWhiteSpace(exampleInput);
		}

		[TestMethod]
		public void CheckingEmptyStringWithGuardShouldThrowException() {
			// Given
			var input = string.Empty;

			// When
			// Then
			Assert.ThrowsException<ArgumentException>(() => { Guard.NotEmpty(input); });
		}

		[TestMethod]
		public void CheckingWhiteSpaceStringWithGuardShouldThrowException() {
			// Given
			const string input = "\t\t\t\t\t\t          \n\n\n\t";

			// When
			// Then
			Assert.ThrowsException<ArgumentException>(() => { Guard.NotNullOrWhiteSpace(input); });
		}

		[TestMethod]
		public void CheckingIncorrectValueWithIsValidGuardShouldThrowException() {
			// Given
			const int input = -1;

			// When
			// Then
			Assert.ThrowsException<ArgumentException>(() => {
				Guard.IsValid(
					input, i => i > 0, "Input has to be greater than 0.");
			});
		}

		[TestMethod]
		public void CheckingCorrectValueWithIsValidGuardShouldNotThrowException() {
			// Given
			const int input = 10;

			// When
			// Then
			Guard.IsValid(input, i => i > 0, "Input has to be greater than 0.");
		}
	}
}