using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehavior : MonoBehaviour
{
    public float animationSpeed = 1;
    Animator animator;
    string currentState;
    bool cutted = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        ChangeAnimationState("grass_idle");
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = animationSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
           if(collision.tag == "Player" && cutted == false)
          ChangeAnimationState("sprite_proximity");
      // if (collision.tag == "Player" && cutted == false)
         //   transform.localScale = new Vector3(3.61f, 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
     //   if (collision.tag == "Player")
      //      transform.localScale = new Vector3(3.61f, 3.61f);


        
         if (collision.tag == "Player" && cutted == false)
          ChangeAnimationState("grass_idle");


    }

    public void Cut()
    {
        cutted = true;
        print("CUTTED");
        ChangeAnimationState("grass_cut");
        StartCoroutine(regrow());

    }

    IEnumerator regrow()
    {
        yield return new WaitForSeconds(10);
        cutted = false;
        ChangeAnimationState("grass_idle");
    }


    

    void ChangeAnimationState(string state)
    {

        if (currentState == state) return;

        animator.Play(state);

        currentState = state;
    }
}
