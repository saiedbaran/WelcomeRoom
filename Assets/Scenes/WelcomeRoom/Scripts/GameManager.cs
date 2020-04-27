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

    public int PlayerID = 0;

    [SerializeField] public GameObject Inventory;
    [SerializeField] public GameObject MenuSpace;
    [SerializeField] public GameObject QuestManagement;
    [SerializeField] public GameObject QMSpace;

    public List<GameObject> DeactiveAtStart = new List<GameObject>();

    public float VolumeAmount = 0.5f;

}