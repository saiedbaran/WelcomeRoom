using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceToInventory : MonoBehaviour
{
    private void Start()
    {
        Invoke("InventoryDeactive", 0.1f);
    }

    private void InventoryDeactive()
    {
        gameObject.SetActive(false);
    }
}
