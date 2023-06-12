hammer-url
---

Simple load-testing tool: hammers the requested url as fast as possible with
the provided concurrency

Usage:
---
hammer-url.exe "http://www.some-service.net/path/to/api?a=b" -n 100

will attempt to retrieve the value at "http://www.some-service.net/path/to/api?a=b" as
quickly as possible with a maximum concurrency of 100
