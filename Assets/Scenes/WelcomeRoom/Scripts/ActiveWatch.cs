using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeRoom.QuestManager;

public class ActiveWatch : MonoBehaviour
{
    [SerializeField] GameObject watchObject;
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.GetComponent<WatchTemprory>())
        {
            trigger.gameObject.GetComponent<WatchTemprory>().parentWatch.SetActive(false);
            watchObject.SetActive(true);
            GameManager.Instance.GetComponentInChildren<QI_GrabWatch>().QuestDone();
            GameManager.Instance.GetComponentInChildren<QI_WearWatch>().QuestDone();
            Destroy(trigger.gameObject.GetComponent<WatchTemprory>().parentWatch);
        }
    }
}