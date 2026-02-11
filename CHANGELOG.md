# Changelog

## [2.0.1](https://github.com/atc-net/atc-microsoft-graph-client/compare/v2.0.0...v2.0.1) (2026-02-11)


### Bug Fixes

* **one-drive:** properly await and use ToListAsync ([5d5977f](https://github.com/atc-net/atc-microsoft-graph-client/commit/5d5977f3d5ce7cccdb1899441445408add22e159))
* remove ClientSecret in GraphServiceOptions.ToString() ([a02100c](https://github.com/atc-net/atc-microsoft-graph-client/commit/a02100c4f7ac55069527caaaaa3451f8afc5486e))
* replace single 429 retry with Polly resilience pipeline using Retry-After header ([d844a03](https://github.com/atc-net/atc-microsoft-graph-client/commit/d844a03d2b62e14e459857a54992639f3921e86e))
