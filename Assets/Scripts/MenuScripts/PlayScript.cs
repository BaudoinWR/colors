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
    static DataScript data;
    private void Start()
    {
        data = FileManagerScript.LoadData();
        textScore.text = "Score : " + MainScript.GetScore();
        textScore.text += "\nHighScore : " + data.topScore;
        textScore.text += "\nLightBugs : " + data.currentBugCount;
        textScore.text += "\nTotal Travelled : " + String.Format("{0:.00}", data.totalDistanceTravelled)+"m";
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    internal static void EndGame(int score, float distanceTravelled)
    {
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
