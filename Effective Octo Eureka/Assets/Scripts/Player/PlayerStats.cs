using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Text[] UiText;

    public int level = 1;
    [ShowOnly] public float health = 100;
    public float maxHealth = 100;

    [ShowOnly] public float mana = 100;
    public float maxMana = 100;

    [ShowOnly] public int xp = 0;
    public float requieredXP = 200;

    float lastRequiered;

    public float PhysicalDamage = 100;
    public float MagicalDamage = 100;

    [ShowOnly] public float Defence = 10;
    public float maxDefence = 2000;
    public float damageMitigation;


    public float ElementalResistance = 10;

    public float MovementSpeed = 10f;

    public bool ImmuneToBleed = false;
    public bool ImmuneToBurning = false;

    public int ArmorEquipped;
    public int WeaponEquipped;

    public int Gold;

    

    float physCombined;
    float defCombined;

    FlaskUsage flask;

    int rnd;

    bool knockedBack = false;
    public bool blocked = false;

    public List<Item> Inventory = new List<Item>();

    ItemHolder iHolder;

    public GameObject itemHolderGO;

    public Transform inventoryGrid;

    public Slider[] hudSlider;

    Canvas canvas;

    AudioSource playerAudio;

    public AudioClip swordBlock;

    private void Awake()
    {
        lastRequiered = requieredXP;
    }



    private void Start()
    {
        playerAudio = transform.GetComponent<AudioSource>();
        flask = GetComponent<FlaskUsage>();

        //HP, Mana and XP Bar initialization
        hudSlider[0].maxValue = maxHealth;
        hudSlider[1].maxValue = maxMana;
        hudSlider[2].maxValue = requieredXP;

        //Visual Stats UI init
        canvas = transform.GetChild(0).GetComponent<Canvas>();
        int count = 0;


        //Declaration of UI Text childs
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

    // Safe Function - Linked to the Save System
    public void SaveGame()
    {

        SaveSystem.SavePlayer(this);

    }

    //Load Function - Linked to the Save System
    public void LoadGame()
    {


        //Loading data from Save file - Always keep up to date with the PlayerData script!!

        PlayerData data = SaveSystem.LoadPlayer();
        maxHealth = data.maxHealth;


        mana = data.mana;
        maxMana = data.maxMana;

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

        ArmorEquipped = data.ArmorEquipped;
        WeaponEquipped = data.WeaponEquipped;

        //Clear inventory window of any items before applying current Inventory
        foreach (Transform child in inventoryGrid.transform)
        {
            print("Scrapped: " + child.name);

            if(child.tag != "VisualIndicator")
            {
                Destroy(child.gameObject);
            }

        }


        //Instantiate item Holders based on our saved inventory List
        foreach (Item item in Inventory)
        {
            print("ID: " + item.id + " | Title: " + item.title + " | Damage: " + item.baseDamage + " | Crit: " + item.critChance +
                " | Prefix 1: " + item.prefix1ID + " | Prefix 2: " + item.prefix2ID + " | Suffix 1: " + item.suffix1ID + " | Suffix 2: " + item.suffix2ID + " | Crafted: " + item.craftedID);


            //  GameObject go = Instantiate(itemHolderGO, transform.GetChild(0).GetChild(12).GetChild(0));
            GameObject go = Instantiate(itemHolderGO, inventoryGrid.transform);
           // go.GetComponent<ItemHolder>().updateInventoryEntries(item);
            go.GetComponentInChildren<ItemHolder>().updateInventoryEntries(item);


            if (item.id == ArmorEquipped)
            {
                ObjectReferences.instance.EquipImageArmor.transform.SetParent(go.transform, false);
                ObjectReferences.instance.EquipImageArmor.GetComponent<Image>().enabled = true;
            }

            if (item.id == WeaponEquipped)
            {
                ObjectReferences.instance.EquipImageWeapon.transform.SetParent(go.transform, false);
                ObjectReferences.instance.EquipImageWeapon.GetComponent<Image>().enabled = true;
            }

            print("Applied: " + item.title + " to: " + go.name);

        }



    }
    private void Update()
    {

        damageMitigation = (Defence / maxDefence) / 2;

        //Prevent mana from goint to minus
        if (mana < 0)
        {
            mana = 0;
        }

        //Hp, Mana and XP Bar UI update
        hudSlider[0].value = health;
        hudSlider[1].value = mana;
        hudSlider[2].value = xp;

        //Debug Key triggers

         rnd = Random.Range(1, 8);

        //Add XP - will be gone when the debug menu is finished
        if (Input.GetKeyDown(KeyCode.R))
        {
            xp += 1000;
            requieredXP -= 1000;

        }

        //Add Item - will be gone when the debug menu is finished
        if (Input.GetKeyDown(KeyCode.T))
        {

            AddItem();

        }

        //Open Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(canvas.enabled == true)
            {
                Time.timeScale = 1;
                canvas.enabled = false;
                
            }else
            {
                Time.timeScale = 0;
                canvas.enabled = true;
            }


        }


        //Player Stats update
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
        UiText[10].text = "Damage Mitigation: " + damageMitigation * 100 + "%";

        //Level up condition
        if (xp >= requieredXP)
        {
            levelUp();
        }

        //Knockback condition
        if(knockedBack == true)
        {
            transform.Translate(Mathf.Lerp(0, -5, Time.deltaTime), 0, 0);
        }


    }

    //Knockback coroutine
    IEnumerator knockback()
    {
        knockedBack = true;
        yield return new WaitForSeconds(0.2f);
        knockedBack = false;
    }


    //Damage income function
    public void takeDamage(float damage)
    {

        if(blocked == false)
        {
            //Wood's super duper defence calculation
            damageMitigation = (Defence / maxDefence) / 2;
            damage = (damage - (damage * damageMitigation));
            health -= damage;
        } else
        {
            playerAudio.PlayOneShot(swordBlock);
        }


      //  StartCoroutine(knockback());


    }

    //Heal function - ist triggered from the FlaskUsage script
    public void heal(float amount)
    {
        health += amount;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    //Mana Regen function - ist triggered from the FlaskUsage script
    public void healMana(float amount)
    {
        mana += amount;
        if (mana >= maxMana)
        {
            mana = maxMana;
        }
    }

    //XP Gaining function - is triggered from the EnemyAi script
    public void getXP(int externalXP)
    {
        xp += externalXP;
    }

    //Level up function
    public void levelUp()
    {
        requieredXP = lastRequiered * 1.25f;
        lastRequiered = requieredXP;
        maxHealth *= 1.1f;
        maxMana *= 1.1f;
        mana = maxMana;
        health = maxHealth;
        hudSlider[2].maxValue = requieredXP;
        hudSlider[0].maxValue = maxHealth;
        hudSlider[1].maxValue = maxMana;

        xp = 0;

        level += 1;

        PhysicalDamage *= 1.15f;
        MagicalDamage *= 1.145f;

        Defence *= 1.16f;
        ElementalResistance *= 1.0595f;

        flask.updateRecoverAmount(maxHealth * 0.1f);

    }


    //Loot item to inventory function - can be triggered from EnemyAi script and the DebugMenu 
    public void LootItem(Item externalItem)
    {
        Inventory.Add(externalItem);

        GameObject go = Instantiate(itemHolderGO, inventoryGrid.transform);
       // go.GetComponent<ItemHolder>().updateInventoryEntries(externalItem);
        go.GetComponentInChildren<ItemHolder>().updateInventoryEntries(externalItem);
        print("Received dropped item to inventory");
    }

    //Add item to inventory function - DEBUG - soon deprecated due to the Debug Menu
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



        //Random ID and Stat generation
        int randomID = Random.Range(1000, 9999);
        int rndPre1 = Random.Range(1, 8);
        int rndPre2 = Random.Range(1, 8);
        int rndSuf1 = Random.Range(1, 8);
        int rndSuf2 = Random.Range(1, 8);



        //Checks if the ID is already in our inventory, if not adds an item -> if already in inventory, rerolls ID until not in inventory
        var found = Inventory.Find(Item => Item.id == randomID);
        if (found == null)
        {

            Item addedItem = new Item(randomID, titleStr, 1, rnd * 20, rnd * 10, rndPre1, rndPre2, rndSuf1, rndSuf2, 0, ItemType.sword);

            Inventory.Add(addedItem);

            GameObject go = Instantiate(itemHolderGO, inventoryGrid.transform);
            //go.GetComponent<ItemHolder>().updateInventoryEntries(addedItem);
            go.GetComponentInChildren<ItemHolder>().updateInventoryEntries(addedItem);

        }
        else
        {
            Debug.LogError("Error: Unique ID already present");
            AddItem();
        }



        //DEBUG: Show current inventory items
        foreach (Item item in Inventory)
        {
            print("ID: " + item.id + " | Title: " + item.title + " | Damage: " + item.baseDamage + " | Crit: " + item.critChance +
                " | Prefix 1: " + item.prefix1ID + " | Prefix 2: " + item.prefix2ID + " | Suffix 1: " + item.suffix1ID + " | Suffix 2: " + item.suffix2ID + " | Crafted: " + item.craftedID );
        }
    }

    //Item deletion function
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

    //Inventory Sorting function -> Work in Progress
    public void SortInventory()
    {
        Inventory = Inventory.OrderBy(Item => Item.baseDamage).ToList();


        foreach (Transform child in inventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in Inventory)
        {

            GameObject go = Instantiate(itemHolderGO, inventoryGrid.transform);
          //  go.GetComponent<ItemHolder>().updateInventoryEntries(item);
            go.GetComponentInChildren<ItemHolder>().updateInventoryEntries(item);

            print("Sorted by Base Damage");

        }

    }

    //Item Equip Function - work in progress - to do: add weapon damage correctly to base character damage
    public void EquipItem(Item item)
    {
        if(item.type == ItemType.sword)
        {
            WeaponEquipped = item.id;
            PhysicalDamage = item.baseDamage;
        }
        if(item.type == ItemType.chest)
        {
            ArmorEquipped = item.id;
            Defence = item.baseDamage;
        }

    }

}
