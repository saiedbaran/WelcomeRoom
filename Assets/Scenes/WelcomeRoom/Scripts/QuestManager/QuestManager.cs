using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;


namespace WelcomeRoom.QuestManager
{
    [ExecuteInEditMode]
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] string QuestsPath = "Quests.xml";
        public GameObject MainQuestBody;
        [SerializeField] Vector3 MQBodyiniOffset = new Vector3(-0.1f, 0.13f, -0.06f);
        [SerializeField] Vector3 MQBodyOffset = new Vector3(-0.1f, -0.13f, 0.0f);
        public GameObject SubQuestBody;
        [SerializeField] Vector3 SQBodyOffset = new Vector3(0f, -0.1f, 0f);
        public GameObject DataObjectRoot;

        // public List<string> mainQuestsList = new List<string>();
        public List<GameObject> MainQuestObjects = new List<GameObject>();



        public void ModifyQuestmanager()
        {

        }

        public void ReadQuestXML()
        {
            // mainQuestsList.Clear();
            MainQuestObjects.Clear();

            var text = Resources.Load<TextAsset>(QuestsPath);
            if (!text)
            {
                Debug.LogError($"QuestManager::ReadQuestXML: Unable to load language file {QuestsPath}");
                return;
            }

            var doc = XDocument.Load(new MemoryStream(text.bytes));

            int _mainoffsetNum = 0;

            foreach (var mainQuestText in doc.Descendants("MainQuest"))
            {
                int _SuboffsetNum = 0;
                var Qtext = mainQuestText.Attribute("Key")?.Value;

                if (Qtext == null) { continue; }

                GameObject mainQuestObject = Instantiate(MainQuestBody);

                mainQuestObject.transform.parent = DataObjectRoot.transform;
                mainQuestObject.transform.localScale = new Vector3(1, 1, 1);
                mainQuestObject.transform.localPosition = MQBodyiniOffset + MQBodyOffset * _mainoffsetNum;
                mainQuestObject.name = Qtext;

                mainQuestObject.GetComponent<MainQuest>().QuestText = Qtext;
                mainQuestObject.GetComponent<MainQuest>().ChangeText(Qtext);

                foreach (var subQuestText in mainQuestText.Elements())
                {
                    GameObject subQuestObject = Instantiate(SubQuestBody);

                    subQuestObject.transform.parent = mainQuestObject.transform;
                    subQuestObject.transform.localScale = new Vector3(1, 1, 1);
                    subQuestObject.transform.localPosition = _SuboffsetNum * SQBodyOffset;
                    subQuestObject.name = subQuestText.Value;

                    subQuestObject.GetComponent<SubQuest>().QuestText = subQuestText.Value;
                    subQuestObject.GetComponent<SubQuest>().ChangeText(subQuestText.Value);

                    LampMethod(subQuestObject);

                    if (subQuestText.Attribute("Key")?.Value == "i") { subQuestObject.GetComponent<SubQuest>().moreInformation.SetActive(true); }

                    mainQuestObject.GetComponent<MainQuest>().subQuests.Add(subQuestObject.GetComponent<SubQuest>());

                    _SuboffsetNum = _SuboffsetNum + 1;
                }

                LampMethod(mainQuestObject);

                MainQuestObjects.Add(mainQuestObject);

                _mainoffsetNum = _mainoffsetNum + _SuboffsetNum;

            }
        }

        public void LampMethod(GameObject _Quest)
        {
            if (_Quest.GetComponent<MainQuest>())
            {
                if (_Quest.GetComponent<MainQuest>().IsDone())
                {
                    GameObject _lamp = Instantiate(_Quest.GetComponent<MainQuest>().DeactiveLamp);
                    _lamp.transform.parent = _Quest.transform;
                    _lamp.transform.position = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.position;
                    _lamp.transform.localScale = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.localScale;
                    _lamp.transform.rotation = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.rotation;
                    Debug.Log("Task done !!!");
                }
                else
                {
                    GameObject _lamp = Instantiate(_Quest.GetComponent<MainQuest>().ActiveLamp);
                    _lamp.transform.parent = _Quest.transform;
                    _lamp.transform.position = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.position;
                    _lamp.transform.localScale = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.localScale;
                    _lamp.transform.rotation = _Quest.GetComponent<MainQuest>().LampPlaceHolder.transform.rotation;
                }
            }

            if (_Quest.GetComponent<SubQuest>())
            {
                if (_Quest.GetComponent<SubQuest>().IsDone())
                {
                    GameObject _lamp = Instantiate(_Quest.GetComponent<SubQuest>().DeactiveLamp);
                    _lamp.transform.parent = _Quest.transform;
                    _lamp.transform.position = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.position;
                    _lamp.transform.localScale = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.localScale;
                    _lamp.transform.rotation = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.rotation;
                    _Quest.GetComponent<SubQuest>().finishLine.SetActive(true);
                }
                else
                {
                    GameObject _lamp = Instantiate(_Quest.GetComponent<SubQuest>().ActiveLamp);
                    _lamp.transform.parent = _Quest.transform;
                    _lamp.transform.position = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.position;
                    _lamp.transform.localScale = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.localScale;
                    _lamp.transform.rotation = _Quest.GetComponent<SubQuest>().LampPlaceHolder.transform.rotation;
                }
            }

        }


        private void OnEnable()
        {
            ReadQuestXML();
        }

        private void OnDisable()
        {
            foreach (var x in MainQuestObjects)
            {
                DestroyImmediate(x.gameObject); //TODO should be romoved
            }
        }
    }
}

