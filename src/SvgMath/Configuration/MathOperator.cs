using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
    public class MathOperator
    {
        public MathOperator(string content)
        {
            Content = content;
            Form = "infix";
            Fence = "false";
            Separator = "false";
            Accent = "false";
            LargeOp = "false";
            LSpace = "thickmathspace";
            RSpace = "thickmathspace";
            Stretchy = "false";
            Scaling = "uniform";
            MinSize = "1";
            MaxSize = "infinity";
            MovableLimits = "false";
            Symmetric = "true";
        }

        public Dictionary<string, string> Dict()
        {
            Dictionary<string, string> mop = new Dictionary<string, string>()
            {
                { "form", Form },
                { "fence", Fence },
                { "separator", Separator },
                { "accent", Accent },
                { "largeop", LargeOp },
                { "lspace", LSpace },
                { "rspace", RSpace },
                { "stretchy", Stretchy },
                { "scaling", Scaling },
                { "minsize", MinSize},
                { "maxsize", MaxSize },
                { "movablelimits", MovableLimits },
                { "symmetric", Symmetric }
            };
			return mop;
            //return new Dictionary<string, Dictionary<string, string>>() { { Content + Form, m_op } };
        }

        public string Key()
        {
            return Content + Form;
        }

        public string Content { get; set; }
        public string Form { get; set; }
        public string Fence { get; set; }
        public string Stretchy { get; set; }
        public string Scaling { get; set; }
        public string LSpace { get; set; }
        public string RSpace { get; set; }
        public string Separator { get; set; }
        public string Accent { get; set; }
        public string LargeOp { get; set; }
        public string MinSize { get; set; }
        public string MaxSize { get; set; }
        public string MovableLimits { get; set; }
        public string Symmetric { get; set; }
    }
}
