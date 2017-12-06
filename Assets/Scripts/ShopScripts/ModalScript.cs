using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalScript : MonoBehaviour {
    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnClickShow()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OnClickHide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
