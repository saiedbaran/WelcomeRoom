using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> MainQuests = new List<GameObject>();

    WelcomeRoom.QuestManager.QuestManager QuestManager;

    void Update()
    {
        QuestManager = FindObjectOfType<WelcomeRoom.QuestManager.QuestManager>();
        if(QuestManager)
        {
            MainQuests.Clear();
            foreach(var mainQuest in QuestManager.MainQuestObjects)
            {
                MainQuests.Add(mainQuest);
            }
        }
    }
}
