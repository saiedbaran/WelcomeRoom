using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerId : MonoBehaviour
{
    [SerializeField] ScreenText screenText;

    public void SetIDMethod()
    {
        GameManager.Instance.PlayerID = screenText.playerId;
    }
}
