using System.Linq;

namespace WelcomeRoom.QuestManager
{
    public class MainQuest : Quest
    {
        public override bool IsDone()
        {
            if (!SubQuests.All(subQuest => subQuest.isFinished)) return false;
            OnQuestFinished.Invoke();
            ActiveNextMainQuest();
            return true;
        }

        public void ActiveNextMainQuest()
        {
            //var mainquestList = GetComponentInParent<QuestManager>().MainQuests;
            var _QuestManagerList = FindObjectsOfType<QuestManager>();
            foreach (var QML in _QuestManagerList)
            {
                foreach (var quest in QML.MainQuests)
                {
                    if (!quest.IsActive)
                    {
                        quest.IsActive = true;
                        quest.ActivateLamp();
                        ActiveNextSubQuest(quest);
                        return;
                    }
                }
            }

        }

        public void ActiveNextSubQuest(MainQuest mquest)
        {
            foreach (var subquest in mquest.SubQuests)
            {
                if (!subquest.IsActive)
                {
                    subquest.IsActive = true;
                    subquest.ActivateLamp();
                    return;
                }
            }
        }
    }
}
