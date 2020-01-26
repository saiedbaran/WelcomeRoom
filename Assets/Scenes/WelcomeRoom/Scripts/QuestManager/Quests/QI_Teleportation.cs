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
        public QI_Teleportation_Helper[] Helpers;

        private List<Quest> subquestList = new List<Quest>();
        void Start()
        {
            teleportAction = Teleport.instance.teleportAction;
            Helpers = FindObjectsOfType<QI_Teleportation_Helper>();
            foreach (var helper in Helpers)
            {
                helper.HelperObject.SetActive(true);
            }
        }

        void Update()
        {
            if (gameObject.GetComponent<SubQuest>().IsActive)
            {
                if (Helpers.Length == 0)
                {
                    Helpers = FindObjectsOfType<QI_Teleportation_Helper>();
                    foreach (var helper in Helpers)
                    {
                        helper.HelperObject.SetActive(true);
                    }
                }
                if (teleportAction.changed)
                {
                    QuestDone();
                }
            }
        }

        private void QuestDone()
        {
            Debug.Log("Teleport Action Changed!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            ActiveNextQuest();

            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }

        public void ActiveNextQuest()
        {
            subquestList = GetComponentInParent<MainQuest>().SubQuests;
            foreach (var quest in subquestList)
            {
                if (!quest.IsActive)
                {
                    quest.IsActive = true;
                    return;
                }
            }
        }
    }
}
