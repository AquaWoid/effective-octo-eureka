using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionRadius : MonoBehaviour
{

    PlayerStats playerStats;

    bool isAttacking = false;
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
        if (isAttacking == false)
        {
            StartCoroutine(attackDelay());
        }
    }

    IEnumerator attackDelay()
    {
        isAttacking = true;
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

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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

        enemyAi.unsetAttacking();

        if (collision.tag == "Player")
        {
            transform.GetComponent<SpriteRenderer>().color = new Vector4(0, 255, 0, 50);
            playerStats = null;
            animator.SetBool("Attacking", false);
            enemyAi.unsetAttacking();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("staying on trigger");
        attack();
    }
}
