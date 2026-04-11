using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
	public class MathVariant
	{
		public MathVariant(string name, string weight, string style)
		{
			Name = name;
			Weight = weight;
			Style = style;
			Families = new List<string>();
		}

		public void AddFamily(string familyName)
		{
			Families.Add(familyName);
		}
		
		public string Name;
		public List<string> Families;
		public string Weight;
		public string Style;
	}
}
