using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_GrabWatch : MonoBehaviour
    {
        public QI_GrabWatch_Helper[] Helpers;
        void Start()
        {
            Helpers = FindObjectsOfType<QI_GrabWatch_Helper>();
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
                gameObject.GetComponentInParent<MainQuest>().ActivateLamp();
                if (Helpers.Length == 0)
                {
                    Helpers = FindObjectsOfType<QI_GrabWatch_Helper>();
                }
                else
                {
                    foreach (var helper in Helpers)
                    {
                        helper.HelperObject.SetActive(true);
                    }
                }
            }
        }

        public void QuestDone()
        {
            Debug.Log("Watch is taken!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();

            foreach (var helper in Helpers)
            {
                Destroy(helper.GetComponent<QI_GrabWatch_Helper>().HelperHint);
            }
            Destroy(this);
        }
    }
}


