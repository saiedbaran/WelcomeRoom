using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LevelSelectionPortal : MonoBehaviour
{
    public GameObject DoorHandle;
    public GameObject OutLight;

    public void ActiveDoorHandle(GameObject RoomPrefab)
    {
        DoorHandle.GetComponent<CircularDrive>().enabled = true;
        RoomPrefab.SetActive(true);
        OutLight.gameObject.SetActive(false);
    }
}
