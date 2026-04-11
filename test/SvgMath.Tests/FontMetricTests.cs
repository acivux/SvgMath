using System;
using System.Xml.Linq;
using Xunit;

namespace SvgMath.Tests
{
	public class FontMetricTests : SvgMathTestBase
	{
		[Fact]
		public void ThrowsClearError_WhenRequiredElementMissing()
		{
			var bad = XDocument.Parse(@"<font>
<fontname>x</fontname><fullname>x</fullname><fontfamily>Arial</fontfamily><weight>Regular</weight>
<fontbbox><xmin>0</xmin><ymin>0</ymin><xmax>1</xmax><ymax>1</ymax></fontbbox>
</font>");
			var ex = Assert.Throws<InvalidOperationException>(() => new FontMetric(bad));
			Assert.Contains("capheight", ex.Message, StringComparison.OrdinalIgnoreCase);
		}
	}
}
