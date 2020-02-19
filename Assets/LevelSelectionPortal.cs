using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LevelSelectionPortal : MonoBehaviour
{
    public GameObject DoorHandle;

    public void ActiveDoorHandle()
    {
        DoorHandle.GetComponent<CircularDrive>().enabled = true;
    }
}
