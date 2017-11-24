using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyGeneratorScript : MonoBehaviour {

    private int maxLights = 5;
    float lastSpawn;
    float spawnRate = 100.0f;
    float height;
    float width;
    public GameObject lightShower;
    // Use this for initialization
    void Start () {
        height = Camera.main.orthographicSize * 0.9f;
        width = height * Camera.main.aspect;
        spawn();
    }

    // Update is called once per frame
    void Update() {
        FireFlyScript[] lights = (FireFlyScript[]) Resources.FindObjectsOfTypeAll(typeof(FireFlyScript));
        if (lights.Length < maxLights)
        {
            attemptSpawn();
        }
	}

    private void attemptSpawn()
    {
        float gen = UnityEngine.Random.Range(Time.time - lastSpawn, spawnRate);

        if (spawnRate - gen < 1)
        {
            spawn();
        }
    }

    private void spawn()
    {
        GameObject light = Instantiate(lightShower);
        light.transform.position = new Vector3(UnityEngine.Random.Range(-width * 0.8f, width), UnityEngine.Random.Range(-height * 0.6f, height), 0);
        //light.transform.position = new Vector3(-width * 0.8f, -height * 0.6f, 0); //as Min
        //light.transform.position = new Vector3(width, height, 0); //as Max
        int direction = UnityEngine.Random.value > 0.5f ? 1 : -1;
        light.transform.localScale = new Vector3(light.transform.localScale.x * direction, light.transform.localScale.y, light.transform.localScale.z);
        lastSpawn = Time.time;
    }
}
