using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
    public class CharMetric
    {
        public string GlyphName { get; set; }
        public List<int> Codes { get; set; }
        public double Width { get; set; }
        public double[] BBox { get; set; } //xMin, yMin, xMax, yMax
    }
}
