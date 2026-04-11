# SvgMath.Benchmarks

BenchmarkDotNet jobs for [`Mml.MakeSvg`](../../src/SvgMath/Mml.cs) (and optional SVG serialization matching golden tests). Use **Release** for comparable numbers.

**Prerequisites:** Same as tests — fixtures link to `test/SvgMath.Tests/TestAssets` and `configuration/fontdata` is copied from the library (see `.csproj`).

## Commands

Run all benchmarks (default job; can take several minutes):

```bash
dotnet run -c Release --project benchmarks/SvgMath.Benchmarks
```

Filter by method name:

```bash
dotnet run -c Release --project benchmarks/SvgMath.Benchmarks -- --filter *SmallSample*
```

Faster smoke run (fewer iterations):

```bash
dotnet run -c Release --project benchmarks/SvgMath.Benchmarks -- --job short --filter *SmallSample_MakeSvg*
```

Reports are written under `BenchmarkDotNet.Artifacts/` (ignored by git when generated from the repo root).

## Jobs

| Benchmark | Role |
|-----------|------|
| `SmallSample_MakeSvg` | `MmlSamples/test1.mml` |
| `LargeTorture_MakeSvg` | `w3tests/.../complex1.mml` |
| `EntityNames_MakeSvg` | entity-heavy W3C sample |
| `SmallSample_MakeSvg_And_SerializeLikeGoldens` | same as golden tests (MakeSvg + XML write) |
| `W3C_Batch10_MakeSvg` | ten mixed presentation fixtures in one measurement |

## Full test duration (harness)

Wall-clock for the W3C suite only (PowerShell):

```powershell
Measure-Command { dotnet test test/SvgMath.Tests/SvgMath.Tests.csproj -c Release --filter Suite=W3C }
```
