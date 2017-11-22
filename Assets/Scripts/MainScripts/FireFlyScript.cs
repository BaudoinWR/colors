using UnityEngine;
using System.Collections;
using System;

public class FireFlyScript : MonoBehaviour {

    SpriteRenderer sourceRenderer;
    SpriteRenderer thisRenderer;
    Color fireFlyColor;
    public float closeness;

    public float timeToLive;
    private float timeValue = 4;
	// Use this for initialization
	void Start () {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        fireFlyColor = ColorScript.generateNewColor();
        thisRenderer.material.SetColor("_DesiredColor", fireFlyColor);
        sourceRenderer = GameObject.Find("FlashLight").GetComponent<SpriteRenderer>();
    }       

// Update is called once per frame
    void Update() {
        if (timeToLive <= 0)
        {
            MainScript.increaseScore();
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.restoreTime(timeValue);
            Destroy(gameObject);
            return;
        }

        CheckTouch();

        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (closeEnough(fireFlyColor, sourceRenderer.color, closeness))
        {
            halo.enabled = true;
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.switchColorChanger(false);
        } else {
            halo.enabled = false;
        }
        HoverScript.doHover(gameObject);
    }

    private void CheckTouch()
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
                    overed();
                }
            }
        }
    }

    private bool closeEnough(Color c1, Color c2, float nearness)
    {
        return Mathf.Abs(c1.r - c2.r) < nearness
            && Mathf.Abs(c1.g - c2.g) < nearness
            && Mathf.Abs(c1.b - c2.b) < nearness;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            overed();
        }
    }

    private void overed()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");

        if (halo.enabled)
        {
            timeToLive -= Time.deltaTime;
        }
    }
}
