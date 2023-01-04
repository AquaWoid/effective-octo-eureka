using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldHandling : MonoBehaviour
{

    public Slider sunValue;
    public Light sun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sun.intensity = sunValue.value;
    }
}
