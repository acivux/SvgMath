using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>LaTeX goldens for the same MathML fixtures as <see cref="MmlSampleGoldenTests"/> (<c>MmlSamples</c>).</summary>
	public sealed class MmlSampleLatexGoldenTests : SvgMathTestBase
	{
		public static IEnumerable<object[]> SampleMmlFiles() => MmlSampleGoldenTests.SampleMmlFiles();

		[Theory]
		[MemberData(nameof(SampleMmlFiles))]
		public void MmlSampleLatex_MatchesGolden(string mmlFileName)
		{
			var baseName = Path.GetFileNameWithoutExtension(mmlFileName);
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			var mmlPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples", mmlFileName);
			var goldenPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples", "Golden", baseName + ".tex");
			Assert.True(File.Exists(goldenPath), "Missing golden file: " + goldenPath);
			var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: false);
			var actual = MmlGoldenTestUtil.NormalizeNewlines(mml.MakeLatex().TrimEnd());
			var expected = MmlGoldenTestUtil.NormalizeNewlines(File.ReadAllText(goldenPath).TrimEnd());
			Assert.Equal(expected, actual);
		}
	}
}
