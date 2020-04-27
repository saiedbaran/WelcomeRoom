using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> PuzzelObjects = new List<GameObject>();
    [SerializeField] GameObject startHints;
    [SerializeField] GameObject continueHints;

    private bool _isAlreadyPuzzle = false;
    private GameObject _activePuzzle = null;

    public void SetNewPuzzle()
    {
        if (GameManager.Instance.PlayerID == 0) { return; }

        if(_activePuzzle == null)
        {
            _activePuzzle = PuzzelObjects[Random.Range(0, (PuzzelObjects.Count))];
        }

        //if (_activePuzzle != null)
        //{
        //    _activePuzzle.SetActive(false);
        //    _activePuzzle = null;
        //}

        //TODO: Remove TheseTwo
        _activePuzzle.SetActive(true);
        _isAlreadyPuzzle = true;

        startHints.SetActive(false);
        continueHints.SetActive(false);

        IntrupptionManager.Instance.Activation();
    }

    public void ContinuePuzzle()
    {
        continueHints.SetActive(true);
        
    }

}
