using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkeleton 
{
    public Vector3 head;
    public Vector3 TORSO;
    public Vector3 Left_ELBOW;
    public Vector3 Left_Hand;
    public Vector3 Right_ELBOW;
    public Vector3 Right_Hand;

    public playerSkeleton()
    {
        head = new Vector3(0, 0, 0);
        TORSO = new Vector3(0, 0, 0);
        Left_ELBOW = new Vector3(0, 0, 0);
        Left_Hand = new Vector3(0, 0, 0);
        Right_ELBOW = new Vector3(0, 0, 0);
        Right_Hand = new Vector3(0, 0, 0);
    }
}
