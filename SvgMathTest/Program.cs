using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SvgMath;
using System.Xml.Linq;

namespace SvgMathTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Mml m = new Mml("test12.mml");
			m.MakeSvg().Save("test1.svg");
		}

		
		public static void ParseMML(XElement root, MathNode parentNode, MathConfig mc, int depth)
		{
			int recDepth = depth + 1;
			foreach (XElement element in root.Elements())
			{
				//ToDo: implement namespaces
				Console.WriteLine("{0} {1}", new String(' ', recDepth), element.Name);
				MathNode mn = new MathNode(
                    element.Name.LocalName,
                    element.Attributes().ToDictionary(kvp => kvp.Name.ToString(), kvp => kvp.Value),
                    mc, parentNode);
                element.Nodes()
                    .Where(x => x.NodeType == System.Xml.XmlNodeType.Text || x.NodeType == System.Xml.XmlNodeType.Whitespace)
                    .ToList()
                    .ForEach(x => mn.Text = mn.Text + string.Join(" ", ((XText)x).Value.Split(null)));
				ParseMML(element, mn, mc, recDepth);
			}
		}

	}
}
