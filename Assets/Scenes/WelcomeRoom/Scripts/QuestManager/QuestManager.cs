using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using TMPro;


namespace WelcomeRoom.QuestManager
{
    public interface IQuest { }
    [ExecuteInEditMode]
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private string QuestsPath = "Quests";
        [SerializeField] private GameObject MainQuestBody;
        [SerializeField] private Vector3 MQBodyiniOffset = new Vector3(-0.1f, 0.13f, -0.06f);
        [SerializeField] private Vector3 MQBodyOffset = new Vector3(-0.1f, -0.13f, 0.0f);
        [SerializeField] private GameObject SubQuestBody;
        [SerializeField] private Vector3 SQBodyOffset = new Vector3(0f, -0.1f, 0f);
        [SerializeField] private GameObject DataObjectRoot;

        private readonly List<MainQuest> mainQuests = new List<MainQuest>();

        public MainQuest[] MainQuests => mainQuests.ToArray();

        public void ReadQuestXml()
        {
            mainQuests.Clear();

            var text = Resources.Load<TextAsset>(QuestsPath);
            if (!text)
            {
                Debug.LogError($"QuestManager::ReadQuestXML: Unable to load quest file {QuestsPath}");
                return;
            }

            var doc = XDocument.Load(new MemoryStream(text.bytes));

            var mainQuestPosition = MQBodyiniOffset;
            foreach (var mainQuestText in doc.Descendants("MainQuest"))
            {
                var subQuestOffsetCount = 0;

                var questText = mainQuestText.Attribute("Key")?.Value;
                if (questText == null) { continue; }

                var mainQuestObject = InstantiateQuestObject<MainQuest>(MainQuestBody, mainQuestPosition, DataObjectRoot.transform);
                mainQuestObject.name = mainQuestObject.GetComponentInChildren<TextMeshPro>().text = questText;
                mainQuestObject.ActivateLamp();

                foreach (var subQuestText in mainQuestText.Elements())
                {
                    var subQuestPosition = subQuestOffsetCount * SQBodyOffset;
                    var subQuestObject = InstantiateQuestObject<SubQuest>(SubQuestBody, subQuestPosition, mainQuestObject.transform);
                    subQuestObject.name = subQuestObject.GetComponentInChildren<TextMeshPro>().text = subQuestText.Value;

                    if (subQuestText.Attribute("Key")?.Value == "i")
                        subQuestObject.HasAdditionalInformation = true;

                    if (subQuestText.Attribute("Script")?.Value != null)
                    {
                        Debug.Log("Script Found!!!");
                        var _script = Type.GetType(subQuestText.Attribute("Script")?.Value);
                        subQuestObject.gameObject.AddComponent(_script);
                    }

                    subQuestObject.ActivateLamp();
                    mainQuestObject.AddSubQuest(subQuestObject);

                    subQuestOffsetCount++;
                }
                mainQuestPosition += MQBodyOffset + SQBodyOffset * subQuestOffsetCount;

                mainQuests.Add(mainQuestObject);
            }
            if (!GetComponentInParent<GameManager>()) { DataObjectRoot.SetActive(false); }
        }

        private static T InstantiateQuestObject<T>(GameObject questPrefab, Vector3 position, Transform parent = null)
        {
            var mainQuestObject = Instantiate(questPrefab, parent);
            mainQuestObject.transform.localScale = Vector3.one;
            mainQuestObject.transform.localPosition = position;
            return mainQuestObject.GetComponent<T>();
        }

        private void OnEnable()
        {
            CleanObjects();
            ReadQuestXml();
        }

        private void OnDisable()
        {
            CleanObjects();
        }

        private void CleanObjects()
        {
            foreach (var x in GetComponentsInChildren<MainQuest>())
            {
                DestroyImmediate(x.gameObject);
            }
            foreach (var mainQuestObject in MainQuests)
            {
                if (mainQuestObject)
                    DestroyImmediate(mainQuestObject.gameObject); //TODO should be removed
            }
        }
    }
}

