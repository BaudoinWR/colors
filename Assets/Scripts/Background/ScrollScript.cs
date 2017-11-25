using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {

    float layer;
    SpriteRenderer spriteRenderer;
    Boolean hasSpawn = false;
    public GameObject[] objects;
    Boolean spawnsObjects;
    public Boolean keepAlive;

    public float density = 0.7f;
    public float spread = 1.0f;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        layer = getLayerSpead(spriteRenderer.sortingLayerName);
        spawnsObjects = objects.Length > 0;
        if (spawnsObjects)
        {
            GenerateObjects();
        }
	}

    private void GenerateObjects()
    {
        float size = spriteRenderer.size.x;
        Debug.Log(size);
        for (float i = 0; i < size; i+= spread)
        {
            if (UnityEngine.Random.value > density)
            {
                
                GameObject newObj = Instantiate(objects[(int) UnityEngine.Random.Range(0, objects.Length)]);
                newObj.transform.position = new Vector3(gameObject.transform.position.x + i, newObj.transform.position.y, newObj.transform.position.z);
            }
        }
    }

    private float getLayerSpead(string sortingLayerName)
    {
        switch(sortingLayerName)
        {
            case "Near": return 3;
            case "Far": return 2;
            case "Farther": return 1;
            case "Farthest": return 0.2f;
            default: return 0;

        }
    }

    // Update is called once per frame
    void Update () {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x - layer/100, position.y, position.z);
        if (keepAlive)
        {
            if (position.x + spriteRenderer.size.x < 30 && !hasSpawn)
            {
                hasSpawn = true;
                GameObject newBG = Instantiate(gameObject);
                newBG.transform.parent = transform.parent;
                newBG.transform.position = gameObject.transform.position;
                newBG.transform.position = new Vector3(position.x - layer / 100 + spriteRenderer.size.x, position.y, position.z);
                newBG.transform.localScale = transform.localScale;
            }
        }
        if (position.x + spriteRenderer.size.x < -20)
        {
            Destroy(gameObject);
        }
    }
}
