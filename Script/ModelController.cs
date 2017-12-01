using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ModelController : MonoBehaviour {

    //get the component and create a empty value 
    private Rigidbody rb;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;
    private CapsuleCollider col;
    private Vector3 movement;
    public float speed = 3;
    private float animSpeed = 1.5f;
    public float jumpPower;
    private bool jumped;
    public bool isGrounded;
    private Transform start;
    private MeshRenderer plane;
    //static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    // Use this for initialization

    //Animator reference to each state 
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Resjumot");

    void Start()
    {
        start = GameObject.Find("spawnPoint").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();//get the rigidbody from the player
        anim = GetComponent<Animator>();//get animator from the player
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0); ;// make rotation 0
        float h = CrossPlatformInputManager.GetAxis("Horizontal");//get horizontal axis from joystick
        float v = CrossPlatformInputManager.GetAxis("Vertical");//get vertical axis from joystick
        jumped = CrossPlatformInputManager.GetButtonDown("Jump");// get jump button to work 
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);//get the animator layer 0 which is base layer of the animation
        anim.speed = animSpeed;
        movement = new Vector3(h, 0, v);//use those both axis to move the player
        rb.useGravity = true;//using gravity on the player

        rb.velocity = movement * speed;//get speed

        if (h != 0 && v != 0)
        {
            //make the rotation right with the joystick or the movement directions
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(h, v) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        if (h != 0 || v != 0)
        {
            anim.SetFloat("Speed", 1); //moving animation play when player is moving          

        }
        else
        {
            anim.SetFloat("Speed", 0); //animation goes to idle if player is not moving
        }


        if (jumped == true)//if jump is true
        {
            if (currentBaseState.nameHash == locoState || currentBaseState.nameHash == idleState)
            {
                if (!anim.IsInTransition(0))//Base layer of the animator controller in a transition is false
                {

                    rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);//make player go up as it jump
                    anim.SetBool("Jump", true);  //jump animation is played
                }
            }
        }
        //make velocity more smooth
        transform.localPosition += rb.velocity * Time.fixedDeltaTime;



        if (currentBaseState.nameHash == jumpState)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Jump", false);
            }


        }

       
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            transform.position = start.position;
        }
    }
}





