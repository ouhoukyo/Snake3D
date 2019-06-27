using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour {

    private Vector3[,] ScenePos;
    private int HPos;
    private int VPos;
    private Vector3 tPos;
    // Use this for initialization
    private void Awake()
    {
        Vector3 TopLeft = new Vector3(-13f, 0, -2.5f);
        Vector3 BottomRight = new Vector3(13f, 0f, -17.5f);
        HPos = (int)(BottomRight.x - TopLeft.x + 1);
        VPos = (int)(-BottomRight.z + TopLeft.z + 1);
        ScenePos = new Vector3[HPos, VPos];
        float yPos = 0f;
        for (int i = 0; i < HPos; i++)
        {
            for (int j = 0; j < VPos; j++)
            {
                float xPos = -13f + i;
                float zPos = -(2.5f + j);
                ScenePos[i, j] = new Vector3(xPos, yPos, zPos);
            }
        }
    }

    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Generate(int type) {
        tPos = ScenePos[Random.Range(0,HPos),Random.Range(0,VPos)];
        GameObject Collector = GameObject.Find("Generater/Apple");
        switch (type) {
            case 0:
                Collector = GameObject.Find("Generater/Apple");
                break;
            case 1:
                Collector = GameObject.Find("Generater/Flash");
                break;
            case 2:
                Collector = GameObject.Find("Generater/Random");
                break;
            case 3:
                Collector = GameObject.Find("Generater/Shield");
                break;
            case 4:
                Collector = GameObject.Find("Generater/SlowDown");
                break;
            case 5:
                Collector = GameObject.Find("Generater/SpeedUp");
                break;
        }
        Collector.transform.position = tPos;
        Collector.GetComponentInChildren<Light>().color= new Color(Random.value, Random.value, Random.value, 1);
        Collector.GetComponentInChildren<Light>().intensity = 2;

    }

    public Vector3 GetPos() {
        return tPos;
    }
}
