﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  WelcomeRoom.QuestManager
{
    public class QI_LearnHover : MonoBehaviour
    {
        public QI_LearnHover_Helper[] Helpers;
        void Start()
        {
            Helpers = FindObjectsOfType<QI_LearnHover_Helper>();
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
                    Helpers = FindObjectsOfType<QI_LearnHover_Helper>();
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
    }
}

