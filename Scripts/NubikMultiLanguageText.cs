using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;
using System;
using SimpleGoogleTranslation;
using TMPro;

namespace NubikClicker
{
[ExecuteAlways]
public class NubikMultiLanguageText : MonoBehaviour
{
    [Title("Russian - Main Text", TitleAlignment = TitleAlignments.Centered)]
    [TextArea]
    [HideLabel]
    [HorizontalGroup("ru")]
    [VerticalGroup("ru/string")]
    [OnValueChanged(nameof(AddTranslates))]
    public string russian;

    [Title("English", TitleAlignment = TitleAlignments.Centered)]
    [TextArea]
    [HideLabel]
    [HorizontalGroup("en")]
    [VerticalGroup("en/string")]
    [OnValueChanged(nameof(AddTranslates))]
    public string english;

    [ReadOnly]
    [MultiLineProperty(3)]
    public List<string> translates;

    Text text;
    string languageCode = "";

    [Obsolete]
    [Title("", TitleAlignment = TitleAlignments.Centered)]
    [VerticalGroup("en/button")]
    [Button(ButtonHeight = 29)]
    [LabelWidth(1)]
    [LabelText("Google Translate")]
    void En()//English
    {
        AdvancedTranslate("en", russian);
        AddTranslates();
    }

    private void OnEnable()
    {
        if (!Application.isPlaying)
        {
            NubikeMultiLangStartText multiLangStart = FindObjectOfType<NubikeMultiLangStartText>();
            if(multiLangStart != null)
            {
                if (!multiLangStart.multiLangTexts.Contains(GetComponent<Text>()))
                {
                    multiLangStart.multiLangTexts.Add(GetComponent<Text>());
                    multiLangStart.multiLangs.Add(this);
                }
            }

            AddTranslates();
        }
    }

    void AddTranslates()
    {
        translates = new List<string>();

        translates.Add(english);
        translates.Add(russian);
    }

    public void AdvancedTranslate(string targetLang, string sourceText)
    {
        if(sourceText != "")
        {
            languageCode = targetLang;
            GoogleTranslationHelper.Trans("en", targetLang, sourceText, AdvancedHandleCallback);
        }
    }

    void AdvancedHandleCallback(List<string> str)
    {
        string final = str[0];

        if (languageCode == "en")
            english = final;

        if (languageCode == "ru")
            russian = final;

        AddTranslates();
    }

    public string GetText()
    {
        if(translates.Count > 0)
        {
            string lang = PlayerPrefs.GetString("Lang");
            int index = 0;

            if (lang == "EN")//English
            {
                index = 0;
            }
            if (lang == "RU")//Russian
            {
                index = 1;
            }

            return translates[index];
        }
        else
        {
            return GetComponent<Text>().text;
        }
    }
}
}
