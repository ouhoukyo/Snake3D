using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

    GameManager gm;
    public Material bodyColor;
    public GameObject shield;
    private bool isTriger = true;
    private bool isShield;
    private bool isRobotShield;
    Material robotColor;
    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (isTriger) {
            switch (other.tag)
            {
                case "Wall":
                    Die();
                    break;
                case "SnakeBody":
                    Die();
                    break;
                case "Apple":
                    TriggerEvent(1);
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
                case "Flash":
                    TriggerEvent(2);
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
                case "Random":
                    TriggerEvent(Random.Range(1, 6));
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
                case "Shield":
                    TriggerEvent(3);
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
                case "SlowDown":
                    TriggerEvent(4);
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
                case "SpeedUp":
                    TriggerEvent(5);
                    bodyColor.color = other.GetComponentInChildren<Light>().color;
                    other.transform.position = new Vector3(0, -100, 0);
                    gm.Generate();
                    break;
            }
            isTriger = false;
            Invoke("TriggerState", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriger = true;
    }

    void TriggerState() {
        isTriger = true;
    }

    void Die() {
        if (gm.GetGameMode() == 3)
        {
            if (gm.GetTimer() > 0)
            {
                GameObject.Find("Snake").GetComponent<Snake>().Revive();
            }
            else {
                gm.Die();
            }
            
        }
        else {
            if (isShield)
            {
                isShield = false;
                shield.SetActive(false);
                GameObject.Find("Snake").GetComponent<Snake>().Revive();
            }
            else
            {
                CancelInvoke();
                if (gm.GetGameMode() == 4)
                {
                    gm.Die(true);
                }
                else
                {
                    gm.Die();
                }

            }
        }
    }

    void TriggerEvent(int type)
    {
        switch (type)
        {
            case 1:
                if (gm.GetGameMode() != 1)
                {
                    if (Random.Range(0, 8) != 0)
                    {
                        gameObject.GetComponent<Snake>().AddBody();
                    }
                    else
                    {
                        gameObject.GetComponent<Snake>().ReduceBody();
                    }
                }
                else {
                    gameObject.GetComponent<Snake>().AddBody();
                }
                break;
            case 2:
                GameObject.Find("FlashBomb").GetComponent<Light>().intensity = 10;
                InvokeRepeating("Flash", 0, 0.1f);
                break;
            case 3:
                if (gm.GetGameMode() != 3)
                {
                    isShield = true;
                    shield.SetActive(true);
                }
                else {
                    TriggerEvent(1);
                }
                break;
            case 4:
                gm.SetSpeed(gm.GetSpeed() * 2);
                CancelInvoke("ResetSpeed");
                Invoke("ResetSpeed", 20f);
                break;
            case 5:
                if (gm.GetSpeed() > 0.1f){
                    gm.SetSpeed(gm.GetSpeed() / 2);
                    CancelInvoke("ResetSpeed");
                    Invoke("ResetSpeed", 20f);
                }
                break;
        }
    }

    void Flash(){
        GameObject.Find("FlashBomb").GetComponent<Light>().intensity -= 0.1f;
        if (GameObject.Find("FlashBomb").GetComponent<Light>().intensity <= 0) {
            GameObject.Find("FlashBomb").GetComponent<Light>().intensity = 0;
            CancelInvoke("Flash");
        }
    }

    void ResetSpeed() {
        gm.SetSpeed(gm.GetSpeed());
    }

    public void ResetGame() {
        isTriger = true;
        isShield =false;
        isRobotShield=false;
        CancelInvoke();
    }
}
