using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public InputField input;
  //  int value = 0;
    public GameObject player;

    public void run()
    {

        if(input.text == "heal")
        {
            player.GetComponent<PlayerStats>();
        }


    }
}
