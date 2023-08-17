using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace NubikClicker
{
    public class CycleText : MonoBehaviour
    {
        [TextArea]
        public string[] texts;  // ?????? ???????, ??????? ????? ???????? ????????
        [TextArea]
        public string[] enTexts;  // ?????? ???????, ??????? ????? ???????? ????????
        public float fadeDuration = 0.5f;  // ???????????? ?????????/????????? ?????? (? ????????)
        public float delayBetweenCycles = 2f;  // ???????? ????? ??????? (? ????????)

        private Text textComponent;
        private int currentIndex = 0;
        private bool isAnimating = false;

        private void Start()
        {
            textComponent = GetComponent<Text>();
            textComponent.text = texts[currentIndex];
            StartTextCycle();
        }

        private void StartTextCycle()
        {
            if (!isAnimating)
            {
                isAnimating = true;
                FadeOutText();
            }
        }

        private void FadeOutText()
        {
            textComponent.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                CycleToNextText();
                FadeInText();
            });
        }

        private void FadeInText()
        {
            textComponent.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                isAnimating = false;
                Invoke("StartTextCycle", delayBetweenCycles);
            });
        }

        private void CycleToNextText()
        {
            currentIndex = (currentIndex + 1) % texts.Length;

            ChangeImmediatelly();
        }

        public void ChangeImmediatelly()
        {
            if (PlayerPrefs.GetString("Lang") == "RU")
            {
                textComponent.text = texts[currentIndex];
            }
            else
            {
                textComponent.text = enTexts[currentIndex];
            }
        }
    }
}

