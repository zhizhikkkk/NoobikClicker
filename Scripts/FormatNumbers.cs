using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NubikClicker
{
    public class FormatNumbers : MonoBehaviour
    {
        public string FormatNumber(int number)
        {

            return number.ToString();
        }

        public int ParseNumber(string formattedNumber)
        {
            //string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nd" };

            //string numberPart = formattedNumber;
            //string suffixPart = "";

            //foreach (string suffix in suffixes)
            //{
            //    if (numberPart.EndsWith(suffix))
            //    {
            //        suffixPart = suffix;
            //        numberPart = numberPart.Substring(0, numberPart.Length - suffixPart.Length);
            //        break;
            //    }
            //}

            //if (float.TryParse(numberPart, out float shortenedNumber))
            //{
            //    int suffixIndex = Array.IndexOf(suffixes, suffixPart);
            //    int multiplier = (int)Math.Pow(1000, suffixIndex);

            //    int originalNumber = (int)(shortenedNumber * multiplier);
            //    return originalNumber;
            //}

            //throw new ArgumentException("Invalid formatted number");
            return Convert.ToInt32(formattedNumber);
        }
    }
}

