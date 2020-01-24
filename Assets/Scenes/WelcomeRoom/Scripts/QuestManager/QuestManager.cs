using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private GameObject questManagerPrefab;
        private List<MainQuest> mainQuests = new List<MainQuest>();

        public void ModifyQuestmanager()
        {

        }
    }
}

