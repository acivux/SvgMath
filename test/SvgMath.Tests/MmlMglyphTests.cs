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
			var glyph = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mglyph alt=""Glyph""/></mrow></math>"),
				TestMathConfigs.MathSize10StaticFonts);
			var text = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mtext>Glyph</mtext></mrow></math>"),
				TestMathConfigs.MathSize10StaticFonts);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(text.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(glyph.MakeSvg()));
		}

		[Fact]
		public void Mglyph_MakeLatex_EmitsTextFromAlt()
		{
			var mml = new Mml(
				XElement.Parse(
					@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mglyph alt=""Z""/></math>"),
				TestMathConfigs.MathSize10StaticFonts);
			Assert.Equal(@"\text{Z}", mml.MakeLatex());
		}
	}
}
