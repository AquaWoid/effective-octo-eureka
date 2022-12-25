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
        /*
                title_text = transform.GetChild(1).GetComponent<Text>();
                baseDamage_text = transform.GetChild(4).GetComponent<Text>();
                critChance_text = transform.GetChild(5).GetComponent<Text>();
                prefix1ID_text = transform.GetChild(6).GetComponent<Text>();
                prefix2ID_text = transform.GetChild(7).GetComponent<Text>();
                suffix1ID_text = transform.GetChild(8).GetComponent<Text>();
                suffix2ID_text = transform.GetChild(9).GetComponent<Text>();
                craftedID_text = transform.GetChild(10).GetComponent<Text>();

        */


        playerStats = transform.parent.parent.parent.parent.GetComponent<PlayerStats>();

        title_text = transform.parent.parent.GetChild(1).GetChild(0).GetComponent<Text>(); 
        baseDamage_text = transform.parent.parent.GetChild(1).GetChild(1).GetComponent<Text>();
        critChance_text = transform.parent.parent.GetChild(1).GetChild(2).GetComponent<Text>();
        prefix1ID_text = transform.parent.parent.GetChild(1).GetChild(3).GetComponent<Text>();
        prefix2ID_text = transform.parent.parent.GetChild(1).GetChild(4).GetComponent<Text>();
        suffix1ID_text = transform.parent.parent.GetChild(1).GetChild(5).GetComponent<Text>();
        suffix2ID_text = transform.parent.parent.GetChild(1).GetChild(6).GetComponent<Text>();
        craftedID_text = transform.parent.parent.GetChild(1).GetChild(7).GetComponent<Text>();

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


        
        //  title_text.text = title;

        /*
        title_text.text = title;
        baseDamage_text.text = "Damage: " + baseDamage.ToString();
        critChance_text.text = "Crit chance: " + critChance.ToString();
        prefix1ID_text.text = "Prefix 1 ID: " + prefix1ID.ToString();
        prefix2ID_text.text = "Prefix 2 ID: " + prefix2ID.ToString();
        suffix1ID_text.text = "Suffix 1 ID: " + suffix1ID.ToString();
        suffix2ID_text.text = "Suffix 2 ID: " + suffix2ID.ToString();
        craftedID_text.text = "Crafted ID " + craftedID.ToString();
        */
    }


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
     //   prefix1ID_text.text = "Prefix 1 ID: " + prefix1ID.ToString();
     //   prefix2ID_text.text = "Prefix 2 ID: " + prefix2ID.ToString();
    //    suffix1ID_text.text = "Suffix 1 ID: " + suffix1ID.ToString();
     //   suffix2ID_text.text = "Suffix 2 ID: " + suffix2ID.ToString();
        craftedID_text.text = "Crafted ID " + craftedID.ToString();

        /*
        baseDamage_text.enabled = true;
        critChance_text.enabled = true;
        prefix1ID_text.enabled = true;
        prefix2ID_text.enabled = true;
        suffix1ID_text.enabled = true;
        suffix2ID_text.enabled = true;
        craftedID_text.enabled = true;
        */
    }

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

    public void Scrap()
    {



        playerStats.ScrapItem(id);
        Destroy(gameObject);
    }
}
