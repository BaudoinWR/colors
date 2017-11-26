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
    private void Start()
    {
        DataScript data = FileManagerScript.LoadData();
        textScore.text = "Score : " + MainScript.GetScore();
        textScore.text += "\nHighScore : " + data.topScore;
        textScore.text += "\nLightBugs : " + data.currentBugCount;
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    internal static void EndGame(int score)
    {
        DataScript data = FileManagerScript.LoadData();
        data.t += score;
        data.currentBugCount += score;

        int topScore = data.topScore;

        if (score > topScore)
        {
            data.topScore = score;
        }
        FileManagerScript.SaveData(data);
        SceneManager.LoadScene("Title");
    }
}
