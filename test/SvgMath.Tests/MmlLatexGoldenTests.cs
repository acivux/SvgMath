using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public sealed class MmlLatexGoldenTests : SvgMathTestBase
	{
		public static IEnumerable<object[]> LatexSampleMmlFiles()
		{
			var dir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlLatex");
			if (!Directory.Exists(dir))
				yield break;

			foreach (var name in Directory.GetFiles(dir, "*.mml").Select(Path.GetFileName).OrderBy(x => x, StringComparer.Ordinal))
				yield return new object[] { name };
		}

		[Theory]
		[MemberData(nameof(LatexSampleMmlFiles))]
		public void MmlLatex_MatchesGolden(string mmlFileName)
		{
			var baseName = Path.GetFileNameWithoutExtension(mmlFileName);
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			var mmlPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlLatex", mmlFileName);
			var goldenPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlLatex", "Golden", baseName + ".tex");
			Assert.True(File.Exists(goldenPath), "Missing golden file: " + goldenPath);
			var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: false);
			var actual = MmlGoldenTestUtil.NormalizeNewlines(mml.MakeLatex());
			var expected = MmlGoldenTestUtil.NormalizeNewlines(File.ReadAllText(goldenPath).TrimEnd());
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void MakeLatex_WrapInDelimiters_Inline()
		{
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			var mmlPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlLatex", "binomial.mml");
			var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: false);
			var s = mml.MakeLatex(new MathLatexOptions { WrapInDelimiters = true, DisplayMode = false });
			Assert.Equal("$\\binom{n}{k}$", s);
		}

		[Fact]
		public void MakeLatex_WrapInDelimiters_Display()
		{
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			var mmlPath = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlLatex", "binomial.mml");
			var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: false);
			var s = mml.MakeLatex(new MathLatexOptions { WrapInDelimiters = true, DisplayMode = true });
			Assert.Equal("\\[\\binom{n}{k}\\]", s);
		}
	}
}
