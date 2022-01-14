using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsLocation
{
    float x = 0;
    float y = 0;
    int arrayIndex = -1;
    public BallsLocation(float inX, float inY, int index) {
        x = inX;
        y = inY;
        arrayIndex = index;
    }
}
