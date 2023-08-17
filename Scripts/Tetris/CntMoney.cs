using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NubikClicker
{
    public class CntMoney : MonoBehaviour
    {
        public Text cntOfMoney;
        private void Start()
        {
            CntManager coockie = CntManager.Instance();
            // cntOfMoney.text = PlayerPrefs.GetString("Coockie");
            if (coockie != null)
            {
                cntOfMoney.text = coockie.cnt.ToString();
            }
        }
    }
}

