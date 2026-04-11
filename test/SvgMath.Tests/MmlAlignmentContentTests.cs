using System;
using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public class MmlAlignmentContentTests : SvgMathTestBase
	{
		[Fact]
		public void Malignmark_InCell_Ignored_MatchesCellWithoutMark()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var withMark = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mtable><mtr>
    <mtd><mi>x</mi><malignmark/></mtd>
  </mtr></mtable>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var plain = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mtable><mtr><mtd><mi>x</mi></mtd></mtr></mtable>
</math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(plain.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(withMark.MakeSvg()));
		}

		[Fact]
		public void Semantics_ApplyFirstChild_ThrowsInvalidOperationException()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var mml = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <semantics>
    <apply><csymbol cd=""arith1"">plus</csymbol><ci>x</ci><ci>y</ci></apply>
    <annotation encoding=""application/x-tex"">x+y</annotation>
  </semantics>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var ex = Assert.Throws<InvalidOperationException>(() => mml.MakeSvg());
			Assert.Contains("Content MathML", ex.Message, StringComparison.Ordinal);
		}
	}
}
