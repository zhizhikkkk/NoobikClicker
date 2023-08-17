using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NubikClicker
{
    public class DontDestroy : MonoBehaviour
    {
        private static DontDestroy instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}

