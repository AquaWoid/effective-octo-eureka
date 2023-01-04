using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{

    public GameObject player;

    public GameObject knight;
    public GameObject magma;
    public GameObject genericEnemy;

    public InputField levelInput;
    public InputField itemLevelInput;


    // Start is called before the first frame update
    void Start()
    {
       // player = ObjectReferences.instance.Player; 
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void DebugLevelUp()
    {

        for (int i = 0; i < int.Parse(levelInput.text); i++)
        {
            player.GetComponent<PlayerStats>().levelUp();
        }


    }

    public void DebugGiveItem()
    {

        int rnd = Random.Range(100, 120);
        int crit = Random.Range(10, 25);
        int itemLevel = int.Parse(itemLevelInput.text);
        int randomID = Random.Range(1000, 9999);
        int rndPre1 = Random.Range(1, 8);
        int rndPre2 = Random.Range(1, 8);
        int rndSuf1 = Random.Range(1, 8);
        int rndSuf2 = Random.Range(1, 8);



        var found = player.GetComponent<PlayerStats>().Inventory.Find(Item => Item.id == randomID);
        if (found == null)
        {
            Item item = new Item(randomID, "Debug Item", itemLevel, rnd * itemLevel, crit, rndPre1, rndPre2, rndSuf1, rndSuf2, 0, ItemType.sword);
            print("Sent dropped item to inventory");
            player.GetComponent<PlayerStats>().LootItem(item);

            Item armor = new Item(randomID, "Debug Item", itemLevel, rnd * itemLevel, crit, rndPre1, rndPre2, rndSuf1, rndSuf2, 0, ItemType.chest);
            player.GetComponent<PlayerStats>().LootItem(armor);

        }
        else
        {
            DebugGiveItem();
        }

    }

    public void SpawnKnight()
    {
       GameObject knightInstance = Instantiate(knight);
       knightInstance.transform.position = player.transform.position;
    }
    public void SpawnMagma()
    {
        GameObject magmatInstance = Instantiate(magma);
        magmatInstance.transform.position = player.transform.position;
    }

    public void SpawnGeneric()
    {
        GameObject genericInstance = Instantiate(genericEnemy);
        genericInstance.transform.position = player.transform.position;
    }

}
