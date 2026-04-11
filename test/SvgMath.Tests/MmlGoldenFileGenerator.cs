#if GENERATE_GOLDENS
using System;
using System.IO;
using System.Linq;
using SvgMath;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>
	/// Regenerate baselines (run from repo root):
	/// <c>dotnet test -p:GenerateGoldens=true --filter WriteGoldenSvgFiles_ToSourceTree</c> (SVG),
	/// <c>dotnet test -p:GenerateGoldens=true --filter WriteGoldenLatexFiles_ToSourceTree</c> (LaTeX for <c>MmlSamples</c>).
	/// </summary>
	public sealed class MmlGoldenFileGenerator : SvgMathTestBase
	{
		[Fact]
		public void WriteGoldenSvgFiles_ToSourceTree()
		{
			var goldenDir = GetRepoGoldenDirectory();
			Directory.CreateDirectory(goldenDir);
			var mmlDir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples");
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			foreach (var mmlPath in Directory.GetFiles(mmlDir, "*.mml").OrderBy(Path.GetFileName, StringComparer.Ordinal))
			{
				var name = Path.GetFileNameWithoutExtension(mmlPath);
				var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: true);
				var svg = mml.MakeSvg();
				var text = MmlGoldenTestUtil.SerializeSvg(svg);
				File.WriteAllText(Path.Combine(goldenDir, name + ".svg"), text);
			}
		}

		[Fact]
		public void WriteGoldenLatexFiles_ToSourceTree()
		{
			var goldenDir = GetRepoGoldenDirectory();
			Directory.CreateDirectory(goldenDir);
			var mmlDir = Path.Combine(TestOutputPaths.OutputDirectory, "TestAssets", "MmlSamples");
			var configPath = TestOutputPaths.AssetPath("config_mml_samples.xml");
			foreach (var mmlPath in Directory.GetFiles(mmlDir, "*.mml").OrderBy(Path.GetFileName, StringComparer.Ordinal))
			{
				var name = Path.GetFileNameWithoutExtension(mmlPath);
				var mml = new Mml(mmlPath, configPath, loadFontsFromStaticFiles: false);
				var tex = MmlGoldenTestUtil.NormalizeNewlines(mml.MakeLatex().TrimEnd());
				File.WriteAllText(Path.Combine(goldenDir, name + ".tex"), tex + "\n");
			}
		}

		static string GetRepoGoldenDirectory()
		{
			for (var dir = new DirectoryInfo(Directory.GetCurrentDirectory()); dir != null; dir = dir.Parent)
			{
				if (File.Exists(Path.Combine(dir.FullName, "SvgMath.Tests.csproj")))
					return Path.Combine(dir.FullName, "TestAssets", "MmlSamples", "Golden");
			}

			throw new InvalidOperationException("Could not locate SvgMath.Tests.csproj above " + Directory.GetCurrentDirectory());
		}
	}
}
#endif
