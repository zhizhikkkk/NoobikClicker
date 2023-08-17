using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

namespace NubikClicker
{
[ExecuteAlways]
public class NubikeMultiLangStartText : MonoBehaviour
{
    public List<Text> multiLangTexts;
    public List<NubikMultiLanguageText> multiLangs;

    DateTime lastTime;

    [Button]
    void FindText()
    {
        string sceneName = gameObject.scene.name;

        multiLangs.Clear();
        multiLangTexts.Clear();

        foreach (Text gameObj in Resources.FindObjectsOfTypeAll(typeof(Text)) as Text[])
        {
            Text text = gameObj;
            NubikMultiLanguageText multiLanguage = text.GetComponent<NubikMultiLanguageText>();

            if (sceneName == text.gameObject.scene.name)
            {
                if (multiLanguage != null)
                {
                    if (!multiLangTexts.Contains(text))
                    {
                        multiLangTexts.Add(text);
                    }
                    if (!multiLangs.Contains(multiLanguage))
                    {
                        multiLangs.Add(multiLanguage);
                    }
                }
            }
        }
    }

    private void Awake()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            PlayerPrefs.SetString("Lang", "RU");
        }
        else
        {
            PlayerPrefs.SetString("Lang", "EN");
        }
    }

    public void Start()
    {
        Load();
    }

    private void Load()
    {
        if (Application.isPlaying)
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

            for (int x = 0; x < multiLangTexts.Count; x++)
            {
                Text text = multiLangTexts[x];

                if (text != null)
                {
                    NubikMultiLanguageText multiLanguage = null;

                    if (multiLangs[x] != null)
                    {
                        multiLanguage = multiLangs[x];
                    }

                    if (multiLanguage != null)
                    {
                        if (multiLanguage.translates.Count > 0)
                        {
                            text.text = multiLanguage.translates[index];
                        }
                        else
                        {
                            if (lang == "EN")//English
                            {
                                if (multiLanguage.english.Length > 0)
                                {
                                    text.text = multiLanguage.english;
                                }
                            }
                            if (lang == "RU")//Russian
                            {
                                if (multiLanguage.russian.Length > 0)
                                {
                                    text.text = multiLanguage.russian;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"<color=red>Error Multilanguage! Has null NubikMultiLanguageText</color>");
                    }
                }
                else
                {
                    Debug.Log($"<color=red>Error Multilanguage! Hasn't Text component</color>");
                }
            }
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if(DateTime.Now > lastTime)
            {
                multiLangs.RemoveAll(item => item == null);
                multiLangTexts.RemoveAll(item => item == null);

                if (multiLangs.Count != multiLangTexts.Count)
                {
                    Debug.LogWarning($"Multi language error, you must find new texts! In gameobject: {gameObject}", gameObject);
                }

                lastTime = DateTime.Now.AddSeconds(15);
            }
        }
    }
}
}
