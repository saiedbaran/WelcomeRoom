using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public abstract class Quest : MonoBehaviour
    {
        [Header("Status Lamp")]
        [SerializeField] private GameObject ActiveLamp;
        [SerializeField] private GameObject DeactiveLamp;

        public TextMeshPro Textfield;
        public UnityEvent OnQuestFinished = new UnityEvent();

        public List<Quest> SubQuests = new List<Quest>();

        public string Text { get; set; }

        public bool IsActive { get; set; }
        

        public abstract bool IsDone();
        public bool isFinished;

        protected virtual void Start()
        {
            //DeactivateLamp();
            OnQuestFinished.AddListener(DeactivateLamp);
        }

        public void AddSubQuest(Quest subQuest)
        {
            SubQuests.Add(subQuest);
        }

        public void ActivateLamp()
        {
            if (DeactiveLamp != null) DeactiveLamp.SetActive(false);
            if (ActiveLamp != null) ActiveLamp.SetActive(true);
        }

        public void DeactivateLamp()
        {
            if (DeactiveLamp != null) DeactiveLamp.SetActive(true);
            if (ActiveLamp != null) ActiveLamp.SetActive(false);
        }
    }
}
