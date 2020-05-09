using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> PuzzelObjects = new List<GameObject>();
    [SerializeField] GameObject startHints;
    [SerializeField] GameObject continueHints;
    private bool _isAlreadyPuzzle = false;
    private GameObject _activePuzzle = null;

    public bool IsButtonActive { get; set; }

    void Awake()
    {
        IsButtonActive = true;
    }

    public void SetNewPuzzle()
    {
        if (GameManager.Instance.PlayerID == 0) { return; }
        if (!IsButtonActive) { return; }

        if (_activePuzzle == null)
        {
            _activePuzzle = PuzzelObjects[Random.Range(0, (PuzzelObjects.Count))];
        }

        //TODO: Remove TheseTwo
        _activePuzzle.SetActive(true);
        _isAlreadyPuzzle = true;

        startHints.SetActive(false);
        continueHints.SetActive(false);

        if (IntrupptionManager.Instance.isLastInterruptionList())
        {
            Debug.Log("This one is the last interruption in the list");
            IntrupptionManager.Instance.BeginQuestionnaire();
            IntrupptionManager.Instance.GoToQuestionnnaire.SetActive(true);
            IsButtonActive = false;

            return;
        }

        IntrupptionManager.Instance.Activation();
    }

    public void ContinuePuzzle()
    {
        continueHints.SetActive(true);
    }

}
