using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public class SubQuest : Quest
    {
        public GameObject finishLine;
        [Header("(i) logo object")]
        public GameObject moreInformation;
        public override bool IsDone()
        {
            return false;
        }

        public override void setPrefab()
        {
            questPrefab = GameManager.Instance.QuestManagement.GetComponent<QuestManager>().SubQuestBody;
        }
    }

}