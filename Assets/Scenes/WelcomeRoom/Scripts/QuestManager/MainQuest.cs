using System.Linq;

namespace WelcomeRoom.QuestManager
{
    public class MainQuest : Quest
    {

        public override bool IsDone()
        {
            if (!SubQuests.All(subQuest => subQuest.IsDone())) return false;
            OnQuestFinished.Invoke();
            return true;
        }
    }
}
