using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    void BuildDatabase()
    {




    }

    public void AddItem()
    {
        items.Add(new Item(1, "Berserkers", 20, 10, 1, 1, 0, 1, 0));
    }


}
