using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

namespace WelcomeRoom.QuestManager
{
    public class QI_GrabQM : MonoBehaviour
    {
        public QI_GrabQM_Helper[] Helpers;
        void Start()
        {
            Helpers = FindObjectsOfType<QI_GrabQM_Helper>();
            foreach (var helper in Helpers)
            {
                if (gameObject.GetComponent<SubQuest>().IsActive)
                {
                    helper.HelperObject.SetActive(true);
                }
            }

        }

        void Update()
        {
            if (gameObject.GetComponent<SubQuest>().IsActive)
            {
                if (Helpers.Length == 0)
                {
                    Helpers = FindObjectsOfType<QI_GrabQM_Helper>();
                }
                else
                {
                    foreach (var helper in Helpers)
                    {
                        helper.HelperObject.SetActive(true);
                    }
                }
            }
            // if (GrabAction.GetStateDown(SteamVR_Input_Sources.Any))
            // {
            //     QuestDone();
            // }
        }

        public void QuestDone()
        {
            Debug.Log("Trigger Pressed!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            foreach (var helper in Helpers)
            {
                //Destroy(helper.gameObject);
            }
            Destroy(this);
        }

    }
}


