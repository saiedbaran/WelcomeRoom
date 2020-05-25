using System.Net.Mime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using GEAR.LimeSurvey;

public class QuestionnaireDevice : MonoBehaviour
{
    [Header("Questionnaires")]
    public List<QuestionList> QuestionLists = new List<QuestionList>();
    public List<int> QuestionnaireRecords = new List<int>();

    [Header("Text Components")]
    public TextMeshPro QuestionPlaceHolder;
    public TextMeshPro MinRange, MaxRange;

    [Header("Features")]
    public GameObject Highlight;
    public List<GameObject> ButtonList = new List<GameObject>();

    [Header("LimeSurveyUploader")]
    public TextAsset SurveyTemplate;
    public LimeSurveyUploader UploaderPrefab;

    [Header("Debug")]
    public QuestionList CurrentQuestionList;

    private int _score;
    private int _currentQuestionId;
    private int _currentListId = 0;

    void Awake()
    {

        foreach (var qL in QuestionLists)
        {
            qL.GenerateList();
        }

        CurrentQuestionList = QuestionLists[_currentListId];
    }

    void OnEnable()
    {
        _currentListId = 0;
        _currentQuestionId = 0;
        UpdateTextFields();
    }

    public void UpdateTextFields()
    {
        if (_currentQuestionId == CurrentQuestionList.Questions.Count)
        {
            _currentListId++;

            if (_currentListId == QuestionLists.Count)
            {
                UploadQuestionnaireData();
                IntrupptionManager.Instance.GoToQuestionnnaire.SetActive(false);
                IntrupptionManager.Instance.PreparationForNextInterruption();
                return;
            }

            CurrentQuestionList = QuestionLists[_currentListId];
            _currentQuestionId = 0;
        }

        QuestionPlaceHolder.text = CurrentQuestionList.Questions[_currentQuestionId].QuestionText;
        MinRange.text = CurrentQuestionList.Questions[_currentQuestionId].MinText;
        MaxRange.text = CurrentQuestionList.Questions[_currentQuestionId].MaxText;

        _currentQuestionId++;
    }

    private void UploadQuestionnaireData()
    {
        //AssetDatabase.Refresh();

        var tempText = SurveyTemplate.text;
        foreach (var record in QuestionnaireRecords)
        {
            tempText += "\t" + record;
        }
        tempText = tempText.Replace("UserID", GameManager.Instance.PlayerID.ToString());

        var userRecords = new TextAsset(tempText);
        //AssetDatabase.CreateAsset(userRecords, "Assets/UserRecords.txt"); TODO: Save the file lateron

        UploaderPrefab.StartUpload(userRecords);
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public void SaveData()
    {
        if (_score == 0) { return; }

        Highlight.SetActive(false);
        QuestionnaireRecords.Add(_score);

        UpdateTextFields();

        _score = 0;
        ReleaseButtons();
    }

    public void ShowHighlight(Transform buttonTransform)
    {
        Highlight.SetActive(true);
        Highlight.transform.position = buttonTransform.position;
    }

    public void ReleaseButtons()
    {
        foreach (var button in ButtonList)
        {
            Destroy(button.GetComponent<FixedJoint>());
        }
    }

}