using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public class MmlMglyphTests : SvgMathTestBase
	{
		[Fact]
		public void Mglyph_AltText_RendersSameAsMtext()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var glyph = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mglyph alt=""Glyph""/></mrow></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var text = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mtext>Glyph</mtext></mrow></math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(text.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(glyph.MakeSvg()));
		}

		[Fact]
		public void Mglyph_MakeLatex_EmitsTextFromAlt()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var mml = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mglyph alt=""Z""/></math>"),
				path,
				loadFontsFromStaticFiles: true);
			Assert.Equal(@"\text{Z}", mml.MakeLatex());
		}
	}
}
