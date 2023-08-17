using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;

namespace NubikClicker
{
    public class Piece : MonoBehaviour
    {
        public Board board { get; private set; }
        public TetrominoData data { get; private set; }
        public Vector3Int[] cells { get; private set; }
        public Vector3Int position { get; private set; }
        public int rotationIndex { get; private set; }

        public float stepDelay = 1f;
        public float lockDelay = 0.5f;
        public float hardDropTime = 1;

        private float stepTime;
        private float lockTime;

        float startStepDelay;
        bool tapping = false;

        private void Awake()
        {
            board = GetComponent<Board>();
            startStepDelay = stepDelay;
        }

        public void Initialize(Board board, Vector3Int position, TetrominoData data)
        {
            this.board = board;
            this.position = position;
            this.data = data;
            this.rotationIndex = 0;
            this.stepTime = Time.time + this.stepDelay;
            this.lockTime = 0f;
            if (this.cells == null)
            {
                this.cells = new Vector3Int[data.cells.Length];
            }

            for (int i = 0; i < data.cells.Length; i++)
            {
                this.cells[i] = (Vector3Int)data.cells[i];
            }
        }

        private void Update()
        {
            if (!board.gameOver)
            {
                this.board.Clear(this);

                this.lockTime += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Rotate(-1);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Rotate(1);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Move(Vector2Int.left);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    Move(Vector2Int.right);
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    Move(Vector2Int.down);
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    HardDrop();
                }

                if (Time.time >= this.stepTime)
                {
                    Step();
                }
                this.board.Set(this);

                if (tapping)
                {
                    stepDelay -= Time.deltaTime;
                    if(stepDelay <= 0.03f)
                    {
                        stepDelay = 0.03f;
                    }
                }
            }
        }

        private void Step()
        {
            this.stepTime = Time.time + this.stepDelay;

            Move(Vector2Int.down);

            if (lockTime >= lockDelay)
            {
                Lock();
            }
        }

        private void Lock()
        {
            this.board.Set(this);
            this.board.ClearLines();
            this.board.SpawnPiece();
        }
        public void HardDrop()
        {
            while (Move(Vector2Int.down))
            {
                continue;
            }
            Lock();
        }
        private bool Move(Vector2Int translation)
        {
            Vector3Int newPosition = this.position;
            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = this.board.IsValidPosition(this, newPosition);

            if (valid)
            {
                this.position = newPosition;
                this.lockTime = 0f;
            }

            return valid;
        }

        private void Rotate(int direction)
        {
            int originalRotation = rotationIndex;

            rotationIndex = Wrap(rotationIndex + direction, 0, 4);
            ApplyRotationMatrix(direction);

            if (!TestWallKicks(rotationIndex, direction))
            {
                rotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        private void ApplyRotationMatrix(int direction)
        {
            float[] matrix = Data.RotationMatrix;

            for (int i = 0; i < cells.Length; i++)
            {
                Vector3 cell = cells[i];

                int x, y;

                switch (data.tetromino)
                {
                    case Tetromino.I:
                    case Tetromino.O:
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;

                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                }

                cells[i] = new Vector3Int(x, y, 0);
            }
        }

        private bool TestWallKicks(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

            for (int i = 0; i < data.wallKicks.GetLength(1); i++)
            {
                Vector2Int translation = data.wallKicks[wallKickIndex, i];

                if (Move(translation))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;

            if (rotationDirection < 0)
            {
                wallKickIndex--;
            }

            return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
        }

        private int Wrap(int input, int min, int max)
        {
            if (input < min)
            {
                return max - (min - input) % (max - min);
            }
            else
            {
                return min + (input - min) % (max - min);
            }
        }

        public void BottomDown()
        {
            tapping = true;
        }

        public void BottomUp()
        {
            tapping = false;
            stepDelay = startStepDelay;
        }

        public void RotateRight()
        {
            this.board.Clear(this);
            Rotate(1);
            this.board.Set(this);
        }

        public void RotateLeft()
        {
            this.board.Clear(this);
            Rotate(-1);
            this.board.Set(this);
        }

        public void MoveLeft()
        {
            this.board.Clear(this);
            Move(Vector2Int.left);
            this.board.Set(this);
        }
        public void MoveRight()
        {

            this.board.Clear(this);
            Move(Vector2Int.right);
            this.board.Set(this);
        }

        public void MoveDown()
        {
            this.board.Clear(this);
            Move(Vector2Int.down);
            this.board.Set(this);
        }

        public void HardDropBtn()
        {
            this.board.Clear(this);
            HardDrop();
            this.board.Set(this);
        }
    }
}

