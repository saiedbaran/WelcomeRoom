using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WelcomeRoom.QuestManager;

[ExecuteInEditMode]
public class ProgressManager : MonoBehaviour
{
    private static ProgressManager _instance;
    public static ProgressManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ProgressManager>();
            }
            return Instance;
        }
    }

    private  List<MainQuest> MainQuests = new List<MainQuest>();
    private QuestManager QuestManager;

    private void Update()
    {
        // Why do we need this?????
        QuestManager = FindObjectOfType<QuestManager>();
        if (!QuestManager) return;

        MainQuests.Clear();
        foreach(var mainQuest in QuestManager.MainQuests)
        {
            MainQuests.Add(mainQuest);
        }
    }
}
