#if GENERATE_GOLDENS
using System;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace SvgMath.Tests
{
	/// <summary>
	/// Regenerate baselines (run from repo root):
	/// <c>dotnet test -p:GenerateGoldens=true --filter WriteGoldenSvgFiles_ToSourceTree</c> (SVG),
	/// <c>dotnet test -p:GenerateGoldens=true --filter WriteGoldenLatexFiles_ToSourceTree</c> (LaTeX for <c>MmlSamples</c>),
	/// <c>dotnet test -p:GenerateGoldens=true --filter WriteW3cGoldenSvgFiles_ToSourceTree</c> (W3C <c>w3tests</c> SVG).
	/// </summary>
	public sealed class MmlGoldenFileGenerator : SvgMathTestBase
	{
		readonly ITestOutputHelper _output;

		public MmlGoldenFileGenerator(ITestOutputHelper output)
		{
			_output = output;
		}
		[Fact]
		public void WriteGoldenSvgFiles_ToSourceTree()
		{
			var goldenDir = GetRepoMmlSamplesGoldenDirectory();
			Directory.CreateDirectory(goldenDir);
			var mmlDir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples");
			foreach (var mmlPath in Directory.GetFiles(mmlDir, "*.mml").OrderBy(Path.GetFileName, StringComparer.Ordinal))
			{
				var name = Path.GetFileNameWithoutExtension(mmlPath);
				var mml = new Mml(mmlPath, TestMathConfigs.MmlSamplesStaticFonts);
				var svg = mml.MakeSvg();
				var text = MmlGoldenTestUtil.SerializeSvg(svg);
				File.WriteAllText(Path.Combine(goldenDir, name + ".svg"), text);
			}
		}

		[Fact]
		public void WriteW3cGoldenSvgFiles_ToSourceTree()
		{
			var goldenRoot = GetRepoW3cGoldenDirectory();
			var mmlRoot = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "w3tests");
			if (!Directory.Exists(mmlRoot))
				throw new DirectoryNotFoundException("Missing W3C inputs: " + mmlRoot);

			var ok = 0;
			var failed = 0;
			foreach (var mmlPath in Directory.EnumerateFiles(mmlRoot, "*.mml", SearchOption.AllDirectories).OrderBy(p => p, StringComparer.Ordinal))
			{
				var rel = Path.GetRelativePath(mmlRoot, mmlPath);
				if (!W3cMmlTestPaths.IsPresentationCase(rel))
					continue;
				var outPath = Path.Combine(goldenRoot, Path.ChangeExtension(rel, ".svg"));
				Directory.CreateDirectory(Path.GetDirectoryName(outPath));
				try
				{
					var mml = new Mml(mmlPath, TestMathConfigs.MmlSamplesStaticFonts);
					var svg = mml.MakeSvg();
					var text = MmlGoldenTestUtil.SerializeSvg(svg);
					File.WriteAllText(outPath, text);
					ok++;
				}
				catch (Exception ex)
				{
					failed++;
					_output?.WriteLine("Skip golden: " + rel + " — " + ex.Message);
				}
			}

			_output?.WriteLine($"W3C goldens written: {ok}, skipped (unsupported/error): {failed}");
		}

		[Fact]
		public void WriteGoldenLatexFiles_ToSourceTree()
		{
			var goldenDir = GetRepoMmlSamplesGoldenDirectory();
			Directory.CreateDirectory(goldenDir);
			var mmlDir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples");
			foreach (var mmlPath in Directory.GetFiles(mmlDir, "*.mml").OrderBy(Path.GetFileName, StringComparer.Ordinal))
			{
				var name = Path.GetFileNameWithoutExtension(mmlPath);
				var mml = new Mml(mmlPath, TestMathConfigs.MmlSamplesTtf);
				var tex = MmlGoldenTestUtil.NormalizeNewlines(mml.MakeLatex().TrimEnd());
				File.WriteAllText(Path.Combine(goldenDir, name + ".tex"), tex + "\n");
			}
		}

		static string FindTestProjectDirectory()
		{
			for (var dir = new DirectoryInfo(Directory.GetCurrentDirectory()); dir != null; dir = dir.Parent)
			{
				if (File.Exists(Path.Combine(dir.FullName, "SvgMath.Tests.csproj")))
					return dir.FullName;
			}

			throw new InvalidOperationException("Could not locate SvgMath.Tests.csproj above " + Directory.GetCurrentDirectory());
		}

		static string GetRepoMmlSamplesGoldenDirectory() =>
			Path.Combine(FindTestProjectDirectory(), "TestAssets", "MmlSamples", "Golden");

		static string GetRepoW3cGoldenDirectory() =>
			Path.Combine(FindTestProjectDirectory(), "TestAssets", "w3tests", "Golden");
	}
}
#endif
