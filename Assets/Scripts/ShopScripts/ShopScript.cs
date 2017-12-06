using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour {
    private const int batteryBoostCost = 10;
    private const int instantLureCost = 150;
    private const int spawnBoostCost = 50;

    public void OnClickBatteryBoost()
    {
        if (PlayScript.data.currentBugCount >= batteryBoostCost && !PlayScript.data.batteryBoost)
        {
            PlayScript.data.batteryBoost = true;
            PlayScript.data.currentBugCount -= batteryBoostCost;
        }
        FileManagerScript.SaveData(PlayScript.data);
    }

    public void OnClickInstantLure()
    {
        if (PlayScript.data.currentBugCount >= instantLureCost && !PlayScript.data.instantLure)
        {
            PlayScript.data.instantLure = true;
            PlayScript.data.currentBugCount -= instantLureCost;
        }
        FileManagerScript.SaveData(PlayScript.data);
    }

    public void OnClickSpawnBoost()
    {
        if (PlayScript.data.currentBugCount >= spawnBoostCost && !PlayScript.data.spawnBoost)
        {
            PlayScript.data.spawnBoost = true;
            PlayScript.data.currentBugCount -= spawnBoostCost;
        }
        FileManagerScript.SaveData(PlayScript.data);
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("Title");
    }

    internal static void ResetTemporaryBoosts(DataScript data)
    {
        data.batteryBoost = false;
        data.spawnBoost = false;
    }
}
