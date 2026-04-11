using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public class MathConfigTests : SvgMathTestBase
	{
		[Fact]
		public void DoesNotShareOrMutate_GlobalDefaults()
		{
			var baselineMathSize = MathDefaults.globalDefaults["mathsize"];
			var path10 = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var path20 = TestOutputPaths.AssetPath("config_mathsize_20.xml");
			var c1 = new MathConfig(path10, loadFontsFromStaticFiles: true);
			var c2 = new MathConfig(path20, loadFontsFromStaticFiles: true);
			Assert.Equal("10pt", c1.Defaults["mathsize"]);
			Assert.Equal("20pt", c2.Defaults["mathsize"]);
			Assert.Equal(baselineMathSize, MathDefaults.globalDefaults["mathsize"]);
		}

		[Fact]
		public void Loads_WhenVerboseAttributeMissing_DefaultsToFalse()
		{
			var path = TestOutputPaths.AssetPath("config_no_verbose.xml");
			var c = new MathConfig(path, loadFontsFromStaticFiles: true);
			Assert.False(c.Verbose);
		}
	}
}
