using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverScript {
    public static void doHover(GameObject obj)
    {
        Transform tr = obj.transform;
        tr.Translate(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), 0);
    }
}
