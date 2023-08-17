using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NubikClicker
{
    public class Hints : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public GameObject targetObject;

        public void OnPointerEnter(PointerEventData eventData)
        {
            targetObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            targetObject.SetActive(false);
        }

        public async void OnPointerClick(PointerEventData eventData)
        {
            targetObject.SetActive(true);
            await Task.Delay(1000);

            targetObject.SetActive(false);
        }
    }
}

