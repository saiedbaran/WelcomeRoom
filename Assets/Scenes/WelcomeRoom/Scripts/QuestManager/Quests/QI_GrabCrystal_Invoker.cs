using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeRoom.QuestManager;

public class QI_GrabCrystal_Invoker : MonoBehaviour
{
    public void InvokeQuestDone()
    {
        var _quests = FindObjectsOfType<QI_GrabCrystal>();
        foreach (var quest in _quests)
        {
            quest.QuestDone();
        }
        Destroy(this);
    }
}
