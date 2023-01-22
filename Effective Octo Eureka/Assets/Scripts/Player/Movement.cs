using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("General Movement")]
    public float speed = 10f;
    public float sprintMult = 1f;
    public Vector2 movement;


    [Header("Dodging (Left Shift)")]
    public float dodgeSpeed = 20;
    public int dodgeDuration = 20;
    public int dodgeDelayTime = 1;
    int dodgecount;

    [Header("Attack Values")]
    public float timeToAttack = 1;


    [Header("States")]


    [ShowOnly]public bool readyToDodge = true;


    [ShowOnly] public bool idle = true;
    [ShowOnly] public bool moving = false;
    [ShowOnly] public bool attacking = false;
    [ShowOnly] public bool bow = false;

    bool dodging = false;
    Rigidbody2D rb;

    Animator anim;

    [ShowOnly] public int direction = 1;

    const string WALKING_DOWN = "walking_down";
    const string WALKING_UP = "walking_up";
    const string WALKING_LEFT = "walking_left";
    const string WALKING_RIGHT = "walking_right";


    const string WALKING_LEFT_BOTTOM = "walking_left_bottom";
    const string WALKING_LEFT_TOP = "walking_left_top";
    const string WALKING_RIGHT_BOTTOM = "walking_right_bottom";
    const string WALKING_RIGHT_TOP = "walking_right_top";


    const string ATTACK_DOWN = "attack_down";
    const string ATTACK_UP = "attack_up";
    const string ATTACK_LEFT = "attack_left";
    const string ATTACK_RIGHT = "attack_right";

    const string SWING_ATTACK_DOWN = "swing_attack_down";
    const string SWING_ATTACK_UP = "swing_attack_up";
    const string SWING_ATTACK_LEFT = "swing_attack_left";
    const string SWING_ATTACK_RIGHT = "swing_attack_right";

    const string IDLE_DOWN = "idle_down";
    const string IDLE_UP = "idle_up";
    const string IDLE_LEFT = "idle_left";
    const string IDLE_RIGHT = "idle_right";

    const string IDLE__LEFT_BOTTOM = "idle_left_bottom";
    const string IDLE__LEFT_TOP = "idle_left_top";
    const string IDLE__RIGHT_BOTTOM = "idle_right_bottom";
    const string IDLE__RIGHT_TOP = "idle_right_top";


    public PlayerStates playerState;


    string currentState;

    [Header("Animation")]
    public float animationSpeed = 0.1f;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }



    void ChangeAnimationState(string state)
    {

        if (currentState == state) return;

        anim.Play(state);

        currentState = state;
    }

    void Update()
    {

        anim.speed = animationSpeed;


        if (Input.GetKeyDown(KeyCode.Space))
        {


         //   anim.SetBool("Attacking", true);

            print("Attacked!!!!");

            if(attacking == false)
            {
                playerState = PlayerStates.attacking;

                StartCoroutine(attackDelay());
                attacking = true;
            }
          

        }



        IEnumerator attackDelay()
        {

            int rnd = Random.Range(0, 2);
            print(rnd + "RND");
            switch (direction)
            {
                case (1):
                    if(rnd == 0)
                        ChangeAnimationState(ATTACK_LEFT);
                    else
                        ChangeAnimationState(SWING_ATTACK_LEFT);
                    break;
                case (2):
                    if (rnd == 0)
                        ChangeAnimationState(ATTACK_RIGHT);
                    else
                        ChangeAnimationState(SWING_ATTACK_RIGHT);
                    break;
                case (3):
                    if (rnd == 0)
                        ChangeAnimationState(ATTACK_DOWN);
                    else
                        ChangeAnimationState(SWING_ATTACK_DOWN);
                    break;
                case (4):
                    if (rnd == 0)
                        ChangeAnimationState(ATTACK_UP);
                    else
                        ChangeAnimationState(SWING_ATTACK_UP);
                    break;
            }

            yield return new WaitForSeconds(timeToAttack);
            playerState = PlayerStates.idle;
           attacking = false;
          //  anim.SetBool("Attacking", false);
           // print("Set bool to false");

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && movement != Vector2.zero && dodgecount == 0) //vector2.zero should be  (0,0) which movement will be if you're not moving.
        {

            dodge();

        }
        //What all this does is set a timer for the dodge and record what direction you're dodging in, if you're moving, trying to dodge, and not standing perfectly still.


        animationUpdate();
    }


    void dodge()
    {

        if(readyToDodge == true)
        {
            playerState = PlayerStates.dodging;
            dodging = true;
            dodgecount = dodgeDuration;
            StartCoroutine(dodgeDelay());
            readyToDodge = false;

        }


    }

    IEnumerator dodgeDelay()
    {
        yield return new WaitForSeconds(dodgeDelayTime);
        readyToDodge = true;
        playerState = PlayerStates.idle;
    }

    void animationUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            idle = true;

            if(attacking == false)
            playerState = PlayerStates.idle;
            //  anim.SetBool("Idle", true);
        }
        else
        {
            idle = false;

            if (attacking == false)
                playerState = PlayerStates.moving;
            //  anim.SetBool("Idle", false);
        }


        if (playerState == PlayerStates.moving && attacking == false)
        {
            // LEFT
            if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0)
            {
                //   anim.SetFloat("Blend", 0);
                direction = 1;
                ChangeAnimationState(WALKING_LEFT);
            }
            // RIGHT
            if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 0)
            {
                //  anim.SetFloat("Blend", 0.66f);
                direction = 2;
                ChangeAnimationState(WALKING_RIGHT);
            }
            // DOWN
            if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                //   anim.SetFloat("Blend", 1);
                direction = 3;
                ChangeAnimationState(WALKING_DOWN);
            }
            // UP
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                //  anim.SetFloat("Blend", 0.33f);
                direction = 4;
                ChangeAnimationState(WALKING_UP);
            }

            //RIGHT TOP
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 1)
            {

                direction = 5;
                ChangeAnimationState(WALKING_RIGHT_TOP);
            }

            //RIGHT BOTTOM 
            if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 1)
            {

                direction = 6;
                ChangeAnimationState(WALKING_RIGHT_BOTTOM);
            }

            //LEFT TOP
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == -1)
            {

                direction = 7;
                ChangeAnimationState(WALKING_LEFT_TOP);
            }

            //LEFT BOTTOM 
            if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == -1)
            {

                direction = 8;
                ChangeAnimationState(WALKING_LEFT_BOTTOM);
            }


        }

        if(playerState == PlayerStates.idle && attacking == false)
        {

            switch (direction)
            {
                case (1):
                    ChangeAnimationState(IDLE_LEFT);
                    break;
                case (2):
                    ChangeAnimationState(IDLE_RIGHT);
                    break;
                case (3):
                    ChangeAnimationState(IDLE_DOWN);
                    break;
                case (4):
                    ChangeAnimationState(IDLE_UP);
                    break;
                case (5):
                    ChangeAnimationState(IDLE__RIGHT_TOP);
                    break;
                case (6):
                    ChangeAnimationState(IDLE__RIGHT_BOTTOM);
                    break;
                case (7):
                    ChangeAnimationState(IDLE__LEFT_TOP);
                    break;
                case (8):
                    ChangeAnimationState(IDLE__LEFT_BOTTOM);
                    break;
            }


        }

        if(playerState == PlayerStates.attacking)
        {

        }

    }


    private void FixedUpdate()
    {



        //rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, verticalMove * speed * Time.deltaTime);


        //get H and V vars
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        //move player
         movement = new Vector2(hMove, vMove);

        if(dodging == false)
        {
            rb.velocity = movement * speed;
        }



        Vector2 dodgeVect = movement.normalized;


        if (dodgecount != 0)
        {
            dodgecount -= 1; 
            rb.velocity = dodgeVect.normalized * dodgeSpeed; 
            print("Dodging");

        }
        else
        {
            dodging = false;
        }



    }


}

