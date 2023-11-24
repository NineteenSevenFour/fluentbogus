[![Github CI](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/ci.yaml/badge.svg)](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/ci.yaml) [![github CD](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/cd.yaml/badge.svg)](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/cd.yaml) [![codecov](https://codecov.io/gh/NineteenSevenFour/fluentbogus/branch/main/graph/badge.svg?token=cXAu8BCw8d)](https://codecov.io/gh/NineteenSevenFour/gate) [![CodeQL](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/NineteenSevenFour/fluentbogus/actions/workflows/github-code-scanning/codeql)
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

# FluentBogus/Relation

AutoFaker provides a convenient FinishWith() action which can be used to 
finalize the data generation such as assigning foreign keys for dependancies.

When overriding AutoFaker<> to have reusable Fakers, the FinishWith() can also 
be defined, this is where FluentBogus/Relation shines as it provides a fluent
language to define the relation between the models and automatically update 
the properties defined in the relation.

As this is using the .net Expression<> to define the relation between the models,
it is compile time safe and use the out-of-the-box intellisense of any recent IDE.

## Contributors
<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="14.28%"><a href="https://www.linkedin.com/in/brunoduval/"><img src="https://avatars.githubusercontent.com/u/48152847?v=4?s=100" width="100px;" alt="Bruno DUVAL"/><br /><sub><b>Bruno DUVAL</b></sub></a><br /><a href="https://github.com/nineteensevenfour/fluentbogus/commits?author=datatunning" title="Code">💻</a> <a href="https://github.com/nineteensevenfour/fluentbogus/commits?author=datatunning" title="Documentation">📖</a> <a href="#projectManagement-datatunning" title="Project Management">📆</a> <a href="https://github.com/nineteensevenfour/fluentbogus/pulls?q=is%3Apr+reviewed-by%3Adatatunning" title="Reviewed Pull Requests">👀</a> <a href="https://github.com/nineteensevenfour/fluentbogus/commits?author=datatunning" title="Tests">⚠️</a> <a href="#translation-datatunning" title="Translation">🌍</a></td>
    </tr>
  </tbody>
  <tfoot>
    <tr>
      <td align="center" size="13px" colspan="7">
        <img src="https://raw.githubusercontent.com/all-contributors/all-contributors-cli/1b8533af435da9854653492b1327a23a4dbd0a10/assets/logo-small.svg">
          <a href="https://all-contributors.js.org/docs/en/bot/usage">Add your contributions</a>
        </img>
      </td>
    </tr>
  </tfoot>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

## Powered by

```text
  _   _ _            _                   _____                      ______               
 | \ | (_)          | |                 / ____|                    |  ____|              
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | (___   _____   _____ _ __ | |__ ___  _   _ _ __ 
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \ \___ \ / _ \ \ / / _ \ '_ \|  __/ _ \| | | | '__|
 | |\  | | | | |  __/ ||  __/  __/ | | |____) |  __/\ V /  __/ | | | | | (_) | |_| | |   
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_____/ \___| \_/ \___|_| |_|_|  \___/ \__,_|_|
```
