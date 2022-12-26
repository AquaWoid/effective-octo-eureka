using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEvadeObstacles : MonoBehaviour
{

    Transform parent;
    NPCBehavior behavior;

    int rotationState;

    // Start is called before the first frame update
    void Start()
    {
        
        parent = transform.parent;
        behavior = parent.GetComponent<NPCBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {



    }

    private void OnTriggerStay2D(Collider2D collision)
    {


        if (collision.tag != "NPC" && collision.tag != "IgnoreCollision")
        {
            float combinedSpeed = behavior.speed * Time.deltaTime;

            Vector2 v = transform.position - collision.transform.position;

            float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            rotationState = (int)((Mathf.Round(a / 90f) + 4) % 4);

            float b = Mathf.FloorToInt(a);

            behavior.ObjectFromRadius = b;

            #region Corner Detection
            //right upper corner
            if (b >= -90 && b <= -180)
            {

            }
            //right under corner
            if (b >= 180 && b <= 90)
            {

            }
            //left upper corner
            if (b >= 0 && b <= -90)
            {

            }
            //right under corner
            if (b >= 0 && b <= 90)
            {

            }
            #endregion

            //obtacle from left
            if (rotationState == 0)
            {
             //   print("Obstacle from left");

                //if facing down
                if (behavior.OrientationRotation == 1)
                {
                    parent.Translate(0, -combinedSpeed, 0);
                }

                //if facing up
                if (behavior.OrientationRotation == 3)
                {
                    parent.Translate(0, combinedSpeed, 0);
                }

                //if facing left
                if (behavior.OrientationRotation == 0)
                {

                    if(b < 0 && b >= -180)
                    {
                        print("Upper corners hit");
                        parent.Translate(0, -combinedSpeed, 0);
                    }
                    if(b > 0 && b <= 180)
                    {
                        print("Lower corners hit");
                        parent.Translate(0, combinedSpeed, 0);
                    }


                }

                //facing right
                if (behavior.OrientationRotation == 2)
                {
                    parent.Translate(combinedSpeed, 0 , 0);
                }

            }
            //obtacle from  down
            if (rotationState == 1)
            {
              //  print("Obstacle from down");
                //if facing down
                if (behavior.OrientationRotation == 1)
                {
                    if(b < 90 && b >= 0)
                    {
                        parent.Translate(combinedSpeed, 0, 0);
                    }
                    if(b > 90 && b <= 180)
                    {
                        parent.Translate(-combinedSpeed, 0, 0);
                    }

                }

                //if facing up
                if (behavior.OrientationRotation == 3)
                {
                    parent.Translate(0, combinedSpeed, 0);
                }

                //if facing left
                if (behavior.OrientationRotation == 0)
                {
                    parent.Translate(-combinedSpeed, 0, 0);
                }

                //facing right
                if (behavior.OrientationRotation == 2)
                {
                    parent.Translate(combinedSpeed,0 , 0);
                }
            }
            //obtacle from  right
            if (rotationState == 2)
            {
              //  print("Obstacle from right");
                //if facing down
                if (behavior.OrientationRotation == 1)
                {
                    parent.Translate(0, -combinedSpeed, 0);
                }

                //if facing up
                if (behavior.OrientationRotation == 3)
                {
                    parent.Translate(0, combinedSpeed, 0);
                }

                //if facing left
                if (behavior.OrientationRotation == 0)
                {
                    parent.Translate(-combinedSpeed,0, 0);
                }

                //facing right
                if (behavior.OrientationRotation == 2)
                {

                    if (b < -90 && b >= -180)
                    {
                        parent.Translate(0, -combinedSpeed, 0);
                    }
                    if (b > 90 && b <= 180)
                    {
                        parent.Translate(0, combinedSpeed, 0);
                    }
                }


            }
            //obtacle from  up
            if (rotationState == 3)
            {
               // print("Obstacle from up");
                //if facing down
                if (behavior.OrientationRotation == 1)
                {
                    parent.Translate(0, -combinedSpeed, 0);
                }

                //if facing up
                if (behavior.OrientationRotation == 3)
                {

                    if (b < 0 && b >= -90)
                    {

                        parent.Translate(combinedSpeed, 0, 0);
                    }
                    if (b < -90 && b <= 180)
                    {

                        parent.Translate(-combinedSpeed, 0, 0);
                    }
                   
                }

                //if facing left
                if (behavior.OrientationRotation == 0)
                {

                    parent.Translate(-combinedSpeed, 0, 0);

                    /*
                    if (b < 0 && b >= -180)
                    {
                        print("Upper corners hit");
                        parent.Translate(0, -combinedSpeed, 0);
                    }
                    if (b > 0 && b <= 180)
                    {
                        print("Lower corners hit");
                        parent.Translate(0, combinedSpeed, 0);
                    }
                    */
                }

                //facing right
                if (behavior.OrientationRotation == 2)
                {
                    parent.Translate(combinedSpeed, 0 , 0);
                    /*
                    if (b < 0 && b >= -180)
                    {
                        print("Upper corners hit");
                        parent.Translate(0, -combinedSpeed, 0);
                    }
                    if (b > 0 && b <= 180)
                    {
                        print("Lower corners hit");
                        parent.Translate(0, combinedSpeed, 0);
                    }
                    */
                }


            }


        }

    }

}
