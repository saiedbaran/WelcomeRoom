﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class QM_InfoObject : MonoBehaviour
{
    public GameObject TextField;
    public Material material;

    private void OnHandHoverBegin(Hand hand)
    {
        var InfoTable = GetComponentInParent<WelcomeRoom.QuestManager.QuestManager>().gameObject.GetComponentInChildren<QM_InfoTable>().gameObject;
        InfoTable.SetActive(true);
        InfoTable.GetComponent<QM_InfoTable>().Text = TextField.GetComponent<TextMeshPro>().text;
        InfoTable.GetComponent<QM_InfoTable>().material = material;
        InfoTable.GetComponent<QM_InfoTable>().SetParameters();
    }

}
