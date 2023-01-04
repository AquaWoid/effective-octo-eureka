using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{

    Slider HpSlider;
    Text statusText;

    Text DamageText;


    public int dropChance;

    public float level = 1;

    public float hp = 100;
    public float maxHp = 100;

    public float damage = 10;

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


    void Start()
    {
        HpSlider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        statusText = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
        DamageText = transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Text>();

        State = EnemyStates.idle;

        hp *= level;
        damage *= level;
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



        }

        if (State == EnemyStates.defending)
        {

        }

        if(State == EnemyStates.chasing)
        {

        }


        if (hp <= 0 )
        {


            DropItem();


            float x = level * Random.Range(10, 50);

            int xp = Mathf.FloorToInt(x);

            Player.GetComponent<PlayerStats>().getXP(xp);
            Player.GetComponent<FlaskUsage>().gainCharge(1);

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


    public void updateStats(int level)
    {
        hp = maxHp * level;
        damage = damage * level;
        HpSlider.maxValue = hp;

    }

    public void MoveToTarget(GameObject target)
    {
        if(isAttacking == false)
        {
            State = EnemyStates.chasing;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5 * Time.deltaTime);
        }

    }

    public void EndChase()
    {
        State = EnemyStates.idle;
    }

    public void DropItem()
    {
        int rndDamage = Random.Range(10, 20);
        int crit = Random.Range(10, 25);
        int randomID = Random.Range(1000, 9999);
        int rndPre1 = Random.Range(1, 8);
        int rndPre2 = Random.Range(1, 8);
        int rndSuf1 = Random.Range(1, 8);
        int rndSuf2 = Random.Range(1, 8);

        int rndDrop = Random.Range(dropChance, 100);

        var found = Player.GetComponent<PlayerStats>().Inventory.Find(Item => Item.id == randomID);
        if (found == null)
        {
            Item item = new Item(randomID, "Dropped Item", Mathf.FloorToInt(level), rndDamage * Mathf.FloorToInt(level), crit, rndPre1, rndPre2, rndSuf1, rndSuf2, 0, ItemType.sword);
            print("Sent dropped item to inventory");
            Player.GetComponent<PlayerStats>().LootItem(item);

        }else
        {
            DropItem();
        }
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
