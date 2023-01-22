using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float Health;
    public float maxHealth;

    public float mana;
    public float maxMana;

    public int level;
    public int exp;
    public float xpneeded;
    public float[] position;

    public float PhysicalDamage = 10;
    public float MagicalDamage = 10;

    public float Defence = 10;
    public float ElementalResistance = 10;

    public float MovementSpeed = 10f;

    public bool ImmuneToBleed = false;
    public bool ImmuneToBurning = false;

    public int ArmorEquipped;
    public int WeaponEquipped;

    public int Gold;

    public List<Item> Inventory = new List<Item>();

    public PlayerData(PlayerStats player)
    {
        maxHealth = player.maxHealth;
        Health = player.health;
        level = player.level;
        exp = player.xp;
        xpneeded = player.requieredXP;

        mana = player.mana;
        maxMana = player.maxMana;

        PhysicalDamage = player.PhysicalDamage;
        MagicalDamage = player.MagicalDamage;
        Defence = player.Defence;
        ElementalResistance = player.ElementalResistance;
        MovementSpeed = player.MovementSpeed;
        ImmuneToBleed = player.ImmuneToBleed;
        ImmuneToBurning = player.ImmuneToBurning;

        Inventory = player.Inventory;

        position = new float[3];

        ArmorEquipped = player.ArmorEquipped;
        WeaponEquipped = player.WeaponEquipped;

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}