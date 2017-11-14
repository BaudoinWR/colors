using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    private static int score = 0;
    float time = 60;
    public Text textScore;
    public Text textTime;
    public GameObject lightGenerator;
    public GameObject sin;
    public GameObject flashLightHaloPrefab;
    
    public bool isChangingColor = false;

    float previousPosition = 0.0f;
    float previousPeak = 0.0f;
    bool isGoingUp = true;
    int numberAveraged = 7;

    static Queue<float> periods = new Queue<float>();

    private GameObject flashLightHalo;
    // Use this for initialization
    void Start()
    {
        periods.Enqueue(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateColor();
        ShowFlashLight();
        textScore.text = "Score : " + score;
        time -= Time.deltaTime;
        textTime.text = "" + time;
        if (time <= 0)
        {
            SceneManager.LoadScene("Title");
        }
    }

    private void ShowFlashLight()
    {
        if (Input.GetMouseButton(0) && !isChangingColor)
        {
            if (flashLightHalo == null)
            {
                flashLightHalo = Instantiate(flashLightHaloPrefab);
                flashLightHalo.GetComponent<Light>().color = lightGenerator.GetComponent<SpriteRenderer>().color;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flashLightHalo.transform.position = new Vector3(mousePos.x, mousePos.y);
        }
        else if (flashLightHalo != null)
        {
            Destroy(flashLightHalo);
            flashLightHalo = null;
        }
    }

    private void UpdateColor()
    {
        if (Input.GetMouseButton(0) && isChangingColor)
        {
            float currentPosition = Input.mousePosition.y;
            UpdateDirection(currentPosition);
            previousPosition = currentPosition;

            float period = getAverage(periods.ToArray());

            Color col = ColorScript.GetColor(period);
            lightGenerator.GetComponent<SpriteRenderer>().color = col;
            sin.GetComponent<SinWaveScript>().period = period;
            sin.GetComponent<SinWaveScript>().c2 = col;

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
}
