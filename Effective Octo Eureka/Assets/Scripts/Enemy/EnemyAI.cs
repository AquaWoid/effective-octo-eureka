using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EurekaEssentials;

public class EnemyAI : MonoBehaviour
{

    Slider HpSlider;
    Text statusText;

    Text DamageText;


    [Header("Stats")]
    [Tooltip("Level, multiplies HP and Damage")]
    public float level = 1;

    [ShowOnly] public float hp = 1000;
    public float maxHp = 1000;

    public float baseDamage = 10;


    [Tooltip("Drop chance in %")]
    public int dropChance;

    [Header("Behavior")]
    [Tooltip("Time to revive in Seconds")]
    public int ReviveTime = 10;

    public float walkDistance = 1;

    public float t;

    int rotationState;

    bool isBleeding = false;

    [SerializeField]
    bool isAttacking = false;

    public bool isDead = false;


    public int CoroutineState = 0;

    public EnemyStates State;
    EnemyMoveStates MoveState;

    GameObject Player;

    EnemyDetectionRadius detectionRadius;
    ChaseRadius chaseRadius;

    Animator animator;

    [Header("Pathing")]

    Pathfinding pathfinding = new Pathfinding();

    [Header("Pathfinding")]
    public float speed = 10;

    [Tooltip("Drag the path to follow here")]
    public GameObject path;

    [Tooltip("OFF = If the last waypoint is reached, NPC goes back on the same path. \nON = NPC Loops trough waypoints, goint straight to the first waypoint after reaching the last")]
    public bool circleMove = true;

    public bool WaypointVisible = true;

    [Tooltip("Waypoints are declared dinamically from the path parent")]
    [SerializeField]
    [HideInInspector] private List<Transform> waypoints;


    private void Awake()
    {
        
        if (path == null)
        {
            EssentialCalculations essentialCalculations = new EssentialCalculations();

            path = essentialCalculations.closest_Object(transform, "Path");
           
        }
    }

    void Start()
    {

                int count = 0;
        foreach (Transform child in path.transform)
        {
            //  Debug.Log(count + " : " + child);
            waypoints.Add(child);
            count += 1;
        }



        pathfinding.initializeWaypoints(path);

        HpSlider = transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        statusText = transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
        DamageText = transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<Text>();

        State = EnemyStates.idle;

        hp *= level;
        maxHp *= level;
        baseDamage *= level;
        HpSlider.maxValue = hp;

        Player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();

        animator.SetBool("Idle", false);

        detectionRadius = GetComponentInChildren<EnemyDetectionRadius>();
        chaseRadius = GetComponentInChildren<ChaseRadius>();

        StartCoroutine(move());




    }


    public void attack()
    {
        detectionRadius.atk();
    }

    public void setNewPath(GameObject newPath)
    {
        path = newPath;

        waypoints.Clear();

        int count = 0;
        foreach (Transform child in path.transform)
        {
            //  Debug.Log(count + " : " + child);
            waypoints.Add(child);
            count += 1;
        }

    }

    public void resetWaypoint()
    {
        pathfinding.resetPosCount();
    }

    void Update()
    {


        if (State == EnemyStates.idle)
        {
            pathfinding.findPath(transform, animator, speed, path, circleMove, WaypointVisible, waypoints);

        }


        if (hp <= 0 )
        {
            Death();
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


    void Death()
    {
        if (isDead == false)
        {
            isDead = true;
            animator.SetBool("Dead", true);
            hp = 0;
            State = EnemyStates.dead;


            DropItem();

            Player.GetComponent<PlayerStats>().getXP(xpCalc());
            Player.GetComponent<FlaskUsage>().gainCharge(1);

            chaseRadius.gameObject.SetActive(false);
            detectionRadius.gameObject.SetActive(false);

            StartCoroutine(revivalTimer());

        }
    }

    int xpCalc()
    {
        float x = level * Random.Range(10, 50);
        int p = Mathf.FloorToInt(x);
        return p;
    }

    IEnumerator revivalTimer()
    {

        yield return new WaitForSeconds(ReviveTime);
        State = EnemyStates.idle;
        hp = maxHp;
        chaseRadius.gameObject.SetActive(true);
        detectionRadius.gameObject.SetActive(true);
        animator.SetBool("Dead", false);
        isDead = false;


    }

    public void updateStats(int level)
    {
        hp = maxHp * level;
        baseDamage = baseDamage * level;
        HpSlider.maxValue = hp;

    }

    public void MoveToTarget(GameObject target)
    {
        if(isAttacking == false && State != EnemyStates.dead)
        {
            State = EnemyStates.chasing;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5 * Time.deltaTime);

        }

    }

    public void EndChase()
    {
        if(isDead == false)
        {
            State = EnemyStates.idle;
        }

    }

    public void DropItem()
    {

        int randRoll = Random.Range(1, 100);
        print("RandRoll was: " + randRoll);
        for (int i = 0; i < dropChance; i++)
        {
            if(i == randRoll)
            {
                addNewItem();
                break;
            }
        }

    }

    void addNewItem()
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

        }
        else
        {
            addNewItem();
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

        if(isDead == false)
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
        if(isDead == false)
        {
            State = EnemyStates.idle;
        }


    }

    IEnumerator Bleeds()
    {
        yield return new WaitForSeconds(4);
        isBleeding = false;
    }

    public void rotateToObject(int OrientationRotation)
    {

        if(isDead == false)
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

}
