using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalScript : MonoBehaviour {
    public GameObject oneBatteryBoostModal;
    
    public void Start()
    {
        oneBatteryBoostModal.SetActive(false);
    }

    public void OnClickShowOneBatteryBoostModal()
    {
        oneBatteryBoostModal.SetActive(true);
    }
    public void OnClickHideOneBatteryBoostModal()
    {
        oneBatteryBoostModal.SetActive(false);
    }
}
