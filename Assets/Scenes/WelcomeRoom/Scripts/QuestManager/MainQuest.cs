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
            isFinished = true;
            //this.IsActive = false;
            return true;
        }

        public void ActiveNextMainQuest()
        {
            
            var mainquestList = GetComponentInParent<QuestManager>().MainQuests;
            foreach (var quest in mainquestList)
            {
                if (!quest.IsActive & !isFinished)
                {
                    quest.ActivateLamp();
                    ActiveNextSubQuest(quest);
                    quest.IsActive = true;
                    return;
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
