using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight : MonoBehaviour {

    private bool isAdd = false;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isAdd)
        {
            gameObject.GetComponentInChildren<Light>().range += Time.deltaTime*5;
            if (gameObject.GetComponentInChildren<Light>().range > 11)
            {
                isAdd = false;
            }
        }
        else {
            gameObject.GetComponentInChildren<Light>().range -= Time.deltaTime*5;
            if (gameObject.GetComponentInChildren<Light>().range < 1)
            {
                isAdd = true;
            }
        } 
    }
}
