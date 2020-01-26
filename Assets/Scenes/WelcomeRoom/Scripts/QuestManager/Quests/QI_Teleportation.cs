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
        public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
        private TeleportMarkerBase teleportingToMarker;
        void Start()
        {
            teleportAction = Teleport.instance.teleportAction;
        }

        void Update()
        {
            if (teleportAction.changed)
            {
                Debug.Log("Teleport Action Changed!!!");
                GetComponent<SubQuest>().isFinished = true;
                GetComponent<SubQuest>().IsDone();
                Destroy(this);
            }
        }

    }
}
