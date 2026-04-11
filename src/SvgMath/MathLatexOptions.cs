namespace SvgMath
{
	/// <summary>Options for <see cref="Mml.MakeLatex(MathLatexOptions)"/>.
	/// Emitted LaTeX assumes <c>amsmath</c>; <c>mathtools</c> is required for <c>\prescript</c> (e.g. <c>mmultiscripts</c>).</summary>
	public sealed class MathLatexOptions
	{
		/// <summary>When true, wrap the math fragment in delimiters (<see cref="DisplayMode"/> chooses <c>\[...\]</c> vs <c>$...$</c>).</summary>
		public bool WrapInDelimiters { get; set; }

		/// <summary>When true and <see cref="WrapInDelimiters"/> is true, use display math <c>\[...\]</c>; otherwise inline.</summary>
		public bool DisplayMode { get; set; }

		/// <summary>Opening delimiter when <see cref="WrapInDelimiters"/> is true and <see cref="DisplayMode"/> is false. Default <c>$</c>.</summary>
		public string InlineOpen { get; set; } = "$";

		/// <summary>Closing delimiter when <see cref="WrapInDelimiters"/> is true and <see cref="DisplayMode"/> is false. Default <c>$</c>.</summary>
		public string InlineClose { get; set; } = "$";
	}
}
