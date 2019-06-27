using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

	private int snakeRot;
    private bool isTurning = false;
    GameManager gm;
    List<Transform> bodyTrans = new List<Transform>();
    Vector3 lastMonseDown;
    // Use this for initialization
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        snakeRot = 0;
        bodyTrans.Add(gameObject.transform);
        InvokeRepeating("Move", 0, 0.5f);
        AddBody();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isTurning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMonseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mouseUp = Input.mousePosition;
                Vector3 touchOffset = mouseUp - lastMonseDown;
                if (Mathf.Abs(touchOffset.x) > 100 || Mathf.Abs(touchOffset.y) > 100)
                {
                    if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x > 0)
                    {
                        snakeRot = 1;
                        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                        isTurning = true;
                    }
                    else if (Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x < 0)
                    {
                        snakeRot = 0;
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                        isTurning = true;
                    }
                    else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y > 0)
                    {

                        snakeRot = 2;
                        gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        isTurning = true;
                    }
                    else if (Mathf.Abs(touchOffset.x) < Mathf.Abs(touchOffset.y) && touchOffset.y < 0)
                    {

                        snakeRot = 3;
                        gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                        isTurning = true;
                    }
                }
            }
            
            if (Input.GetKeyDown("left") && snakeRot != 1)
            {
                snakeRot = 0;
                gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                isTurning = true;
            }

            if (Input.GetKeyDown("right") && snakeRot != 0)
            {
                snakeRot = 1;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                isTurning = true;
            }

            if (Input.GetKeyDown("up") && snakeRot != 3)
            {
                snakeRot = 2;
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                isTurning = true;
            }

            if (Input.GetKeyDown("down") && snakeRot != 2)
            {
                snakeRot = 3;
                gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                isTurning = true;
            }
        }
    }

    void Move(){
        for (int i = bodyTrans.Count - 2; i >= 0; i--)
        {
            bodyTrans[i + 1].position = bodyTrans[i].position;
        }
        gameObject.transform.Translate(new Vector3(-1, 0, 0));
        if (isTurning)
        {
            isTurning = false;
        }
    }

    public int GetBodyCount() {
        return bodyTrans.Count;
    }

    public void AddBody() {
        Vector3 newBodyPos = new Vector3(2000f, 2000f, 0);
        GameObject aBody = Instantiate(Resources.Load<GameObject>("SnakeBody"), newBodyPos, Quaternion.identity);
        bodyTrans.Add(aBody.transform);
        if (gm.GetSpeed() >= 0.1f) {
            gm.SetMoveSpeed(gm.GetSpeed() - 0.01f);
        }
        GameObject.Find("GameInfo").GetComponent<Text>().text = "身体长度：" + bodyTrans.Count.ToString();
    }

    public bool ReduceBody() {
        if (bodyTrans.Count > 1)
        {
            bodyTrans[bodyTrans.Count - 1].position = new Vector3(2000, 2000, 0);
            bodyTrans.Remove(bodyTrans[bodyTrans.Count - 1]);
            return true;
        }
        else {
            return false;
        }
    }

    public void SetSpeed(float speed) {
        CancelInvoke("Move");
        InvokeRepeating("Move", speed, speed);
    }

    public void Die() {
        CancelInvoke("Move");
    }

    public List<Transform> GetBodyTrans() {
        return bodyTrans;
    }

    public void ResetGame() {
        bodyTrans.Clear();
        GameObject[] body = GameObject.FindGameObjectsWithTag("SnakeBody");
        for (int i = 0; i < body.Length; i++) {
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
        for (int i = 1; i < bodyTrans.Count; i++) {
            bodyTrans[i].position = new Vector3(2000, 2000, 0);
        }
    }
        
}
