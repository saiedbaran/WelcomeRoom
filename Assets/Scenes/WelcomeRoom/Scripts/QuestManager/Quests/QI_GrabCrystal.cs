using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_GrabCrystal : MonoBehaviour
    {
        public QI_GrabCrystal_Helper[] Helpers;
        void Start()
        {
            Helpers = FindObjectsOfType<QI_GrabCrystal_Helper>();
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
                gameObject.GetComponentInParent<MainQuest>().ActivateLamp(); //TODO should be removed later

                if (Helpers.Length == 0)
                {
                    Helpers = FindObjectsOfType<QI_GrabCrystal_Helper>();
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
            Debug.Log("You Grabbed the Crystal!!!");
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
