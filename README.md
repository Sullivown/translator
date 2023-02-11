# Translator

An ASP.NET MVC application that takes a string from the user and returns said string in 'l33tsp34k' using the [Fun Translations API](https://funtranslations.com/api/).

# Features
- Translate text to chosen translation
- Additional translations can be added easily via the online form
- Record API calls and responses in view in the Call Logger
- Translations and calls ca be sorted and filtered via the search field


# User Guide

Type or paste text to be translated into the '**Text to Translate**' input and click the 'Translate' button to translate the text into the selected translation. The translation can be changed by selecting from the '**Choose Translator**' dropdown (currently only Leetspeak is available).
The translated text can be selected and copied to the clipboard manually or by using the '**Copy Translation to Clipboard**' button.

# Admin Guide

## View, create, edit and delete Translations

Translations can be viewed at [/Translations](#) or by navigating via the menu.
Translation fields:

| Field | Description |
| ------ | ------ |
| Id | Database Id (cannot be manually assigned or edited) |
| Name | Display name for the translation (e.g. Leetspeak, Klingon, etc.) |
| Url | API Translation URL (e.g. [https://api.funtranslations.com/translate/leetspeak.json](https://api.funtranslations.com/translate/leetspeak.json) |

## View, create, edit and delete Calls

API Calls and responses can be viewed at [/Calls](#) or by navigating via the menu.
Call fields:

| Field | Description |
| ------ | ------ |
| Id | Database Id (cannot be manually assigned or edited) |
| Original Text | Original text inputted by the user |
| Translator Url | API Translation URL |
| Successful? | Boolean recording if the API call was successful |
| Translated Text | Translated text returned from the API |
| Date Created | Date/time of the call |

# Future Improvements & Features
- Add unit tests
- Translator and calls search/filtering using AJAX
- Add new feature to allow translation back to original text

