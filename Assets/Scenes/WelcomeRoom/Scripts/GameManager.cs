using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    [SerializeField] public GameObject Inventory;
    [SerializeField] public GameObject MenuSpace;
    [SerializeField] public GameObject QuestManagement;
    public float VolumeAmount = 0.5f;
}
