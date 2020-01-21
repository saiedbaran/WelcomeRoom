using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMenuHint : MonoBehaviour
{
    [SerializeField] GameObject HintText;
    
    private void OnTriggerEnter(Collider collider)
    {
        if(!collider.GetComponent<ShowHintBool>().isShowingHint)
        {
            HintText.SetActive(true);
            collider.GetComponent<ShowHintBool>().isShowingHint = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        HintText.SetActive(false);
        collider.GetComponent<ShowHintBool>().isShowingHint = false;
    }
}
