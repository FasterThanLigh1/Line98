using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    int EAT_BALL_NUMBER = 5;
    int vertical = -1;
    int horizontal = -1;
    float cellSize = -1;
    int[,] boardMatrix;
    bool[,] resMatrix;
    GameObject[,] delMatrix;
    [SerializeField]int numberOfBalls = -1;
    int currentNumberOfBalls = 0;
    [SerializeField]List<BallsEchoLocation> prelocationBalls = new List<BallsEchoLocation>();
    [SerializeField]List<GameObject> toDestroy = new List<GameObject>();
    public Board(int column, int row, float inputCellSize) {
        vertical = column;
        horizontal = row;
        numberOfBalls = column * row;
        cellSize = inputCellSize;
        boardMatrix = new int[column, row];
        resMatrix = new bool[column, row];
        delMatrix = new GameObject[column, row];
    }

    public void Draw(Vector2 initPosition, GameObject gameObject) {
        currentNumberOfBalls++;
        resMatrix[(int)initPosition.x, (int)initPosition.y] = true;
        delMatrix[(int)initPosition.x, (int)initPosition.y] = gameObject;
        float x = initPosition.x + cellSize / 2;
        float y = initPosition.y + cellSize / 2;
        Instantiate(gameObject, new Vector2(x, y), gameObject.transform.rotation);
    }
    public float getCellSize() {
        return cellSize;
    }
    public void RandomArrayBallsGenerator(Vector2 initPosition, GameObject[] gameObject, int size, int arraySize) {
        for (int i = 0; i < size; i++){
            int randIndex = Random.Range(0, arraySize);
            if (currentNumberOfBalls >= numberOfBalls) {
                return;
            }
            int randX = Random.Range(0, vertical);
            int randY = Random.Range(0, horizontal);
            while(resMatrix[randX, randY] == true) {
                randX = Random.Range(0, vertical);
                randY = Random.Range(0, horizontal);
            }
            resMatrix[randX, randY] = true;
            Draw(new Vector2(randX, randY), gameObject[randIndex]);
        }
    }

    public void MiniBallsGenerator(Vector2 initPosition, GameObject[] miniBalls, int size, int arraySize, GameObject[] balls) {
        if (prelocationBalls.Count != 0) {
            for (int i = 0; i < prelocationBalls.Count; i++) {
                Draw(new Vector2(prelocationBalls[i].x, prelocationBalls[i].y), balls[prelocationBalls[i].arrayIndex]);
                Destroy(toDestroy[i]);
            }
            prelocationBalls.Clear();
        }
        for (int i = 0; i < size; i++){
            int randIndex = Random.Range(0, arraySize);
            if (currentNumberOfBalls >= numberOfBalls) {
                return;
            }
            int randX = Random.Range(0, vertical);
            int randY = Random.Range(0, horizontal);
            while(resMatrix[randX, randY] == true) {
                randX = Random.Range(0, vertical);
                randY = Random.Range(0, horizontal);
            }
            float x = randX + cellSize/2;
            float y = randY + cellSize/2;
            prelocationBalls.Add(new BallsEchoLocation(randX, randY, randIndex));
            GameObject temp = Instantiate(miniBalls[randIndex], new Vector2(x, y), miniBalls[randIndex].transform.rotation);
            toDestroy.Add(temp);
        }
    }

    public void BallMoves(Vector2 startPoint, Vector2 endPoint) {
        Debug.Log(Mathf.FloorToInt(startPoint.x) + " " + Mathf.FloorToInt(startPoint.y));
        Debug.Log(Mathf.FloorToInt(endPoint.x) + " " + Mathf.FloorToInt(endPoint.y));
        resMatrix[Mathf.FloorToInt(startPoint.x), Mathf.FloorToInt(startPoint.y)] = false;
        resMatrix[Mathf.FloorToInt(endPoint.x), Mathf.FloorToInt(endPoint.y)] = true;
    }

    public void EatBall(GameObject current) {
        Vector3 currentPos = current.gameObject.transform.position;
        int matchIndex = current.gameObject.GetComponent<GotoMouse>().ballIndex;
        //UP
        int counter = 0;
        for (int i = 1; i < 5; i++) {
            if(i > horizontal) break;
            if(delMatrix[(int)currentPos.x, (int)currentPos.y + i].gameObject.GetComponent<GotoMouse>().ballIndex == matchIndex) {
                counter++;
            }else break;
        }
        //VERTICAL
        
        //LINE

    }
}

public class BallsEchoLocation
{
    public int x = 0;
    public int y = 0;
    public int arrayIndex = -1;
    public BallsEchoLocation(int inX, int inY, int index) {
        x = inX;
        y = inY;
        arrayIndex = index;
    }
}
