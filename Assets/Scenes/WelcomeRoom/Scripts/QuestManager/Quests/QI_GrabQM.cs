using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}


