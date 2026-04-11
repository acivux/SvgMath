using System;
using System.IO;

namespace SvgMath.Tests
{
	/// <summary>W3C <c>build/main</c> tree mixes presentation and (strict) content MathML; SvgMath only renders presentation.</summary>
	static class W3cMmlTestPaths
	{
		internal static bool IsPresentationCase(string relativePathFromW3Root)
		{
			var n = relativePathFromW3Root.Replace('/', Path.DirectorySeparatorChar);
			if (n.StartsWith("Content" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
				return false;
			if (n.StartsWith("StrictContent" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
				return false;
			return true;
		}
	}
}
