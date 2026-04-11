using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>MathML 2/3/4 shared namespace, <c>math@version</c>, and MathML 4 passthrough attributes.</summary>
	public class MmlMathMlNamespaceTests : SvgMathTestBase
	{
		static readonly XNamespace MathNs = "http://www.w3.org/1998/Math/MathML";
		static readonly XNamespace ForeignNs = "http://example.com/svgmath-test-foreign";

		[Theory]
		[InlineData("2.0")]
		[InlineData("3.0")]
		[InlineData("4.0")]
		public void SamePresentation_WithDifferentMathVersion_YieldsIdenticalSvg(string version)
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var baseline = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var baselineSvg = MmlGoldenTestUtil.SerializeSvg(baseline.MakeSvg());

			var mml = new Mml(
				XElement.Parse($@"<math xmlns=""http://www.w3.org/1998/Math/MathML"" version=""{version}""><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var svg = MmlGoldenTestUtil.SerializeSvg(mml.MakeSvg());

			Assert.Equal(baselineSvg, svg);
		}

		[Fact]
		public void IntentAndArgOnToken_Passthrough_DoesNotBreakRendering()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var baseline = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var baselineSvg = MmlGoldenTestUtil.SerializeSvg(baseline.MakeSvg());

			var mml = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi intent=""identifier"">x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var svg = MmlGoldenTestUtil.SerializeSvg(mml.MakeSvg());

			Assert.Equal(baselineSvg, svg);
		}

		[Fact]
		public void ForeignNamespaceChild_IsIgnored_DoesNotThrow()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var math = new XElement(MathNs + "math",
				new XElement(MathNs + "mi", "x"),
				new XElement(ForeignNs + "extra", "should-not-appear"));

			var mml = new Mml(math, path, loadFontsFromStaticFiles: true);
			var baseline = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(baseline.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(mml.MakeSvg()));
		}

		[Fact]
		public void LegacyNoNamespaceElements_AreStillParsed()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var mml = new Mml(
				XElement.Parse(@"<math><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var svg = mml.MakeSvg();
			Assert.NotNull(svg);
			Assert.Equal("svg", svg.Name.LocalName);
		}
	}
}
