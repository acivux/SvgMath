using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>Golden SVG baselines for legacy <c>test/SvgMathTest</c> MathML samples (static fontdata).</summary>
	public sealed class MmlSampleGoldenTests : SvgMathTestBase
	{
		public static IEnumerable<object[]> SampleMmlFiles()
		{
			var dir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples");
			if (!Directory.Exists(dir))
				yield break;

			foreach (var name in Directory.GetFiles(dir, "*.mml").Select(Path.GetFileName).OrderBy(x => x, StringComparer.Ordinal))
				yield return new object[] { name };
		}

		[Theory]
		[MemberData(nameof(SampleMmlFiles))]
		public void MmlSample_MatchesGolden(string mmlFileName)
		{
			var baseName = Path.GetFileNameWithoutExtension(mmlFileName);
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			var mmlPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples", mmlFileName);
			var goldenPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples", "Golden", baseName + ".svg");
			Assert.True(File.Exists(goldenPath), "Missing golden file: " + goldenPath);
			var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: true);
			var svg = mml.MakeSvg();
			var actual = MmlGoldenTestUtil.SerializeSvg(svg);
			var expected = MmlGoldenTestUtil.NormalizeNewlines(File.ReadAllText(goldenPath));
			Assert.Equal(expected, actual);
		}
	}
}
