using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize += 1;
        }

        if(cam.orthographicSize <= 0)
        {
            cam.orthographicSize = 1;
        }
    }
}
