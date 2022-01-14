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
    float timer = 3.0f;

    void Start () {
        pathway = new List<PathNode>();
        matrix = GameObject.FindObjectOfType<TestMatrix>();
        canMoved = false;
        gameMaster = GameObject.FindObjectOfType<GameMaster>();
        target = transform.position;
    }
    
    void FixedUpdate () {
        if (canMoved) {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if(hit.collider != null) {
                    if (hit.collider.tag == "Tilemap") {
                        Debug.Log("hit tilemap");
                        canMoved = false;
                        float cellSize = gameMaster.gameBoard.getCellSize();
                        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        target.z = transform.position.z;

                        target = new Vector3((float)Mathf.FloorToInt(target.x) + (cellSize / 2), (float)Mathf.FloorToInt(target.y) + (cellSize / 2));
                        pathway = matrix.PathFinding(matrix.arrayMatrix[(int)this.transform.position.x, (int)this.transform.position.y],
                                                                    matrix.arrayMatrix[(int)target.x, (int)target.y]);
                        matrix.PrintList(pathway);
                    }
                }
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
