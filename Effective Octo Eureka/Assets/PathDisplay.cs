using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathDisplay : MonoBehaviour
{

    private List<Transform> waypoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

        int count = 0;
        foreach (Transform child in this.transform)
        {
            waypoints.Add(child);
            count += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Debug.DrawLine(waypoints[i].position, waypoints[i + 1].position);

        }
    }
}
