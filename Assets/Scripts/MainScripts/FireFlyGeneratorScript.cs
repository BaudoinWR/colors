using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyGeneratorScript : MonoBehaviour {

    private int maxLights = PlayScript.data.spawnBoost ? 7:5;
    float lastSpawn;
    float spawnRate = 100.0f;
    float height;
    float width;
    public GameObject lightShower;
    // Use this for initialization
    void Start () {
        height = Camera.main.orthographicSize * 0.9f;
        width = height * Camera.main.aspect;
        Spawn();
    }

    // Update is called once per frame
    void Update() {
        FireFlyScript[] lights = (FireFlyScript[]) Resources.FindObjectsOfTypeAll(typeof(FireFlyScript));
        if (lights.Length < maxLights)
        {
            AttemptSpawn();
        }
	}

    private void AttemptSpawn()
    {
        float gen = UnityEngine.Random.Range(Time.time - lastSpawn, spawnRate);

        if (spawnRate - gen < 1)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject light = Instantiate(lightShower);
        light.transform.position = new Vector3(UnityEngine.Random.Range(-width * 0.8f, width), UnityEngine.Random.Range(-height * 0.6f, height), 0);
        //light.transform.position = new Vector3(-width * 0.8f, -height * 0.6f, 0); //as Min
        //light.transform.position = new Vector3(width, height, 0); //as Max
        light.GetComponent<FireFlyScript>().direction = UnityEngine.Random.value > 0.5f ? 1 : -1;
        light.transform.localScale = new Vector3(0.0001f, 0.0001f, light.transform.localScale.z);
        lastSpawn = Time.time;
    }
}
