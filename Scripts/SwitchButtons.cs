using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NubikClicker
{
    public class SwitchButtons : MonoBehaviour
    {
        public Image buyBtn;
        public Image sellBtn;
        public Image enBtn;
        public Image ruBtn;
        public GameManager gameManager;
        private List<Text> priceOfTool;
        private List<Text> cntOfTools;
        public FormatNumbers format;
        public NubikeMultiLangStartText multiLang;
        public CycleText cycleText;
        List<int> addPrices = new List<int> { 2, 20, 200, 1200, 15000, 300000, 1300000, 11000000 };

        private void Awake()
        {
            priceOfTool = gameManager.GetComponent<BuyNewTools>().priceOfTool;
            cntOfTools = gameManager.GetComponent<BuyNewTools>().cntOfTool;
        }
        public void ChooseBuyBtn()
        {
            buyBtn.color = Color.red;
            sellBtn.color = Color.white;
            gameManager.isBuy = true;
        }

        public void ChooseSellBtn()
        {
            buyBtn.color = Color.white;
            sellBtn.color = Color.red;
            gameManager.isBuy = false;

        }

        public void ChooseEn()
        {
            Color buttonColor = enBtn.color;
            buttonColor.a = 1f;
            enBtn.color = buttonColor;
            Color buttonColor1 = ruBtn.color;
            buttonColor1.a = 0.5f;
            ruBtn.color = buttonColor1;
            PlayerPrefs.SetString("Lang", "EN");
            multiLang.Start();
            cycleText.ChangeImmediatelly();
        }

        public void ChooseRu()
        {
            Color buttonColor = enBtn.color;
            buttonColor.a = 0.5f;
            enBtn.color = buttonColor;

            Color buttonColor1 = ruBtn.color;
            buttonColor1.a = 1f;
            ruBtn.color = buttonColor1;
            PlayerPrefs.SetString("Lang", "RU");
            multiLang.Start();
            cycleText.ChangeImmediatelly();
        }
    }
}


