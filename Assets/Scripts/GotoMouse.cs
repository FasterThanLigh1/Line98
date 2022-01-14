using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoMouse : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector3 target;
    [SerializeField]bool canMoved = false;
    TestMatrix matrix;
    List<PathNode> pathway;
    GameMaster gameMaster;
    Board b;
    bool moving = false;
    public int ballIndex = -1;
    float timer = 3.0f;
    float cellSize;
    [SerializeField]int waypointIndex = 0;

    void Start () {
        pathway = new List<PathNode>();
        matrix = GameObject.FindObjectOfType<TestMatrix>();
        canMoved = false;
        gameMaster = GameObject.FindObjectOfType<GameMaster>();
        cellSize = gameMaster.gameBoard.getCellSize();
        b = GameObject.FindObjectOfType<Board>();
        target = transform.position;
        waypointIndex = 1;
    }
    
    void Update () {
        if (canMoved) {
            if (Input.GetMouseButtonDown(1)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                canMoved = false;
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                target.z = transform.position.z;
                target = new Vector3((float)Mathf.FloorToInt(target.x) + (cellSize / 2), (float)Mathf.FloorToInt(target.y) + (cellSize / 2));
                Debug.Log(target);
                pathway = matrix.PathFinding(matrix.arrayMatrix[(int)this.transform.position.x, (int)this.transform.position.y],
                                                            matrix.arrayMatrix[(int)target.x, (int)target.y]);
                Debug.Log(pathway.Count);
                target = new Vector3(pathway[0].x + cellSize / 2, pathway[0].y + cellSize / 2, 0);
                moving = true;
                //GAME MASTER ACTION
                gameMaster.BallMoveUpdate(this.transform.position, mousePos2D);
                gameMaster.Spawn();
            }
        }
        if(transform.position == target && moving) {
            target = new Vector3(pathway[waypointIndex].x + cellSize / 2, pathway[waypointIndex].y + cellSize / 2, 0);
            if(waypointIndex == pathway.Count - 1) {
                waypointIndex = 1;
                moving = false;
            } else {
                waypointIndex++;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    public void SetMoved() {
        canMoved = true;
    }

    public void Delete() {
        Destroy(this.gameObject);
    }

}
