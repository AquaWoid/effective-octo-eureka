using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string title;


    public int baseDamage;
    public float critChance;

    public int prefix1ID;
    public int prefix2ID;

    public int suffix1ID;
    public int suffix2ID;

    public int craftedID;





    public Item(int id, string title,  int damage, float crit, int prefix1, int prefix2, int suffix1, int suffix2, int crafted)
    {
        this.id = id;
        this.baseDamage = damage;
        this.critChance = crit;
        this.title = title;
        this.prefix1ID = prefix1;
        this.prefix2ID = suffix1;
        this.suffix1ID = suffix1;
        this.suffix2ID = suffix2;
        this.craftedID = crafted;
    }


    /*
    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.icon = item.icon;
        this.modifiers = item.modifiers;
    }
*/

}
