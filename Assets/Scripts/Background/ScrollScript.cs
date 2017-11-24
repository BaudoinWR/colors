using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    float layer;
    SpriteRenderer spriteRenderer;
    Boolean hasSpawn = false;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        layer = getLayerOrder(spriteRenderer.sortingLayerName);
	}

    private int getLayerOrder(string sortingLayerName)
    {
        switch(sortingLayerName)
        {
            case "Near": return 4;
            case "Far": return 3;
            case "Farther": return 2;
            case "Farthest": return 1;
            default: return 0;

        }
    }

    // Update is called once per frame
    void Update () {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x - layer/100, position.y, position.z);
        
        if (position.x + spriteRenderer.size.x < 30 && !hasSpawn)
        {
            hasSpawn = true;
            GameObject newBG = Instantiate(gameObject);
            newBG.transform.parent = transform.parent;
            newBG.transform.position = gameObject.transform.position;
            newBG.transform.position = new Vector3(position.x - layer / 100 + spriteRenderer.size.x, position.y, position.z);
            newBG.transform.localScale = transform.localScale;
        }
        if (position.x + spriteRenderer.size.x < -10)
        {
            Destroy(gameObject);
        }
	}
}
