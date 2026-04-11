using BenchmarkDotNet.Running;

namespace SvgMath.Benchmarks
{
	static class Program
	{
		static void Main(string[] args)
		{
			// Examples:
			//   dotnet run -c Release --project benchmarks/SvgMath.Benchmarks
			//   dotnet run -c Release --project benchmarks/SvgMath.Benchmarks -- --filter *MakeSvg*
			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
		}
	}
}
