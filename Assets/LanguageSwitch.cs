using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSwitch : MonoBehaviour {
    public string languageCode;
    public void SetLocale(string languageCode) {
        //Žaidimo kalbos keitimas pagal duotą reikšmę
        switch (languageCode) {
            case "en":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                languageCode = "en"; 
                break;
            case "lt":
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                languageCode = "lt";
                break;
            default:
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                languageCode = "en";
                break;
        }
    }
}