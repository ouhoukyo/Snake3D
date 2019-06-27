using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{

    private int snakeRot;
    GameManager gm;
    List<Transform> bodyTrans = new List<Transform>();
    private bool isTriger = true;
    private bool isShield;
    public Material bodyColor;
    public GameObject shield;
    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        snakeRot = 0;
        bodyTrans.Add(gameObject.transform);
        InvokeRepeating("Move", 0, 0.5f);
        AddBody();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Turn(int Rot) {
        if (Rot==0 && snakeRot != 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            snakeRot = Rot;
        }

        if (Rot==1 && snakeRot != 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            snakeRot = Rot;
        }

        if (Rot==2 && snakeRot != 3)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            snakeRot = Rot;
        }

        if (Rot==3 && snakeRot != 2)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            snakeRot = Rot;
        }
    }

    void Move()
    {
        Turn(RobotAI());
        for (int i = bodyTrans.Count - 2; i >= 0; i--)
        {
            bodyTrans[i + 1].position = bodyTrans[i].position;
        }
        gameObject.transform.Translate(new Vector3(-1, 0, 0));
    }

    public int GetBodyCount()
    {
        return bodyTrans.Count;
    }

    public void AddBody()
    {
        Vector3 newBodyPos = new Vector3(2000f, 2000f, 0);
        GameObject aBody = Instantiate(Resources.Load<GameObject>("RobotBody"), newBodyPos, Quaternion.identity);
        aBody.name = "RobotBody";
        bodyTrans.Add(aBody.transform);
        if (gm.GetSpeed() >= 0.1f)
        {
            gm.SetMoveSpeed(gm.GetSpeed() - 0.01f);
        }
    }

    public bool ReduceBody()
    {
        if (bodyTrans.Count > 1)
        {
            bodyTrans[bodyTrans.Count - 1].position = new Vector3(2000, 2000, 0);
            bodyTrans.Remove(bodyTrans[bodyTrans.Count - 1]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetSpeed(float speed)
    {
        CancelInvoke("Move");
        InvokeRepeating("Move", speed, speed);
    }

    public void Die()
    {
        CancelInvoke("Move");
    }

    bool isEqual(Vector3 a, Vector3 b)
    {

        if (((a.x-b.x)<=1 && (a.x - b.x) >= -1) && ((a.z-b.z)>=-1 && (a.z - b.z)>=1))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    int RobotAI() {
        Vector3 tPos = GameObject.Find("Generater").GetComponent<Generater>().GetPos();
        Vector3 sPos = bodyTrans[0].position;
        if ((int)sPos.x==(int)tPos.x) {
            if (sPos.z > tPos.z) {
                if (snakeRot < 2)
                {
                    return 3;
                }
                else if (snakeRot == 2)
                {
                    return (sPos.x > 0) ? 0 : 1;
                }
            }else
            {
                if (snakeRot < 2)
                {
                    return 2;
                }
                else if (snakeRot == 3)
                {
                    return (sPos.x > 0) ? 0 : 1;
                }
            }
        }

        if ((int)sPos.z==(int)tPos.z)
        {
            if (sPos.x > tPos.x)
            {
                if (snakeRot >1)
                {
                    return 0;
                }
                else if (snakeRot == 1)
                {
                    return (sPos.z > -9.5) ? 2 : 3;
                }
            }
            else
            {
                if (snakeRot > 1)
                {
                    return 1;
                }
                else if (snakeRot == 0)
                {
                    return (sPos.z > -9.5) ? 2 : 3;

                }
            }
        }

        if (sPos.x > tPos.x)
        {
            if (sPos.z > tPos.z)
            {
                if (snakeRot == 1)
                {
                    return 3;
                }
                else if (snakeRot == 2)
                {
                    return 0;
                }

            }
            else
            {  
                if (snakeRot == 1)
                {
                    return 2;
                }
                else if (snakeRot == 3)
                {
                    return 0;
                }
            }
        }
        else
        {
            if (sPos.z > tPos.z)
            {
                if (snakeRot == 2)
                {
                    return 1;
                }
                else if (snakeRot == 0)
                {
                    return 3;
                }

            }
            else
            {   
                if (snakeRot == 0)
                {
                    return 2;
                }
                else if (snakeRot == 3)
                {
                    return 1;
                }
            }
        }

        return snakeRot;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isTriger)
        {
            switch (other.tag)
            {
                case "Wall":
                    RobotDie();
                    break;
                case "SnakeBody":
                    RobotDie();
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

    void TriggerState()
    {
        isTriger = true;
    }

    void RobotDie()
    {
        if (isShield)
        {
            isShield = false;
            shield.SetActive(false);
            Revive();
        }
        else
        {
            CancelInvoke();
            if (gm.GetGameMode() == 4)
            {
                gm.Die(true);
            }
        }
    }

    void TriggerEvent(int type)
    {
        switch (type)
        {
            case 1:
                if (Random.Range(0, 5) != 0)
                {
                    AddBody();
                }
                else
                {
                    ReduceBody();
                }
                break;
            case 2:
                GameObject.Find("FlashBomb").GetComponent<Light>().intensity = 10;
                InvokeRepeating("Flash", 0, 0.1f);
                break;
            case 3:
                isShield = true;
                shield.SetActive(true);
                break;
            case 4:
                SetSpeed(gm.GetSpeed() * 2);
                CancelInvoke("ResetSpeed");
                Invoke("ResetSpeed", 20f);
                break;
            case 5:
                if (gm.GetSpeed() > 0.1f)
                {
                    SetSpeed(gm.GetSpeed() / 2);
                    CancelInvoke("ResetSpeed");
                    Invoke("ResetSpeed", 20f);
                }
                break;
        }
    }

    void Flash()
    {
        GameObject.Find("FlashBomb").GetComponent<Light>().intensity -= 0.1f;
        if (GameObject.Find("FlashBomb").GetComponent<Light>().intensity <= 0)
        {
            GameObject.Find("FlashBomb").GetComponent<Light>().intensity = 0;
            CancelInvoke("Flash");
        }
    }

    void ResetSpeed()
    {
        SetSpeed(gm.GetSpeed());
    }

    public void ResetGame()
    {
        bodyTrans.Clear();
        GameObject[] body = GameObject.FindGameObjectsWithTag("SnakeBody");
        for (int i = 0; i < body.Length; i++)
        {
            Destroy(body[i]);
        }
        gameObject.transform.position = new Vector3(0, 0, -9.5f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        snakeRot = 0;
        bodyTrans.Add(gameObject.transform);
        AddBody();
    }

    public void Revive()
    {
        gameObject.transform.position = new Vector3(0, 0, -9.5f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        snakeRot = 0;
        for (int i = 1; i < bodyTrans.Count; i++)
        {
            bodyTrans[i].position = new Vector3(1, 0, -9.5f);
        }
    }
}
