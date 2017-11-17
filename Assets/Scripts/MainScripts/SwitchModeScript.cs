using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeScript : MonoBehaviour
{
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit[] hits = Physics.RaycastAll(touchRay);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject == gameObject)
                {
                    SwitchChangingColor();
                }
            }
        }
    }

    void OnMouseDown()
    {
        SwitchChangingColor();
   }

    private void SwitchChangingColor()
    {
        MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
        script.switchColorChanger();
    }
}