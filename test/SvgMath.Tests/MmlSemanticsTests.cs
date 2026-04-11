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
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var withSemantics = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <semantics>
    <mrow><mi>x</mi></mrow>
    <annotation encoding=""application/x-tex"">x</annotation>
  </semantics>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var plain = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mrow><mi>x</mi></mrow></math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(plain.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(withSemantics.MakeSvg()));
		}

		[Fact]
		public void Semantics_MakeLatex_MatchesUnwrapped()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var withSemantics = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <semantics>
    <mi>y</mi>
    <annotation>note</annotation>
  </semantics>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var plain = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>y</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(plain.MakeLatex(), withSemantics.MakeLatex());
		}
	}
}
