# Changelog
All notable changes to this project will be documented in this file.

>The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- A dropdown script that can be used to make a dropdown for changing the current locale.
- A method to query the available locales from Il8n.
- A script that can be used to control the current locale e.g. from a UnityEvent: LocaleSetter

### Fixed

- The LocalizedText Editor
- Load locales from the correct path

## [0.1.1-alpha] - 2019-03-05

### Added

- Validation of loaded locales.
- This file.

### Fixed

- [#4]: Text not updating when locale changed

[#4]: https://github.com/ettmetal/Translations/issues/4

## 0.0.1-alpha - 2019-03-03

### Added

- Core translations engine for working with locales and retreving stored translations.
- Component for translating UI Texts.
- Ability to Edit translations in the Unity editor.
- Extended UI Text which does not require external translation component: LocalizedText.

### Fixed

- [#3]: Translations window fails to refresh
- [#2]: Creating new locale when Locales exist with keys results in editing failure
- [#1]: Creating locale fails if folder specified in settings does not exist

[#3]: https://github.com/ettmetal/Translations/issues/3
[#2]: https://github.com/ettmetal/Translations/issues/2
[#1]: https://github.com/ettmetal/Translations/issues/1

[Unreleased]: https://github.com/ettmetal/Translations/compare/v0.1.1-alpha...HEAD
[0.1.1-alpha]: https://github.com/ettmetal/Translations/compare/v0.1.0-alpha...v0.1.1-alpha
