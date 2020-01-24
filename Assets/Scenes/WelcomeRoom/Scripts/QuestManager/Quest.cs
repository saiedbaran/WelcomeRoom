using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WelcomeRoom.QuestManager
{
    public abstract class Quest : MonoBehaviour
    {
        [SerializeField] protected GameObject questPrefab;

        [SerializeField] protected string QuestText;

        [SerializeField] protected Vector3 QuestOffset = Vector3.zero; // subquest (0, -0.1, 0), mainquest 0.13, 0.16

        public bool isActive { get; set; }

        public abstract bool IsDone();

        public void CreateQuestObject(Transform parent)
        {
            var questObject = GameObject.Instantiate(questPrefab, parent);
            var textComponent = questObject.GetComponentInChildren<TextMeshPro>();
            if (textComponent == null)
                return;

            textComponent.text = QuestText;
        }
    }
}
