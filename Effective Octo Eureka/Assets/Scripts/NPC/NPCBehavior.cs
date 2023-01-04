using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class NPCBehavior : MonoBehaviour
{

    [Header("Movement Values")]
    public float speed = 10;

    [Header("Pathing")]
    [Tooltip("Drag the path to follow here")]
    public GameObject path;

    [Tooltip("OFF = If the last waypoint is reached, NPC goes back on the same path. \nON = NPC Loops trough waypoints, goint straight to the first waypoint after reaching the last")]
    public bool circleMove = true;

    private int posCount = 0;

    [SerializeField]
    [ShowOnly] private int OnWaypoint = 0;

    [SerializeField]
    [ShowOnly] private int WaypointCount = 0;

    public bool WaypointVisible = true;



    [Tooltip("Waypoints are declared dinamically from the path parent")]
    [SerializeField]
    [HideInInspector] private List<Transform> waypoints;


    [Header("States")]

    [SerializeField]
    private NpcStates state;

    public bool hasBeenVisited;

    private bool posLock = false;
    private Vector3 nextPos;


    private int posCountIncrement = 1;

    [Header("Orientation")]
    public FacingStates facing;
    public int OrientationRotation;
    public float OrientationRaw;

    public float ObjectFromRadius;

    Animator anim;



    void Start()
    {

        anim = GetComponent<Animator>();

        int count = 0;

        foreach (Transform child in path.transform)
        {
          //  Debug.Log(count + " : " + child);
            waypoints.Add(child);
            count += 1;
        }

        WaypointCount = waypoints.Count;
    }


    public void inRange()
    {

        state = NpcStates.interacting;
        anim.SetBool("Idle", true);

    }

    public void outOfRange()
    {
        state = NpcStates.pathing;
        anim.SetBool("Idle", false);
    }


    void Update()
    {
        Vector2 v = transform.position - nextPos;

        float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        OrientationRaw = a;

        OrientationRotation = (int)((Mathf.Round(a / 90f) + 4) % 4);



        if(OrientationRotation == 0)
        {
            facing = FacingStates.left;
            anim.SetFloat("Blend", 0);
        }
        if (OrientationRotation == 1)
        {
            facing = FacingStates.down;
            anim.SetFloat("Blend", 1);
        }
        if (OrientationRotation == 2)
        {
            facing = FacingStates.right;
            anim.SetFloat("Blend", 0.66f);
        }
        if (OrientationRotation == 3)
        {
            facing = FacingStates.up;
            anim.SetFloat("Blend", 0.33f);
        }


        if (WaypointVisible == true)
        {
            foreach (var item in waypoints)
            {
                item.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            foreach (var item in waypoints)
            {
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        ///STATES

        if(state == NpcStates.interacting)
        {

        }

        //PATHING
        if(state == NpcStates.pathing)
        {

            anim.SetBool("Idle", false);

            if (posLock == false)
            {
                nextPos = waypoints[posCount].position;
            }


            Debug.DrawLine(transform.position, nextPos, Color.green);

            if (Vector3.Distance(transform.position, nextPos) > 1)
                posLock = true;


            //On waypoint reach
            if (Vector3.Distance(transform.position, nextPos) <= 1)
            {


                if (circleMove == true)
                {
                    if (posCount < waypoints.Count - 1)
                    {
                        posCount += posCountIncrement;
                    }
                    else
                    {
                        posCount = 0;
                    }

                    posLock = false;
                }

                if (circleMove == false)
                {
                    if (posCount < waypoints.Count - 1)
                    {
                        posCount += posCountIncrement;
                    }
                    else
                    {
                        posCountIncrement = -1;
                        posCount += posCountIncrement;
                    }


                    if (posCount == 0)
                    {
                        posCountIncrement = 1;
                    }

                    posLock = false;
                }


            }

            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);



            OnWaypoint = posCount + 1;

            

        }

  

    }
}
