using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public Board gameBoard;
    [SerializeField] GameObject[] balls;
    [SerializeField] GameObject[] miniBalls;
    void Start()
    {
        gameBoard = new Board(9, 9, 1f);
        gameBoard.RandomArrayBallsGenerator(this.transform.position, balls, 3, balls.Length);
        SpawnMiniBalls(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null) {
                
                if (hit.collider.tag == "Balls") {
                    GotoMouse go = hit.collider.GetComponent<GotoMouse>();
                    go.SetMoved();
                }
            }
        }
        
    }
    public void BallMoveUpdate(Vector2 startPoint, Vector2 endPoint) {
        gameBoard.BallMoves(startPoint, endPoint);
        //SetWalkableStatus(endPoint, false);
        //SetWalkableStatus(startPoint, true);
    }
    void SpawnMiniBalls (int n) {
        gameBoard.MiniBallsGenerator(this.transform.position, miniBalls, n, miniBalls.Length, balls);
    }
    public void Spawn() {
        SpawnMiniBalls(3);
    }
    public void CheckIfEatean(GameObject thisBall) {
        gameBoard.EatBall(thisBall);
    }
}
