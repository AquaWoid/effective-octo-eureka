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


    int rnd;

    bool knockedBack = false;

    public List<Item> Inventory = new List<Item>();

    ItemHolder iHolder;

    public GameObject itemHolderGO;

    public Transform inventoryGrid;

    private void Awake()
    {
        lastRequiered = requieredXP;
    }



    private void Start()
    {

      //  inventoryGrid = transform.GetChild(0).GetChild(12).GetChild(0);
        int count = 0;
     //   iHolder = transform.GetChild(0).GetChild(12).GetChild(0).GetComponent<ItemHolder>();

        
        foreach (Transform child in transform)
        {
            if(child.name == "Canvas")
            {
                foreach (Transform grandchild in child.transform)
                {
                    if(grandchild.GetComponent<Text>() != null)
                    {
                        UiText[count] = grandchild.transform.GetComponent<Text>();
                        count += 1;
                    }
                }
            }
        }
    }


    public void SaveGame()
    {

        SaveSystem.SavePlayer(this);

    }

    public void LoadGame()
    {

        PlayerData data = SaveSystem.LoadPlayer();

        health = data.Health;
        level = data.level;
        xp = data.exp;
        requieredXP = data.xpneeded;
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);



        PhysicalDamage = data.PhysicalDamage;
        MagicalDamage = data.MagicalDamage;

        Defence = data.Defence;
        ElementalResistance = data.ElementalResistance;

        MovementSpeed = data.MovementSpeed;
        Inventory = data.Inventory;

        /*
        foreach (GameObject child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }
        */

        foreach (Transform child in inventoryGrid.transform)
        {
            print("Scrapped: " + child.name);
            Destroy(child.gameObject);
        }

        foreach (Item item in Inventory)
        {
            print("ID: " + item.id + " | Title: " + item.title + " | Damage: " + item.baseDamage + " | Crit: " + item.critChance +
                " | Prefix 1: " + item.prefix1ID + " | Prefix 2: " + item.prefix2ID + " | Suffix 1: " + item.suffix1ID + " | Suffix 2: " + item.suffix2ID + " | Crafted: " + item.craftedID);

              GameObject go = Instantiate(itemHolderGO, transform.GetChild(0).GetChild(12).GetChild(0));
            go.GetComponent<ItemHolder>().updateInventoryEntries(item);

            print("Applied: " + item.title + " to: " + go.name);

        }



    }
    private void Update()
    {

         rnd = Random.Range(1, 8);
      //  print(rnd);

        if (Input.GetKeyDown(KeyCode.R))
        {
            xp += 1000;
            requieredXP -= 1000;
            Inventory.Clear();
            AddItem();

        }


        if (Input.GetKeyDown(KeyCode.T))
        {

            AddItem();

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {



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


        if(knockedBack == true)
        {
            transform.Translate(Mathf.Lerp(0, -2, Time.deltaTime), 0, 0);
        }


    }


    IEnumerator knockback()
    {
        knockedBack = true;
        yield return new WaitForSeconds(0.2f);
        knockedBack = false;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(knockback());


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


    public void AddItem()
    {




        string titleStr = "";


        if(rnd == 1)
        {
            titleStr = "Berserker";
        }
        if (rnd == 2)
        {
            titleStr = "Knight's";
        }
        if (rnd == 3)
        {
            titleStr = "Incinerating";
        }
        if (rnd == 4)
        {
            titleStr = "Sorcerer's";
        }
        if (rnd == 5)
        {
            titleStr = "Shieldbreaker";
        }
        if (rnd == 6)
        {
            titleStr = "Medic's";
        }
        if (rnd == 7)
        {
            titleStr = "Looter's";
        }

        Inventory.Add(new Item(Inventory.Count + 1, titleStr,  rnd*20, rnd*10, rnd, rnd, rnd, rnd, 0));

      //  Inventory.Add(new Item(Inventory.Count + 1, titleStr,  10, 50, rnd, rnd, rnd, rnd, 0));


        foreach (Item item in Inventory)
        {
            print("ID: " + item.id + " | Title: " + item.title + " | Damage: " + item.baseDamage + " | Crit: " + item.critChance +
                " | Prefix 1: " + item.prefix1ID + " | Prefix 2: " + item.prefix2ID + " | Suffix 1: " + item.suffix1ID + " | Suffix 2: " + item.suffix2ID + " | Crafted: " + item.craftedID );
        }
    }

    public void ScrapItem(int id_)
    {
        print("Inventory Size: " + Inventory.Count);
      //  Inventory.RemoveAt(id_);

        var found = Inventory.Find(Item => Item.id == id_);
        if(found != null)
        {
            Inventory.Remove(found);
        }
    }

}
