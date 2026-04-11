# SvgMath

C# (minimal effort) port of the Python SvgMath utility at http://sourceforge.net/projects/svgmath/.

**Current release:** **1.1.0** — see [`releasenotes.md`](releasenotes.md) for changes (including broader MathML presentation handling).

**Layout:** C# library in [`src/SvgMath`](src/SvgMath), automated tests in [`test/SvgMath.Tests`](test/SvgMath.Tests) (including golden SVG baselines for MathML samples under `TestAssets/MmlSamples`), original Python 0.3.3 tree in [`src/reference/SVGMath-0.3.3`](src/reference/SVGMath-0.3.3) (reference only).

**Build:** Install a [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), then run `dotnet build SvgMathCs.sln` from the repository root.

**Benchmarks:** [`benchmarks/SvgMath.Benchmarks`](benchmarks/SvgMath.Benchmarks) (BenchmarkDotNet) measures `Mml.MakeSvg` on representative inputs; see that folder’s README for commands and for optional wall-clock timing of `dotnet test`.

Version history: [`releasenotes.md`](releasenotes.md).

### License

This repository is under the [MIT License](LICENSE). The same text is copied at [`src/SvgMath/LICENSE.txt`](src/SvgMath/LICENSE.txt) for builds that ship that file next to the library.

**Upstream:** SVGMath was created by Nikolai Grigoriev (<svgmath@grigoriev.ru>). The original software is Copyright (c) 2005 Nikolai Grigoriev and licensed under the MIT License.

**C# port:** The C# port and tooling in this repository are a derivative work offered under the same MIT terms. Copyright for the port is held by the SvgMath C# port contributors (see copyright lines in [LICENSE](LICENSE)).

Third-party and reference details are summarized in [NOTICE.md](NOTICE.md).
