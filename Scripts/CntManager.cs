using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NubikClicker
{
    public class CntManager : MonoBehaviour
    {
        private static CntManager instance;

        public Text curCoockieCnt;
        public Text curCoockiePerSec;
        public BuyNewTools gameManager;
        public int cnt = 0;
        private int cur = 1;
        private Text curSecondScene;
        public int cntOfHits = 0;
        public Text cntOfHitsText;

        private void Awake()
        {
            gameManager = FindObjectOfType<BuyNewTools>();
            curCoockieCnt = gameManager.cntOfMoney;
            curCoockiePerSec = gameManager.curCntPerSec;

            if (instance != null)
            {
                Destroy(instance.gameObject);
            }

            instance = this;
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            if (PlayerPrefs.GetString("Coockie").Length > 0)
            {
                cnt = Convert.ToInt32(PlayerPrefs.GetString("Coockie"));
            }
            else
            {
                cnt = 0;
            }

            if (PlayerPrefs.GetString("CoockiePerSec").Length > 0)
            {
                curCoockiePerSec.text = PlayerPrefs.GetString("CoockiePerSec");
            }
            else
            {
                curCoockiePerSec.text = "1";
            }
            if (PlayerPrefs.GetString("CntOfHits").Length > 0)
            {
                cntOfHits = Convert.ToInt32(PlayerPrefs.GetString("CntOfHits"));
                cntOfHitsText.text = cntOfHits.ToString();
            }
            else
            {
                cntOfHits = 0;
                cntOfHitsText.text = cntOfHits.ToString();
            }
            StartCoroutine(CoockieAddPerSec());
        }

        IEnumerator CoockieAddPerSec()
        {
            while (true)
            {
                if (curCoockiePerSec != null)
                {
                    cur = Convert.ToInt32(curCoockiePerSec.text);
                }

                cnt += cur;
                if (curCoockieCnt != null)
                {
                    curCoockieCnt.text = cnt.ToString();
                }
                else if (FindObjectOfType<BuyNewTools>())
                {
                    gameManager = FindObjectOfType<BuyNewTools>();
                    curCoockieCnt = gameManager.cntOfMoney;
                    curCoockiePerSec = gameManager.curCntPerSec;
                    curCoockieCnt.text = cnt.ToString();
                }
                else if (FindObjectOfType<CntMoney>() != null)
                {
                    curSecondScene = FindObjectOfType<CntMoney>().cntOfMoney;

                    curSecondScene.text = cnt.ToString();
                }

                PlayerPrefs.SetString("Coockie", cnt.ToString());

                yield return new WaitForSeconds(1f);
            }
        }

        //public string FormatNumber(int number)
        //{
        //    string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nd" };

        //    int suffixIndex = 0;
        //    float shortenedNumber = number;
        //    string format = "F0";

        //    if (number >= 10000)
        //    {
        //        while (shortenedNumber >= 1000f && suffixIndex < suffixes.Length - 1)
        //        {
        //            shortenedNumber /= 1000f;
        //            suffixIndex++;
        //        }
        //    }
        //    if (number >= 1000000)
        //    {
        //        format = "F1";
        //    }

        //    string formattedNumber = shortenedNumber.ToString(format) + suffixes[suffixIndex];
        //    return formattedNumber;
        //}

        public static CntManager Instance()
        {
            return instance;
        }

    }
}

