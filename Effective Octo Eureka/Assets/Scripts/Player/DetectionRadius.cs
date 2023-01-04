using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectionRadius : MonoBehaviour
{
    [SerializeField]
    EnemyAI eAI;

    [SerializeField]
    bool isAttacking = false;

    PlayerStats Stats;

    // Start is called before the first frame update
    void Start()
    {
        Stats = transform.parent.GetComponent<PlayerStats>();
        
    }

    // Update is called once per frame
    void Update()
    {



      

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector3 axes = new Vector3((Input.GetAxisRaw("Horizontal") * 2f), (Input.GetAxisRaw("Vertical") * 2f), 0.5f);


            Vector3 combined = transform.parent.position + axes;

            transform.SetPositionAndRotation(combined, Quaternion.Euler(0, 0, 0));

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (eAI != null && isAttacking == false)
            {
                isAttacking = true;
                StartCoroutine(attackDelay());

            }




        }
        if (eAI == null)
        {
            isAttacking = false;
        }


    }

    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(0.5f);

        float damageMult = Random.Range(1, 1.5f);


        if(eAI != null)
        {
            eAI.takeDamage(Stats.PhysicalDamage);

            float rnd = Random.Range(0, 5);

            if (rnd == 0)
            {
                eAI.bleeding(1);

             //   print("rnd was: " + rnd);
            }
        }




     //   print("took damage: 10");
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 50);




    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            eAI = collision.GetComponent<EnemyAI>();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetComponent<SpriteRenderer>().color = new Vector4(0, 255, 0, 50);
     //   eAI = null;
    }
}
