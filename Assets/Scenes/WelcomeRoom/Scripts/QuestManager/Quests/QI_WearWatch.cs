﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_WearWatch : MonoBehaviour
    {
        public void QuestDone()
        {
            Debug.Log("Watch is on your hand!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();
            Destroy(this);
        }
    }
}


