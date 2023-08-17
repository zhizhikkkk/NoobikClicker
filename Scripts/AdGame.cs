using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static UnityEngine.AudioSettings;

namespace NubikClicker
{
    public class AdGame : MonoBehaviour
    {
        public YandexGame sdk;
        public ClickOnCoockie clickOnCoockie1;
        public GameObject adStarts;
        public float timerDuration = 30f;

        private float timer = 0f;
        private bool isTimerRunning = false;

        public Text timerText;

        float adScreenTime = 70;

        private void Start()
        {
            timerText.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (isTimerRunning)
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    TimerExpired();
                    isTimerRunning = false;
                }
                UpdateTimerText();
            }

            adScreenTime -= Time.deltaTime;
            if(adScreenTime <= 3)
            {
                if (!adStarts.activeSelf)
                {
                    adStarts.SetActive(true);
                }
            }
            if(adScreenTime <= 0)
            {
                adStarts.SetActive(false);
                sdk._FullscreenShow();
                adScreenTime = 70;
            }
        }
        private void TimerExpired()
        {
            clickOnCoockie1.amount = 1;
            timerText.gameObject.SetActive(false);
        }
        public void StartTimer()
        {
            timer = timerDuration;
            isTimerRunning = true;
            timerText.gameObject.SetActive(true);
        }

        public void AdButton()
        {
            if (timer <= 0)
            {
                sdk._RewardedShow(1);
            }
        }

        public void AdButtonCul()
        {
            if (timer <= 0)
            {
                StartTimer();
                clickOnCoockie1.amount = 10;
            }
        }
        private void UpdateTimerText()
        {
            if (timerText != null)
            {
                timerText.text = Mathf.CeilToInt(timer).ToString();
            }
        }
    }
}
