using System;
using SvgMath;

namespace SvgMath.Tests.Infrastructure
{
	/// <summary>
	/// Shared <see cref="MathConfig"/> instances for test assets. Loading static fontdata is expensive; reuse across cases.
	/// </summary>
	internal static class TestMathConfigs
	{
		static readonly Lazy<MathConfig> _mmlSamplesStaticFonts = new Lazy<MathConfig>(() =>
			new MathConfig(TestOutputPaths.AssetPath("config_mml_samples.xml"), loadFontsFromStaticFiles: true));

		static readonly Lazy<MathConfig> _mmlSamplesTtf = new Lazy<MathConfig>(() =>
			new MathConfig(TestOutputPaths.AssetPath("config_mml_samples.xml"), loadFontsFromStaticFiles: false));

		static readonly Lazy<MathConfig> _mathSize10StaticFonts = new Lazy<MathConfig>(() =>
			new MathConfig(TestOutputPaths.AssetPath("config_mathsize_10.xml"), loadFontsFromStaticFiles: true));

		internal static MathConfig MmlSamplesStaticFonts => _mmlSamplesStaticFonts.Value;

		internal static MathConfig MmlSamplesTtf => _mmlSamplesTtf.Value;

		internal static MathConfig MathSize10StaticFonts => _mathSize10StaticFonts.Value;
	}
}
