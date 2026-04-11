using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SvgMath
{
	/// <summary>Presentation MathML → LaTeX (pdfLaTeX-oriented). Walks a <see cref="MathNode"/> tree without layout/metrics.</summary>
	public static class MathmlLatexEmitter
	{
		static readonly HashSet<string> KnownOperatorNames = new HashSet<string>(StringComparer.Ordinal)
		{
			"arcsin", "arccos", "arctan", "arg", "cos", "cosh", "cot", "coth", "csc", "deg", "det", "dim", "exp", "gcd", "hom", "inf", "ker",
			"lg", "lim", "liminf", "limsup", "ln", "log", "max", "min", "Pr", "sec", "sin", "sinh", "sup", "tan", "tanh"
		};

		static readonly Dictionary<char, string> UnicodeToLatex = BuildUnicodeMap();

		static Dictionary<char, string> BuildUnicodeMap()
		{
			var m = new Dictionary<char, string>();
			void add(char c, string s) => m[c] = s;
			const string greekChars = "αβγδεζηθικλμνξοπρστυφχψω";
			string[] greekNames = { "alpha", "beta", "gamma", "delta", "epsilon", "zeta", "eta", "theta", "iota", "kappa", "lambda", "mu",
				"nu", "xi", "pi", "rho", "sigma", "tau", "upsilon", "phi", "chi", "psi", "omega" };
			for (int i = 0; i < greekChars.Length && i < greekNames.Length; i++)
				add(greekChars[i], "\\" + greekNames[i]);
			add('\u03D5', "\\varphi");
			add('\u03F5', "\\varepsilon");
			add('\u03D1', "\\vartheta");
			// Greek uppercase (subset)
			add('\u0393', "\\Gamma"); add('\u0394', "\\Delta"); add('\u0398', "\\Theta"); add('\u039B', "\\Lambda");
			add('\u039E', "\\Xi"); add('\u03A0', "\\Pi"); add('\u03A3', "\\Sigma"); add('\u03A5', "\\Upsilon"); add('\u03A6', "\\Phi");
			add('\u03A8', "\\Psi"); add('\u03A9', "\\Omega");
			// Common symbols
			add('\u221E', "\\infty"); add('\u00B1', "\\pm"); add('\u2213', "\\mp"); add('\u00D7', "\\times"); add('\u00F7', "\\div");
			add('\u22C5', "\\cdot"); add('\u2212', "-"); add('\u003C', "<"); add('\u003E', ">"); add('\u2264', "\\leq"); add('\u2265', "\\geq");
			add('\u2260', "\\neq"); add('\u2248', "\\approx"); add('\u223C', "\\sim"); add('\u2208', "\\in"); add('\u2209', "\\notin");
			add('\u222A', "\\cup"); add('\u2229', "\\cap"); add('\u2202', "\\partial"); add('\u2207', "\\nabla"); add('\u221A', "\\surd"); // sqrt usually msqrt
			add('\u2211', "\\sum"); add('\u222B', "\\int"); add('\u222C', "\\iint"); add('\u222D', "\\iiint");
			add('\u220F', "\\prod"); add('\u22C0', "\\bigwedge"); add('\u22C1', "\\bigvee"); add('\u22C3', "\\bigcup"); add('\u22C2', "\\bigcap");
			add('\u2026', "\\ldots"); add('\u22EF', "\\cdots"); add('\u22EE', "\\vdots"); add('\u22F1', "\\ddots");
			add('\u2192', "\\rightarrow"); add('\u21D2', "\\Rightarrow"); add('\u2190', "\\leftarrow"); add('\u21D0', "\\Leftarrow");
			add('\u21A6', "\\mapsto"); add('\u22A5', "\\perp"); add('\u2225', "\\parallel"); add('\u2220', "\\angle");
			add('\u00B0', "^\\circ"); add('\u2032', "'"); add('\u2033', "''");
			add('\u2205', "\\emptyset"); add('\u2203', "\\exists"); add('\u2200', "\\forall"); 			add('\u2225', "\\parallel");
			add('\u00AC', "\\neg"); add('\u2227', "\\land"); add('\u2228', "\\lor"); add('\u21D4', "\\Leftrightarrow");
			add('\u2282', "\\subset"); add('\u2283', "\\supset"); add('\u2286', "\\subseteq"); add('\u2287', "\\supseteq");
			return m;
		}

		/// <summary>Emit LaTeX for a MathML tree rooted at <paramref name="node"/> (typically the <c>math</c> element).</summary>
		public static string Emit(MathNode node)
		{
			if (node == null)
				return "";
			return EmitElement(node);
		}

		static string EmitElement(MathNode node)
		{
			switch (node.ElementName)
			{
				case "math":
					return EmitChildren(node);
				case "mrow":
				case "mstyle":
					return EmitChildren(node);
				case "mphantom":
					return "\\phantom{" + EmitChildren(node) + "}";
				case "none":
					return "";
				case "mprescripts":
					return "";
				case "maction":
					return node.Children != null && node.Children.Count > 0 ? EmitElement(node.Children[0]) : "";
				case "merror":
					return "\\text{MathML error}\\quad " + EmitChildren(node);
				case "mpadded":
					return EmitChildren(node);
				case "mfenced":
					return EmitMfenced(node);
				case "menclose":
					return EmitMenclose(node);
				case "mfrac":
					return EmitMfrac(node);
				case "mo":
					return EmitMo(node);
				case "mi":
					return EmitMi(node);
				case "mn":
					return EmitMn(node);
				case "mtext":
				case "ms":
					return "\\text{" + EscapeText(node.Text ?? "") + "}";
				case "mspace":
					return EmitMspace(node);
				case "msqrt":
					return "\\sqrt{" + EmitChildren(node) + "}";
				case "mroot":
					return EmitMroot(node);
				case "msub":
					return EmitMsub(node);
				case "msup":
					return EmitMsup(node);
				case "msubsup":
					return EmitMsubsup(node);
				case "mmultiscripts":
					return EmitMmultiscripts(node);
				case "munder":
					return EmitMunder(node);
				case "mover":
					return EmitMover(node);
				case "munderover":
					return EmitMunderover(node);
				case "mtable":
					return EmitMtable(node);
				case "mtr":
					return EmitChildren(node);
				case "mlabeledtr":
					if (node.Children == null || node.Children.Count <= 1)
						return "";
					return string.Concat(node.Children.Skip(1).Select(EmitElement));
				case "mtd":
					return EmitChildren(node);
				default:
					return EmitChildren(node);
			}
		}

		static string EmitChildren(MathNode node)
		{
			if (node.Children == null || node.Children.Count == 0)
				return "";
			var sb = new StringBuilder();
			foreach (var ch in node.Children)
				sb.Append(EmitElement(ch));
			return sb.ToString();
		}

		static string EmitMfrac(MathNode node)
		{
			if (node.Children.Count < 2)
				return EmitChildren(node);
			var lt = GetAttr(node, "linethickness");
			if (lt == "0" || lt == "0pt" || string.Equals(lt, "0px", StringComparison.OrdinalIgnoreCase))
				return "\\binom{" + EmitElement(node.Children[0]) + "}{" + EmitElement(node.Children[1]) + "}";
			return "\\frac{" + EmitElement(node.Children[0]) + "}{" + EmitElement(node.Children[1]) + "}";
		}

		static string EmitMroot(MathNode node)
		{
			if (node.Children.Count < 2)
				return "\\sqrt{" + EmitChildren(node) + "}";
			return "\\sqrt[" + EmitElement(node.Children[1]) + "]{" + EmitElement(node.Children[0]) + "}";
		}

		static string EmitMsub(MathNode node)
		{
			if (node.Children.Count < 2)
				return EmitChildren(node);
			return "{" + EmitElement(node.Children[0]) + "}_{" + EmitElement(node.Children[1]) + "}";
		}

		static string EmitMsup(MathNode node)
		{
			if (node.Children.Count < 2)
				return EmitChildren(node);
			return "{" + EmitElement(node.Children[0]) + "}^{" + EmitElement(node.Children[1]) + "}";
		}

		static string EmitMsubsup(MathNode node)
		{
			if (node.Children.Count < 3)
			{
				if (node.Children.Count == 2)
					return EmitMsub(node);
				return EmitChildren(node);
			}
			return "{" + EmitElement(node.Children[0]) + "}_{" + EmitElement(node.Children[1]) + "}^{" + EmitElement(node.Children[2]) + "}";
		}

		static string EmitMunder(MathNode node)
		{
			if (node.Children.Count < 2)
				return EmitChildren(node);
			return "\\underset{" + EmitElement(node.Children[1]) + "}{" + EmitElement(node.Children[0]) + "}";
		}

		static string EmitMover(MathNode node)
		{
			if (node.Children.Count < 2)
				return EmitChildren(node);
			return "\\overset{" + EmitElement(node.Children[1]) + "}{" + EmitElement(node.Children[0]) + "}";
		}

		static string EmitMunderover(MathNode node)
		{
			if (node.Children.Count < 3)
			{
				if (node.Children.Count == 2)
					return EmitMunder(node);
				return EmitChildren(node);
			}
			return "\\underset{" + EmitElement(node.Children[1]) + "}{\\overset{" + EmitElement(node.Children[2]) + "}{" + EmitElement(node.Children[0]) + "}}";
		}

		static void CollectMultiscripts(MathNode node,
			out List<MathNode> postSub, out List<MathNode> postSup,
			out List<MathNode> preSub, out List<MathNode> preSup)
		{
			postSub = new List<MathNode>();
			postSup = new List<MathNode>();
			preSub = new List<MathNode>();
			preSup = new List<MathNode>();
			if (node.Children == null || node.Children.Count == 0)
				return;
			bool isPre = false;
			bool isSub = true;
			foreach (var ch in node.Children.Skip(1))
			{
				if (ch.ElementName == "mprescripts")
				{
					isPre = true;
					isSub = true;
					continue;
				}
				if (isSub)
				{
					if (isPre)
						preSub.Add(ch);
					else
						postSub.Add(ch);
				}
				else
				{
					if (isPre)
						preSup.Add(ch);
					else
						postSup.Add(ch);
				}
				isSub = !isSub;
			}
		}

		static bool IsNone(MathNode n) => n == null || n.ElementName == "none";

		static string EmitMmultiscripts(MathNode node)
		{
			if (node.Children == null || node.Children.Count == 0)
				return "";
			var baseNode = node.Children[0];
			CollectMultiscripts(node, out var postSub, out var postSup, out var preSub, out var preSup);

			string post = "";
			int nPairs = Math.Max(postSub.Count, postSup.Count);
			for (int i = 0; i < nPairs; i++)
			{
				var s = i < postSub.Count ? postSub[i] : null;
				var p = i < postSup.Count ? postSup[i] : null;
				post += EmitSubSupPair(s, p);
			}

			bool hasPre = preSub.Count > 0 || preSup.Count > 0;
			if (!hasPre)
				return EmitElement(baseNode) + post;

			string preS = preSup.Count > 0 && !IsNone(preSup[0]) ? EmitElement(preSup[0]) : "";
			string preB = preSub.Count > 0 && !IsNone(preSub[0]) ? EmitElement(preSub[0]) : "";
			return "\\prescript{" + preS + "}{" + preB + "}{" + EmitElement(baseNode) + "}" + post;
		}

		static string EmitSubSupPair(MathNode sub, MathNode sup)
		{
			string s = IsNone(sub) ? "" : EmitElement(sub);
			string p = IsNone(sup) ? "" : EmitElement(sup);
			if (s.Length == 0 && p.Length == 0)
				return "";
			if (s.Length > 0 && p.Length > 0)
				return "_{" + s + "}^{" + p + "}";
			if (s.Length > 0)
				return "_{" + s + "}";
			return "^{" + p + "}";
		}

		static string EmitMfenced(MathNode node)
		{
			string open = GetAttr(node, "open", "(");
			string close = GetAttr(node, "close", ")");
			// Use explicit separators only; svgmath.xml defaults contain a legacy typo ("},").
			string sepStr = ",";
			if (node.Attributes != null && node.Attributes.TryGetValue("separators", out var sepAttr) && !string.IsNullOrEmpty(sepAttr))
				sepStr = sepAttr;
			if (node.Children == null || node.Children.Count == 0)
				return Fence(open) + Fence(close);
			var parts = new StringBuilder();
			parts.Append(Fence(open));
			for (int i = 0; i < node.Children.Count; i++)
			{
				if (i > 0)
				{
					char sepCh = sepStr.Length > 0 ? sepStr[Math.Min(i - 1, sepStr.Length - 1)] : ',';
					parts.Append(EmitMoChar(sepCh));
				}
				parts.Append(EmitElement(node.Children[i]));
			}
			parts.Append(Fence(close));
			return parts.ToString();
		}

		static string Fence(string s)
		{
			if (string.IsNullOrEmpty(s))
				return "";
			if (s == "{")
				return "\\{";
			if (s == "}")
				return "\\}";
			return s;
		}

		static string EmitMenclose(MathNode node)
		{
			string notation = GetAttr(node, "notation", "");
			var parts = notation.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Contains("box") || parts.Contains("roundedbox"))
				return "\\boxed{" + EmitChildren(node) + "}";
			return EmitChildren(node);
		}

		static string EmitMspace(MathNode node)
		{
			string w = GetAttr(node, "width", "");
			if (string.IsNullOrEmpty(w))
				return "\\,";
			if (w.EndsWith("em", StringComparison.OrdinalIgnoreCase) && double.TryParse(w.Substring(0, w.Length - 2).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var em))
				return "\\hspace{" + em.ToString(CultureInfo.InvariantCulture) + "em}";
			return "\\,";
		}

		static string EmitMtable(MathNode node)
		{
			var rows = new List<List<MathNode>>();
			foreach (var ch in node.Children ?? Enumerable.Empty<MathNode>())
			{
				if (ch.ElementName == "mtr")
				{
					var cells = new List<MathNode>();
					foreach (var c in ch.Children ?? Enumerable.Empty<MathNode>())
					{
						if (c.ElementName == "mtd")
							cells.Add(c);
					}
					if (cells.Count > 0)
						rows.Add(cells);
				}
				else if (ch.ElementName == "mlabeledtr")
				{
					var cells = new List<MathNode>();
					bool first = true;
					foreach (var c in ch.Children ?? Enumerable.Empty<MathNode>())
					{
						if (first)
						{
							first = false;
							continue;
						}
						if (c.ElementName == "mtd")
							cells.Add(c);
					}
					if (cells.Count > 0)
						rows.Add(cells);
				}
			}
			if (rows.Count == 0)
				return EmitChildren(node);
			int cols = rows.Max(r => r.Count);
			var align = new StringBuilder();
			for (int c = 0; c < cols; c++)
			{
				char a = 'c';
				foreach (var row in rows)
				{
					if (c < row.Count)
					{
						var ca = GetAttr(row[c], "columnalign", "center");
						if (ca == "left")
						{
							a = 'l';
							break;
						}
						if (ca == "right")
						{
							a = 'r';
							break;
						}
					}
				}
				align.Append(a);
			}
			var sb = new StringBuilder();
			sb.Append("\\begin{array}{").Append(align.ToString()).Append('}');
			for (int r = 0; r < rows.Count; r++)
			{
				if (r > 0)
					sb.Append(" \\\\ ");
				for (int c = 0; c < rows[r].Count; c++)
				{
					if (c > 0)
						sb.Append(" & ");
					sb.Append(EmitChildren(rows[r][c]));
				}
			}
			sb.Append("\\end{array}");
			return sb.ToString();
		}

		static string EmitMi(MathNode node)
		{
			string text = node.Text ?? "";
			if (text.Length == 0)
				return "";
			// Do not use inherited NodeDefaults for mathvariant: global config sets "normal" for all nodes,
			// but MathML default for <mi> is italic single-character identifiers.
			string mv = "";
			if (node.Attributes != null && node.Attributes.TryGetValue("mathvariant", out var explicitMv) && explicitMv != null)
				mv = explicitMv;
			return MapMathVariant(mv, text);
		}

		static string MapMathVariant(string mv, string text)
		{
			string inner = EmitMiText(text);
			if (mv == "normal")
				return "\\mathrm{" + inner + "}";

			if (string.IsNullOrEmpty(mv) || mv == "italic")
			{
				if (text.Length == 1)
					return inner;
				if (text.All(char.IsLetter))
				{
					string lower = text.ToLowerInvariant();
					if (KnownOperatorNames.Contains(lower))
						return "\\" + lower + " ";
					return "\\mathrm{" + inner + "}";
				}
				return "\\mathit{" + inner + "}";
			}

			switch (mv)
			{
				case "bold":
					return "\\mathbf{" + inner + "}";
				case "bold-italic":
					return "\\boldsymbol{" + inner + "}";
				case "double-struck":
					return "\\mathbb{" + inner + "}";
				case "fraktur":
					return "\\mathfrak{" + inner + "}";
				case "bold-fraktur":
					return "\\mathfrak{\\mathbf{" + inner + "}}";
				case "script":
				case "bold-script":
					return "\\mathcal{" + inner + "}";
				case "sans-serif":
				case "sans-serif-italic":
				case "sans-serif-bold":
				case "sans-serif-bold-italic":
					return "\\mathsf{" + inner + "}";
				case "monospace":
					return "\\mathtt{" + inner + "}";
				default:
					return inner;
			}
		}

		static string EmitMiText(string text)
		{
			var sb = new StringBuilder();
			foreach (var rune in text)
			{
				if (UnicodeToLatex.TryGetValue(rune, out var cmd))
					sb.Append(cmd);
				else if (rune < 128 && char.IsLetter(rune))
					sb.Append(rune);
				else
					sb.Append("\\text{" + EscapeText(rune.ToString()) + "}");
			}
			return sb.ToString();
		}

		static string EmitMn(MathNode node)
		{
			string t = node.Text ?? "";
			if (t.Length == 0)
				return "";
			return EscapeMathLiteral(t);
		}

		static string EmitMo(MathNode node)
		{
			string t = node.Text ?? "";
			if (t.Length == 0)
				return "";
			var sb = new StringBuilder();
			foreach (var ch in t)
				sb.Append(EmitMoChar(ch));
			return sb.ToString();
		}

		static string EmitMoChar(char ch)
		{
			if (UnicodeToLatex.TryGetValue(ch, out var cmd))
				return cmd;
			if (ch < 128)
			{
				switch (ch)
				{
					case '\\':
						return "\\backslash ";
					case '{':
					case '}':
					case '#':
					case '$':
					case '%':
					case '&':
						return "\\" + ch;
					case '^':
						return "\\wedge ";
					case '_':
						return "\\_";
					case '~':
						return "\\sim ";
					default:
						return ch.ToString();
				}
			}
			return "\\text{" + EscapeText(ch.ToString()) + "}";
		}

		static string EscapeMathLiteral(string s)
		{
			var sb = new StringBuilder();
			foreach (var ch in s)
			{
				if (UnicodeToLatex.TryGetValue(ch, out var cmd))
					sb.Append(cmd);
				else
					sb.Append(EmitMoChar(ch));
			}
			return sb.ToString();
		}

		static string EscapeText(string s)
		{
			var sb = new StringBuilder();
			foreach (var ch in s)
			{
				switch (ch)
				{
					case '\\':
						sb.Append("\\textbackslash "); break;
					case '{':
						sb.Append("\\{"); break;
					case '}':
						sb.Append("\\}"); break;
					case '#':
						sb.Append("\\#"); break;
					case '$':
						sb.Append("\\$"); break;
					case '%':
						sb.Append("\\%"); break;
					case '&':
						sb.Append("\\&"); break;
					case '^':
						sb.Append("\\textasciicircum "); break;
					case '_':
						sb.Append("\\_"); break;
					case '~':
						sb.Append("\\textasciitilde "); break;
					default:
						sb.Append(ch);
						break;
				}
			}
			return sb.ToString();
		}

		static string GetAttr(MathNode node, string key, string def = null)
		{
			if (node.Attributes != null && node.Attributes.TryGetValue(key, out var v) && v != null)
				return v;
			if (node.NodeDefaults != null && node.NodeDefaults.TryGetValue(key, out var d) && d != null)
				return d;
			return def;
		}
	}
}
