using UnityEngine;
using System.Collections;
using System;

public class FireFlyScript : MonoBehaviour {

    SpriteRenderer sourceRenderer;
    SpriteRenderer thisRenderer;
    Color fireFlyColor;
    Behaviour halo;
    public float closeness;

    private float timeToLive = PlayScript.data.instantLure ? 0.01f : 1.0f;
    private float timeValue = 4;
    public int direction;

	// Use this for initialization
	void Start () {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        fireFlyColor = ColorScript.generateNewColor();
        halo = (Behaviour)GetComponent("Halo");
        thisRenderer.material.SetColor("_DesiredColor", fireFlyColor);
        sourceRenderer = GameObject.Find("FlashLight").GetComponent<SpriteRenderer>();
        halo.GetComponent<SpriteRenderer>().sortingLayerID = thisRenderer.sortingLayerID;
        halo.GetComponent<SpriteRenderer>().sortingOrder = thisRenderer.sortingOrder;
    }       

// Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0.8)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.08f * direction, 0.08f, 1), 0.0005f);
        }
        if (timeToLive <= 0)
        {
            DoEndOfLife();
            return;
        }
        CheckTouch();
        UpdateHalo();
        HoverScript.doHover(gameObject);
    }

    private void UpdateHalo()
    {
        
        if (CloseEnough(fireFlyColor, sourceRenderer.material.GetColor("_DesiredColor"), closeness))
        {
            halo.enabled = true;
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.SwitchColorChanger(false);
        }
        else
        {
            halo.enabled = false;
        }
    }

    private void DoEndOfLife()
    {
        if (gameObject.transform.position == sourceRenderer.transform.position)
        {
            MainScript.IncreaseScore();
            MainScript script = (MainScript)UnityEngine.Object.FindObjectOfType(typeof(MainScript));
            script.RestoreTime(timeValue);
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
                    Overed();
                }
            }
        }
    }

    private bool CloseEnough(Color c1, Color c2, float nearness)
    {
        return Mathf.Abs(c1.r - c2.r) < nearness
            && Mathf.Abs(c1.g - c2.g) < nearness
            && Mathf.Abs(c1.b - c2.b) < nearness;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Overed();
        }
    }

    private void Overed()
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
        thisTransform.position = Vector3.MoveTowards(thisTransform.position, sourceRenderer.transform.position, 0.5f);
        thisTransform.localScale = Vector3.Scale(thisTransform.localScale, new Vector3(0.97f, 0.97f, 1));
    }
}
