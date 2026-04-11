using System.IO;

namespace SvgMath.Tests.Infrastructure
{
	/// <summary>Paths relative to the test assembly output directory (where TestAssets are copied).</summary>
	internal static class TestOutputPaths
	{
		public static string OutputDirectory =>
			Path.GetDirectoryName(typeof(TestOutputPaths).Assembly.Location);

		public static string AssetPath(string fileName) =>
			Path.Combine(OutputDirectory, "TestAssets", fileName);
	}
}
