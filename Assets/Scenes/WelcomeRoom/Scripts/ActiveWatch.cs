using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWatch : MonoBehaviour
{
    [SerializeField] GameObject watchObject;
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.GetComponent<WatchTemprory>())
        {
            trigger.gameObject.GetComponent<WatchTemprory>().parentWatch.SetActive(false);
            watchObject.SetActive(true);
            Destroy(trigger.gameObject.GetComponent<WatchTemprory>().parentWatch);

        }
    }
}