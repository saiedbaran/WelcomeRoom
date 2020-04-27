using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenText : MonoBehaviour
{
    public int playerId { get; set; }
    private TextMeshPro _textField;
    void Start()
    {
        _textField = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        
    }

    public void UpdateTextField(int InputNumber)
    {
        if (playerId < 100 && InputNumber >= 0)
            playerId = playerId * 10 + InputNumber;
        else
            playerId = 0;


        _textField.text = "ID: " + playerId.ToString();
    }
}
