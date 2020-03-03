using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramaphoneReader : MonoBehaviour
{
    [SerializeField] Vector3 DiskOffset = new Vector3(0f, 0.01f, 0f);
    [SerializeField] GameObject MidScreen;

    Material ScreenMaterial;

    private void Start()
    {
        ScreenMaterial = MidScreen.GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.GetComponent<GramaphoneDisks>())
        {
            DiskPosition(trigger);
            MidScreen.GetComponent<MeshRenderer>().material = trigger.GetComponent<MeshRenderer>().materials[1];
            GetComponent<LevelSelectionPortal>().ActiveDoorHandle(trigger.GetComponent<GramaphoneDisks>().ScenePrefab);
        }
    }

    private void DiskPosition(Collider trigger)
    {
        trigger.GetComponent<Rigidbody>().isKinematic = true;
        trigger.transform.position = gameObject.transform.position + DiskOffset;
        trigger.gameObject.GetComponent<GramaphoneDisks_Rotation>().enabled = true;
    }
}