﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SvgMath
{
	public class MathConfig
	{
		
		public MathConfig(string configFile)
		{
			
			if (configFile == null)
			{
				DirectoryInfo configFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "configuration"));
				FileInfo configFileInfo = new FileInfo(Path.Combine(configFolder.FullName, "svgmath.xml"));
				if (!configFileInfo.Exists)
					throw new InvalidOperationException("default configuration file not found");

				configFile = configFileInfo.FullName;
			}
			
			m_configDoc = XDocument.Load(configFile);

			m_verbose = bool.Parse(m_configDoc.Element("config").Attributes().FirstOrDefault(x => x.Name == "verbose").Value);
			//Debug not implemented.
			m_debug = null;
			m_configDoc.Descendants("defaults").ToList().ForEach(elem => SetDefaults(elem));
			m_configDoc.Descendants("fallback").ToList().ForEach(elem => SetFallBackFamilies(elem));
            m_configDoc.Descendants("mathvariant").ToList().ForEach(elem => SetMathVariant(elem));
			m_configDoc.Descendants("operator-style").ToList().ForEach(elem => SetOperatorStyle(elem));
            SetFontFamilies();
        }
		void SetDefaults(XElement defaultsElement)
		{
			defaultsElement.Attributes().ToList().ForEach(attr => Defaults[attr.Name.LocalName] = attr.Value);
		}
		void SetFallBackFamilies(XElement fallbackElement)
		{
			FallbackFamilies.AddRange(fallbackElement.Attribute("family").Value.Split(',').Select(x => x.Trim()).ToList());
		}
		void SetFontFamilies()
		{
			DirectoryInfo fontFolder = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "configuration", "fontdata"));
            foreach (FileInfo fontFile in fontFolder.EnumerateFiles("*.xml"))
            {
                XDocument fontXDoc = XDocument.Load(fontFile.FullName);

                string familyName = fontXDoc.Descendants("fontfamily").FirstOrDefault().Value;
                string weight = fontXDoc.Descendants("weight").FirstOrDefault().Value.ToLower();
                if (weight == null || weight == "regular")
                    weight = "normal";
                //figger style
                double italicangle;
                bool result = double.TryParse(fontXDoc.Descendants("italicangle").FirstOrDefault().Value, out italicangle);
                string style = result && italicangle != 0 ? "italic" : "normal";
                string key = string.Format("{0} {1} {2}", weight, style, string.Join("", familyName.Trim().ToLower().Split(null)));

                if (!m_fontFamilies.ContainsKey(key))
                {
                    m_fontFamilies.Add(key, new FontMetric(fontXDoc));
                }
                else
                {
                    throw new InvalidOperationException(string.Format("{0} already has metric", key));
                }
            }
		}
		void SetMathVariant(XElement mathVariant)
		{
			string name = mathVariant.Attribute("name").Value;
			string weight = mathVariant.Attributes("weight").FirstOrDefault() != null ? mathVariant.Attribute("weight").Value : "normal";
			string style = mathVariant.Attributes("style").FirstOrDefault() != null ? mathVariant.Attribute("style").Value : "normal";
			MathVariant mv = new MathVariant(name, weight, style);
			mathVariant.Attribute("family").Value.Split(',').Select(x => x.Trim()).ToList().ForEach(x=> mv.AddFamily(x));
			Variants.Add(name, mv);
		}
		void SetOperatorStyle(XElement opElement)
		{
			string name = opElement.Attribute("operator").Value;
			OperatorStyle os = new OperatorStyle(name);
			opElement.Attributes().Where(x=>x.Name != "operator").ToList().ForEach(x=> os.AddStyle(x.Name.LocalName, x.Value));
			OperatorStyles.Add(os);
		}
		public FontMetric FindFont(string weight, string style, string family)
		{
			if (string.IsNullOrEmpty(weight))
				weight = "normal";
			if (string.IsNullOrEmpty(style))
				style = "normal";
			if (string.IsNullOrEmpty(family))
				family = "";
			weight = weight.Trim();
			style = style.Trim();
			family = string.Join("",family.Trim().ToLower().Split(null));
			foreach (string w in new string[]{weight, "normal"})
			{
				foreach (string s in new string[]{style,"normal"})
				{
					if (m_fontFamilies.ContainsKey(w + " " + s + " " + family))
						return m_fontFamilies[w + " " + s + " " + family];
				}
			}
			return null;
		}


		public List<string> FallbackFamilies = new List<string>();
		public Dictionary<string, string> Defaults = MathDefaults.globalDefaults;
		public Dictionary<string, MathVariant> Variants = new Dictionary<string, MathVariant>();
		public List<OperatorStyle> OperatorStyles = new List<OperatorStyle>();
		public MathOperators MathOperators = new MathOperators();
		readonly XDocument m_configDoc;
		bool m_verbose;
		IEnumerable<string> m_debug;
		List<string> m_currentFamily;
		Dictionary<string, FontMetric> m_fontFamilies = new Dictionary<string, FontMetric>();

	}
}
