using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainScript : MonoBehaviour
{
    private static int score = 0;
    float time = 60;
    public Text textScore;
    public Text textTime;
    public Text textDebug;
    public GameObject lightGenerator;
    public GameObject sin;
    public GameObject flashLightHaloPrefab;
    
    private bool isChangingColor = false;

    float previousPosition = 0.0f;
    float previousPeak = 0.0f;
    bool isGoingUp = true;
    int numberAveraged = 7;

    static Queue<float> periods = new Queue<float>();

    private GameObject flashLightHalo;
    // Use this for initialization
    void Start()
    {
        score = 0;
        periods.Enqueue(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UpdateWithTouchInteraction();
        }
        else
        {
            UpdateWithMouseInteraction();
        }
        textScore.text = "Score : " + score;
        time -= Time.deltaTime;
        textTime.text = "" + time;
        if (time <= 0)
        {
            SceneManager.LoadScene("Title");
        }
    }

    private void UpdateWithTouchInteraction()
    {
        float x = 0;
        float y = 0;
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.touches[0];
            x = Camera.main.ScreenToWorldPoint(touch.position).x;
            y = Camera.main.ScreenToWorldPoint(touch.position).y;
            UpdateColor(touch.position.y);
        }
        ShowFlashLight(Input.touches.Length > 0, x, y);
    }

    private void UpdateWithMouseInteraction()
    {
        if (Input.GetMouseButton(0)) {
            UpdateColor(Input.mousePosition.y);
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShowFlashLight(Input.GetMouseButton(0), mousePos.x, mousePos.y);
    }

    private void ShowFlashLight(bool touching, float x, float y)
    {
        textDebug.text += "\nflashlight x : " + x;
        textDebug.text += "\nflashlight y : " + y;
        if (touching && !isChangingColor)
        {
            if (flashLightHalo == null)
            {
                flashLightHalo = Instantiate(flashLightHaloPrefab);
                flashLightHalo.GetComponent<Light>().color = lightGenerator.GetComponent<SpriteRenderer>().color;
            }

            flashLightHalo.transform.position = new Vector3(x, y);
        }
        else if (flashLightHalo != null)
        {
            Destroy(flashLightHalo);
            flashLightHalo = null;
        }
    }

    private void UpdateColor(float currentPosition)
    {
        textDebug.text = "Mouse : " + Input.GetMouseButton(0);
        textDebug.text += "\ncurrentPosition : " + currentPosition;
        textDebug.text += "\npreviousPosition : " + previousPosition;
        textDebug.text += "\nchangingColor : " + isChangingColor;

        if (isChangingColor)
        {
            UpdateDirection(currentPosition);
            textDebug.text += "\ngoingUp : " + isGoingUp;

            previousPosition = currentPosition;

            float period = getAverage(periods.ToArray());
            textDebug.text += "\nperiod : " + period;

            Color col = ColorScript.GetColor(period);
            lightGenerator.GetComponent<SpriteRenderer>().color = col;
            sin.GetComponent<SinWaveScript>().period = period;
            sin.GetComponent<SinWaveScript>().c2 = col;
            textDebug.text += "\ncolor : " + col;

            while (periods.ToArray().Length > numberAveraged)
            {
                periods.Dequeue();
            }
        }
    }

    private void UpdateDirection(float currentPosition)
    {
        if (isGoingUp)
        {
            if (previousPosition > currentPosition)
            {
                isGoingUp = false;
                periods.Enqueue(calculatePeriod());
            }
        }
        else
        {
            if (previousPosition < currentPosition)
            {
                isGoingUp = true;
                periods.Enqueue(calculatePeriod());
            }
        }
    }

    private float getAverage(float[] arr)
    {
        float tot = 0;
        foreach(float value in arr)
        {
            tot += value;
        }
        return tot / arr.Length;
    }

    private float calculatePeriod()
    {
        float currentTime = Time.time;
        float period = currentTime - previousPeak;
        previousPeak = currentTime;
        return period;
    }

    public static void increaseScore()
    {
        score++;
        periods = new Queue<float>();
        periods.Enqueue(1);
    }

    public static int getScore()
    {
        return score;
    }

    public void switchColorChanger()
    {
        this.isChangingColor = !this.isChangingColor;
    }
    public void switchColorChanger(Boolean force)
    {
        this.isChangingColor = force;
    }
}
