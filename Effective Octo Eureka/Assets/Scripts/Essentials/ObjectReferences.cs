using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferences : MonoBehaviour
{

    public static ObjectReferences instance;


    public GameObject Player;
    public GameObject InventoryStats;
    public GameObject Stats;


    public Sprite swordSprite;
    public Sprite chestSprite;

    public GameObject EquipImageWeapon;
    public GameObject EquipImageArmor;

    public GameObject InventoryGrid;
   // public GameObject BowHandling;


    private void Awake()
    {
        instance = this;
    }
}
