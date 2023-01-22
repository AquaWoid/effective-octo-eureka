using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{

    PlayerStats player;
    public float delay = 1;
    public float blockTime = 0.5f;
    bool blocking = false;
    bool blockWindowOpen = false;


    void Start()
    {
        player = transform.GetComponent<PlayerStats>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && blocking == false)
        {
            StartCoroutine(blockDelay());

        }
    }

    IEnumerator blockWindow()
    {

        player.blocked = true;
        yield return new WaitForSeconds(blockTime);
        player.blocked = false;

    }

    IEnumerator blockDelay()
    {
        blocking = true;
        StartCoroutine(blockWindow());
        yield return new WaitForSeconds(delay);
        blocking = false;

    }
}
