using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtoInventory : MonoBehaviour
{
    [SerializeField] GameObject AttacherGameobject;
    Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<InventoryItems>() != null)
        {   
            collider.gameObject.transform.parent = AttacherGameobject.transform;
            collider.gameObject.GetComponent<Rigidbody>().drag = 0.9f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<InventoryItems>() != null)
        {   
            collider.gameObject.transform.parent = null;
        }
    }
}
