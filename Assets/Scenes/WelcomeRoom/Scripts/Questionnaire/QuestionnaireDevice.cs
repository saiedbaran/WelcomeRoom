using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionnaireDevice : MonoBehaviour
{
    [Header("Text Components")]
    public TextMeshPro QuestionPlaceHolder;
    public TextMeshPro MinRange, MaxRange;

    [Header("Features")]
    public GameObject Highlight;

    public QuestionList CurrentQuestionList;

    private int _score;
    private int _currentQuestionId;

    //TODO: Remove this start
    void Start()
    {
        CurrentQuestionList = GetComponent<QuestionList>();
        CurrentQuestionList.GenerateList();

        UpdateTextFields();
    }

    public void UpdateTextFields()
    {
        QuestionPlaceHolder.text = CurrentQuestionList.Questions[_currentQuestionId].QuestionText;
        MinRange.text = CurrentQuestionList.Questions[_currentQuestionId].MinText;
        MaxRange.text = CurrentQuestionList.Questions[_currentQuestionId].MaxText;

        _currentQuestionId++;
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public void SaveData()
    {
        Highlight.SetActive(false);
        UpdateTextFields();
    }

    public void ShowHighlight(Transform buttonTransform)
    {
        Highlight.SetActive(true);
        Highlight.transform.position = buttonTransform.position;
    }

}
