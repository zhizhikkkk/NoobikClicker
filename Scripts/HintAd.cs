using System.Threading.Tasks;
using UnityEngine;

namespace NubikClicker
{
    public class HintAd : MonoBehaviour
    {
        public GameObject hint;

        public async void ShowHint()
        {
            hint.SetActive(true);
            await Task.Delay(3000);
            hint.SetActive(false);
        }
    }
}

