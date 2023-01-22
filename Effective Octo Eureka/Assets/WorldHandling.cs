using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldHandling : MonoBehaviour
{

    public Slider sunValue;
    public Light sun;
    public Text timeText;


    public float StartTime = 1f;

    public float timeFactor = 1f;
    public float fadeFactor = 1000f;

    float nTime;

    string defTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sun.intensity = sunValue.value;
    }

    private void FixedUpdate()
    {
        nTime = (Time.fixedDeltaTime * timeFactor);

        if (StartTime > 0)
        {
            StartTime += nTime;
        }

        DisplayTime(StartTime);

    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        defTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = defTime;

        //  timerText.text = nTime.ToString();
    }
}
