using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StudyCase
{
    A, B, C, D, E, F
}

public class SetPlayerId : MonoBehaviour
{
    [SerializeField] ScreenText screenText;

    public void SetIDMethod()
    {
        GameManager.Instance.PlayerID = screenText.playerId;

        IntrupptionManager.Instance.CurrentCase = SetStudyCase(screenText.playerId);
        IntrupptionManager.Instance.InterruptionListGenerator();
        IntrupptionManager.Instance.InitializeInterruption();
    }

    private int SetStudyCase(int playerId)
    {
        var NumberOfCases = IntrupptionManager.Instance.InterruptionCollections.Count;
        var Permutations = AdditionalMath.Factorial(NumberOfCases);
        var Starting = IntrupptionManager.Instance.StartingNumber;
        var reminder = (playerId - Starting) % Permutations;

        return reminder;
    }


}
