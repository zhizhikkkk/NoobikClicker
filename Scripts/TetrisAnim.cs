using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NubikClicker
{
    public class TetrisAnim : MonoBehaviour
    {
        void Start()
        {
            gameObject.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}

