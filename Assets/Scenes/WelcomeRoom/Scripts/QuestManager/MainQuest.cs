using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public class MainQuest : Quest
    {
        //private List<SubQuest> subQuests = new List<SubQuest>();

        public override bool IsDone()
        {
            foreach (var subQuest in subQuests)
            {
                if (!subQuest.GetComponent<SubQuest>().IsDone())
                    return false;
            }
            return true;
        }

        public override void setPrefab()
        {
            questPrefab = GameManager.Instance.QuestManagement.GetComponent<QuestManager>().MainQuestBody;
        }
    }
}
