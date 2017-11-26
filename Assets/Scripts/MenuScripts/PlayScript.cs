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
        Debug.Log("nsaveFile : " + FileManagerScript.pathForDocumentsFile("colorSave"));
        String topScore = FileManagerScript.readStringFromFile(saveFile);
        if (topScore != null)
        {
            highScore = Int32.Parse(topScore);
        }
        textScore.text = "HighScore : " + highScore;
        textScore.text += "\nScore : " + MainScript.GetScore();
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
        String topScore = FileManagerScript.readStringFromFile(saveFile);
        int highScore = 0;
        if (topScore != null)
        {
            highScore = Int32.Parse(topScore);
        }

        if (score > highScore)
        {
            FileManagerScript.writeStringToFile(""+score, saveFile);
        }
        SceneManager.LoadScene("Title");
    }
}
