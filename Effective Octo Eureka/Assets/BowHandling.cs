using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandling : MonoBehaviour
{

    public GameObject Arrow;

    Animator anim;

    public float shootingDelay = 0.5f;

    bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(isShooting == false)
            {
                StartCoroutine(bowDelay(shootingDelay));
            }

        }
        
    }

    IEnumerator bowDelay(float delay)
    {
        anim.SetBool("Bow", true);
        isShooting = true;
        yield return new WaitForSeconds(delay);
        Instantiate(Arrow);
        isShooting = false;
        anim.SetBool("Bow", false);
    }
}
