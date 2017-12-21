using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainScript : MonoBehaviour
{
    private static int score = 0;
    public Slider time;
    public Text textScore;
    public Text textDebug;
    public GameObject lightGenerator;
    public GameObject sin;
    public GameObject flashLightHaloPrefab;
    
    private bool isChangingColor = false;

    float previousPosition = 0.0f;
    float previousPeak = 0.0f;
    bool isGoingUp = true;
    int numberAveraged = 5;
    float speed = 1.3f;

    private float distance = 0.0f;

    static Queue<float> periods = new Queue<float>();

    private GameObject flashLightHalo;
    // Use this for initialization
    void Start()
    {
        // PlayScript.SetAspectRatio(Camera.allCameras);
        score = 0;
        periods.Enqueue(1);
        if (PlayScript.data.batteryBoost)
        {
            time.maxValue += 15;
            time.value = time.maxValue;
            Debug.Log("Battery Boosted.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance += speed * Time.deltaTime;

        if (Application.platform == RuntimePlatform.Android)
        {
            UpdateWithTouchInteraction();
        }
        else
        {
            UpdateWithMouseInteraction();
        }
        textScore.text = "Score : " + score;
        textScore.text += "\nDistance : " + String.Format("{0:.00}", distance);
        time.value -= Time.deltaTime;

        if (time.value <= 0)
        {
            PlayScript.EndGame(score, distance);
        }
    }

    internal void RestoreTime(float timeValue)
    {
        time.value += timeValue;
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
        if (touching && !isChangingColor)
        {
            if (flashLightHalo == null)
            {
                flashLightHalo = Instantiate(flashLightHaloPrefab);
                flashLightHalo.GetComponent<Light>().color = lightGenerator.GetComponent<SpriteRenderer>().material.GetColor("_DesiredColor");
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
        textDebug.text += "\nsaveFile : " + FileManagerScript.pathForDocumentsFile("colorSave");

        if (isChangingColor)
        {
            UpdateDirection(currentPosition);
            textDebug.text += "\ngoingUp : " + isGoingUp;

            previousPosition = currentPosition;

            float period = GetAverage(periods.ToArray());
            textDebug.text += "\nperiod : " + period;

            Color col = ColorScript.GetColor(period);
            lightGenerator.GetComponent<SpriteRenderer>().material.SetColor("_DesiredColor", col);
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
                periods.Enqueue(CalculatePeriod());
            }
        }
        else
        {
            if (previousPosition < currentPosition)
            {
                isGoingUp = true;
                periods.Enqueue(CalculatePeriod());
            }
        }
    }

    private float GetAverage(float[] arr)
    {
        float tot = 0;
        if (arr.Length > 0)
        {
            foreach(float value in arr)
            {
                tot += value;
            }
            return tot / arr.Length;
        }
        else return 1;
    }

    private float CalculatePeriod()
    {
        float currentTime = Time.time;
        float period = currentTime - previousPeak;
        previousPeak = currentTime;
        float average = GetAverage(periods.ToArray());
        if (average > 2 * period || average < 0.5 * period)
        {
            ResetPeriods();
        }
        return period;
    }

    public static void IncreaseScore()
    {
        score++;
        ResetPeriods();
    }

    private static void ResetPeriods()
    {
        periods.Clear();
    }

    public static int GetScore()
    {
        return score;
    }

    public void SwitchColorChanger()
    {
        this.isChangingColor = !this.isChangingColor;
    }
    public void SwitchColorChanger(Boolean force)
    {
        this.isChangingColor = force;
    }
}
