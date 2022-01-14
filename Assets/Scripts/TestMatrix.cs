using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    int STRAIGHT_VALUE = 10;
    int width = 9;
    int height = 9;
    public PathNode[,] arrayMatrix;
    //list for current item
    public List<PathNode> openList = new List<PathNode>();
    //list for traveled item
    public List<PathNode> closeList = new List<PathNode>();
    void Start()
    {
        arrayMatrix = new PathNode[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                PathNode temp = new PathNode(0, 0, null);
                arrayMatrix[i, j] = temp;
                arrayMatrix[i, j].x = i;
                arrayMatrix[i, j].y = j;
                arrayMatrix[i, j].gCOst = int.MaxValue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0)) {
            List<PathNode> res = PathFinding(arrayMatrix[1,3], arrayMatrix[7,3]);
            PrintList(res);
        }*/
    }
    public List<PathNode> PathFinding(PathNode startNode, PathNode endNode) {
        Debug.Log("start " + startNode.x + " l " +startNode.y);
        Debug.Log("end " + endNode.x + " l " + endNode.y);
        startNode.gCOst = 0;
        startNode.hCost = CalcDistance(startNode, endNode);
        startNode.CalculateFCost();
        openList.Add(startNode);
        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCost();
            if (currentNode == endNode) {
                //return condition
                return PathFromEndNode(endNode);
            }
            //update value in list
            openList.Remove(currentNode);
            closeList.Add(currentNode);
            foreach (PathNode neighborNode in GetNeighborList(currentNode)) {
                if (closeList.Contains(neighborNode)) continue;
                if (!neighborNode.isWalkable) {
                    closeList.Add(neighborNode);
                    continue;
                }
                int tentativeGCost = currentNode.gCOst + CalcDistance(currentNode, neighborNode);
                if (tentativeGCost < neighborNode.gCOst) {
                    neighborNode.gCOst = tentativeGCost;
                    neighborNode.hCost = CalcDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();
                    neighborNode.prevNode = currentNode;

                    if (!openList.Contains(neighborNode)) {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
        Debug.Log("no path");
        return null;
    }

    List<PathNode> GetNeighborList (PathNode currentNode) {
        List<PathNode> neighborList = new List<PathNode>();
        //TOP
        if (currentNode.y + 1 < height) {
            neighborList.Add(arrayMatrix[currentNode.x, currentNode.y + 1]);
        }
        //DOWN
        if (currentNode.y - 1 >= 0) {
            neighborList.Add(arrayMatrix[currentNode.x, currentNode.y - 1]);
        }
        //LEFT
        if (currentNode.x + 1 < width) {
            neighborList.Add(arrayMatrix[currentNode.x + 1, currentNode.y]);
        }
        //RIGHT
        if (currentNode.x - 1 >= 0) {
            neighborList.Add(arrayMatrix[currentNode.x - 1, currentNode.y]);
        }
        return neighborList;
    }
    PathNode GetLowestFCost() {
        PathNode lowestFCost = openList[0];
        for (int i = 0; i < openList.Count; i++) {
            if (lowestFCost.fCost > openList[i].fCost) {
                lowestFCost = openList[i];
            }
        }
        return lowestFCost;
    }

    List<PathNode> PathFromEndNode(PathNode endNode) {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;
        while (currentNode.prevNode != null) {
            path.Add(currentNode);
            currentNode = currentNode.prevNode;
        }
        path.Reverse();
        return path;
    }

    public void PrintList(List<PathNode> list) {
        foreach (PathNode node in list) {
            Debug.Log(node.x + "||" + node.y);
        }
    }

    public int CalcDistance(PathNode a, PathNode b) {
        int x = Mathf.Abs(a.x - b.x);
        int y = Mathf.Abs(a.y - b.y);
        return (x + y) * STRAIGHT_VALUE;
    }
}

public class PathNode
{
    // Start is called before the first frame update
    public int x;
    public int y;
    public int gCOst;
    public int hCost;
    public int fCost;
    public bool isWalkable;
    public PathNode prevNode;
    public PathNode(int x, int y, PathNode prevNode) {
        this.x = x;
        this.y = y;
        this.prevNode = prevNode;
        isWalkable = true;
    }
    public void CalculateFCost() {
        fCost = gCOst + hCost;
    }
    public void Print() {
        Debug.Log("gCost: " + gCOst + " + hCost: " + hCost + " = fCost: " + fCost);
    }
}