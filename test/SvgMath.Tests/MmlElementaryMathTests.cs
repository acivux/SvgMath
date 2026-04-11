using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public class MmlElementaryMathTests : SvgMathTestBase
	{
		[Fact]
		public void Mstack_TwoMsrows_MatchesEquivalentMtableSvg()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var stack = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mstack>
    <msrow><mn>1</mn><mn>2</mn></msrow>
    <msrow><mn>3</mn><mn>4</mn></msrow>
  </mstack>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var table = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mtable>
    <mtr><mtd><mn>1</mn></mtd><mtd><mn>2</mn></mtd></mtr>
    <mtr><mtd><mn>3</mn></mtd><mtd><mn>4</mn></mtd></mtr>
  </mtable>
</math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(table.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(stack.MakeSvg()));
		}

		[Fact]
		public void Mstack_Msgroup_RendersAsRowOfCells()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var stack = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mstack>
    <msgroup><mn>5</mn><mn>0</mn></msgroup>
  </mstack>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var table = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mtable><mtr><mtd><mn>5</mn></mtd><mtd><mn>0</mn></mtd></mtr></mtable>
</math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(table.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(stack.MakeSvg()));
		}

		[Fact]
		public void Mstack_Msline_ProducesNonEmptySvg()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var mml = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mstack>
    <msrow><mn>1</mn></msrow>
    <msline/>
    <msrow><mn>2</mn></msrow>
  </mstack>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var svg = mml.MakeSvg();
			Assert.NotNull(svg);
			Assert.Equal("svg", svg.Name.LocalName);
			var s = MmlGoldenTestUtil.SerializeSvg(svg);
			Assert.True(s.Contains("path") || s.Contains("line") || s.Contains("polyline"), "expected vector output for digits and rule");
		}

		[Fact]
		public void Mstack_MscarriesRow_MatchesEquivalentMtable()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var stack = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mstack>
    <mscarries><mscarry><mn>1</mn></mscarry><none/></mscarries>
    <msrow><mn>1</mn><mn>1</mn></msrow>
  </mstack>
</math>"),
				path,
				loadFontsFromStaticFiles: true);
			var table = new Mml(
				XElement.Parse(@"
<math xmlns=""http://www.w3.org/1998/Math/MathML"">
  <mtable>
    <mtr><mtd><mn>1</mn></mtd><mtd><none/></mtd></mtr>
    <mtr><mtd><mn>1</mn></mtd><mtd><mn>1</mn></mtd></mtr>
  </mtable>
</math>"),
				path,
				loadFontsFromStaticFiles: true);

			Assert.Equal(
				MmlGoldenTestUtil.SerializeSvg(table.MakeSvg()),
				MmlGoldenTestUtil.SerializeSvg(stack.MakeSvg()));
		}
	}
}
