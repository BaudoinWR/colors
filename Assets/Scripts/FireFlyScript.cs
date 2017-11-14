using UnityEngine;
using System.Collections;

public class FireFlyScript : MonoBehaviour {

    SpriteRenderer sourceRenderer;
    SpriteRenderer thisRenderer;
    public float closeness;

    public float timeToLive;

	// Use this for initialization
	void Start () {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisRenderer.color = ColorScript.generateNewColor();
        sourceRenderer = GameObject.Find("FlashLight").GetComponent<SpriteRenderer>();
    }       

// Update is called once per frame
    void Update() {
        if (timeToLive <= 0)
        {
            MainScript.score++;
            Destroy(gameObject);
            return;
        }

        Behaviour halo = (Behaviour)GetComponent("Halo");
        if (closeEnough(thisRenderer.color, sourceRenderer.color, closeness))
        {
            halo.enabled = true;
            MainScript script = (MainScript)Object.FindObjectOfType(typeof(MainScript));
            script.isChangingColor = false;
        } else {
            halo.enabled = false;
        }
        HoverScript.doHover(gameObject);
    }

    private bool closeEnough(Color c1, Color c2, float nearness)
    {
        return Mathf.Abs(c1.r - c2.r) < nearness
            && Mathf.Abs(c1.g - c2.g) < nearness
            && Mathf.Abs(c1.b - c2.b) < nearness;
    }

    void OnMouseOver()
    {
        Behaviour halo = (Behaviour)GetComponent("Halo");
        
        if (halo.enabled && Input.GetMouseButton(0))
        {
            timeToLive -= Time.deltaTime;
        }
    }

}
