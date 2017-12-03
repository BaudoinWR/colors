using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour {
    public Text textScore;
    static string saveFile = "colorSave";
    public int highScore = 0;
    public static DataScript data;

    private void Start()
    {
        // SetAspectRatio(Camera.allCameras);
        data = FileManagerScript.LoadData();
        textScore.text = "Score : " + MainScript.GetScore();
        textScore.text += "\nHighScore : " + data.topScore;
        textScore.text += "\nLightBugs : " + data.currentBugCount;
        textScore.text += "\nTotal Travelled : " + String.Format("{0:.00}", data.totalDistanceTravelled) + "m";
    }

    public static void SetAspectRatio(Camera[] cameras)
    {
        
        float targetaspect = 16.0f / 10.0f;
        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;
        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;
        // obtain camera component so we can modify its viewport
        
        for(int i = 0; i < cameras.Length; i++)
        {
            Debug.Log("Setting camera " + i);
            Camera camera = cameras[i];
            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;
                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;
                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;
                Rect rect = camera.rect;
                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;
                camera.rect = rect;
            }
        }
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    internal static void EndGame(int score, float distanceTravelled)
    {
        ShopScript.ResetTemporaryBoosts(data);
        data.totalBugCaught += score;
        data.currentBugCount += score;
        data.totalDistanceTravelled += distanceTravelled;
        data.tripsTaken++;
        int topScore = data.topScore;

        if (score > topScore)
        {
            data.topScore = score;
        }
        FileManagerScript.SaveData(data);
        SceneManager.LoadScene("Title");
    }


}
