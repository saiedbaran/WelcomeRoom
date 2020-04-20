using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> PuzzelObjects = new List<GameObject>();
    [SerializeField] GameObject Hints;
    private GameObject ActivePuzzle = null;

    public void SetNewPuzzle()
    {
        if (ActivePuzzle != null)
        {
            ActivePuzzle.SetActive(false);
            ActivePuzzle = null;
        }

        ActivePuzzle = PuzzelObjects[Random.Range(0, (PuzzelObjects.Count))];
        ActivePuzzle.SetActive(true);

        Hints.SetActive(false);

        IntrupptionManager.Instance.Activation();
    }

}
