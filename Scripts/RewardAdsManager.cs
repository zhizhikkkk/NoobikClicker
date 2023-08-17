using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace NubikClicker
{
    public class RewardAdsManager : MonoBehaviour
    {
        public YandexGame sdk;
        public List<Tile> mobsTile;
        public Board board;

        public void AdButton()
        {
            sdk._RewardedShow(1);
        }

        public void AdButtonCul()
        {
            int randPos = Random.Range(0, board.tetrominos.Length);
            int randomTile = Random.Range(0, mobsTile.Count);

            board.tetrominos[randPos].tile = mobsTile[randomTile];

            PlayerPrefs.SetInt("Tetromino" + randPos.ToString(), randomTile);

        }
    }
}

