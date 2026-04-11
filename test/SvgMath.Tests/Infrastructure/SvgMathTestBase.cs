using System.IO;
using SvgMath.Tests.Infrastructure;

namespace SvgMath.Tests
{
	/// <summary>Sets current directory so <see cref="MathConfig"/> can resolve configuration/fontdata next to the test assembly.</summary>
	public abstract class SvgMathTestBase
	{
		protected SvgMathTestBase()
		{
			Directory.SetCurrentDirectory(TestOutputPaths.OutputDirectory);
		}
	}
}
