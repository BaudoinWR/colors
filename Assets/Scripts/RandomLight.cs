using UnityEngine;
using System.Collections;

public class RandomLight : MonoBehaviour {

    public GameObject lightSource;
    SpriteRenderer sourceRenderer;
    SpriteRenderer thisRenderer;
    public float closeness = 0.05f;
	// Use this for initialization
	void Start () {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        thisRenderer.color = ColorPicker.generateNewColor();
        sourceRenderer = lightSource.GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update() {
        if (closeEnough(thisRenderer.color, sourceRenderer.color, closeness))
        {
            Debug.Log("Matched!");
            thisRenderer.color = ColorPicker.generateNewColor();
        }
    }

    private bool closeEnough(Color c1, Color c2, float nearness)
    {
        return Mathf.Abs(c1.r - c2.r) < nearness
            && Mathf.Abs(c1.g - c2.g) < nearness
            && Mathf.Abs(c1.b - c2.b) < nearness;
    }

}
