using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoMouse : MonoBehaviour
{
    public float speed = 1.5f;
    private Vector3 target;
    [SerializeField]bool canMoved = false;
    [SerializeField]bool move = false;
    GameMaster gameMaster;

    void Start () {
        canMoved = false;
        gameMaster = GameObject.FindObjectOfType<GameMaster>();
        target = transform.position;
    }
    
    void Update () {
        if (canMoved) {
            if (Input.GetMouseButtonDown(0)) {
                canMoved = false;
                float cellSize = gameMaster.gameBoard.getCellSize();
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                target = new Vector3((float)Mathf.FloorToInt(target.x) + (cellSize / 2), (float)Mathf.FloorToInt(target.y) + (cellSize / 2));
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
