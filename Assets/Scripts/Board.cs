using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    int vertical = -1;
    int horizontal = -1;
    float cellSize = -1;
    int[,] boardMatrix;
    bool[,] resMatrix;
    [SerializeField]int numberOfBalls = -1;
    [SerializeField]int currentNumberOfBalls = 0;
    public Board(int column, int row, float inputCellSize) {
        vertical = column;
        horizontal = row;
        numberOfBalls = column * row;
        cellSize = inputCellSize;
        boardMatrix = new int[column, row];
        resMatrix = new bool[column, row];
    }

    public void Draw(Vector2 initPosition, GameObject gameObject) {
        currentNumberOfBalls++;
        float x = initPosition.x + cellSize/2;
        float y = initPosition.y + cellSize/2;
        Instantiate(gameObject, new Vector2(x, y), gameObject.transform.rotation);
    }
    public float getCellSize() {
        return cellSize;
    }
    public void RandomBallsGenerator(Vector2 initPosition, GameObject gameObject, int size) {
        for (int i = 0; i < size; i++){
            if (currentNumberOfBalls >= numberOfBalls) {
                return;
            }
            int randX = Random.Range(0, vertical);
            int randY = Random.Range(0, horizontal);
            float x = initPosition.x + cellSize/2;
            float y = initPosition.y + cellSize/2;
            while(resMatrix[randX, randY] == true) {
                randX = Random.Range(0, vertical);
                randY = Random.Range(0, horizontal);
            }
            resMatrix[randX, randY] = true;
            Draw(new Vector2(randX, randY), gameObject);
        }
    }

    public void RandomArrayBallsGenerator(Vector2 initPosition, GameObject[] gameObject, int size, int arraySize) {
        for (int i = 0; i < size; i++){
            int randIndex = Random.Range(0, arraySize);
            if (currentNumberOfBalls >= numberOfBalls) {
                return;
            }
            int randX = Random.Range(0, vertical);
            int randY = Random.Range(0, horizontal);
            float x = initPosition.x + cellSize/2;
            float y = initPosition.y + cellSize/2;
            while(resMatrix[randX, randY] == true) {
                randX = Random.Range(0, vertical);
                randY = Random.Range(0, horizontal);
            }
            resMatrix[randX, randY] = true;
            Draw(new Vector2(randX, randY), gameObject[randIndex]);
        }
    }
}
