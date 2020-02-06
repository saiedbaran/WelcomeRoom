using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public class QI_LearnGrab : MonoBehaviour, IQuest
    {
        [SerializeField] int MaxTryNumber = 3;
        public QI_LearnGrab_Helper[] Helpers;
        public SteamVR_Action_Boolean GrabAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        private SteamVR_Behaviour_Pose Pose = null;

        int _tryNumber = 0;
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

                if (GrabAction.GetStateUp(SteamVR_Input_Sources.Any))
                {
                    _tryNumber++;
                    ModifyText();
                }

                if (_tryNumber >= MaxTryNumber)
                {
                    QuestDone();
                }
            }

        }

        private void ModifyText()
        {
            foreach (var helper in Helpers)
            {
                //if (helper.Current.GetComponent<TextMeshPro>())
                //{ helper.Current.GetComponent<TextMeshPro>().text = _tryNumber.ToString(); }
                if (helper.Total.GetComponent<TextMeshPro>())
                { helper.Total.GetComponent<TextMeshPro>().text = _tryNumber.ToString() + " / " + MaxTryNumber + " repeat"; }
            }
        }
        private void QuestDone()
        {
            Debug.Log("Trigger Pressed!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }
    }


}
