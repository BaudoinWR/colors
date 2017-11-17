using UnityEngine;
using System.Collections;

public class SinWaveScript : MonoBehaviour
{
    Color c1 = Color.white;
    public Color c2 = Color.yellow;
    int lengthOfLineRenderer = 150;
    public float period = 0.5f;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.startWidth = 0.05F;
        //lineRenderer.endWidth = 0.05F;
    }
    void Update()
    {
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.positionCount = lengthOfLineRenderer;
        int i = 0;
        //Debug.Log(period);
        while (i < lengthOfLineRenderer)
        {
            float x = transform.position.x + i * period;
            if (x > -2)
            {
                lineRenderer.positionCount = i;
                break;
            }
            Vector3 pos = new Vector3(x, transform.position.y + Mathf.Sin(i + Time.time*2/period)*0.5f, 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}