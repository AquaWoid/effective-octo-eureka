using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandling : MonoBehaviour
{

    public GameObject Arrow;
    PlayerStats player;
   // public GameObject player;


    public float ArrowSpeed = 1;

    GameObject trns;

    Animator anim;

    public float shootingDelay = 0.5f;

    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
     //  transform.position = player.transform.position;
        anim = GetComponent<Animator>();
        player = transform.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E))
        {

            if(player.mana >= 10)
            {
                if (isShooting == false)
                {
                    StartCoroutine(bowDelay(shootingDelay));
                }
            }



        }
        
    }

    IEnumerator bowDelay(float delay)
    {
        anim.SetBool("Bow", true);
        isShooting = true;
        player.mana -= 10;
        yield return new WaitForSeconds(delay);
        Instantiate(Arrow);



        isShooting = false;
        anim.SetBool("Bow", false);
    }
}
