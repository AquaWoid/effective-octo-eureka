using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    Slider HpSlider;
    Text statusText;

    Text DamageText;


    public float level = 1;

    public float hp = 100;

    public float walkDistance = 1;

    public float t;

    int rotationState;

    bool isBleeding = false;

    [SerializeField]
    bool isAttacking = false;

    public int CoroutineState = 0;

    EnemyStates State;
    EnemyMoveStates MoveState;

    GameObject Player;

    Animator animator;

    IEnumerator co;

    // Start is called before the first frame update
    void Start()
    {
        HpSlider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        statusText = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
        DamageText = transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Text>();

        State = EnemyStates.idle;

        hp *= level;
        HpSlider.maxValue = hp;

        Player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();

        animator.SetBool("Idle", false);


        StartCoroutine(move());
    }


    void Update()
    {


        if (State == EnemyStates.idle)
        {

            if(MoveState == EnemyMoveStates.left)
            {
                transform.Translate(-0.5f * Time.deltaTime, 0, 0);
               // Debug.Log("left");
            }
            if (MoveState == EnemyMoveStates.up)
            {
                transform.Translate(0, 0.5f * Time.deltaTime, 0);
               // Debug.Log("up");
            }
            if (MoveState == EnemyMoveStates.right)
            {
                transform.Translate(0.5f * Time.deltaTime, 0, 0);
               // Debug.Log("right");
            }
            if (MoveState == EnemyMoveStates.down)
            {
                transform.Translate(0, -0.5f * Time.deltaTime, 0);
              //  Debug.Log("down");
            }

        }

        if (State == EnemyStates.attacking)
        {



            /*
            if(Player.transform.position.x < transform.position.x - 5)
            {
                MoveRight();
            }
            */


        }

        if (State == EnemyStates.defending)
        {

        }



        if (hp <= 0 )
        {
            Destroy(gameObject);
        }

        HpSlider.value = hp;

        t += 0.5f * Time.deltaTime;

        if(t >= 1.0f)
        {
            t = 0;
        }

        if(isBleeding)
        {
            float tempHp = hp;

            hp -= Time.deltaTime * 4;
            statusText.text = "Status: Bleeding";
            DamageText.color = Color.red;



        }

        if(!isBleeding)
        {
            statusText.text = "Status: Normal";
            DamageText.color = Color.white;
        }

        DamageText.text = hp.ToString("F2");

    }

    public void takeDamage(float damage)
    {
        hp -= damage;
       // DamageText.text = damage.ToString();
    }

    public void bleeding(float bleedDot)
    {
        if(isBleeding == false)
        {
            isBleeding = true;
            StartCoroutine(Bleeds());
        }

    }

    /// BUG - Coroutine geht weiter / stop richtig machen!
    IEnumerator move ()
    {

            CoroutineState = 1;
            MoveRight();
            yield return new WaitForSeconds(walkDistance);


            CoroutineState = 2;
            MoveDown();
            yield return new WaitForSeconds(walkDistance);

            CoroutineState = 3;
            MoveLeft();
            yield return new WaitForSeconds(walkDistance);

            CoroutineState = 4;
            MoveUp();
            yield return new WaitForSeconds(walkDistance);
       

        StartCoroutine(move());
    }


    private void MoveRight()
    {
        if(isAttacking == false)
        {
            MoveState = EnemyMoveStates.right;
            animator.SetFloat("Blend", 0.66f);
        } 

    }
    private void MoveDown()
    {
        if (isAttacking == false)
        {
            MoveState = EnemyMoveStates.down;
            animator.SetFloat("Blend", 1);
        }

    }
    private void MoveLeft()
    {
        if (isAttacking == false)
        {
            MoveState = EnemyMoveStates.left;
            animator.SetFloat("Blend", 0);
        }

    }
    private void MoveUp()
    {
        if (isAttacking == false)
        {
            MoveState = EnemyMoveStates.up;
            animator.SetFloat("Blend", 0.33f);
        }

    }

    public void setAttacking()
    {
    //    co = move();
    //    StopCoroutine(co);
        isAttacking = true;
        State = EnemyStates.attacking;
    }

    public void unsetAttacking()
    {
      //  co = move();
     //   StartCoroutine(co);
        isAttacking = false;
        State = EnemyStates.idle;

    }

    IEnumerator Bleeds()
    {
        yield return new WaitForSeconds(4);
        isBleeding = false;
    }

    public void rotateToObject(int OrientationRotation)
    {


        if (OrientationRotation == 0)
        {

            animator.SetFloat("Blend", 0);
        }
        if (OrientationRotation == 1)
        {

            animator.SetFloat("Blend", 1);
        }
        if (OrientationRotation == 2)
        {

            animator.SetFloat("Blend", 0.66f);
        }
        if (OrientationRotation == 3)
        {

            animator.SetFloat("Blend", 0.33f);
        }


    }

}
