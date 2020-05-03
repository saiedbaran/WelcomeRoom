﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.Rendering;

public interface IIntruption
{
    void OnBeginIntrupption();
    void OnEndIntruption();
    bool CheckSuccess();
    bool isDone();
}

class IntrupptionManager : MonoBehaviour
{
    private static IntrupptionManager _instance;
    public static IntrupptionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<IntrupptionManager>();
            return _instance;
        }
    }

    [Header("Required Objects")]
    public GameObject tangramPuzzle;
    public GameObject activationDevice;

    [Header("Interruptions")]
    public bool useRandomList = false;
    //[SerializeField] List<GameObject> Interruptions = new List<GameObject>();
    public List<InterruptionCollection> InterruptionCollections = new List<InterruptionCollection>();
    public List<int> StudyList = new List<int>();

    [Header("Parameters")]
    public float InterruptionDelay = 10.0f;
    public int StartingNumber = 100;
    public string PrefixUnityAnalytic = "Prototype: ";
    public int CurrentCase;


    //public IIntruption currentIntruption;

    public int currentInterruptionId { get; set; }
    public int currentIntCollId { get; set; }

    private float _beginTime, _timeSinceBegin;
    private bool _activeTimer;
    private int _id = 0;
    private List<int> executeInterruptionOrder = new List<int>();
    private List<GameObject> Interruptions = new List<GameObject>();

    private void Start()
    {
        InitializeInterruption();
    }

    public void InitializeInterruption()
    {
        InterruptionListGenerator();
        Interruptions = InterruptionCollections[StudyList[currentIntCollId]].Interruptions;

        for (int i = 0; i < InterruptionCollections.Count; i++)
        {
            executeInterruptionOrder.Add(i);
        }

        if (useRandomList) { RandomList.RandomIntList(executeInterruptionOrder); }

        currentInterruptionId = executeInterruptionOrder[_id];
    }

    private void Update()
    {
        if (!_activeTimer) { return; }

        _timeSinceBegin = Time.time - _beginTime;

        if (_timeSinceBegin > InterruptionDelay)
        {
            _activeTimer = false;

            CallIntrupption();
        }
    }

    public void Activation()
    {
        Interruptions[currentInterruptionId].SetActive(false);

        _beginTime = Time.time;
        _activeTimer = true;

        tangramPuzzle.SetActive(true);
    }

    public void CallIntrupption()
    {
        if (Interruptions[currentInterruptionId].GetComponent<IIntruption>().isDone())
        {
            _id++;
            if (_id == executeInterruptionOrder.Count) { FinishInterrupptionCollection(); }
            currentInterruptionId = executeInterruptionOrder[_id];
        }

        Interruptions[currentInterruptionId].SetActive(true);
        Interruptions[currentInterruptionId].GetComponent<IIntruption>().OnBeginIntrupption();

        tangramPuzzle.SetActive(false);
    }

    private void FinishInterrupptionCollection()
    {
        _id = 0;
        currentIntCollId++;
        executeInterruptionOrder.Clear();

        InitializeInterruption();

        //Application.Quit();
    }

    public GameObject CurrentInterruption()
    {
        return Interruptions[currentInterruptionId];
    }

    //TODO: Remove this method
    public float GetTimeSinceBegin()
    {
        return _timeSinceBegin;
    }

    public void InvokeEvents(string eventName, string exportType, float value)
    {

        var NotificationID = exportType + GameManager.Instance.PlayerID.ToString();

        Analytics.CustomEvent(eventName,
            new Dictionary<string, object>
            {
                { NotificationID, value }
            }
        );
    }


    private void InterruptionListGenerator()
    {
        var numberOfInterruptions = InterruptionCollections.Count;

        var tempInterruptionOrder = new List<int>();
        for (int i = 0; i < numberOfInterruptions; i++)
        {
            tempInterruptionOrder.Add(i);
        }

        var PermutateCollection = AdditionalMath.Permutate(tempInterruptionOrder, tempInterruptionOrder.Count);

        StudyList = PermutateCollection.ElementAt(CurrentCase) as List<int>;
    }

}

[System.Serializable]
public class OnNotificationCatch : UnityEvent
{
}

[System.Serializable]
public class OnEndCatch : UnityEvent
{

}

