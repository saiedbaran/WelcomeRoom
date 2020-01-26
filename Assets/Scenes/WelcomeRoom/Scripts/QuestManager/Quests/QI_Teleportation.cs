using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WelcomeRoom.QuestManager
{
    public class QI_Teleportation : MonoBehaviour
    {
        private TeleportMarkerBase teleportingToMarker;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (teleportingToMarker.ShouldMovePlayer()) { Debug.Log("We should teleport@@@"); }
        }
    }
}
