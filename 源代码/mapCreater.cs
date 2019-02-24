using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreater : MonoBehaviour {
    //3 -16 120
    //30 -180 0
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject prefab3;
    public float moveSpeed = 0;
    private GameObject curMap;
    private GameObject nextMap;
    private float timer = 0;
    private float nextNum;
    private Vector3 nextPosition;
	// Use this for initialization
	void Start () {
        curMap =  Instantiate<GameObject>(prefab1,transform.position,transform.rotation);
        nextMap = CreateNextMap();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        curMap.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        nextMap.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        if (curMap.transform.position.z > 230)
        {
            Destroy(curMap);
            curMap = nextMap;
            nextMap = CreateNextMap();
        }
    }
    private GameObject CreateNextMap()
    {
        GameObject map;
        nextNum = Random.value;
        nextPosition.Set(transform.position.x, transform.position.y, transform.position.z - 230.0f);
        if (nextNum < 0.33f)
            map = Instantiate<GameObject>(prefab1, nextPosition, transform.rotation);
        else
            if (nextNum > 0.66f)
            map = Instantiate<GameObject>(prefab2, nextPosition, transform.rotation);
        else
            map = Instantiate<GameObject>(prefab3, nextPosition, transform.rotation);
        return map;
    }

    public void setMoveSpeed(float i) {
        moveSpeed = i;
    }
}
