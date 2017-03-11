using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothingFilter : MonoBehaviour{

    Vector3 x0;
    Vector3 s;
    Vector3 b;
    float alpha = 0.5f;
    float beta = 0.5f;
    int time;

    public SmoothingFilter()
    {
        time = 0;
        s = Vector3.zero;
        b = Vector3.zero;
        x0 = Vector3.zero;
    }

    public Vector3 Filter(Vector3 current)
    {

        if (time == 0) {
            x0 = current;
            time++;
            return current;
        }
        if (time == 1) {
            s = current;
            b = current - x0;
            time++;
            return current;
        }

        Vector3 prevS = s;
        s.x = alpha * current.x + (1.0f - alpha) * (s.x + b.x);     b.x = beta * (s.x - prevS.x) + (1.0f - beta) * b.x;
        s.y = alpha * current.y + (1.0f - alpha) * (s.y + b.y);     b.y = beta * (s.y - prevS.y) + (1.0f - beta) * b.y;
        s.z = alpha * current.z + (1.0f - alpha) * (s.z + b.z);     b.z = beta * (s.z - prevS.z) + (1.0f - beta) * b.z;
        return s;
    }
}
