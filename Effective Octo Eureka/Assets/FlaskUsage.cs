using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaskUsage : MonoBehaviour
{

    public Slider Flask1;
    public Slider Flask2;

    public int charge1 = 5;
    int maxCharge1 = 5;
    public float restoreAmount = 20f;


    public int charge2 = 5;
    int maxCharge2 = 5;


    PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        Flask1.maxValue = maxCharge1;
        Flask2.maxValue = maxCharge2;
        player = this.gameObject.GetComponent<PlayerStats>();
    }

    public void updateRecoverAmount(float rAmount)
    {
        restoreAmount = rAmount;
    }

    // Update is called once per frame
    void Update()
    {

        Flask1.value = charge1;
        Flask2.value = charge2;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(charge1 >= 1)
            {
                player.heal(restoreAmount);
                charge1 -= 1;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (charge2 >= 1)
            {
                player.healMana(restoreAmount);
                charge2 -= 1;
            }

        }

    }

    public void gainCharge(int amount)
    {
        charge1 += amount;
        charge2 += amount;
        if (charge1 >= maxCharge1)
        {
            charge1 = maxCharge1;
        }


        if (charge2 >= maxCharge2)
        {
            charge2 = maxCharge2;
        }
    }
}
