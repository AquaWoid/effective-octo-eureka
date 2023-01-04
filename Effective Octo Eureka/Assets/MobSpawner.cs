using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    private List<GameObject> spawnPoints = new List<GameObject>();

    public GameObject mob;
    public bool allDead = false;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in this.transform)
        {
            spawnPoints.Add(child.gameObject);
            print("Added point: " + child.name);
            //  Debug.Log(count + " : " + child);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach (GameObject go in spawnPoints)
            {
                print("instantiated enemies: " + go.name);
                Instantiate(mob, go.transform);
                //  Debug.Log(count + " : " + child);

            }
        }


    }
}
