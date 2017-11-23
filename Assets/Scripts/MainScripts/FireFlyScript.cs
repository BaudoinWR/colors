using UnityEngine;
using System.Collections;
using System;

public class FireFlyScript : MonoBehaviour {

    SpriteRenderer sourceRenderer;
    SpriteRenderer thisRenderer;
    Color fireFlyColor;
    Behaviour halo;
    public float closeness;

    public float timeToLive;
    private float timeValue = 4;
	// Use this for initialization
	void Start () {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        fireFlyColor = ColorScript.generateNewColor();
        halo = (Behaviour)GetComponent("Halo");
        thisRenderer.material.SetColor("_DesiredColor", fireFlyColor);
        sourceRenderer = GameObject.Find("FlashLight").GetComponent<SpriteRenderer>();
    }       

// Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0)
        {
            doEndOfLife();
            return;
        }
        CheckTouch();
        UpdateHalo();
        HoverScript.doHover(gameObject);
    }

    private void UpdateHalo()
    {
        
        if (closeEnough(fireFlyColor, sourceRenderer.color, closeness))
        {
            halo.enabled = true;
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.switchColorChanger(false);
        }
        else
        {
            halo.enabled = false;
        }
    }

    private void doEndOfLife()
    {
        if (gameObject.transform.position == sourceRenderer.transform.position)
        {
            MainScript.increaseScore();
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.restoreTime(timeValue);
            Destroy(gameObject);
        }
        else
        {
            GoToFlashLight();
        }
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

    private void GoToFlashLight()
    {
        Transform thisTransform = gameObject.transform;
        thisTransform.position = Vector3.MoveTowards(thisTransform.position, sourceRenderer.transform.position, 0.15f);
        thisTransform.localScale = Vector3.Scale(thisTransform.localScale, new Vector3(0.95f, 0.95f, 1));
    }
}
