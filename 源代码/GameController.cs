using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    private playerState state;
    public GameObject enterGameUI;
    public GameObject enterText;
    public GameObject playerBoom;
    public GameObject playerCollider;
    public GameObject player;
    public GameObject BGM;
    public GameObject timerVoice;
    public GameObject loseVoice;
    public GameObject GameOverUI;
    public Text GameOverScore;
    public GameObject boomVoice;
    public GameObject skeleton;
    public GameObject scoreText;
    public GameObject GameingUI;
    public GameObject skeletonCamera;


    public int score = 0;

    public float timeScore = 0.1f;
    public int start = 0;
    private float time = 1;
    public float deltaTime = 0;
    public float timer =0;
    // Start is called before the first frame update
    void Start()
    {
        state = GameObject.Find("ybot").GetComponent<Player>().getPlayerState();
    }

    // Update is called once per frame
    void Update()
    {
        if (state.flag == 0)
        {
            GameObject.Find("MapCreater2").GetComponent<mapCreater>().setMoveSpeed(0);

            deltaTime += Time.deltaTime;
            if (deltaTime > time)
            {

                string test = "正在检测玩家";
                switch (timer)
                {
                    case 0:
                        timer++;
                        break;
                    case 1:
                        test += "·";
                        timer++;
                        break;
                    case 2:
                        test += "··";
                        timer++;
                        break;
                    case 3:
                        test += "···";
                        timer = 0;
                        break;
                }


                enterText.GetComponent<Text>().text = test;
                deltaTime = 0;
            }
        }
        else
        {
            if (start == 0)
            {
                enterText.GetComponent<Text>().text = "";
                timer = 0;
                deltaTime = 0;
                start = 1;
                enterText.GetComponent<Text>().alignment =TextAnchor.MiddleCenter;
                enterText.GetComponent<Text>().fontSize *= 3;
            }
      

            if(start == 1)
            {
              
                deltaTime += Time.deltaTime;
                if (deltaTime > time)
                {

                    string test = "";
                    switch (timer)
                    {
                        case 0:
                            timer++;
                            break;
                        case 1:
                            test += "3";
                            timerVoice.SetActive(true);
                            timer++;
                            break;
                        case 2:
                            test += "2";
                            timer++;
                            break;
                        case 3:
                            test += "1";
                            timer++;
                            break;
                        case 4:
                            timer = 0;
                            GameObject.Find("MapCreater2").GetComponent<mapCreater>().setMoveSpeed(5);
                            player.GetComponent<Animator>().SetBool("flag", true);
                            skeletonCamera.SetActive(true);
                            enterGameUI.SetActive(false);
                            skeleton.SetActive(true);
                            start = 2;
                            BGM.SetActive(true);
                            GameingUI.SetActive(true);
                            break;
                    }
                    enterText.GetComponent<Text>().text = test;
                    deltaTime = 0;

                }

               

            }
            if (start == 2)
            {
                deltaTime += Time.deltaTime;
                if (deltaTime > timeScore)
                {
                    score++;
                    scoreText.GetComponent<Text>().text = score.ToString();
                    deltaTime = 0;
                }

            }

            int gameOver = playerCollider.GetComponent<GameJudge>().getGameOver();
            if(gameOver == 1)
            {
                start = 3;
                timerVoice.SetActive(false);
                boomVoice.SetActive(true);
                playerBoom.SetActive(true);
                BGM.SetActive(false);
                loseVoice.SetActive(true);
                StartCoroutine(EndGame());
            }
        }
    }


    public int getStart()
    {

        return start;

    }


    IEnumerator EndGame() {
        GameObject.Find("MapCreater2").GetComponent<mapCreater>().setMoveSpeed(0);
        player.GetComponent<Animator>().speed = 0;
        player.GetComponent<Player>().endGame();
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(0);
        GameOverScore.text = "得分：" + score.ToString();
        GameOverUI.SetActive(true);
    }
}
