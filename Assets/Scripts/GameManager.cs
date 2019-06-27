using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private int GameMode;
    private float moveSpeed;
    public static bool isDie;
    public GameObject gameOverPanel;
    int timer=60;
    Snake snake;
    Robot robot;
    // Use this for initialization
	void Start () {
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Classic":
                GameMode = 1;
                break;
            case "Props":
                GameMode = 2;
                break;
            case "Time":
                GameMode = 3;
                break;
            case "Robot":
                GameMode = 4;
                break;
        }
        moveSpeed = 0.5f;
        isDie = false;
        if (scene.name != "Start")
        {
            snake = GameObject.Find("Snake").GetComponent<Snake>();
            Generate();
        }
        else {
            GameObject.Find("Highest").GetComponent<Text>().text = "最高分：" + PlayerPrefs.GetInt("Highest").ToString();
        }
        if (GameMode == 4) {
            robot = GameObject.Find("Robot").GetComponent<Robot>();
        }
        if (GameMode == 3) {
            timer = PlayerPrefs.GetInt("Timer");
            InvokeRepeating("Timer", 1f, 1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public int GetGameMode() {
        return GameMode;
    }

    public int GetBodyCount() {
        return snake.GetBodyCount();
    }

    public void Generate() {
        int t = Random.Range(0, 15);
        if (GameMode == 1 || t > 5) {
            t = 0;
        }
        GameObject.Find("Generater").GetComponent<Generater>().Generate(t);
    }

    public void Die(bool isRobotWin = false) {
        snake.Die();
        if (PlayerPrefs.GetInt("Highest") < snake.GetBodyCount()) {
            PlayerPrefs.SetInt("Highest", snake.GetBodyCount());
        }
        if (GameMode == 4)
        {
            robot.Die();
            if (isRobotWin)
            {
                gameOverPanel.SetActive(true);
                gameOverPanel.GetComponentInChildren<Text>().text = "很遗憾，你输了。";
                if (GameObject.Find("BackBtn"))
                {
                    GameObject.Find("BackBtn").SetActive(false);
                }
            }
            else
            {
                gameOverPanel.SetActive(true);
                gameOverPanel.GetComponentInChildren<Text>().text = "恭喜恭喜！你赢了！";
                if (GameObject.Find("BackBtn"))
                {
                    GameObject.Find("BackBtn").SetActive(false);
                }
            }
        }
        else {
            gameOverPanel.SetActive(true);
            gameOverPanel.GetComponentInChildren<Text>().text = "游戏结束啦~";
            if (GameObject.Find("BackBtn"))
            {
                GameObject.Find("BackBtn").SetActive(false);
            }
        }
    }

    public void ResetGame() {
        CancelInvoke();
        moveSpeed = 0.5f;
        snake.ResetGame();
        GameObject.Find("Snake").GetComponent<Event>().ResetGame();
        Time.timeScale = 1;
        if (GameMode == 4) {
            robot.ResetGame();
        }
    }

    public void SetSpeed(float speed) {
        snake.SetSpeed(speed);
    }

    public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
    }

    public float GetSpeed() {
        return moveSpeed;
    }

    public void SetRobotSpeed(float speed) {
        robot.SetSpeed(speed);
    }


    public void ObjectEvent(string objectName,string eventName) {
        GameObject.Find(objectName).SendMessage(eventName);
    }

    public int GetTimer() {
        return timer;
    }

    void Timer() {
        timer--;
        GameObject.Find("Canvas/TimerText").GetComponent<Text>().text = timer.ToString();
        if (timer <= 0)
        {
            Die();
            CancelInvoke();
        }
    }
}
