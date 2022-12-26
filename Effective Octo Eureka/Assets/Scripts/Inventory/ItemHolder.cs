using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{

     public int id;
     public System.Guid guid;
     string title;


     int baseDamage;
     float critChance;

     int prefix1ID;
     int prefix2ID;

     int suffix1ID;
     int suffix2ID;

     int craftedID;

    public Text title_text;
    public Text baseDamage_text;
    public Text critChance_text;

    public Text prefix1ID_text;
    public Text prefix2ID_text;

    public Text suffix1ID_text;
    public Text suffix2ID_text;

    public Text craftedID_text;

    PlayerStats playerStats;

    public 


    // Start is called before the first frame update
    void Start()
    {

        playerStats = transform.parent.parent.parent.parent.parent.parent.GetComponent<PlayerStats>();


        //Description text reference - Indexed Reference -> not optimal! may be switching to a singleton reference schema

        title_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(0).GetComponent<Text>(); 
        baseDamage_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(1).GetComponent<Text>();
        critChance_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(2).GetComponent<Text>();
        prefix1ID_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(3).GetComponent<Text>();
        prefix2ID_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(4).GetComponent<Text>();
        suffix1ID_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(5).GetComponent<Text>();
        suffix2ID_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(6).GetComponent<Text>();
        craftedID_text = transform.parent.parent.parent.parent.GetChild(0).GetChild(7).GetComponent<Text>();


        //Stat text init
        baseDamage_text.text = "";
        critChance_text.text = "";
        prefix1ID_text.text = "";
        prefix2ID_text.text = "";
        suffix1ID_text.text = "";
        suffix2ID_text.text = "";
        craftedID_text.text = "";

    }

    // Update is called once per frame
    void Update()
    {



    }

    //Main function to update item Stats from external classes
    public void updateInventoryEntries(Item item)
    {
        id = item.id;
        title = item.title;
        baseDamage = item.baseDamage;
        critChance = item.critChance;
        prefix1ID = item.prefix1ID;
        prefix2ID = item.prefix2ID;
        suffix1ID = item.suffix1ID;
        suffix2ID = item.suffix2ID;
        craftedID = item.craftedID;

    }


    //Stat show hover event
    public void enableStats()
    {
        switch (prefix1ID)
        {
            case (1):
                prefix1ID_text.text = "#% increased Weapon Damage";
                break;
            case (2):
                prefix1ID_text.text = "Adds # to Weapon Damage";
                break;
            case (3):
                prefix1ID_text.text = "Weapon is burning";
                break;
            case (4):
                prefix1ID_text.text = "Weapon fires a magic projectile";
                break;
            case (5):
                prefix1ID_text.text = "Weapon deals more damage agains Shielded Enemies";
                break;
            case (6):
                prefix1ID_text.text = "Kills with this weapon have #% chance to spawn healing orbs";
                break;
            case (7):
                prefix1ID_text.text = "Kills with this weapon have #% chance to award bonus gold";
                break;
        }

        switch (prefix2ID)
        {
            case (1):
                prefix2ID_text.text = "#% increased Weapon Damage";
                break;
            case (2):
                prefix2ID_text.text = "Adds # to Weapon Damage";
                break;
            case (3):
                prefix2ID_text.text = "Weapon is burning";
                break;
            case (4):
                prefix2ID_text.text = "Weapon fires a magic projectile";
                break;
            case (5):
                prefix2ID_text.text = "Weapon deals more damage agains Shielded Enemies";
                break;
            case (6):
                prefix2ID_text.text = "Kills with this weapon have #% chance to spawn healing orbs";
                break;
            case (7):
                prefix2ID_text.text = "Kills with this weapon have #% chance to award bonus gold";
                break;
        }

        switch (suffix1ID)
        {
            case (1):
                suffix1ID_text.text = "Weapon has #% chance to Poison on hit";
                break;
            case (2):
                suffix1ID_text.text = "Weapon grants # increased chance to Bleed ";
                break;
            case (3):
                suffix1ID_text.text = "Weapon has #% increased critical strike chance";
                break;
            case (4):
                suffix1ID_text.text = "Kills with this weapon have #% chance to leech life";
                break;
            case (5):
                suffix1ID_text.text = "Hits with this weapon have #% chance to leech life";
                break;
            case (6):
                suffix1ID_text.text = "continous hits with this weapon increase weapon damage by #%";
                break;
            case (7):
                suffix1ID_text.text = "kills with this weapon have #% chanceto create a poison cloud";
                break;
        }

        switch (suffix2ID)
        {
            case (1):
                suffix2ID_text.text = "Weapon has #% chance to Poison on hit";
                break;
            case (2):
                suffix2ID_text.text = "Weapon grants # increased chance to Bleed ";
                break;
            case (3):
                suffix2ID_text.text = "Weapon has #% increased critical strike chance";
                break;
            case (4):
                suffix2ID_text.text = "Kills with this weapon have #% chance to leech life";
                break;
            case (5):
                suffix2ID_text.text = "Hits with this weapon have #% chance to leech life";
                break;
            case (6):
                suffix2ID_text.text = "continous hits with this weapon increase weapon damage by #%";
                break;
            case (7):
                suffix2ID_text.text = "kills with this weapon have #% chanceto create a poison cloud";
                break;
        }

        title_text.text = title;
        baseDamage_text.text = "Damage: " + baseDamage.ToString();
        critChance_text.text = "Crit chance: " + critChance.ToString();
        craftedID_text.text = "Unique ID " + id.ToString();


    }

    //Stat hide hover event
    public void disableStats()
    {
        title_text.text = "";
        baseDamage_text.text = "";
        critChance_text.text = "";
        prefix1ID_text.text = "";
        prefix2ID_text.text = "";
        suffix1ID_text.text = "";
        suffix2ID_text.text = "";
        craftedID_text.text = "";

    }

    //Delete item  -> also deletes from the Inventory DB
    public void Scrap()
    {



        playerStats.ScrapItem(id);
        Destroy(gameObject);
    }

    //Equip function -> Work in progress
    public void Equip()
    {
        print("Equipped: " + this.name);
    }
}
