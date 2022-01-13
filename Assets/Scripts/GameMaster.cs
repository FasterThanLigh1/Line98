using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public Board gameBoard;
    [SerializeField] GameObject[] balls;
    void Start()
    {
        gameBoard = new Board(9, 9, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            gameBoard.RandomArrayBallsGenerator(this.transform.position, balls, 5, balls.Length);
        }
        if (Input.GetMouseButtonDown(1)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider.tag == "Balls") {
                Debug.Log(hit.collider.gameObject.name);
                GotoMouse go = hit.collider.GetComponent<GotoMouse>();
                go.SetMoved();
            }
            if (hit.collider.tag == "Tilemap") {
                Debug.Log("Hit tilemap");
            }
        }
        
    }
}
