using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scrj : MonoBehaviour
{
    public GameObject lightGenerator;
    public GameObject sin;
    float previousPosition = 0.0f;
    float previousPeak = 0.0f;
    bool isGoingUp = true;

    Queue<float> periods = new Queue<float>();
    
    // Use this for initialization
    void Start()
    {
        periods.Enqueue(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (Input.GetMouseButton(0))
        {
            float currentPosition = Input.mousePosition.y;
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
            previousPosition = currentPosition;
            float period = getAverage(periods.ToArray());
            Color col = ColorPicker.GetColor(period);
            lightGenerator.GetComponent<SpriteRenderer>().color = col;
            sin.GetComponent<SinScript>().period = period;
            sin.GetComponent<SinScript>().c2 = col;
            //Camera.main.backgroundColor = col;
            while (periods.ToArray().Length > 10)
            {
                periods.Dequeue();
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

}
