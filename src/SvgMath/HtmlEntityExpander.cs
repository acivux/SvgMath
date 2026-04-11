using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SvgMath
{
	/// <summary>
	/// Expands HTML/MathML named character references using WHATWG <c>entities.json</c> so
	/// <see cref="System.Xml.Linq.XDocument"/> can load W3C MathML tests that use entities without an internal DTD.
	/// XML predefined entities (<c>&amp;lt;</c> etc.) are left unchanged.
	/// </summary>
	static class HtmlEntityExpander
	{
		static readonly Lazy<IReadOnlyDictionary<string, string>> s_named =
			new Lazy<IReadOnlyDictionary<string, string>>(LoadNamedEntities);

		static readonly Regex s_entityRef = new Regex(
			@"&(#x[0-9A-Fa-f]+|#[0-9]+|[a-zA-Z][a-zA-Z0-9]*);",
			RegexOptions.Compiled | RegexOptions.CultureInvariant);

		internal static string PrepareMathMlXml(string filePath)
		{
			var text = File.ReadAllText(filePath);
			return ExpandEntityReferences(text);
		}

		internal static string ExpandEntityReferences(string text)
		{
			var map = s_named.Value;
			return s_entityRef.Replace(text, m =>
			{
				var inner = m.Groups[1].Value;
				if (inner.Length > 0 && inner[0] == '#')
					return m.Value;
				switch (inner)
				{
					case "amp":
					case "lt":
					case "gt":
					case "quot":
					case "apos":
						return m.Value;
					default:
						if (map.TryGetValue(inner, out var chars))
							return EscapeXmlTextForReinsertion(chars);
						return m.Value;
				}
			});
		}

		/// <summary>
		/// WHATWG expansions can contain raw U+003C (e.g. <c>&nvlt;</c> → "&lt;⃒"). If inserted verbatim, the next
		/// character would be parsed as a tag name — escape markup-significant characters for the outer XML parse.
		/// </summary>
		static string EscapeXmlTextForReinsertion(string s)
		{
			if (s.IndexOf('&') < 0 && s.IndexOf('<') < 0 && s.IndexOf('>') < 0)
				return s;
			var sb = new StringBuilder(s.Length + 8);
			foreach (var c in s)
			{
				switch (c)
				{
					case '&':
						sb.Append("&amp;");
						break;
					case '<':
						sb.Append("&lt;");
						break;
					case '>':
						sb.Append("&gt;");
						break;
					default:
						sb.Append(c);
						break;
				}
			}

			return sb.ToString();
		}

		static IReadOnlyDictionary<string, string> LoadNamedEntities()
		{
			var assembly = typeof(HtmlEntityExpander).Assembly;
			var resourceName = assembly.GetManifestResourceNames()
				.FirstOrDefault(n => n.EndsWith("html-entities.json", StringComparison.Ordinal));
			if (resourceName == null)
				throw new InvalidOperationException("Embedded WHATWG entity map (html-entities.json) not found.");

			using (var stream = assembly.GetManifestResourceStream(resourceName))
			using (var doc = JsonDocument.Parse(stream))
			{
				var dict = new Dictionary<string, string>(StringComparer.Ordinal);
				foreach (var prop in doc.RootElement.EnumerateObject())
				{
					var key = prop.Name;
					if (key.Length < 2 || key[0] != '&')
						continue;
					var name = key.Substring(1);
					if (name.EndsWith(";", StringComparison.Ordinal))
						name = name.Substring(0, name.Length - 1);
					var chars = prop.Value.GetProperty("characters").GetString();
					if (chars != null)
						dict[name] = chars;
				}

				return dict;
			}
		}
	}
}
