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
            var mainquestList = GetComponentInParent<QuestManager>().MainQuests;
            foreach (var quest in mainquestList)
            {
                if (!quest.IsActive)
                {
                    quest.IsActive = true;
                    quest.ActivateLamp();
                    ActiveNextSubQuest();
                    return;
                }
            }
        }

        public void ActiveNextSubQuest()
        {
            foreach (var quest in SubQuests)
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
