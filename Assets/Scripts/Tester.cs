using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    // Use this for initialization
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fetching");
            Activity[] activities = await ActivityTracker.GetActivities();

            foreach(Activity act in activities)
            {
                Debug.Log(act.additionalObject.project.name);
            }
        }
    }
}
