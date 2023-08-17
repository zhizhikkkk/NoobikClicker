using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace NubikClicker
{
    public class Board : MonoBehaviour
    {
        public Tilemap tilemap { get; private set; }

        public Piece activepiece { get; private set; }
        public TetrominoData[] tetrominos;
        public Vector3Int spawnPosition;
        public Vector2Int boardSize = new Vector2Int(10, 20);
        public Text cntMoney;
        public AnimationPro screenTetris;
        public AnimationPro x2;
        public GameObject playTetris;
        public GameObject playButtons;
        private CntManager cntManager;
        public List<Tile> mobsTiles;
        public RectInt Bounds
        {
            get
            {
                Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
                return new RectInt(position, this.boardSize);
            }
        }

        [HideInInspector]
        public bool gameOver;

        private void Awake()
        {
            gameOver = true;
            this.tilemap = GetComponentInChildren<Tilemap>();
            this.activepiece = GetComponentInChildren<Piece>();
            for (int i = 0; i < this.tetrominos.Length; i++)
            {
                this.tetrominos[i].Initialize();
                if (PlayerPrefs.GetInt("Tetromino" + i.ToString()) != 0)
                {
                    tetrominos[i].tile = mobsTiles[PlayerPrefs.GetInt("Tetromino" + i.ToString())];
                }
                else
                {
                    tetrominos[i].tile = mobsTiles[i];
                }
            }
        }

        private void Start()
        {
            cntManager = FindObjectOfType<CntManager>();
        }

        public void SpawnPiece()
        {
            int random = Random.Range(0, this.tetrominos.Length);
            TetrominoData data = this.tetrominos[random];

            this.activepiece.Initialize(this, this.spawnPosition, data);

            if (IsValidPosition(this.activepiece, this.spawnPosition))
            {
                Set(this.activepiece);
            }
            else
            {
                GameOver();
            }
        }
        public void StartGame()
        {
            if (gameOver)
            {
                gameOver = false;
                playTetris.SetActive(false);
                playButtons.SetActive(true);
                SpawnPiece();
            }
        }
        private void GameOver()
        {
            gameOver = true;

            screenTetris.CallByName("On");
            playTetris.SetActive(true);
            playButtons.SetActive(false);

            if(activepiece != null)
            {
                activepiece.BottomUp();
            }

            this.tilemap.ClearAllTiles();
        }
        public void Set(Piece piece)
        {
            if (!gameOver)
            {
                for (int i = 0; i < piece.cells.Length; i++)
                {
                    Vector3Int tilePosition = piece.cells[i] + piece.position;
                    this.tilemap.SetTile(tilePosition, piece.data.tile);
                }
            }
        }
        public void Clear(Piece piece)
        {
            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + piece.position;
                this.tilemap.SetTile(tilePosition, null);
            }
        }

        public bool IsValidPosition(Piece piece, Vector3Int position)
        {
            RectInt bounds = Bounds;
            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + position;

                if (!bounds.Contains((Vector2Int)tilePosition))
                {
                    return false;
                }
                if (tilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }

            return true;
        }
        //public bool IsValidPosition2(Piece piece, Vector3Int position)
        //{
        //    RectInt bounds = Bounds;
        //    for (int i = 0; i < piece.cells.Length; i++)
        //    {
        //        Vector3Int tilePosition = piece.cells[i] + position;

        //        if (!bounds.Contains((Vector2Int)tilePosition))
        //        {
        //            return false;
        //        }

        //        if (tilemap.HasTile(tilePosition))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        public void ClearLines()
        {
            RectInt bounds = Bounds;
            int row = bounds.yMin;

            while (row < bounds.yMax)
            {

                if (IsLineFull(row))
                {
                    float final = cntManager.cnt * 2;
                    cntManager.cnt = (int)(MathF.Abs(final));
                    cntMoney.text = cntManager.cnt.ToString();
                    x2.CallByName("Play");
                    PlayerPrefs.SetString("Coockie", cntMoney.text);
                    LineClear(row);
                }
                else
                {
                    row++;
                }
            }
        }
        public bool IsLineFull(int row)
        {
            RectInt bounds = Bounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);

                if (!tilemap.HasTile(position))
                {
                    return false;
                }
            }

            return true;
        }

        public void LineClear(int row)
        {
            RectInt bounds = Bounds;

            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, null);
            }

            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new Vector3Int(col, row + 1, 0);
                    TileBase above = tilemap.GetTile(position);

                    position = new Vector3Int(col, row, 0);
                    tilemap.SetTile(position, above);
                }

                row++;
            }
        }
    }
}

