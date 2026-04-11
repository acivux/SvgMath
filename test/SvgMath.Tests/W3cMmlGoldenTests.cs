using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>Golden SVG baselines for W3C MathML 3 test suite inputs under <c>TestAssets/w3tests</c>.</summary>
	[Trait("Suite", "W3C")]
	public sealed class W3cMmlGoldenTests : SvgMathTestBase
	{
		public static IEnumerable<object[]> W3cMmlRelativePaths()
		{
			var root = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "w3tests");
			var goldenRoot = Path.Combine(root, "Golden");
			if (!Directory.Exists(goldenRoot))
				yield break;

			foreach (var relMml in Directory.EnumerateFiles(goldenRoot, "*.svg", SearchOption.AllDirectories)
				.Select(p => Path.ChangeExtension(Path.GetRelativePath(goldenRoot, p), ".mml"))
				.Where(W3cMmlTestPaths.IsPresentationCase)
				.OrderBy(r => r, StringComparer.Ordinal))
			{
				if (File.Exists(Path.Combine(root, relMml)))
					yield return new object[] { relMml };
			}
		}

		[Theory]
		[MemberData(nameof(W3cMmlRelativePaths))]
		public void W3cMml_MatchesGolden(string relativeMmlPath)
		{
			var w3Root = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "w3tests");
			var mmlPath = Path.Combine(w3Root, relativeMmlPath);
			var goldenPath = Path.Combine(w3Root, "Golden", Path.ChangeExtension(relativeMmlPath, ".svg"));
			Assert.True(File.Exists(goldenPath), "Missing golden file: " + goldenPath);
			var mml = new Mml(mmlPath, TestMathConfigs.MmlSamplesStaticFonts);
			var svg = mml.MakeSvg();
			var actual = MmlGoldenTestUtil.SerializeSvg(svg);
			var expected = MmlGoldenTestUtil.NormalizeNewlines(File.ReadAllText(goldenPath));
			Assert.Equal(expected, actual);
		}
	}
}
