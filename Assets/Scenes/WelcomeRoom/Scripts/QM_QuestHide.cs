using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_QuestHide : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider trigger)
    {
        HideObjects(trigger);
    }

    private void OnTriggerExit(Collider trigger)
    {
        RevealObjects(trigger);
    }

    private static void HideObjects(Collider trigger)
    {
        Debug.Log(trigger.gameObject.name + " is hidden now");

        // if(trigger.GetComponentInParent<WelcomeRoom.QuestManager.MainQuest>())
        // {
        //     var parent = trigger.GetComponentInParent<WelcomeRoom.QuestManager.MainQuest>();
        //     foreach (var item in parent.GetComponentsInChildren<MeshRenderer>())
        //     {
        //         item.enabled = false;
        //     }
        // }

        trigger.gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (var item in trigger.gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            item.enabled = false;
        }
    }

    private static void RevealObjects(Collider trigger)
    {
        Debug.Log(trigger.gameObject.name + " is visible now");
        trigger.gameObject.GetComponent<MeshRenderer>().enabled = true;
        foreach (var item in trigger.gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            item.enabled = true;
        }
    }
}
