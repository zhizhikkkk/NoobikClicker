using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NubikClicker
{
    public class BuyNewTools : MonoBehaviour
    {
        public Text cntOfMoney;
        public Text curCntPerSec;
        public List<Text> cntOfTool;

        public List<Text> priceOfTool;
        public GameManager gameManager;
        public FormatNumbers format;
        public CntManager cntManager;
        List<int> addPrices = new List<int> { 2, 20, 200, 1200, 15000, 300000, 1300000, 11000000 };

        private void Start()
        {
            cntManager = FindObjectOfType<CntManager>();
            cntOfMoney.text = cntManager.cnt.ToString();
            format.FormatNumber(Convert.ToInt32(curCntPerSec.text));

            for (int i = 0; i < priceOfTool.Count; i++)
            {
                format.FormatNumber(Convert.ToInt32(priceOfTool[i].text));
            }

            cntOfTool[0].text = PlayerPrefs.GetString("WoodCnt");
            cntOfTool[1].text = PlayerPrefs.GetString("StoneCnt");
            cntOfTool[2].text = PlayerPrefs.GetString("CopperCnt");
            cntOfTool[3].text = PlayerPrefs.GetString("SilverCnt");
            cntOfTool[4].text = PlayerPrefs.GetString("GoldCnt");
            cntOfTool[5].text = PlayerPrefs.GetString("DiamondCnt");
            cntOfTool[6].text = PlayerPrefs.GetString("NetheriteCnt");
            cntOfTool[7].text = PlayerPrefs.GetString("EnderPearlCnt");

            priceOfTool[0].text = PlayerPrefs.GetString("WoodPrice");
            priceOfTool[1].text = PlayerPrefs.GetString("StonePrice");
            priceOfTool[2].text = PlayerPrefs.GetString("CopperPrice");
            priceOfTool[3].text = PlayerPrefs.GetString("SilverPrice");
            priceOfTool[4].text = PlayerPrefs.GetString("GoldPrice");
            priceOfTool[5].text = PlayerPrefs.GetString("DiamondPrice");
            priceOfTool[6].text = PlayerPrefs.GetString("NetheritePrice");
            priceOfTool[7].text = PlayerPrefs.GetString("EnderPearlPrice");

            for (int i = 0; i < cntOfTool.Count; i++)
            {
                if (cntOfTool[i].text.Length == 0)
                {
                    cntOfTool[i].text = "0";
                }
            }

            for (int i = 0; i < priceOfTool.Count; i++)
            {
                if (priceOfTool[i].text.Length == 0)
                {
                    priceOfTool[i].text = (Math.Pow(10, i + 1)).ToString();
                }
            }
        }
        public void Buy(int val)
        {
            int curSec = Convert.ToInt32(format.ParseNumber(curCntPerSec.text));


            if (gameManager.isBuy)
            {
                if (cntManager.cnt >= Convert.ToInt32(priceOfTool[val - 1].text))
                {

                    cntOfTool[val - 1].text = (Convert.ToInt32(cntOfTool[val - 1].text) + 1).ToString();
                    cntManager.cnt -= Convert.ToInt32(priceOfTool[val - 1].text);
                    curCntPerSec.text = (format.FormatNumber((int)(curSec + Math.Pow(5, val - 1)))).ToString();
                    cntOfMoney.text = cntManager.cnt.ToString();
                    priceOfTool[val - 1].text = (Convert.ToInt32(priceOfTool[val - 1].text) + addPrices[val - 1]).ToString();

                    UpdateCntOfTool(val);
                    UpdatePriceOfTool(val);
                }
            }
            else
            {
                if (Convert.ToInt32(cntOfTool[val - 1].text) > 0)
                {

                    cntOfTool[val - 1].text = (Convert.ToInt32(cntOfTool[val - 1].text) - 1).ToString();
                    cntManager.cnt += Convert.ToInt32(priceOfTool[val - 1].text);
                    curCntPerSec.text = (format.FormatNumber((int)(curSec - Math.Pow(5, val - 1)))).ToString();
                    cntOfMoney.text = cntManager.cnt.ToString();
                    priceOfTool[val - 1].text = (Convert.ToInt32(priceOfTool[val - 1].text) - addPrices[val - 1]).ToString();
                    UpdateCntOfTool(val);
                    UpdatePriceOfTool(val);
                }
            }
            PlayerPrefs.SetString("CoockiePerSec", curCntPerSec.text);
        }

        public void UpdatePriceOfTool(int val)
        {
            if (val == 1)
            {
                PlayerPrefs.SetString("WoodPrice", priceOfTool[val - 1].text);
            }
            else if (val == 2)
            {
                PlayerPrefs.SetString("StonePrice", priceOfTool[val - 1].text);
            }
            else if (val == 3)
            {
                PlayerPrefs.SetString("CopperPrice", priceOfTool[val - 1].text);
            }
            else if (val == 4)
            {
                PlayerPrefs.SetString("SilverPrice", priceOfTool[val - 1].text);
            }
            else if (val == 5)
            {
                PlayerPrefs.SetString("GoldPrice", priceOfTool[val - 1].text);
            }
            else if (val == 6)
            {
                PlayerPrefs.SetString("DiamondPrice", priceOfTool[val - 1].text);
            }
            else if (val == 7)
            {
                PlayerPrefs.SetString("NetheritePrice", priceOfTool[val - 1].text);
            }
            else if (val == 8)
            {
                PlayerPrefs.SetString("EnderPearlPrice", priceOfTool[val - 1].text);
            }
        }
        public void UpdateCntOfTool(int val)
        {
            if (val == 1)
            {
                PlayerPrefs.SetString("WoodCnt", cntOfTool[val - 1].text);
            }
            else if (val == 2)
            {
                PlayerPrefs.SetString("StoneCnt", cntOfTool[val - 1].text);
            }
            else if (val == 3)
            {
                PlayerPrefs.SetString("CopperCnt", cntOfTool[val - 1].text);
            }
            else if (val == 4)
            {
                PlayerPrefs.SetString("SilverCnt", cntOfTool[val - 1].text);
            }
            else if (val == 5)
            {
                PlayerPrefs.SetString("GoldCnt", cntOfTool[val - 1].text);
            }
            else if (val == 6)
            {
                PlayerPrefs.SetString("DiamondCnt", cntOfTool[val - 1].text);
            }
            else if (val == 7)
            {
                PlayerPrefs.SetString("NetheriteCnt", cntOfTool[val - 1].text);
            }
            else if (val == 8)
            {
                PlayerPrefs.SetString("EnderPearlCnt", cntOfTool[val - 1].text);
            }
        }
    }

}
