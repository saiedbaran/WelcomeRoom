using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QM_DataEnable : MonoBehaviour
    {
        private void OnEnable()
        {
            if (!gameObject.GetComponentInParent<GameManager>())
            {
                int i = 0;
                foreach (var mainquest in GetComponentsInChildren<MainQuest>())
                {
                    int j = 0;
                    foreach (var subquest in mainquest.GetComponentsInChildren<SubQuest>())
                    {
                        subquest.isFinished = GameManager.Instance.QuestManagement.GetComponent<QuestManager>().MainQuests[i].SubQuests[j].isFinished;
                        subquest.IsActive = GameManager.Instance.QuestManagement.GetComponent<QuestManager>().MainQuests[i].SubQuests[j].IsActive;
                        subquest.IsDone();
                        j++;
                    }
                    i++;
                }
            }
        }
    }
}

