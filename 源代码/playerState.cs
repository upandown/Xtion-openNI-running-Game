using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState 
{
    public int flag;
    public int idle;
    public int jump; //跳起的信号
    public int squat;  //跳起的信号
    public int left;
    public int right;
    public int path; //玩家所在的位置 0为左 1为中 2为右

    public playerState(int flag, int idle, int jump, int squat, int left, int right, int path)
    {
        this.flag = flag;
        this.idle = idle;
        this.jump = jump;
        this.squat = squat;
        this.left = left;
        this.right = right;
        this.path = path;
    }

    public void setState(int flag, int idle, int jump, int squat, int left, int right, int path) {
        this.flag = flag;
        this.idle = idle;
        this.jump = jump;
        this.squat = squat;
        this.left = left;
        this.right = right;
        this.path = path;
    }

    public bool isJumping() {

        if (this.squat == 0 && this.left == 0 && this.right == 0)
            return true;
        else
            return false;
    }
    public bool isSquating()
    {

        if (this.jump == 0 && this.left == 0 && this.right == 0)
            return true;
        else
            return false;
    }
    public bool isLefting()
    {

        if (this.squat == 0 && this.jump == 0 && this.right == 0)
            return true;
        else
            return false;
    }
    public bool isRighting()
    {

        if (this.squat == 0 && this.left == 0 && this.jump == 0)
            return true;
        else
            return false;
    }


}
