using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace SvgMath
{
	public class FontMetric
	{
		//To Do: Implement. (/fonts/metric)
		public FontMetric(XDocument fontXDoc)
		{
            FontName = fontXDoc.Descendants("fontname").FirstOrDefault().Value;
            FullName = fontXDoc.Descendants("fullname").FirstOrDefault().Value;
            Family = fontXDoc.Descendants("fontfamily").FirstOrDefault().Value;
            Weight = fontXDoc.Descendants("weight").FirstOrDefault().Value;
            BBox[0] = fontXDoc.Descendants("fontbbox").Where(x=>x.Name=="xmin").Select(x=> double.Parse(x.Value)).FirstOrDefault();
            BBox[1] = fontXDoc.Descendants("fontbbox").Where(x => x.Name == "ymin").Select(x => double.Parse(x.Value)).FirstOrDefault();
            BBox[2] = fontXDoc.Descendants("fontbbox").Where(x => x.Name == "xmax").Select(x => double.Parse(x.Value)).FirstOrDefault();
            BBox[3] = fontXDoc.Descendants("fontbbox").Where(x => x.Name == "ymax").Select(x => double.Parse(x.Value)).FirstOrDefault();
            CapHeight = DoubleOrNull(fontXDoc.Descendants("capheight").FirstOrDefault().Value);
            XHeight = DoubleOrNull(fontXDoc.Descendants("xheight").FirstOrDefault().Value);
            Ascender= DoubleOrNull(fontXDoc.Descendants("ascender").FirstOrDefault().Value);
            Descender = DoubleOrNull(fontXDoc.Descendants("descender").FirstOrDefault().Value);
            RuleWidth = DoubleOrNull(fontXDoc.Descendants("rulewidth").FirstOrDefault().Value);
            VGap = double.Parse(fontXDoc.Descendants("vgap").FirstOrDefault().Value);
            StdHw = DoubleOrNull(fontXDoc.Descendants("stdhw").FirstOrDefault().Value);
            StdVw = DoubleOrNull(fontXDoc.Descendants("stdvw").FirstOrDefault().Value);
            UndelinePosition = DoubleOrNull(fontXDoc.Descendants("underlineposition").FirstOrDefault().Value);
            UnderlineThickness = DoubleOrNull(fontXDoc.Descendants("underlinethickness").FirstOrDefault().Value);
            ItalicAngle = DoubleOrNull(fontXDoc.Descendants("italicangle").FirstOrDefault().Value);
            CharWidth = DoubleOrNull(fontXDoc.Descendants("charwidth").FirstOrDefault().Value);
            AxisPosition = DoubleOrNull(fontXDoc.Descendants("axisposition").FirstOrDefault().Value);
            CharData = fontXDoc.Descendants("char").Select(x => new KeyValuePair<int, CharMetric>(int.Parse(x.Element("charid").Value.Substring(2), NumberStyles.HexNumber),
                new CharMetric()
                {
                    GlyphName = x.Element("name").Value,
                    Width = double.Parse(x.Element("w").Value),
                    Codes = x.Descendants("charid").Select(z => int.Parse(z.Value.Substring(2), NumberStyles.HexNumber)).ToList(),
                    BBox = new double[]
                        {
                            double.Parse(x.Element("b").Element("xmin").Value),
                            double.Parse(x.Element("b").Element("ymin").Value),
                            double.Parse(x.Element("b").Element("xmax").Value),
                            double.Parse(x.Element("b").Element("ymax").Value)
                        }
                })).ToDictionary(kvp => kvp.Key, kvp=> kvp.Value);
            XElement mg = fontXDoc.Descendants("missingglyph").FirstOrDefault();
            MissingGlyph = new CharMetric()
            {
                GlyphName = mg.Element("name").Value,
                Width = double.Parse(mg.Element("w").Value),
                Codes = mg.Descendants("charid").Select(x => int.Parse(x.Value)).ToList(),
                BBox = new double[]
                    {
                            double.Parse(mg.Element("b").Element("xmin").Value),
                            double.Parse(mg.Element("b").Element("ymin").Value),
                            double.Parse(mg.Element("b").Element("xmax").Value),
                            double.Parse(mg.Element("b").Element("ymax").Value)
                    }
            };
        }

        private double? DoubleOrNull(string value)
        {
            double dbl;
            bool result = double.TryParse(value, out dbl);
            return result ? (double?)dbl : null;
        }

		public string FontName;
		public string FullName;
        public string Family;
        public string Weight;
        public double[] BBox = new double[] {0,0,0,0};
		public double? CapHeight;
		public double? XHeight;
		public double? Ascender;
		public double? Descender;
		public double? RuleWidth;
		public double VGap;
        public double? StdHw;
        public double? StdVw;
        public double? UndelinePosition;
        public double? UnderlineThickness;
        public double? ItalicAngle;
        public double? CharWidth;
        public double? AxisPosition;
		public Dictionary<int, CharMetric> CharData;
		public CharMetric MissingGlyph;
	}
}
