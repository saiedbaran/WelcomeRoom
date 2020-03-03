using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Localization;
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
        [SerializeField] private GameObject DataObject;
        [SerializeField] private GameObject Cover;
        public GameObject ScrollingColliders;
        public GameObject ScrollHandle;

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
            foreach (var mainQuest in doc.Descendants("MainQuest"))
            {
                var subQuestOffsetCount = 0;

                var mainQuestObject = InstantiateQuestObject<MainQuest>(MainQuestBody, mainQuestPosition, DataObjectRoot.transform);
                mainQuestObject.name = mainQuest.Element("TranslationKey")?.Value ?? "";
                mainQuestObject.GetComponentInChildren<LocalizedTMP>().Key = mainQuestObject.name;
                mainQuestObject.GetComponentInChildren<TextMeshPro>().text = mainQuest.Element("DefaultText")?.Value ?? "";
                mainQuestObject.ActivateLamp();

                foreach (var subQuest in mainQuest.Elements().Where(element => element.Name == "SubQuest"))
                {
                    var subQuestPosition = subQuestOffsetCount * SQBodyOffset;
                    var subQuestObject = InstantiateQuestObject<SubQuest>(SubQuestBody, subQuestPosition, mainQuestObject.transform);

                    subQuestObject.name = subQuest.Element("TranslationKey")?.Value ?? "";
                    subQuestObject.GetComponentInChildren<LocalizedTMP>().Key = subQuestObject.name;
                    subQuestObject.GetComponentInChildren<TextMeshPro>().text = subQuest.Element("DefaultText")?.Value ?? "";

                    if (subQuest.Attribute("HasAdditionalInformation")?.Value == "True")
                        subQuestObject.HasAdditionalInformation = true;

                    var subQuestScript = subQuest.Element("Script")?.Value;
                    if (subQuestScript != null)
                    {
                        var script = Type.GetType(subQuestScript);
                        subQuestObject.gameObject.AddComponent(script);
                        subQuestObject.gameObject.AddComponent<AudioSource>();
                    }

                    subQuestObject.ActivateLamp();
                    mainQuestObject.AddSubQuest(subQuestObject);


                    subQuestOffsetCount++;
                }

                foreach (var subQuest in mainQuestObject.SubQuests)
                {
                    subQuest.IsActive = false;
                    subQuest.DeactivateLamp();
                }

                mainQuestPosition += MQBodyOffset + SQBodyOffset * subQuestOffsetCount;
                mainQuestObject.DeactivateLamp();
                mainQuests.Add(mainQuestObject);

            }

            Active_FirstQuest();
        }

        private void Active_FirstQuest()
        {
            if (mainQuests.Count == 0)
                return;

            mainQuests[0].SubQuests[0].IsActive = true;
            mainQuests[0].SubQuests[0].ActivateLamp();
            mainQuests[0].IsActive = true;
            mainQuests[0].ActivateLamp();
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
            if (!GetComponentInParent<GameManager>())
            {
                DataObject.SetActive(false);
            }
            else
            {
                CleanObjects();
                ReadQuestXml();
            }
        }

        private void OnDisable()
        {
            if (GetComponentInParent<GameManager>())
            {
                CleanObjects();
            }
        }

        private void CleanObjects()
        {
            foreach (var x in GetComponentsInChildren<MainQuest>())
            {
                DestroyImmediate(x.gameObject);
            }
            if (Cover) { Cover.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f)); }
        }
    }
}

