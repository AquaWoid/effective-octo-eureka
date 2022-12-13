using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Text[] UiText;

    public int level = 1;
    public int health = 100;
    public int xp = 0;
    public int requieredXP = 200;

    int lastRequiered;

    public float PhysicalDamage = 10;
    public float MagicalDamage = 10;

    public float Defence = 10;
    public float ElementalResistance = 10;

    public float MovementSpeed = 10f;

    public bool ImmuneToBleed = false;
    public bool ImmuneToBurning = false;

    public int Gold;


    private void Awake()
    {
        lastRequiered = requieredXP;
    }



    private void Start()
    {

        int count = 0;
        
        foreach (Transform child in transform)
        {
            if(child.name == "Canvas")
            {
                foreach (Transform grandchild in child.transform)
                {

                    UiText[count] = grandchild.transform.GetComponent<Text>();
                    count += 1;
                }
            }


        }

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            xp += 1000;
            requieredXP -= 1000;
        }

        UiText[0].text = "Level " + level;
        UiText[1].text = "Health " + health;
        UiText[2].text = "Experience " + xp;
        UiText[3].text = "Requiered XP " + requieredXP;
        UiText[4].text = "Physical Damage " + PhysicalDamage;
        UiText[5].text = "Magical Damage " + MagicalDamage;
        UiText[6].text = "Defence " + Defence;
        UiText[7].text = "Elemental Resistance " + ElementalResistance;
        UiText[8].text = "Movement Speed " + MovementSpeed;
        UiText[9].text = "Gold " + Gold;

        if (requieredXP <= 0)
        {
            levelUp();
        }

    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    void levelUp()
    {
        requieredXP = lastRequiered * 2;
        lastRequiered = requieredXP;

        xp = 0;

        level += 1;

        PhysicalDamage *= 1.5f;
        MagicalDamage *= 1.5f;

        Defence *= 1.5f;
        ElementalResistance *= 1.5f;


    }
}
