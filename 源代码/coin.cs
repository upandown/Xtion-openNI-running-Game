using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour {

    private bool isTrigger;
    public GameObject coiner;
    public float moveSpeed;
    private float timer;
    private GameObject c;
	// Use this for initialization
	void Start () {
        isTrigger = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer++;
		if((!isTrigger)&&(timer>10))
        {
            timer = 0;
            c = Instantiate<GameObject>(coiner, transform.position, transform.rotation);
        }
        if(c)
            c.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        if (timer > 100)
        {
            Destroy(c);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
    }
}
