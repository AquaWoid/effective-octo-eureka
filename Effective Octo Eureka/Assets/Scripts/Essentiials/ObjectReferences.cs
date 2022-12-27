using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferences : MonoBehaviour
{

    public static ObjectReferences instance;


    public GameObject Player;
    public GameObject InventoryStats;
    public GameObject Stats;


    private void Awake()
    {
        instance = this;
    }
}
