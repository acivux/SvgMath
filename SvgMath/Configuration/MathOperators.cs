using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgMath
{
    public class MathOperators
    {
        public MathOperators()
        {
			OperatorList = new List<MathOperator>();
			OperatorList.Add(new MathOperator(""));
            OperatorList.Add(new MathOperator("(") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator(")") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("[") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("]") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("{") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("}") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("\u222C") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // Double Integral
			OperatorList.Add(new MathOperator("\u23DE") { Form = "postfix", LargeOp = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // Double Integral
            OperatorList.Add(new MathOperator("\u201D") { Form = "postfix", Fence = "true", LSpace = "0em", RSpace = "0em" }); // CloseCurlyDoubleQuote
            OperatorList.Add(new MathOperator("\u2019") { Form = "postfix", Fence = "true", LSpace = "0em", RSpace = "0em" }); // CloseCurlyQuote
            OperatorList.Add(new MathOperator("\u2329") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // LeftAngleBracket
            OperatorList.Add(new MathOperator("\u2308") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // LeftCeiling
            OperatorList.Add(new MathOperator("\u301A") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // LeftDoubleBracket
            OperatorList.Add(new MathOperator("\u230A") { Form = "prefix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // LeftFloor
            OperatorList.Add(new MathOperator("\u201C") { Form = "prefix", Fence = "true", LSpace = "0em", RSpace = "0em" }); // OpenCurlyDoubleQuote
            OperatorList.Add(new MathOperator("\u2018") { Form = "prefix", Fence = "true", LSpace = "0em", RSpace = "0em" }); // OpenCurlyQuote
            OperatorList.Add(new MathOperator("\u232A") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // RightAngleBracket
            OperatorList.Add(new MathOperator("\u2309") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // RightCeiling
            OperatorList.Add(new MathOperator("\u301B") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // RightDoubleBracket
            OperatorList.Add(new MathOperator("\u230B") { Form = "postfix", Fence = "true", Stretchy = "true", Scaling = "vertical", LSpace = "0em", RSpace = "0em" }); // RightFloor
            OperatorList.Add(new MathOperator("\u2063") { Form = "infix", Separator = "true", LSpace = "0em", RSpace = "0em" }); // InvisibleComma
            OperatorList.Add(new MathOperator(",") { Form = "infix", Separator = "true", LSpace = "0em", RSpace = "verythickmathspace" });
            OperatorList.Add(new MathOperator("\u2500") { Form = "infix", Stretchy = "true", Scaling = "horizontal", MinSize = "0", LSpace = "0em", RSpace = "0em" }); // HorizontalLine
            // Commented out: collides with '|'. See http://lists.w3.org/Archives/Public/www-math/2004Mar/0028.html
            // OperatorList.Add(new MathOperator("|"){Form="infix", Stretchy="true", Scaling="vertical", minsize="0", LSpace="0em", RSpace="0em" }); // VerticalLine 
            OperatorList.Add(new MathOperator(";") { Form = "infix", Separator = "true", LSpace = "0em", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator(";") { Form = "postfix", Separator = "true", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator(":=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("\u2254") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Assign
            OperatorList.Add(new MathOperator("\u2235") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Because
            OperatorList.Add(new MathOperator("\u2234") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Therefore
            OperatorList.Add(new MathOperator("\u2758") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // VerticalSeparator
            OperatorList.Add(new MathOperator("//") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            // Commented out: collides with Proportional
            // OperatorList.Add(new MathOperator("\u2237"){Form="infix", LSpace="thickmathspace", RSpace="thickmathspace" }); // Colon
            OperatorList.Add(new MathOperator("&") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("&") { Form = "postfix", LSpace = "thickmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("*=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("-=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("+=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("/=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("->") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator(":") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("..") { Form = "postfix", LSpace = "mediummathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("...") { Form = "postfix", LSpace = "mediummathspace", RSpace = "0em" });
            // Commented out: collides with ReverseElement
            // OperatorList.Add(new MathOperator("\u220B"){Form="infix", LSpace="thickmathspace", RSpace="thickmathspace" }); // SuchThat
            OperatorList.Add(new MathOperator("\u2AE4") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleLeftTee
            OperatorList.Add(new MathOperator("\u22A8") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleRightTee
            OperatorList.Add(new MathOperator("\u22A4") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownTee
            OperatorList.Add(new MathOperator("\u22A3") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTee
            OperatorList.Add(new MathOperator("\u22A2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTee
            // Commented out: collides with DoubleRightArrow
            // OperatorList.Add(new MathOperator("\u21D2"){Form="infix", Stretchy="true", Scaling="horizontal", LSpace="thickmathspace", RSpace="thickmathspace") // Implies
            OperatorList.Add(new MathOperator("\u2970") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RoundImplies
            OperatorList.Add(new MathOperator("|") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("||") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" });
            OperatorList.Add(new MathOperator("\u2A54") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // Or
            OperatorList.Add(new MathOperator("&&") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("\u2A53") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // And
            OperatorList.Add(new MathOperator("&") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("!") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("\u2AEC") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" }); // Not
            OperatorList.Add(new MathOperator("\u2203") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" }); // Exists
            OperatorList.Add(new MathOperator("\u2200") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" }); // ForAll
            OperatorList.Add(new MathOperator("\u2204") { Form = "prefix", LSpace = "0em", RSpace = "thickmathspace" }); // NotExists
            OperatorList.Add(new MathOperator("\u2208") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Element
            OperatorList.Add(new MathOperator("\u2209") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotElement
            OperatorList.Add(new MathOperator("\u220C") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotReverseElement
            OperatorList.Add(new MathOperator("\u228F\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSquareSubset
            OperatorList.Add(new MathOperator("\u22E2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSquareSubsetEqual
            OperatorList.Add(new MathOperator("\u2290\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSquareSuperset
            OperatorList.Add(new MathOperator("\u22E3") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSquareSupersetEqual
            OperatorList.Add(new MathOperator("\u2282\u20D2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSubset
            OperatorList.Add(new MathOperator("\u2288") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSubsetEqual
            OperatorList.Add(new MathOperator("\u2283\u20D2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSuperset
            OperatorList.Add(new MathOperator("\u2289") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSupersetEqual
            OperatorList.Add(new MathOperator("\u220B") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // ReverseElement
            OperatorList.Add(new MathOperator("\u228F") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SquareSubset
            OperatorList.Add(new MathOperator("\u2291") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SquareSubsetEqual
            OperatorList.Add(new MathOperator("\u2290") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SquareSuperset
            OperatorList.Add(new MathOperator("\u2292") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SquareSupersetEqual
            OperatorList.Add(new MathOperator("\u22D0") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Subset
            OperatorList.Add(new MathOperator("\u2286") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SubsetEqual
            OperatorList.Add(new MathOperator("\u2283") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Superset
            OperatorList.Add(new MathOperator("\u2287") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SupersetEqual
            OperatorList.Add(new MathOperator("\u21D0") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleLeftArrow
            OperatorList.Add(new MathOperator("\u21D4") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleLeftRightArrow
            OperatorList.Add(new MathOperator("\u21D2") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleRightArrow
            OperatorList.Add(new MathOperator("\u2950") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownLeftRightVector
            OperatorList.Add(new MathOperator("\u295E") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownLeftTeeVector
            OperatorList.Add(new MathOperator("\u21BD") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownLeftVector
            OperatorList.Add(new MathOperator("\u2956") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownLeftVectorBar
            OperatorList.Add(new MathOperator("\u295F") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownRightTeeVector
            OperatorList.Add(new MathOperator("\u21C1") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownRightVector
            OperatorList.Add(new MathOperator("\u2957") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DownRightVectorBar
            OperatorList.Add(new MathOperator("\u2190") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftArrow
            OperatorList.Add(new MathOperator("\u21E4") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftArrowBar
            OperatorList.Add(new MathOperator("\u21C6") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftArrowRightArrow
            OperatorList.Add(new MathOperator("\u2194") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftRightArrow
            OperatorList.Add(new MathOperator("\u294E") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftRightVector
            OperatorList.Add(new MathOperator("\u21A4") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTeeArrow
            OperatorList.Add(new MathOperator("\u295A") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTeeVector
            OperatorList.Add(new MathOperator("\u21BC") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftVector
            OperatorList.Add(new MathOperator("\u2952") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftVectorBar
            OperatorList.Add(new MathOperator("\u2199") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LowerLeftArrow
            OperatorList.Add(new MathOperator("\u2198") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LowerRightArrow
            OperatorList.Add(new MathOperator("\u2192") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightArrow
            OperatorList.Add(new MathOperator("\u21E5") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightArrowBar
            OperatorList.Add(new MathOperator("\u21C4") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightArrowLeftArrow
            OperatorList.Add(new MathOperator("\u21A6") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTeeArrow
            OperatorList.Add(new MathOperator("\u295B") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTeeVector
            OperatorList.Add(new MathOperator("\u21C0") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightVector
            OperatorList.Add(new MathOperator("\u2953") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightVectorBar
            // Commented out: collides with LeftArrow
            // OperatorList.Add(new MathOperator("\u2190"){Form="infix", LSpace="thickmathspace", RSpace="thickmathspace" }); // ShortLeftArrow
            // Commented out: collides with RightArrow
            // OperatorList.Add(new MathOperator("\u2192"){Form="infix", LSpace="thickmathspace", RSpace="thickmathspace" }); // ShortRightArrow
            OperatorList.Add(new MathOperator("\u2196") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // UpperLeftArrow
            OperatorList.Add(new MathOperator("\u2197") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // UpperRightArrow
            OperatorList.Add(new MathOperator("=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("<") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator(">") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("!=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("==") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("<=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator(">=") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" });
            OperatorList.Add(new MathOperator("\u2261") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Congruent
            OperatorList.Add(new MathOperator("\u224D") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // CupCap
            OperatorList.Add(new MathOperator("\u2250") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DotEqual
            OperatorList.Add(new MathOperator("\u2225") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // DoubleVerticalBar
            OperatorList.Add(new MathOperator("\u2A75") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Equal
            OperatorList.Add(new MathOperator("\u2242") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // EqualTilde
            OperatorList.Add(new MathOperator("\u21CC") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Equilibrium
            OperatorList.Add(new MathOperator("\u2265") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterEqual
            OperatorList.Add(new MathOperator("\u22DB") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterEqualLess
            OperatorList.Add(new MathOperator("\u2267") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterFullEqual
            OperatorList.Add(new MathOperator("\u2AA2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterGreater
            OperatorList.Add(new MathOperator("\u2277") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterLess
            OperatorList.Add(new MathOperator("\u2A7E") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterSlantEqual
            OperatorList.Add(new MathOperator("\u2273") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // GreaterTilde
            OperatorList.Add(new MathOperator("\u224E") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // HumpDownHump
            OperatorList.Add(new MathOperator("\u224F") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // HumpEqual
            OperatorList.Add(new MathOperator("\u22B2") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTriangle
            OperatorList.Add(new MathOperator("\u29CF") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTriangleBar
            OperatorList.Add(new MathOperator("\u22B4") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LeftTriangleEqual
            OperatorList.Add(new MathOperator("\u2264") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // le
            OperatorList.Add(new MathOperator("\u22DA") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessEqualGreater
            OperatorList.Add(new MathOperator("\u2266") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessFullEqual
            OperatorList.Add(new MathOperator("\u2276") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessGreater
            OperatorList.Add(new MathOperator("\u2AA1") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessLess
            OperatorList.Add(new MathOperator("\u2A7D") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessSlantEqual
            OperatorList.Add(new MathOperator("\u2272") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // LessTilde
            OperatorList.Add(new MathOperator("\u226B") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NestedGreaterGreater
            OperatorList.Add(new MathOperator("\u226A") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NestedLessLess
            OperatorList.Add(new MathOperator("\u2262") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotCongruent
            OperatorList.Add(new MathOperator("\u226D") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotCupCap
            OperatorList.Add(new MathOperator("\u2226") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotDoubleVerticalBar
            OperatorList.Add(new MathOperator("\u2260") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotEqual
            OperatorList.Add(new MathOperator("\u2242\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotEqualTilde
            OperatorList.Add(new MathOperator("\u226F") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreater
            OperatorList.Add(new MathOperator("\u2271") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterEqual
            OperatorList.Add(new MathOperator("\u2266\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterFullEqual
            OperatorList.Add(new MathOperator("\u226B\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterGreater
            OperatorList.Add(new MathOperator("\u2279") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterLess
            OperatorList.Add(new MathOperator("\u2A7E\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterSlantEqual
            OperatorList.Add(new MathOperator("\u2275") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotGreaterTilde
            OperatorList.Add(new MathOperator("\u224E\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotHumpDownHump
            OperatorList.Add(new MathOperator("\u224F\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotHumpEqual
            OperatorList.Add(new MathOperator("\u22EA") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLeftTriangle
            OperatorList.Add(new MathOperator("\u29CF\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLeftTriangleBar
            OperatorList.Add(new MathOperator("\u22EC") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLeftTriangleEqual
            OperatorList.Add(new MathOperator("\u226E") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLess
            OperatorList.Add(new MathOperator("\u2270") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLessEqual
            OperatorList.Add(new MathOperator("\u2278") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLessGreater
            OperatorList.Add(new MathOperator("\u226A\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLessLess
            OperatorList.Add(new MathOperator("\u2A7D\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLessSlantEqual
            OperatorList.Add(new MathOperator("\u2274") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotLessTilde
            OperatorList.Add(new MathOperator("\u2AA2\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotNestedGreaterGreater
            OperatorList.Add(new MathOperator("\u2AA1\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotNestedLessLess
            OperatorList.Add(new MathOperator("\u2280") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotPrecedes
            OperatorList.Add(new MathOperator("\u2AAF\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotPrecedesEqual
            OperatorList.Add(new MathOperator("\u22E0") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotPrecedesSlantEqual
            OperatorList.Add(new MathOperator("\u22EB") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotRightTriangle
            OperatorList.Add(new MathOperator("\u29D0\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotRightTriangleBar
            OperatorList.Add(new MathOperator("\u22ED") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotRightTriangleEqual
            OperatorList.Add(new MathOperator("\u2281") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSucceeds
            OperatorList.Add(new MathOperator("\u2AB0\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSucceedsEqual
            OperatorList.Add(new MathOperator("\u22E1") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSucceedsSlantEqual
            OperatorList.Add(new MathOperator("\u227F\u0338") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotSucceedsTilde
            OperatorList.Add(new MathOperator("\u2241") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotTilde
            OperatorList.Add(new MathOperator("\u2244") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotTildeEqual
            OperatorList.Add(new MathOperator("\u2247") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotTildeFullEqual
            OperatorList.Add(new MathOperator("\u2249") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotTildeTilde
            OperatorList.Add(new MathOperator("\u2224") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // NotVerticalBar
            OperatorList.Add(new MathOperator("\u227A") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Precedes
            OperatorList.Add(new MathOperator("\u2AAF") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // PrecedesEqual
            OperatorList.Add(new MathOperator("\u227C") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // PrecedesSlantEqual
            OperatorList.Add(new MathOperator("\u227E") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // PrecedesTilde
            OperatorList.Add(new MathOperator("\u2237") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Proportion
            OperatorList.Add(new MathOperator("\u221D") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Proportional
            OperatorList.Add(new MathOperator("\u21CB") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // ReverseEquilibrium
            OperatorList.Add(new MathOperator("\u22B3") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTriangle
            OperatorList.Add(new MathOperator("\u29D0") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTriangleBar
            OperatorList.Add(new MathOperator("\u22B5") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // RightTriangleEqual
            OperatorList.Add(new MathOperator("\u227B") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Succeeds
            OperatorList.Add(new MathOperator("\u2AB0") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SucceedsEqual
            OperatorList.Add(new MathOperator("\u227D") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SucceedsSlantEqual
            OperatorList.Add(new MathOperator("\u227F") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // SucceedsTilde
            OperatorList.Add(new MathOperator("\u223C") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // Tilde
            OperatorList.Add(new MathOperator("\u2243") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // TildeEqual
            OperatorList.Add(new MathOperator("\u2245") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // TildeFullEqual
            OperatorList.Add(new MathOperator("\u2248") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // TildeTilde
            OperatorList.Add(new MathOperator("\u22A5") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // UpTee
            OperatorList.Add(new MathOperator("\u2223") { Form = "infix", LSpace = "thickmathspace", RSpace = "thickmathspace" }); // VerticalBar
            OperatorList.Add(new MathOperator("\u2294") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // SquareUnion
            OperatorList.Add(new MathOperator("\u22C3") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // Union
            OperatorList.Add(new MathOperator("\u228E") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // UnionPlus
            OperatorList.Add(new MathOperator("-") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" });
            // Added an entry for minus sign, separate from hyphen
            OperatorList.Add(new MathOperator("\u2212") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" });
            OperatorList.Add(new MathOperator("+") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" });
            OperatorList.Add(new MathOperator("\u22C2") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // Intersection
            OperatorList.Add(new MathOperator("\u2213") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // MinusPlus
            OperatorList.Add(new MathOperator("\u00B1") { Form = "infix", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // PlusMinus
            OperatorList.Add(new MathOperator("\u2293") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "mediummathspace", RSpace = "mediummathspace" }); // SquareIntersection
            OperatorList.Add(new MathOperator("\u22C1") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Vee
            OperatorList.Add(new MathOperator("\u2296") { Form = "prefix", LargeOp = "true", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" }); // CircleMinus
            OperatorList.Add(new MathOperator("\u2295") { Form = "prefix", LargeOp = "true", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" }); // CirclePlus
            OperatorList.Add(new MathOperator("\u2211") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Sum
            OperatorList.Add(new MathOperator("\u22C3") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Union
            OperatorList.Add(new MathOperator("\u228E") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // UnionPlus
            OperatorList.Add(new MathOperator("lim") { Form = "prefix", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" });
            OperatorList.Add(new MathOperator("max") { Form = "prefix", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" });
            OperatorList.Add(new MathOperator("min") { Form = "prefix", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" });
            OperatorList.Add(new MathOperator("\u2296") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // CircleMinus
            OperatorList.Add(new MathOperator("\u2295") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // CirclePlus
            OperatorList.Add(new MathOperator("\u2232") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // ClockwiseContourIntegral
            OperatorList.Add(new MathOperator("\u222E") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // ContourIntegral
            OperatorList.Add(new MathOperator("\u2233") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // CounterClockwiseContourIntegral
            OperatorList.Add(new MathOperator("\u222F") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // DoubleContourIntegral
            OperatorList.Add(new MathOperator("\u222B") { Form = "prefix", LargeOp = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "0em" }); // Integral
            OperatorList.Add(new MathOperator("\u22D3") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Cup
            OperatorList.Add(new MathOperator("\u22D2") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Cap
            OperatorList.Add(new MathOperator("\u2240") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // VerticalTilde
            OperatorList.Add(new MathOperator("\u22C0") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Wedge
            OperatorList.Add(new MathOperator("\u2297") { Form = "prefix", LargeOp = "true", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" }); // CircleTimes
            OperatorList.Add(new MathOperator("\u2210") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Coproduct
            OperatorList.Add(new MathOperator("\u220F") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Product
            OperatorList.Add(new MathOperator("\u22C2") { Form = "prefix", LargeOp = "true", MovableLimits = "true", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "thinmathspace" }); // Intersection
            OperatorList.Add(new MathOperator("\u2210") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Coproduct
            OperatorList.Add(new MathOperator("\u22C6") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Star
            OperatorList.Add(new MathOperator("\u2299") { Form = "prefix", LargeOp = "true", MovableLimits = "true", LSpace = "0em", RSpace = "thinmathspace" }); // CircleDot
            OperatorList.Add(new MathOperator("*") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" });
            OperatorList.Add(new MathOperator("\u2062") { Form = "infix", LSpace = "0em", RSpace = "0em" }); // InvisibleTimes
            OperatorList.Add(new MathOperator("\u00B7") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // CenterDot
            OperatorList.Add(new MathOperator("\u2297") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // CircleTimes
            OperatorList.Add(new MathOperator("\u22C1") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Vee
            OperatorList.Add(new MathOperator("\u22C0") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Wedge
            OperatorList.Add(new MathOperator("\u22C4") { Form = "infix", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Diamond
            OperatorList.Add(new MathOperator("\u2216") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "thinmathspace", RSpace = "thinmathspace" }); // Backslash
            OperatorList.Add(new MathOperator("/") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "thinmathspace", RSpace = "thinmathspace" });
            OperatorList.Add(new MathOperator("-") { Form = "prefix", LSpace = "0em", RSpace = "veryverythinmathspace" });
            // Added an entry for minus sign, separate from hyphen
            OperatorList.Add(new MathOperator("\u2212") { Form = "prefix", LSpace = "0em", RSpace = "veryverythinmathspace" });
            OperatorList.Add(new MathOperator("+") { Form = "prefix", LSpace = "0em", RSpace = "veryverythinmathspace" });
            OperatorList.Add(new MathOperator("\u2213") { Form = "prefix", LSpace = "0em", RSpace = "veryverythinmathspace" }); // MinusPlus
            OperatorList.Add(new MathOperator("\u00B1") { Form = "prefix", LSpace = "0em", RSpace = "veryverythinmathspace" }); // PlusMinus
            OperatorList.Add(new MathOperator(".") { Form = "infix", LSpace = "0em", RSpace = "0em" });
            OperatorList.Add(new MathOperator("\u2A2F") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // Cross
            OperatorList.Add(new MathOperator("**") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("\u2299") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // CircleDot
            OperatorList.Add(new MathOperator("\u2218") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // SmallCircle
            OperatorList.Add(new MathOperator("\u25A1") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" }); // Square
            OperatorList.Add(new MathOperator("\u2207") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" }); // Del
            OperatorList.Add(new MathOperator("\u2202") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" }); // PartialD
            OperatorList.Add(new MathOperator("\u2145") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" }); // CapitalDifferentialD
            OperatorList.Add(new MathOperator("\u2146") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" }); // DifferentialD
            OperatorList.Add(new MathOperator("\u221A") { Form = "prefix", Stretchy = "true", Scaling = "uniform", LSpace = "0em", RSpace = "verythinmathspace" }); // Sqrt
            OperatorList.Add(new MathOperator("\u21D3") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleDownArrow
            OperatorList.Add(new MathOperator("\u27F8") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleLongLeftArrow
            OperatorList.Add(new MathOperator("\u27FA") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleLongLeftRightArrow
            OperatorList.Add(new MathOperator("\u27F9") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleLongRightArrow
            OperatorList.Add(new MathOperator("\u21D1") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleUpArrow
            OperatorList.Add(new MathOperator("\u21D5") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DoubleUpDownArrow
            OperatorList.Add(new MathOperator("\u2193") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DownArrow
            OperatorList.Add(new MathOperator("\u2913") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DownArrowBar
            OperatorList.Add(new MathOperator("\u21F5") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DownArrowUpArrow
            OperatorList.Add(new MathOperator("\u21A7") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // DownTeeArrow
            OperatorList.Add(new MathOperator("\u2961") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftDownTeeVector
            OperatorList.Add(new MathOperator("\u21C3") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftDownVector
            OperatorList.Add(new MathOperator("\u2959") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftDownVectorBar
            OperatorList.Add(new MathOperator("\u2951") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftUpDownVector
            OperatorList.Add(new MathOperator("\u2960") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftUpTeeVector
            OperatorList.Add(new MathOperator("\u21BF") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftUpVector
            OperatorList.Add(new MathOperator("\u2958") { Form = "infix", Stretchy = "true", Scaling = "uniform", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LeftUpVectorBar
            OperatorList.Add(new MathOperator("\u27F5") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LongLeftArrow
            OperatorList.Add(new MathOperator("\u27F7") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LongLeftRightArrow
            OperatorList.Add(new MathOperator("\u27F6") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // LongRightArrow
            OperatorList.Add(new MathOperator("\u296F") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // ReverseUpEquilibrium
            OperatorList.Add(new MathOperator("\u295D") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightDownTeeVector
            OperatorList.Add(new MathOperator("\u21C2") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightDownVector
            OperatorList.Add(new MathOperator("\u2955") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightDownVectorBar
            OperatorList.Add(new MathOperator("\u294F") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightUpDownVector
            OperatorList.Add(new MathOperator("\u295C") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightUpTeeVector
            OperatorList.Add(new MathOperator("\u21BE") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightUpVector
            OperatorList.Add(new MathOperator("\u2954") { Form = "infix", Stretchy = "true", Scaling = "horizontal", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // RightUpVectorBar
            // Commented out: collides with DownArrow
            // OperatorList.Add(new MathOperator("\u2193"){Form="infix", LSpace="verythinmathspace", RSpace="verythinmathspace" }); // ShortDownArrow
            // Commented out: collides with UpArrow
            // OperatorList.Add(new MathOperator("\u2191"){Form="infix", LSpace="verythinmathspace", RSpace="verythinmathspace" }); // ShortUpArrow
            OperatorList.Add(new MathOperator("\u2191") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpArrow
            OperatorList.Add(new MathOperator("\u2912") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpArrowBar
            OperatorList.Add(new MathOperator("\u21C5") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpArrowDownArrow
            OperatorList.Add(new MathOperator("\u2195") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpDownArrow
            OperatorList.Add(new MathOperator("\u296E") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpEquilibrium
            OperatorList.Add(new MathOperator("\u21A5") { Form = "infix", Stretchy = "true", Scaling = "vertical", LSpace = "verythinmathspace", RSpace = "verythinmathspace" }); // UpTeeArrow
            OperatorList.Add(new MathOperator("^") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("<>") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("'") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            // Added an entry for prime, separate from apostrophe
            OperatorList.Add(new MathOperator("\u2032") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("!") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("!!") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("~") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("@") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("--") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("--") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("++") { Form = "postfix", LSpace = "verythinmathspace", RSpace = "0em" });
            OperatorList.Add(new MathOperator("++") { Form = "prefix", LSpace = "0em", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("\u2061") { Form = "infix", LSpace = "0em", RSpace = "0em" }); // ApplyFunction
            OperatorList.Add(new MathOperator("?") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("_") { Form = "infix", LSpace = "verythinmathspace", RSpace = "verythinmathspace" });
            OperatorList.Add(new MathOperator("\u02D8") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // Breve
            OperatorList.Add(new MathOperator("\u00B8") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // Cedilla
            OperatorList.Add(new MathOperator("`") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DiacriticalGrave
            OperatorList.Add(new MathOperator("\u02D9") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DiacriticalDot
            OperatorList.Add(new MathOperator("\u02DD") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DiacriticalDoubleAcute
            OperatorList.Add(new MathOperator("\u2190") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // LeftArrow
            OperatorList.Add(new MathOperator("\u2194") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // LeftRightArrow
            OperatorList.Add(new MathOperator("\u294E") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // LeftRightVector
            OperatorList.Add(new MathOperator("\u21BC") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // LeftVector
            OperatorList.Add(new MathOperator("\u00B4") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DiacriticalAcute
            OperatorList.Add(new MathOperator("\u2192") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // RightArrow
            OperatorList.Add(new MathOperator("\u21C0") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // RightVector
            OperatorList.Add(new MathOperator("\u02DC") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // DiacriticalTilde
            OperatorList.Add(new MathOperator("\u00A8") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DoubleDot
            OperatorList.Add(new MathOperator("\u0311") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // DownBreve
            OperatorList.Add(new MathOperator("\u02C7") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // Hacek
            OperatorList.Add(new MathOperator("^") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // Hat
            OperatorList.Add(new MathOperator("\u00AF") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // OverBar
            OperatorList.Add(new MathOperator("\uFE37") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // OverBrace
            OperatorList.Add(new MathOperator("\u23B4") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // OverBracket
            OperatorList.Add(new MathOperator("\uFE35") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // OverParenthesis
            OperatorList.Add(new MathOperator("\u20DB") { Form = "postfix", Accent = "true", LSpace = "0em", RSpace = "0em" }); // TripleDot
            OperatorList.Add(new MathOperator("\u0332") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // UnderBar
            OperatorList.Add(new MathOperator("\uFE38") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // UnderBrace
            OperatorList.Add(new MathOperator("\u23B5") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // UnderBracket
            OperatorList.Add(new MathOperator("\uFE36") { Form = "postfix", Accent = "true", Stretchy = "true", Scaling = "horizontal", LSpace = "0em", RSpace = "0em" }); // UnderParenthesis
        }

        public MathOperator LookUp(string op, string form = "infix")
        {
            MathOperator mop = OperatorList.Where(x => x.Key() == (op + form)).FirstOrDefault();
            if (mop != null)
                return mop;

            foreach (string dform in m_forms)
            {
                mop = OperatorList.Where(x => x.Key() == (op + dform)).FirstOrDefault();
                if (mop != null)
                    return mop;
            }
            return OperatorList.Where(x => x.Key() == ("infix")).FirstOrDefault();
        }

        public readonly List<MathOperator> OperatorList;
        readonly List<string> m_forms = new List<string>() { "infix", "postfix", "prefix" };
    }

}
