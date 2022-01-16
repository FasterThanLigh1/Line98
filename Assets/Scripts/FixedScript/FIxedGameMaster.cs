using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIxedGameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    int boardWidth = 9;
    int boardHeight = 9;
    int totalBall;
    int ballCount = 0;
    MatrixObjects[,] board;
    float cellSize = 1;
    [SerializeField] List<GameObject> arrayOfBalls; 
    void Start()
    {
        totalBall = boardWidth * boardHeight;
        board = new MatrixObjects[boardWidth, boardHeight];
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            SpawnBalls(3);
        }
    }
    void InitBoard() {
        for (int x = 0; x < boardWidth; x++) {
            for (int y = 0; y < boardHeight; y++) {
                board[x, y] = new MatrixObjects(null, false, false, -1);
            }
        }
    }
    void SpawnBalls(int number) {
        int randX, randY, randIndex;
        for (int i = 0; i < number; i++) {
            if (ballCount >= totalBall) return;
            RandomPos(out randX, out randY, out randIndex);
            while (board[randX, randY].isOccupied) {
                RandomPos(out randX, out randY, out randIndex);
            }
            board[randX, randY].isOccupied = true;
            ballCount++;
            Instantiate(arrayOfBalls[randIndex], new Vector2((float)randX + cellSize / 2, (float)randY + cellSize / 2), arrayOfBalls[randIndex].transform.rotation);
        }
    }
    void RandomPos(out int x, out int y, out int index) {
        x = Random.Range(0, boardWidth);
        y = Random.Range(0, boardHeight);
        index = Random.Range(0, arrayOfBalls.Count);
    }
}
