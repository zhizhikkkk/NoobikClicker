using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace NubikClicker
{
    public class ClickOnCoockie : MonoBehaviour
    {
        public Text moneyText;
        public CntManager cntManager;
        public int amount = 1;
        public GameObject targetTransform;
        public Text cntOfHitsText;
        private void Start()
        {
            cntManager = FindObjectOfType<CntManager>();

        }

        private void Update()
        {
            if (cntManager.cntOfHits == 1000)
            {
                targetTransform.GetComponent<NubikAnimPro>().CallByName("Play");
            }
        }

        public void AddMoney(int cnt)
        {
            cntManager.cnt += cnt * amount;
            moneyText.text = (cntManager.cnt).ToString();
            cntManager.cntOfHits += amount;

            cntOfHitsText.text = cntManager.cntOfHits.ToString();
            PlayerPrefs.SetString("CntOfHits", cntManager.cntOfHits.ToString());

        }

    }
}

