using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace WelcomeRoom.QuestManager
{
    public class QI_LearnGrab : MonoBehaviour, IQuest
    {
        public QI_LearnGrab_Helper[] Helpers;
        public SteamVR_Action_Boolean GrabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        public SteamVR_Behaviour_Pose Pose;
        void Start()
        {
            Pose = GetComponent<SteamVR_Behaviour_Pose>();
            Helpers = FindObjectsOfType<QI_LearnGrab_Helper>();
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
                    Helpers = FindObjectsOfType<QI_LearnGrab_Helper>();
                    foreach (var helper in Helpers)
                    {
                        helper.HelperObject.SetActive(true);
                    }
                }
                if (GrabAction.GetStateDown(Pose.inputSource))
                {
                    QuestDone();
                }
            }

        }
        private void QuestDone()
        {
            Debug.Log("Trigger Pressed!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            //ActiveNextQuest();

            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }
    }


}
