using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class QM_InfoClose : MonoBehaviour
{
    private void OnHandHoverBegin(Hand hand)
    {
        var InfoTable = GetComponentInParent<WelcomeRoom.QuestManager.QuestManager>().gameObject.GetComponentInChildren<QM_InfoTable>().gameObject;
        InfoTable.GetComponent<QM_InfoTable>().CollectionofObjects.SetActive(false);
    }
}
