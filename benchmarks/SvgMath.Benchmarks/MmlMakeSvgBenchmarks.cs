using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using BenchmarkDotNet.Attributes;
using SvgMath;

namespace SvgMath.Benchmarks
{
	/// <summary>
	/// Benchmarks <see cref="Mml.MakeSvg"/> (and optional SVG serialization like golden tests).
	/// Requires cwd = output directory (see <see cref="GlobalSetup"/>).
	/// </summary>
	[MemoryDiagnoser]
	public class MmlMakeSvgBenchmarks
	{
		string _configPath;
		string _small;
		string _large;
		string _entityHeavy;
		string[] _batch10;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var dir = AppContext.BaseDirectory;
			Directory.SetCurrentDirectory(dir);
			_configPath = Path.Combine(dir, "TestAssets", "config_mml_samples.xml");
			var f = Path.Combine(dir, "Fixtures");
			_small = Path.Combine(f, "test1.mml");
			_large = Path.Combine(f, "complex1.mml");
			_entityHeavy = Path.Combine(f, "EntityNames_a.mml");
			_batch10 = new[]
			{
				Path.Combine(f, "test1.mml"),
				Path.Combine(f, "complex1.mml"),
				Path.Combine(f, "EntityNames_a.mml"),
				Path.Combine(f, "mfrac2.mml"),
				Path.Combine(f, "mi2.mml"),
				Path.Combine(f, "mtable2.mml"),
				Path.Combine(f, "mover1.mml"),
				Path.Combine(f, "white9.mml"),
				Path.Combine(f, "mrow1.mml"),
				Path.Combine(f, "mo6.mml"),
			};
		}

		[Benchmark(Description = "MmlSamples/test1.mml")]
		public void SmallSample_MakeSvg()
		{
			var mml = new Mml(_small, _configPath, loadFontsFromStaticFiles: true);
			mml.MakeSvg();
		}

		[Benchmark(Description = "TortureTests/Complexity/complex1.mml")]
		public void LargeTorture_MakeSvg()
		{
			var mml = new Mml(_large, _configPath, loadFontsFromStaticFiles: true);
			mml.MakeSvg();
		}

		[Benchmark(Description = "Characters/EntityNames/a.mml")]
		public void EntityNames_MakeSvg()
		{
			var mml = new Mml(_entityHeavy, _configPath, loadFontsFromStaticFiles: true);
			mml.MakeSvg();
		}

		[Benchmark(Description = "Golden-test parity: MakeSvg + XML serialize (test1)")]
		public void SmallSample_MakeSvg_And_SerializeLikeGoldens()
		{
			var mml = new Mml(_small, _configPath, loadFontsFromStaticFiles: true);
			var svg = mml.MakeSvg();
			SerializeSvg(svg);
		}

		[Benchmark(Description = "10 fixed W3C presentation fixtures, MakeSvg each")]
		public void W3C_Batch10_MakeSvg()
		{
			for (var i = 0; i < _batch10.Length; i++)
			{
				var mml = new Mml(_batch10[i], _configPath, loadFontsFromStaticFiles: true);
				mml.MakeSvg();
			}
		}

		static string SerializeSvg(XElement root)
		{
			var doc = new XDocument(root);
			var settings = new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true };
			using (var sw = new StringWriter())
			{
				using (var xw = XmlWriter.Create(sw, settings))
					doc.Save(xw);
				return sw.ToString();
			}
		}
	}
}
