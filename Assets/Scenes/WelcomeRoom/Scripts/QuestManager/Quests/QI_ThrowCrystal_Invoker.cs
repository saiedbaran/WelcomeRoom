using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeRoom.QuestManager;

public class QI_ThrowCrystal_Invoker : MonoBehaviour
{
    public void InvokeQuestDone()
    {
        var _quests = FindObjectsOfType<QI_ThrowCrystal>();
        foreach (var quest in _quests)
        {
            quest.QuestDone();
        }
        Destroy(this);
    }
}
