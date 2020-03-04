using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_EnterTriggerArea : MonoBehaviour
{
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.GetComponent<QM_TriggerArea>())
        {
            GetComponent<QM_EMove>().enabled = true;
            GetComponent<LookToPlayer>().enabled = true;

            GameManager.Instance.QMSpace.SetActive(false);
        }
    }
}