using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TimeStop() {
        Time.timeScale = 0;
    }

    public void TimeStart() {
        Time.timeScale = 1;
    }

    public void ExitGame() {
        GameObject.Find("GameManager").GetComponent<GameManager>().ResetGame();
        SceneManager.LoadScene("Start");
    }

    public void ShowPanel() {
        if (GameObject.Find("ExitPanel"))
        {
            TimeStop();
        }
        else {
            TimeStart();
        }
    }
}
