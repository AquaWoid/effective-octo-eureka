using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float lifeTime = 4;
    public float speedMultiplier = 1;

    GameObject player;
    Animator anim;

    Vector3 movement;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.SetParent(player.transform);
        transform.position = player.transform.position;
        StartCoroutine(lifetimer(lifeTime));

        anim = player.GetComponent<Animator>();

        //LEFT
        if (anim.GetFloat("Blend") == 0)
        {

            movement = new Vector3(Mathf.Lerp(0, -5, Time.deltaTime * speedMultiplier), 0, 0);
        }
        //UP
        if (anim.GetFloat("Blend") == 0.33f)
        {

            transform.rotation = Quaternion.Euler(0, 0, 90);

            movement = new Vector3(Mathf.Lerp(0, 5, Time.deltaTime * speedMultiplier), 0, 0);
        }
        //RIGHT
        if (anim.GetFloat("Blend") == 0.66f)
        {
            movement = new Vector3(Mathf.Lerp(0, 5, Time.deltaTime * speedMultiplier), 0, 0);
        }
        //DOWN
        if (anim.GetFloat("Blend") == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);

            movement = new Vector3(Mathf.Lerp(0, -5, Time.deltaTime * speedMultiplier), 0, 0);
        }


    }

    void Update()
    {
        transform.Translate(movement);
    }

    IEnumerator lifetimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
