using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeRoom.QuestManager;

public class GM_SkipQuests : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            foreach (var quest in GameManager.Instance.GetComponentsInChildren<SubQuest>())
            {
                if (quest.IsActive)
                {
                    Debug.Log("Quest skipped!!!");
                    quest.isFinished = true;
                    quest.IsDone();
                    return;
                }
            }
        }

    }
}
