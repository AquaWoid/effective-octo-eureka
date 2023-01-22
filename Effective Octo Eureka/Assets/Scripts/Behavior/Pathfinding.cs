using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pathfinding 
{



    public bool WaypointVisible = true;

    private int posCount = 0;
    private bool posLock = false;
    private Vector3 nextPos;
    private int posCountIncrement = 1;

    [Header("Orientation")]
    public FacingStates facing;

    float OrientationRaw;
    float OrientationRotation;

    public void initializeWaypoints(GameObject pathIn)
    {
        int count = 0;
        foreach (Transform child in pathIn.transform)
        {
            //  Debug.Log(count + " : " + child);
       //     waypoints.Add(child);
            count += 1;
        }
    }


    public void findPath(Transform transform, Animator anim, float speed, GameObject path, bool circleMove, bool WaypointVisible, List<Transform> waypoints)
    {



        Vector2 v = transform.position - nextPos;

        float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

         OrientationRaw = a;

         OrientationRotation = (int)((Mathf.Round(a / 90f) + 4) % 4);

        //0 = facing from left
        //1 = facing from down
        //2 = facing from right
        //3 = facing from up

        if (OrientationRotation == 0)
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

    }

    public void resetPosCount()
    {
        posCount = -1;
    }

}
