using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Health;
    public int level;
    public int exp;
    public int xpneeded;
    public float[] position;

    public float PhysicalDamage = 10;
    public float MagicalDamage = 10;

    public float Defence = 10;
    public float ElementalResistance = 10;

    public float MovementSpeed = 10f;

    public bool ImmuneToBleed = false;
    public bool ImmuneToBurning = false;

    public int Gold;

    public List<Item> Inventory = new List<Item>();

    public PlayerData(PlayerStats player)
    {

        Health = player.health;
        level = player.level;
        exp = player.xp;
        xpneeded = player.requieredXP;

        PhysicalDamage = player.PhysicalDamage;
        MagicalDamage = player.MagicalDamage;
        Defence = player.Defence;
        ElementalResistance = player.ElementalResistance;
        MovementSpeed = player.MovementSpeed;
        ImmuneToBleed = player.ImmuneToBleed;
        ImmuneToBurning = player.ImmuneToBurning;

        Inventory = player.Inventory;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}