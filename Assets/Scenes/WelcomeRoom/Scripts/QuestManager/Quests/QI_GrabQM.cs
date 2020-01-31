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

        private void Start()
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

        private void Update()
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

                    foreach (var helper_Handle in FindObjectsOfType<QI_QMHandle_Help>())
                    {
                        if (!helper_Handle.GetComponentInParent<GameManager>())
                        {
                            helper_Handle.gameObject.SetActive(true);
                        }
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


