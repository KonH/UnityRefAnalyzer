using System.IO;
using NUnit.Framework;
using RefAnalyzer.Data;

namespace RefAnalyzer.Tests {
	[TestFixture]
	public class RefDataSaverTests {
		string _path = TestSettings.TempFilePath;

		[SetUp]
		public void SetUp() {
			if ( File.Exists(_path) ) {
				File.Delete(_path);
			}
		}

		[Test]
		public void IsDataSaved() {
			var contents = "{ \"key\": \"some JSON data\" }";
			var saver = new RefDataSaver(_path, contents);
			saver.Save();
			Assert.True(File.Exists(_path));
			Assert.AreEqual(contents, File.ReadAllText(_path));
		}

		[TearDown]
		public void TearDown() {
			if ( File.Exists(_path) ) {
				File.Delete(_path);
			}
		}
	}
}