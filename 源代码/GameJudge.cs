using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJudge : MonoBehaviour
{
    // Start is called before the first frame update

    public int GameOver = 0;

  
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver == 1) {


        };
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        GameOver = 1;
    }

    public int getGameOver()
    {
        return GameOver;

    }

    public int setGameOver(int i)
    {
        return GameOver;
    }
}
