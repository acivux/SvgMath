using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
    public static class DoubleOrNullExtension
    {
        public static double? DoubleOrNull(this string value)
        {
            double dbl;
            bool result = double.TryParse(value, out dbl);
            return result ? (double?) dbl : null;
        }
    }
}
