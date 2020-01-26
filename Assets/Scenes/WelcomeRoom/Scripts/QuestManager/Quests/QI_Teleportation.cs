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
        public SteamVR_Action_Boolean teleportAction;
        private TeleportMarkerBase teleportingToMarker;
        // Start is called before the first frame update
        void Start()
        {
            teleportAction = Teleport.instance.teleportAction;
        }

        // Update is called once per frame
        void Update()
        {
            //if (teleportingToMarker.ShouldMovePlayer()) { Debug.Log("We should teleport@@@"); }

            if (teleportAction.active)
            {
                Debug.Log("Teleport Action Active!!!");
            }

            if (teleportAction.changed) { Debug.Log("changed"); }
            //teleportAction.onStateDown
        }

    }
}
