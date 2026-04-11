using System.Linq;
using System.Xml.Linq;


namespace SvgMath
{
	/* Minimal effort port of http://sourceforge.net/projects/svgmath/?source=navbar
	 * See also License.txt
	 */
	public class Mml
	{
		/// <summary>W3C MathML namespace (MathML 2, 3, and 4 use the same URI).</summary>
		public const string MathMlNamespaceUri = "http://www.w3.org/1998/Math/MathML";

		static bool IsMathMLElement(XElement e)
		{
			var ns = e.Name.NamespaceName;
			return ns.Length == 0 || ns == MathMlNamespaceUri;
		}
		public Mml(string mmlFile, string configFile = null, bool loadFontsFromStaticFiles = false)
		{
			m_configFileName = configFile;
			m_loadFontsFromStaticFiles = loadFontsFromStaticFiles;
			m_mathDocument = XDocument.Load(mmlFile);
		}
		public Mml(XElement mmlContent, string configFile = null, bool loadFontsFromStaticFiles = false)
		{
			m_configFileName = configFile;
			m_loadFontsFromStaticFiles = loadFontsFromStaticFiles;
			m_mathDocument = new XDocument(mmlContent);
		}

		/// <summary>Render Presentation MathML to SVG (existing behavior).</summary>
		public XElement MakeSvg()
		{
			var root = BuildRootNode();
			return root.MakeImage();
		}

		/// <summary>Convert Presentation MathML to a LaTeX math fragment (no surrounding <c>$</c> by default).
		/// Assumes <c>amsmath</c>; <c>mathtools</c> is required for <c>\prescript</c> when using <c>mmultiscripts</c>.</summary>
		public string MakeLatex(MathLatexOptions options = null)
		{
			var root = BuildRootNode();
			var fragment = MathmlLatexEmitter.Emit(root);
			options = options ?? new MathLatexOptions();
			if (!options.WrapInDelimiters)
				return fragment;
			if (options.DisplayMode)
				return "\\[" + fragment + "\\]";
			return options.InlineOpen + fragment + options.InlineClose;
		}

		MathNode BuildRootNode()
		{
			m_mathConfig = new MathConfig(m_configFileName, m_loadFontsFromStaticFiles);
			var currentNode = new MathNode(
				m_mathDocument.Root.Name.LocalName,
				m_mathDocument.Root.Attributes().ToDictionary(kvp => kvp.Name.ToString(), kvp => kvp.Value),
				m_mathConfig,
				null);
			ParseMML(m_mathDocument.Root, currentNode, 0);
			return currentNode;
		}

		
		void ParseMML(XElement root, MathNode parentNode, int depth)
		{
			int recDepth = depth + 1;
			foreach (XElement element in root.Elements().Where(IsMathMLElement).Where(e => !IsTableAlignmentAuxiliary(e)))
				AddMathSubtree(element, parentNode, recDepth);
		}

		static bool IsTableAlignmentAuxiliary(XElement e)
		{
			var n = e.Name.LocalName;
			return n == "malignmark" || n == "maligngroup";
		}

		/// <summary>First child of <c>semantics</c> is Content MathML — not rendered by this engine.</summary>
		internal static bool IsUnsupportedContentMathRoot(XElement e)
		{
			if (!IsMathMLElement(e))
				return false;
			switch (e.Name.LocalName)
			{
				case "apply":
				case "bind":
				case "csymbol":
				case "cn":
				case "ci":
				case "cerror":
				case "cs":
				case "share":
				case "cbytes":
					return true;
				default:
					return false;
			}
		}

		/// <summary>Elementary math <c>mstack</c> is lowered to <c>mtable</c> (rows as <c>mtr</c>/<c>mtd</c>) for layout.</summary>
		internal static XElement MStackToMtable(XElement mstack)
		{
			var ns = mstack.Name.Namespace;
			var table = new XElement(ns + "mtable");
			foreach (XElement row in mstack.Elements().Where(IsMathMLElement))
			{
				string ln = row.Name.LocalName;
				if (ln == "msrow" || ln == "msgroup" || ln == "mscarries")
				{
					var mtr = new XElement(ns + "mtr");
					foreach (XElement cell in row.Elements().Where(IsMathMLElement))
						mtr.Add(new XElement(ns + "mtd", MtdContentForStackCell(cell)));
					table.Add(mtr);
				}
				else if (ln == "msline")
				{
					var lineMo = new XElement(ns + "mo", new XAttribute("stretchy", "true"), "\u2015");
					table.Add(new XElement(ns + "mtr", new XElement(ns + "mtd", lineMo)));
				}
				else
					table.Add(new XElement(ns + "mtr", new XElement(ns + "mtd", new XElement(row))));
			}

			return table;
		}

		static XElement MtdContentForStackCell(XElement cell)
		{
			if (cell.Name.LocalName == "mscarry")
			{
				var inner = cell.Elements().Where(IsMathMLElement).ToList();
				var ns = cell.Name.Namespace;
				if (inner.Count == 0)
					return new XElement(ns + "none");
				if (inner.Count == 1)
					return new XElement(inner[0]);
				return new XElement(ns + "mrow", inner.Select(c => new XElement(c)));
			}

			return new XElement(cell);
		}

		/// <summary>First child of <c>semantics</c> used for layout: not <c>annotation</c> / <c>annotation-xml</c>.</summary>
		static XElement FirstSemanticsExpressionChild(XElement semantics)
		{
			return semantics.Elements()
				.Where(IsMathMLElement)
				.FirstOrDefault(e =>
				{
					var n = e.Name.LocalName;
					return n != "annotation" && n != "annotation-xml";
				});
		}

		void AddMathSubtree(XElement element, MathNode parentNode, int recDepth)
		{
			if (element.Name.LocalName == "semantics")
			{
				var first = FirstSemanticsExpressionChild(element);
				if (first != null)
				{
					if (IsUnsupportedContentMathRoot(first))
						throw new System.InvalidOperationException(
							"Content MathML is not supported for layout; use presentation MathML as the first child of semantics.");
					AddMathSubtree(first, parentNode, recDepth);
				}
				return;
			}

			if (element.Name.LocalName == "mstack")
			{
				AddMathSubtree(MStackToMtable(element), parentNode, recDepth);
				return;
			}

			MathNode mn = new MathNode(
				element.Name.LocalName,
				element.Attributes().ToDictionary(kvp => kvp.Name.ToString(), kvp => kvp.Value),
				m_mathConfig, parentNode);

			element.Nodes()
				.Where(x => x.NodeType == System.Xml.XmlNodeType.Text || x.NodeType == System.Xml.XmlNodeType.Whitespace)
				.ToList()
				.ForEach(x => mn.Text = mn.Text + string.Join(" ", ((XText)x).Value.Split(null)));

			ParseMML(element, mn, recDepth);
		}


		readonly string m_configFileName;
		readonly bool m_loadFontsFromStaticFiles;
		readonly XDocument m_mathDocument;
		MathConfig m_mathConfig;
	}
}
