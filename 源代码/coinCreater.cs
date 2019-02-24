using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCreater : MonoBehaviour {
    // 2 -21.4 100
    public GameObject tester;
   // public GameObject coiner;
    private Vector3 coinPosition1;
    private Vector3 coinPosition2;
    private Vector3 coinPosition3;
    private GameObject test;
    private float timer;
   // private GameObject coin;
    // Use this for initialization
    void Start () {
        timer = 10;
        coinPosition1.Set(-2.0f, -21.4f, 100.0f);
        coinPosition2.Set( 2.0f, -21.4f, 100.0f);
        coinPosition3.Set( 6.0f, -21.4f, 100.0f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer++;
        if(timer > 10)
        test = CreateTest();
        if (timer > 200)
        { 
            Destroy(test);
            timer = 0;
        }
	}
    private GameObject CreateTest()
    {
        GameObject t;
        float nextNum = Random.value;
        if (nextNum < 0.33f)
            t = Instantiate<GameObject>(tester, coinPosition1, transform.rotation);
        else
            if (nextNum > 0.66f)
            t = Instantiate<GameObject>(tester, coinPosition2, transform.rotation);
        else
            t = Instantiate<GameObject>(tester, coinPosition3, transform.rotation);
        return t;
    }
}
