using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;



// jumpDis = 0.13 limitTime = 0.7
public class Player : MonoBehaviour
{
    [DllImport("OpenNIGetSkeleton")]
    static extern void GetMySkelenton(float[] b);
    [DllImport("OpenNIGetSkeleton")]
    static extern void Test(float[] b);


    static float[] skeleton;



    //public GameObject head;
    //public GameObject TORSO;
    //public GameObject Left_ELBOW;
    //public GameObject Left_Hand;
    //public GameObject Right_ELBOW;
    //public GameObject Right_Hand;

    public GameObject player;
    public playerState state;

    public int tflag = 0;
    public int tjump = 0;  //跳起的信号
    public int tsquat = 0;  //跳起的信号
    public int tleft = 0;
    public int tright = 0;
    public int tpath = 0;


    public int StartTime = 3;

    public float jumpDistence =100.0f;
    public float squatDistence = 180.0f;
    public float leftDistence = 200f;
    public float rightDistence = 200f;
    public float accuracy = 1;

    private playerSkeleton nowPlayerSk;
    private playerSkeleton priPlayerSk;

    public float testTime = 0;
    public float limitTime = 0.9f;
   
    void Start()
    {
        //a = 0;
        //b = 0;
        //c = 0;
        //p = 0;
        //StartCoroutine(getSkeleton(a, b, c));
        skeleton = new float[19];
        for (int i = 0; i < 19; i++)
            skeleton[i] = 0;
        StartNewThread();
        nowPlayerSk = new playerSkeleton();
        priPlayerSk = new playerSkeleton();
        state = new playerState(0, 0, 0, 0, 0, 0, 1);
    }

    void getCoordSkeleton(playerSkeleton playerSkl) {
        //float x = 0, y = 0, z = 0;
        //for (int i = 0; i < 6; i++) {
        //    x += b[i * 3];
        //    y += b[i * 3 + 1];
        //    z += b[i * 3 + 2];
        //}
        //x /= 6;
        //y /= 6;
        //z /= 6;
        playerSkl.head =  new Vector3(skeleton[0] / accuracy, skeleton[1] / accuracy, skeleton[2] / accuracy);
        playerSkl.Left_ELBOW = new Vector3(skeleton[3] / accuracy, skeleton[4] / accuracy, skeleton[5] / accuracy);
        playerSkl.Left_Hand = new Vector3(skeleton[6] / accuracy, skeleton[7] / accuracy, skeleton[8] / accuracy);
        playerSkl.Right_ELBOW = new Vector3(skeleton[9] / accuracy, skeleton[10] / accuracy, skeleton[11] / accuracy);
        playerSkl.Right_Hand = new Vector3(skeleton[12] / accuracy, skeleton[13] / accuracy, skeleton[14] / accuracy);
        playerSkl.TORSO = new Vector3(skeleton[15] / accuracy, skeleton[16] / accuracy, skeleton[17] / accuracy);
     
    }


    void isJump(playerSkeleton pri, playerSkeleton now) {
        if (state.jump == 0 && ((Mathf.Abs(pri.head.y - now.head.y))  > jumpDistence )&& state.flag != 0 && (pri.head.y - now.head.y ) < 0 && state.isJumping() && pri.head.y != 0)
            state.jump = 1;      //跳起
    }

    void isSquat(playerSkeleton pri, playerSkeleton now)
    {
        if (state.squat  == 0 && ((Mathf.Abs(pri.head.y - now.head.y)) > squatDistence) && state.flag != 0 && (pri.head.y - now.head.y) > 0 && state.isSquating())
            state.squat = 1;      //跳起
    }

    void isLeft(playerSkeleton pri, playerSkeleton now)
    {
       


        //}
        if (state.left == 0 && now.Left_Hand.z - now.head.z > leftDistence && state.flag != 0 && state.isLefting())
        {
            state.left = 1;      //跳起
            //StartCoroutine(stopSkeleton(1 , true));


        }
    }

    void isRight(playerSkeleton pri, playerSkeleton now)
    {
       
        if (state.right == 0 && now.Right_Hand.z - now.head.z > leftDistence && state.flag != 0 && state.isRighting())
        {
            state.right = 1;      //跳起
                                  //StartCoroutine(stopSkeleton(1, false));
                                  //for (float i = 0; i < 10; i += Time.deltaTime) ;


        }
    }


    // Update is called once per frame
    void Update()
    {
        if (nowPlayerSk.head.x != 0 && state.flag == 0) {
            print("succeed detect an user!");
            state.flag = 1;
            //StartCoroutine(writePriSkeleton());

        }
         
       


            getCoordSkeleton(nowPlayerSk);

            isLeft(priPlayerSk, nowPlayerSk);
            isRight(priPlayerSk, nowPlayerSk);
            isJump(priPlayerSk, nowPlayerSk);
            isSquat(priPlayerSk, nowPlayerSk);


            if (Input.GetKeyDown(KeyCode.A))
            {
                skeleton[18] = 1.0f;
                print("proccess quit....");
            }
            if (testTime > limitTime)
            {
                getCoordSkeleton(priPlayerSk);
                //isJump(priPlayerSk, nowPlayerSk);
                //isSquat(priPlayerSk, nowPlayerSk);
                testTime = 0;
            }
            else
                testTime += Time.deltaTime;

            tjump = state.jump;
            tsquat = state.squat;
            tleft = state.left;
            tright = state.right;


        

    }

    Thread thread;
    private void StartNewThread() {
        thread = new Thread(new ThreadStart(SubThread));
        thread.Start();
    }

    private void SubThread() {
        GetMySkelenton(skeleton);
    }

    private void OnDestroy() {
        if (thread != null)
            thread.Abort();

    }

    //IEnumerator writePriSkeleton()
    //{
    //    yield return new WaitForSeconds(5);
    //    getCoordSkeleton(priPlayerSk);
    //    print("record over");
    //}
    public playerState getPlayerState()
    {

        return state;
    }


    public playerSkeleton GetSkeleton()
    {
        return this.nowPlayerSk;

    }


    public void endGame()
    {

        skeleton[18] = 1.0f;

    }

    //IEnumerator stopSkeleton(int i, bool a)
    //{
       
    //    if (a)
    //        state.left = i;
    //    else
    //        state.right = i;
    //    yield return new WaitForSeconds(1.2f);
    //}
}
