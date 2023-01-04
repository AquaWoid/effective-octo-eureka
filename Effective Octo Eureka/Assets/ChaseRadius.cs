using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRadius : MonoBehaviour
{

    EnemyAI enemyAi;
    int rotationState;

    public float x_radius;
    public float y_radius;

    void Start()
    {
        enemyAi = transform.parent.GetComponent<EnemyAI>();
    }


    private void Update()
    {
       // GetComponent<BoxCollider2D>().
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Vector2 v = transform.position - collision.transform.position;

            float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            rotationState = (int)((Mathf.Round(a / 90f) + 4) % 4);


            enemyAi.rotateToObject(rotationState);
            enemyAi.MoveToTarget(collision.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyAi.EndChase();
    }
}
