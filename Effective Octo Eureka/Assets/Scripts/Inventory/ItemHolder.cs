using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{

     public int id;
     public System.Guid guid;
     string title;

     int itemLevel;
     
     int baseDamage;
     float critChance;

     int prefix1ID;
     int prefix2ID;

     int suffix1ID;
     int suffix2ID;

     int craftedID;

    ItemType type;


    public Text type_text;
    public Text title_text;
    public Text baseDamage_text;
    public Text critChance_text;

    public Text prefix1ID_text;
    public Text prefix2ID_text;

    public Text suffix1ID_text;
    public Text suffix2ID_text;

    public Text craftedID_text;

    public Text itemLevel_text;


    PlayerStats playerStats;




    // Start is called before the first frame update
    void Start()
    {

        if(type == ItemType.sword)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = ObjectReferences.instance.swordSprite;
        }
        if(type == ItemType.chest)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = ObjectReferences.instance.chestSprite;
        }

        playerStats = transform.parent.parent.parent.parent.parent.parent.GetComponent<PlayerStats>();

        playerStats = ObjectReferences.instance.Player.GetComponent<PlayerStats>();

        //Description text reference - Indexed Reference -> not optimal! may be switching to a singleton reference schema

        Transform stats = ObjectReferences.instance.InventoryStats.transform;

        title_text = stats.GetChild(0).GetComponent<Text>();
        baseDamage_text = stats.GetChild(1).GetComponent<Text>();
        critChance_text = stats.GetChild(2).GetComponent<Text>();
        prefix1ID_text = stats.GetChild(3).GetComponent<Text>();
        prefix2ID_text = stats.GetChild(4).GetComponent<Text>();
        suffix1ID_text = stats.GetChild(5).GetComponent<Text>();
        suffix2ID_text = stats.GetChild(6).GetComponent<Text>();
        craftedID_text = stats.GetChild(7).GetComponent<Text>();
        itemLevel_text = stats.GetChild(8).GetComponent<Text>();
        type_text = stats.GetChild(9).GetComponent<Text>();
        //Stat text init
        itemLevel_text.text = "";
        baseDamage_text.text = "";
        critChance_text.text = "";
        prefix1ID_text.text = "";
        prefix2ID_text.text = "";
        suffix1ID_text.text = "";
        suffix2ID_text.text = "";
        craftedID_text.text = "";
        type_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {



    }

    //Main function to update item Stats from external classes
    public void updateInventoryEntries(Item item)
    {
        id = item.id;
        itemLevel = item.itemLevel;
        title = item.title;
        baseDamage = item.baseDamage;
        critChance = item.critChance;
        prefix1ID = item.prefix1ID;
        prefix2ID = item.prefix2ID;
        suffix1ID = item.suffix1ID;
        suffix2ID = item.suffix2ID;
        craftedID = item.craftedID;
        type = item.type;

    }


    //Stat show hover event
    public void enableStats()
    {

        if(type == ItemType.sword)
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
            critChance_text.text = "Crit chance: " + critChance.ToString();
            baseDamage_text.text = "Damage: " + baseDamage.ToString();
        }


        if(type == ItemType.chest)
        {
            baseDamage_text.text = "Armor: " + baseDamage.ToString();
        }

        itemLevel_text.text = "Item Leve: " + itemLevel.ToString();
        title_text.text = title;


        craftedID_text.text = "Unique ID " + id.ToString();
        type_text.text = "Type: " + type;

    }

    //Stat hide hover event
    public void disableStats()
    {
        itemLevel_text.text = "";
        title_text.text = "";
        baseDamage_text.text = "";
        critChance_text.text = "";
        prefix1ID_text.text = "";
        prefix2ID_text.text = "";
        suffix1ID_text.text = "";
        suffix2ID_text.text = "";
        craftedID_text.text = "";
        type_text.text = "";

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

        if(type == ItemType.chest)
        {
            ObjectReferences.instance.EquipImageArmor.transform.SetParent(transform.parent, false);
         //   ObjectReferences.instance.EquipImageArmor.GetComponent<RectTransform>().position = new Vector3(50, -50, 0);
        }

        if (type == ItemType.sword)
        {
            ObjectReferences.instance.EquipImageWeapon.transform.SetParent(transform.parent, false);
      //      ObjectReferences.instance.EquipImageWeapon.GetComponent<RectTransform>().position = new Vector3(50, -50, 0);
        }

        Item i = new Item(id, title, itemLevel, baseDamage, critChance, prefix1ID, prefix2ID, suffix1ID, suffix2ID, craftedID, type);

        playerStats.EquipItem(i);

        print("Equipped: " + this.name);
    }
}
