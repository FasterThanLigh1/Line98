using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixObjects 
{
    public GameObject gameObject = null;
    public bool isOccupied = false;
    public bool canMove = false;
    public int index = -1;
    public MatrixObjects(GameObject gameObject, bool isOccupied, bool canMove, int index) {
        this.gameObject = gameObject;
        this.isOccupied = isOccupied;
        this.canMove = canMove;
        this.index = index;
    }   
}
