using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeScript : MonoBehaviour
{

    void OnMouseDown()
    {
        MainScript script = (MainScript)Object.FindObjectOfType(typeof(MainScript));
        script.isChangingColor = !script.isChangingColor;
    }
}