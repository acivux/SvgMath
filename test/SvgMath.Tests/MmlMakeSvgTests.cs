using System;
using System.Xml.Linq;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	/// <summary>End-to-end tests for <see cref="Mml.MakeSvg"/> (layout, errors, font resolution).</summary>
	public class MmlMakeSvgTests : SvgMathTestBase
	{
		[Fact]
		public void UsesFallbackFont_WhenPrimaryVariantFamiliesHaveNoMetrics()
		{
			var path = TestOutputPaths.AssetPath("config_primary_ghost_fallback_arial.xml");
			var mml = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><mi>x</mi></math>"),
				path,
				loadFontsFromStaticFiles: true);
			var svg = mml.MakeSvg();
			Assert.NotNull(svg);
			Assert.Equal("svg", svg.Name.LocalName);
		}

		[Fact]
		public void MAction_InvalidSelection_ThrowsInvalidOperationException()
		{
			var mml = new Mml(
				XElement.Parse(@"<math xmlns=""http://www.w3.org/1998/Math/MathML""><maction selection=""not-an-int""><mn>1</mn></maction></math>"),
				TestMathConfigs.MathSize10StaticFonts);
			var ex = Assert.Throws<InvalidOperationException>(() => mml.MakeSvg());
			Assert.Contains("selection", ex.Message, StringComparison.OrdinalIgnoreCase);
		}

		[Fact]
		public void Throws_WhenRootElementIsNotMath()
		{
			var mml = new Mml(
				XElement.Parse(@"<mrow xmlns=""http://www.w3.org/1998/Math/MathML""><mi>x</mi></mrow>"),
				TestMathConfigs.MathSize10StaticFonts);
			Assert.Throws<InvalidOperationException>(() => mml.MakeSvg());
		}
	}
}
