using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

    private playerState state;
    private Rigidbody rd;
    public GameObject jumpVoice;
    public GameObject squatVoice;


    public float leftAndRightDistance = 1.0f;
    public float deltaDis = 0;
    public float moveSpeed = 3;
    public Animator animator;
    public GameObject controller;
    //private Transform priTransform;
    private Vector3 priPosition;
    public  GameObject colliderObject;

    public float jumpHeight = 5;

    // Start is called before the first frame update
    void Start()
    {
        state = this.GetComponent<Player>().getPlayerState();
        rd = GetComponent<Rigidbody>();
        priPosition = transform.position;
        //animator = gameObject.GetComponent<Animator>(); 
        //boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.GetComponent<GameController>().getStart() == 2)
        {
            if (state.flag == 1)
            {
                //animator.SetBool("flag", true);

            }
            if (state.jump == 1)
            {
                rd.velocity = (new Vector3(0, 1.0f, 0)) * jumpHeight;
                jumpVoice.SetActive(true);
                animator.SetBool("jump", true);
                //state.setState(1, 0, 0, 0, 0, 0, state.path);
                StartCoroutine(jump());
            }
            else if (state.squat == 1)
            {
                //state.setState(1, 0, 0, 0, 0, 0, state.path);
                animator.SetBool("squat", true);
                //boxCollider.enabled = false;
                squatVoice.SetActive(true);
                StartCoroutine(squat());

            }
            else if (state.left == 1)
            {
                if (state.path == 2)
                {
                    if (deltaDis < leftAndRightDistance)
                    {
                        transform.Translate(new Vector3(leftAndRightDistance, 0, 0) * Time.deltaTime * moveSpeed);
                        deltaDis += leftAndRightDistance * Time.deltaTime * moveSpeed;
                    }
                    else
                    {
                        deltaDis = 0;
                        transform.position = priPosition;
                        //StartCoroutine(stopSkeleton(1));
                        state.setState(1, 0, 0, 0, 0, 0, 1);
                    }
                }
                else if (state.path == 1)
                {
                    if (deltaDis < leftAndRightDistance)
                    {
                        transform.Translate(new Vector3(leftAndRightDistance, 0, 0) * Time.deltaTime * moveSpeed);
                        deltaDis += leftAndRightDistance * Time.deltaTime * moveSpeed;
                    }
                    else
                    {
                        deltaDis = 0;

                        state.setState(1, 0, 0, 0, 0, 0, 0);
                    }
                }
                else
                {
                    state.setState(1, 0, 0, 0, 0, 0, state.path);
                }

            }
            else if (state.right == 1)
            {
                if (state.path == 0)
                {
                    if (deltaDis < leftAndRightDistance)
                    {
                        transform.Translate(new Vector3(-leftAndRightDistance, 0, 0) * Time.deltaTime * moveSpeed);
                        deltaDis += leftAndRightDistance * Time.deltaTime * moveSpeed;
                    }
                    else
                    {
                        deltaDis = 0;
                        transform.position = priPosition;
                        //StartCoroutine(stopSkeleton(1));
                        state.setState(1, 0, 0, 0, 0, 0, 1);
                    }
                }
                else if (state.path == 1)
                {
                    if (deltaDis < leftAndRightDistance)
                    {
                        transform.Translate(new Vector3(-leftAndRightDistance, 0, 0) * Time.deltaTime * moveSpeed);
                        deltaDis += leftAndRightDistance * Time.deltaTime * moveSpeed;
                    }
                    else
                    {
                        deltaDis = 0;

                        state.setState(1, 0, 0, 0, 0, 0, 2);
                    }
                }
                else
                {
                    state.setState(1, 0, 0, 0, 0, 0, state.path);
                }

            }

        }
      
    }


    IEnumerator jump()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("jump", false);
        state.setState(1, 0, 0, 0, 0, 0, state.path);
        rd.velocity = (new Vector3(0, -1.0f, 0)) * jumpHeight;
        jumpVoice.SetActive(false);
    }

    IEnumerator squat()
    {
        colliderObject.GetComponent<CapsuleCollider>().height = 0.47f;
        colliderObject.GetComponent<CapsuleCollider>().center = new Vector3(0, -0.52f, 0);/*.transform.Translate(new Vector3(0, -0.48f, 0), Space.Self);*/
        //colliderObject.transform.Translate(new Vector3(0, -0.3f, 0), Space.Self);
        yield return new WaitForSeconds(1.167f);

        animator.SetBool("squat", false);
        state.setState(1, 0, 0, 0, 0, 0, state.path);
        //boxCollider.enabled = true;
        //colliderObject.GetComponent<CapsuleCollider>().transform.Translate(new Vector3(0, 0.48f, 0), Space.Self);
        colliderObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0); /*Set(0, 0, 0);*/
        colliderObject.GetComponent<CapsuleCollider>().height = 1.3f;
        squatVoice.SetActive(false);
        //colliderObject.transform.Translate(new Vector3(0, 0.3f, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "plane" )
        //{

        //    animator.SetBool("jump", false);

        //}

    }
}
