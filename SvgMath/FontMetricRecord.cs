namespace SvgMath
{
	public class FontMetricRecord
	{
		public FontMetricRecord(string family, FontMetric metric)
		{
			Family = family;
			Metric = metric;
			Used = false;
		}
		public string Family;
		public FontMetric Metric;
		public bool Used;
	}
}
