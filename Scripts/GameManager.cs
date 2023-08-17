using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace NubikClicker
{
    public class GameManager : MonoBehaviour
    {
        public AudioSource nubikTap;
        public AudioClip[] nubikeTapSounds;

        public YandexGame sdk;

        [HideInInspector] public bool isBuy = true;

        void Start()
        {
            isBuy = true;
            sdk._FullscreenShow();
        }

        public void NubikTap()
        {
            int rand = Random.Range(0, nubikeTapSounds.Length);
            nubikTap.clip = nubikeTapSounds[rand];
            nubikTap.PlayOneShot(nubikTap.clip);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);

        }
        public void LoadAdditionalScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}

