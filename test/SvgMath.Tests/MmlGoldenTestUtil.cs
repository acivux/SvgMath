using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace SvgMath.Tests
{
	internal static class MmlGoldenTestUtil
	{
		internal static string NormalizeNewlines(string s) =>
			s.Replace("\r\n", "\n").Replace('\r', '\n');

		/// <summary>Stable SVG serialization for golden comparison (no XML declaration, no indent).</summary>
		internal static string SerializeSvg(XElement root)
		{
			var doc = new XDocument(root);
			var settings = new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true };
			using (var sw = new StringWriter())
			{
				using (var xw = XmlWriter.Create(sw, settings))
				{
					doc.Save(xw);
				}

				return NormalizeNewlines(sw.ToString());
			}
		}
	}
}
