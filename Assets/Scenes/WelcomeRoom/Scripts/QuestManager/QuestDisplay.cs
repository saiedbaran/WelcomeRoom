using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    [RequireComponent(typeof(TextMeshPro))]
    public class QuestDisplay : MonoBehaviour
    {
        [SerializeField] private Quest quest;

        private void Start()
        {
            if (quest == null)
                return;

            var textMesh = GetComponent<TextMeshPro>();
            textMesh.text = quest.Text;
        }
    }
}
