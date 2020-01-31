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
        public QI_QMHandle_Help[] HelperHandle;

        private void Start()
        {
            Helpers = FindObjectsOfType<QI_GrabQM_Helper>();
            HelperHandle = FindObjectsOfType<QI_QMHandle_Help>();

            foreach (var helper in Helpers)
            {
                if (gameObject.GetComponent<SubQuest>().IsActive)
                {
                    helper.HelperObject.SetActive(true);
                }
            }

            foreach (var helperhandle in HelperHandle)
            {
                helperhandle.HelperObject.SetActive(true);
            }

        }

        private void Update()
        {
            ActiveHints();

            if (gameObject.GetComponent<SubQuest>().IsActive)
            {
                foreach (var helper in Helpers)
                {
                    if (helper.GetComponentInChildren<QuestManager>().ScrollHandle.transform.localPosition.y < -0.3f)
                    {
                        QuestDone();
                    }
                }
            }
        }



        public void DestroyHints()
        {
            foreach (var helper in Helpers)
            {
                Destroy(helper.Helper_hints);
            }
        }

        public void QuestDone()
        {
            Debug.Log("You already Grab and throw the Quest Manager!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            foreach (var helper in Helpers)
            {
                //Destroy(helper.gameObject);
            }
            Destroy(this);
        }

        private void ActiveHints()
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

                if (HelperHandle.Length == 0)
                {
                    HelperHandle = FindObjectsOfType<QI_QMHandle_Help>();
                }
                else
                {
                    foreach (var helperhandle in HelperHandle)
                    {
                        helperhandle.HelperObject.SetActive(true);
                    }
                }
            }
        }

    }
}


