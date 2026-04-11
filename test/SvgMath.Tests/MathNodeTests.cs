using System;
using System.Collections.Generic;
using SvgMath.Tests.Infrastructure;
using Xunit;

namespace SvgMath.Tests
{
	public class MathNodeTests : SvgMathTestBase
	{
		[Fact]
		public void Mi_AppliesItalicDefault_ToSupplementaryPlaneIdentifier()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var cfg = new MathConfig(path, loadFontsFromStaticFiles: true);
			var math = new MathNode("math", new Dictionary<string, string>(), cfg, null);
			var mi = new MathNode("mi", new Dictionary<string, string>(), cfg, math);
			mi.Text = "\uD835\uDC00";
			mi.MakeContext();
			Assert.Equal("italic", mi.FontStyle);
		}

		[Fact]
		public void GetListProperty_ReturnsEmpty_WhenPropertyMissing()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var cfg = new MathConfig(path, loadFontsFromStaticFiles: true);
			var math = new MathNode("math", new Dictionary<string, string>(), cfg, null);
			var node = new MathNode("menclose", new Dictionary<string, string>(), cfg, math);
			node.NodeDefaults = new Dictionary<string, string> { { "mathsize", "12pt" } };
			var list = node.GetListProperty("nonexistentlistattr");
			Assert.NotNull(list);
			Assert.Empty(list);
		}

		[Fact]
		public void ParseSpace_Throws_ForUnknownMathspaceToken()
		{
			var path = TestOutputPaths.AssetPath("config_mathsize_10.xml");
			var cfg = new MathConfig(path, loadFontsFromStaticFiles: true);
			var math = new MathNode("math", new Dictionary<string, string>(), cfg, null);
			var node = new MathNode("mo", new Dictionary<string, string>(), cfg, math);
			node.NodeDefaults = new Dictionary<string, string>(cfg.Defaults);
			Assert.Throws<InvalidOperationException>(() => node.ParseSpace("unknownmathspace"));
		}
	}
}
