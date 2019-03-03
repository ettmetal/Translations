# Translations 🌐
Localise UI text 📄 (and more) in your Unity games.

✨ __Features:__
- Edit translations inside Unity
- Extended [Text] component for Unity UI integration
- Sidecar component for adding to existing projects

🚧 __Translations is still under construction.__ *There is __no stable version__ yet* 🚧

__Current Version:__ [0.1.0-alpha](../../releases/tag/v0.1.0-alpha)

✨[Feature Wishlist](../../wiki/Wishlist)✨

[Text]: https://docs.unity3d.com/Manual/script-Text.html

## Getting Started

You don't need code for the basic functionality of translations to work.

>⏳🌌 *The screencap gif travelled back in time to let us know it'd be here, but couldn't stick around for now. It'll join us soon, don't worry!*

>1. Windows/Translations
>2. Add locales
>3. Setup strings & translations in editor
>4. Add TextTranslator component to your UI Texts (Or add a LocalizedText component)
>5. Select target string. Does not currently update at edit time.

If you want to do custom stuff, there's an API for that!

```csharp
using namespace Ettmetal.Translations;

Il8n.__("string-key"); // returns translation stored with "string-key" in the active locale.

Il8n.OnLocaleChanged += MyAction; // Subscribe an Action to be run when locale changes

Il8n.ChangeLocale("iso-locale-code"); // Changes the active locale to "iso-locale-code", e.g. "de", "fr-FR", etc.
```
Translations will manage storing and retrieving the current selected locale (using `PlayerPrefs`) automagically 🔮.

Full details can be found at the [API Wiki](../../wiki/API).

## Installing
For now, cloning the repository will clone a full Unity project. That's fine if you want to work on Translations itself, but you'll need to copy the full contents of the `Assets/Translations` folder into your own project's `Assets` folder if you want to use it.

Head over to [Releases](../../releases) and download the most recent version appropriate for your Unity version. Import the package into your Unity project.

## Contributing
Found a bug 🐛?
Have some feedback 💭?
Want to add a feature to the wishlist ✨?

Here are some ways you can help make Translations better:
- Open an [Issue](../../issues) 🐛💭✨
- Contact [@ettmetal] on Twitter 💭✨
- Send a [PR](../../pulls) 🐛✨

[@ettmetal]: https://twitter.com/ettmetal

## License
Copyright © 2019 [The Translations Contributors](../../graphs/contributors).

Translations is released under the MIT license. Refer to [LICENSE.md](LICENSE.md) in this repository for more information.
