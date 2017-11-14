using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour {
    public Text textScore;
    private void Start()
    {
        textScore.text = "Score : " + MainScript.getScore();
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
    }
}
