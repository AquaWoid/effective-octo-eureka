using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionRadius : MonoBehaviour
{

    int rotationState;

    PlayerStats playerStats;

    bool localAttack = false;
    Animator animator;

    EnemyAI enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        enemyAi = transform.parent.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void attack()
    {
        if(localAttack == false)
        {
            StartCoroutine(attackDelay());
        }
    }

    IEnumerator attackDelay()
    {

        localAttack = true;

        yield return new WaitForSeconds(0.5f);



        if (playerStats != null)
        {
            playerStats.takeDamage(5);

            float rnd = Random.Range(0, 5);

            if (rnd == 0)
            {
              //  playerStats.bleeding(1);

                print("rnd was: " + rnd);
            }
        }


        localAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {




        if (collision.tag == "Arrow")
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Player")
        {
            transform.GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 50);
            playerStats = collision.GetComponent<PlayerStats>();
            print("got playerstats: " + playerStats);
            animator.SetBool("Attacking", true);
            enemyAi.setAttacking();
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {



        if (collision.tag == "Player")
        {
            enemyAi.unsetAttacking();


            transform.GetComponent<SpriteRenderer>().color = new Vector4(0, 255, 0, 50);
            playerStats = null;
            animator.SetBool("Attacking", false);
            enemyAi.unsetAttacking();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {



        if (collision.tag == "Player")
        {

            Vector2 v = transform.position - collision.transform.position;

            float a = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            rotationState = (int)((Mathf.Round(a / 90f) + 4) % 4);


            enemyAi.rotateToObject(rotationState);

            attack();
            Debug.Log("Staying on trigger");
        }

    }
}
