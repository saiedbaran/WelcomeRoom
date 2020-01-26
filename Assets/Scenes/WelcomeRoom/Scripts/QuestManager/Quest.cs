﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WelcomeRoom.QuestManager
{
    public abstract class Quest : MonoBehaviour
    {
        [Header("Status Lamp")]
        [SerializeField] private GameObject ActiveLamp;
        [SerializeField] private GameObject DeactiveLamp;

        public UnityEvent OnQuestFinished = new UnityEvent();

        protected List<Quest> SubQuests = new List<Quest>();

        public string Text { get; set; }

        public bool IsActive { get; set; }

        public abstract bool IsDone();
        public bool isFinished;

        protected virtual void Start()
        {
            ActivateLamp();
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
