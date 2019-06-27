using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
	}

    public void LoadClassic() {
        SceneManager.LoadScene("Classic");
    }

    public void LoadProps() {
        SceneManager.LoadScene("Props");
    }

    public void LoadTime() {
        int time = (int)GameObject.Find("TimeSlider").GetComponent<Slider>().value;
        PlayerPrefs.SetInt("Timer", time);
        SceneManager.LoadScene("Time");
    }

    public void SliderValueChanged() {
        int time = (int)GameObject.Find("TimeSlider").GetComponent<Slider>().value;
        GameObject.Find("TimeText").GetComponent<Text>().text = time.ToString()+"s";
    }

    public void LoadRobot() {
        SceneManager.LoadScene("Robot");
    }

    public void Exit() {
        if (GameObject.Find("TimePanel"))
        {
            GameObject.Find("TimePanel").SetActive(false);
        }
        if (GameObject.Find("ExitPanel"))
        {
            GameObject.Find("ExitPanel").SetActive(false);
        }
    }

    public void EixtGame() {
        Application.Quit();
    }

    
}
