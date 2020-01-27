using UnityEngine;
using UnityEngine.Serialization;

namespace WelcomeRoom.QuestManager
{
    public class SubQuest : Quest
    {
        [SerializeField] private GameObject finishLine;

        [SerializeField] private GameObject infoLogoObject;

        private bool hasAdditionalInformation;

        public bool HasAdditionalInformation
        {
            get => hasAdditionalInformation;
            set
            {
                hasAdditionalInformation = value;
                if (infoLogoObject != null) infoLogoObject.SetActive(true);
            }
        }

        public override bool IsDone()
        {
            if (isFinished)
            {
                OnQuestFinished.Invoke();
                ActiveNextSubQuest();
                GetComponentInParent<MainQuest>().IsDone();
                return true;
            }
            return false;
        }

        public void ActiveNextSubQuest()
        {
            var subquestList = GetComponentInParent<MainQuest>().SubQuests;
            foreach (var quest in subquestList)
            {
                if (!quest.IsActive)
                {
                    quest.IsActive = true;
                    quest.ActivateLamp();
                    return;
                }
            }
        }
    }

}