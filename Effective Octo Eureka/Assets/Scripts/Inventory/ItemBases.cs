using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBases : MonoBehaviour
{

    public static ItemBases bases;

    public Item sword = new Item(1, "Sword", 1, 100, 10, 1, 1, 1, 1, 0, ItemType.sword);
    public Item armor = new Item(1, "Armor", 1, 100, 10, 1, 1, 1, 1, 0, ItemType.chest);

    private void Awake()
    {
        bases = this;
    }


}
