using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShow : MonoBehaviour
{
    public GameObject player;
    public GameObject Head;
    public GameObject LF;
    public GameObject LE;
    public GameObject RF;
    public GameObject RE;
    public GameObject Body;

    private playerSkeleton pSkeleton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pSkeleton = player.GetComponent<Player>().GetSkeleton();

        Head.transform.position = new Vector3(pSkeleton.head.x, pSkeleton.head.y - 150, pSkeleton.head.z);
        LF.transform.position = pSkeleton.Left_Hand;
        LE.transform.position = pSkeleton.Left_ELBOW;
        RF.transform.position = pSkeleton.Right_Hand;
        RE.transform.position = pSkeleton.Right_ELBOW;
        Body.transform.position = pSkeleton.TORSO;
    }
}
