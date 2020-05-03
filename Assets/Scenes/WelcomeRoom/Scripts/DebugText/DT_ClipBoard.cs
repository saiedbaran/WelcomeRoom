using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DT_ClipBoard : MonoBehaviour
{
    [SerializeField] List<TextMeshPro> textFields = new List<TextMeshPro>();

    private void Update()
    {
        textFields[0].text = "User ID: " + GameManager.Instance.PlayerID;
        textFields[1].text = "Current Interruption: " + IntrupptionManager.Instance.CurrentInterruption().name;
        textFields[2].text = "Current Interruption ID: " + IntrupptionManager.Instance.currentInterruptionId;
        textFields[3].text = "Interruption Done? " + IntrupptionManager.Instance.CurrentInterruption().GetComponent<IIntruption>().isDone();
        textFields[4].text = "Time since begin: " + IntrupptionManager.Instance.GetTimeSinceBegin();
        //textFields[6].text = "Debug Log: " + this.name;
    }

}



