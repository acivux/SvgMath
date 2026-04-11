using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SvgMath
{
    public class FontMetric : GenericFontMetric
    {
        public FontMetric() { }

        public FontMetric(XDocument fontXDoc)
        {
            m_fontName = RequireDescendantValue(fontXDoc, "fontname");
            m_fullName = RequireDescendantValue(fontXDoc, "fullname");
            m_family = RequireDescendantValue(fontXDoc, "fontfamily");
            m_weight = RequireDescendantValue(fontXDoc, "weight");
            m_boundingBox[0] = BboxComponent(fontXDoc, "xmin");
            m_boundingBox[1] = BboxComponent(fontXDoc, "ymin");
            m_boundingBox[2] = BboxComponent(fontXDoc, "xmax");
            m_boundingBox[3] = BboxComponent(fontXDoc, "ymax");
            m_capHeight = RequireDouble(fontXDoc, "capheight");
            m_xHeight = RequireDouble(fontXDoc, "xheight");
            m_ascender = RequireDouble(fontXDoc, "ascender");
            m_descender = RequireDouble(fontXDoc, "descender");
            m_ruleWidth = RequireDouble(fontXDoc, "rulewidth");
            m_vGap = RequireDouble(fontXDoc, "vgap");
            m_stdVw = RequireDouble(fontXDoc, "stdvw");
            m_undelinePosition = RequireDouble(fontXDoc, "underlineposition");
            m_underlineThickness = RequireDouble(fontXDoc, "underlinethickness");
            m_italicAngle = RequireDouble(fontXDoc, "italicangle");
            m_charWidth = RequireDouble(fontXDoc, "charwidth");
            m_axisPosition = RequireDouble(fontXDoc, "axisposition");

            CharData = fontXDoc.Descendants("char")
                .Select(x => new KeyValuePair<int, CharMetric>(
                    int.Parse(x.Element("charid").Value.Substring(2), NumberStyles.HexNumber),
                    new CharMetric(
                        charName: x.Element("name").Value,
                        codes: x.Descendants("charid").Select(z => (uint) int.Parse(z.Value.Substring(2), NumberStyles.HexNumber)).ToList(),
                        width: double.Parse(x.Element("w").Value, CultureInfo.InvariantCulture),
                        bbox: new double []
                            {
                                double.Parse(x.Element("b").Element("xmin").Value, CultureInfo.InvariantCulture),
                                double.Parse(x.Element("b").Element("ymin").Value, CultureInfo.InvariantCulture),
                                double.Parse(x.Element("b").Element("xmax").Value, CultureInfo.InvariantCulture),
                                double.Parse(x.Element("b").Element("ymax").Value, CultureInfo.InvariantCulture)
                            }))).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            XElement mg = fontXDoc.Descendants("missingglyph").FirstOrDefault();
            if (mg == null)
                throw new InvalidOperationException("Font metric XML is missing required element 'missingglyph'.");
            m_missingGlyph = new CharMetric(
                charName: mg.Element("name").Value,
                width: double.Parse(mg.Element("w").Value, CultureInfo.InvariantCulture),
                codes: mg.Descendants("charid").Select(x => (uint) int.Parse(x.Value, CultureInfo.InvariantCulture)).ToList(),
                bbox: new double[]
                    {
                            double.Parse(mg.Element("b").Element("xmin").Value, CultureInfo.InvariantCulture),
                            double.Parse(mg.Element("b").Element("ymin").Value, CultureInfo.InvariantCulture),
                            double.Parse(mg.Element("b").Element("xmax").Value, CultureInfo.InvariantCulture),
                            double.Parse(mg.Element("b").Element("ymax").Value, CultureInfo.InvariantCulture)
                    });
        }

        static string RequireDescendantValue(XDocument doc, string localName)
        {
            XElement el = doc.Descendants(localName).FirstOrDefault();
            if (el == null)
                throw new InvalidOperationException(string.Format("Font metric XML is missing required element '{0}'.", localName));
            return el.Value;
        }

        static double RequireDouble(XDocument doc, string localName)
        {
            string s = RequireDescendantValue(doc, localName);
            double? d = s.DoubleOrNull();
            if (d == null)
                throw new InvalidOperationException(string.Format("Font metric element '{0}' has invalid or missing numeric value.", localName));
            return d.Value;
        }

        static double BboxComponent(XDocument doc, string corner)
        {
            XElement el = doc.Descendants("fontbbox").Elements().FirstOrDefault(x => x.Name.LocalName == corner);
            if (el == null)
                throw new InvalidOperationException(string.Format("Font metric XML is missing fontbbox element '{0}'.", corner));
            return double.Parse(el.Value, CultureInfo.InvariantCulture);
        }
    }
}
