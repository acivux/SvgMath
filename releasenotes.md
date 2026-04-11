# Release notes

## 1.1.0

- **MathML (W3C MathML 2 / 3 / 4 presentation):** Child elements are parsed only in the MathML namespace (`http://www.w3.org/1998/Math/MathML`) or with no namespace (legacy unprefixed XML); other namespaces are skipped. The `math` element may carry a `version` attribute; attributes such as MathML 4 `intent` on tokens are stored but do not change SVG layout.
- **`semantics`:** The first expression child is used for rendering; `annotation` and `annotation-xml` are ignored for layout. If that first child is Content MathML (for example `apply`, `ci`, `csymbol`), layout throws a clear error—use presentation markup as the first child.
- **`mglyph`:** The `alt` string is drawn like other text tokens; `src` is not embedded as an image.
- **Elementary math:** `mstack` is lowered to `mtable`: `msrow`, `msgroup`, and `mscarries` become rows of cells; `msline` becomes a horizontal rule row; `mscarry` cells are unwrapped to their contents.
- **Tables:** `malignmark` and `maligngroup` are ignored during parsing (no sub-cell alignment).

## 0.2.1

- Clarified licensing: canonical [MIT](LICENSE) at the repository root, aligned assembly metadata, [NOTICE.md](NOTICE.md) for upstream and reference, and updated `src/SvgMath/LICENSE.txt` to match.

## 0.2

- Projects use the .NET SDK in SDK-style format and target **.NET 10** (`net10.0`). Building requires a [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

## 0.1

- The initial release was purely an effort to do the least amount of work to get a working C# port up and running.
