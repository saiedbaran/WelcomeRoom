using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAppearing : MonoBehaviour
{
    [SerializeField] float InventroyGenerationDelay = 1.0f;
    //[SerializeField] GameObject InventoryPrefab;

    GameObject inventoryPrefab;

    private void Start()
    {
        inventoryPrefab = GameManager.Instance.Inventory;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "MenuPlace")
        {
            Invoke("InventoryGeneration", InventroyGenerationDelay);
            //InventoryGeneration();
        }
    }

    private void InventoryGeneration()
    {
        //if (GameManager.Instance.Inventory) {GameManager.Instance.Inventory.GetComponent<DestroyMethod>().DestroyObject();}

        //inventoryPrefab = Instantiate(InventoryPrefab) as GameObject;
        //inventoryPrefab = InventoryPrefab;
        inventoryPrefab.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        inventoryPrefab.SetActive(true);

        //GameManager.Instance.Inventory = inventoryPrefab;
        Destroy(gameObject, 0.1f);
    }


}
