using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary><c>semantics</c> unwraps to the first expression child; annotations are ignored for layout.</summary>
	public class MmlSemanticsTests : SvgMathTestBase
	{
		[Fact]
		public void Semantics_WithPresentationFirstChild_MatchesUnwrappedSvg()
		{
			var withSemantics = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <semantics>
    <mrow><mi>x</mi></mrow>
    <annotation encoding=""application/x-tex"">x</annotation>
  </semantics>
</math>"),
				TestMathConfigs.MathSize10StaticFonts);
			var plain = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mi>x</mi></mrow></math>"),
				TestMathConfigs.MathSize10StaticFonts);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(plain.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(withSemantics.MakeSvg()));
		}

		[Fact]
		public void Semantics_MakeLatex_MatchesUnwrapped()
		{
			var withSemantics = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <semantics>
    <mi>y</mi>
    <annotation>note</annotation>
  </semantics>
</math>"),
				TestMathConfigs.MathSize10StaticFonts);
			var plain = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>y</mi></math>"),
				TestMathConfigs.MathSize10StaticFonts);

			Assert.Equal(plain.MakeLatex(), withSemantics.MakeLatex());
		}
	}
}
