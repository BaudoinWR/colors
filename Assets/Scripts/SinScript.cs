using UnityEngine;
using System.Collections;

public class SinScript : MonoBehaviour
{
    Color c1 = Color.white;
    public Color c2 = Color.yellow;
    int lengthOfLineRenderer = 150;
    public float period = 0.5f;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.05F, 0.05F);
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
        int i = 0;
        //Debug.Log(period);
        while (i < lengthOfLineRenderer)
        {
            float x = transform.position.x + i * period;
            if (x > -2)
            {
                lineRenderer.SetVertexCount(i);
                break;
            }
            Vector3 pos = new Vector3(x, transform.position.y + Mathf.Sin(i + Time.time*2/period)*0.5f, 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}