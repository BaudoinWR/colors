using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyGeneratorScript : MonoBehaviour {

    private int maxLights = 5;
    float lastSpawn;
    float spawnRate = 100.0f;
    public GameObject lightShower;
    // Use this for initialization
    void Start () {
        lastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update() {
        FireFlyScript[] lights = (FireFlyScript[]) Resources.FindObjectsOfTypeAll(typeof(FireFlyScript));
        if (lights.Length < maxLights)
        {
            spawn();
        }
	}

    private void spawn()
    {
        float gen = UnityEngine.Random.Range(Time.time - lastSpawn, spawnRate);

        if (spawnRate - gen < 1)
        {
            GameObject light = Instantiate(lightShower);
            light.transform.position = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-2, 4.5f), 0);
            int direction = UnityEngine.Random.value > 0.5f ? 1 : -1;
            light.transform.localScale = new Vector3(light.transform.localScale.x * direction, light.transform.localScale.y, light.transform.localScale.z);
            light.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(light.GetComponent<SpriteRenderer>().transform.localScale.x * direction,
                light.GetComponent<SpriteRenderer>().transform.localScale.y,
                light.GetComponent<SpriteRenderer>().transform.localScale.z);
            lastSpawn = Time.time;
        }
    }
}
