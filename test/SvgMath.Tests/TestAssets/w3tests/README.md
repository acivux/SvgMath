# W3C MathML 3 test inputs (presentation subset in use)

These `.mml` files come from the **MathML 3 full testsuite** published by W3C (`build/main` tree), originally obtained from the bundled archive that mirrors that tree (see [MathML test suite overview](https://www.w3.org/Math/testsuite/overview.html) and [full suite overview](https://www.w3.org/Math/testsuite/build/main/overview.html)).

**Golden SVG baselines** used by `W3cMmlGoldenTests` live under `Golden/` with the same relative paths as the inputs. Only **presentation** MathML under this folder is exercised; `Content/` and `StrictContent/` are present in the corpus but excluded from SvgMath goldens (content MathML is not rendered by this engine).

## License

Unless otherwise stated, W3C test materials are copyright © W3C and are licensed for use under the **[W3C Document License](https://www.w3.org/Consortium/Legal/copyright-documents)**. See the [MathML test suite overview](https://www.w3.org/Math/testsuite/overview.html) for context and contribution terms.
