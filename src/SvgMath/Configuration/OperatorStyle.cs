using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
	public class OperatorStyle
	{
		public OperatorStyle(string name)
		{
			OpName = name;
		}

		public void AddStyle(string name, string style)
		{
			Styling.Add(name, style);
		}

		public string OpName;
		public Dictionary<string, string> Styling = new Dictionary<string,string>();
	}
}
