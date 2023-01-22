using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassPatch : MonoBehaviour
{
    public GameObject grass;
    public int width = 10;
    public int height = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int ii = 0; ii < height; ii++)
            {

                int rnd = Random.Range(160, 255);

               var go = Instantiate(grass, transform);
                go.transform.Translate(i, ii, 0);
            }
        }
    }

}
