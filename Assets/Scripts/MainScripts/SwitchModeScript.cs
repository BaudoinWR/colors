using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeScript : MonoBehaviour { 

    public void SwitchChangingColor()
    {
        MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
        script.SwitchColorChanger();
    }
}