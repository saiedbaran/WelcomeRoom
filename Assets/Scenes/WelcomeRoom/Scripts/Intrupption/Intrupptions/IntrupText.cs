using System;
using System.Collections.Generic;
using UnityEngine;

public class IntrupText : MonoBehaviour, IIntruption
{
    [Header("Quest Hint")]
    [SerializeField] GameObject questObject;
    [SerializeField] GameObject questText, continueText;

    [Header("Measurements")]
    public float dTimeTotal;
    public float dTimeNotification;
    public float dTimeTask;

    [SerializeField] float SuccessThresholdAngle = 15f;

    //[Header("Measurements")]
    //public float MeasuringDeltaTime;

    private float _timer;
    private float _measuringStartTime, _measuringEndTime;
    private float _angle;
    private bool _isLighting = false;
    private bool _isDone = false;

    public OnNotificationCatch onNotification;



    private void Start()
    {
        if (onNotification == null)
            onNotification = new OnNotificationCatch();
    }

    public void OnBeginIntrupption()
    {
        _measuringStartTime = Time.time;
        _isLighting = true;
    }

    public void OnEndIntruption()
    {
        _measuringEndTime = Time.time;
        dTimeTotal = _measuringEndTime - _measuringStartTime;
        dTimeTask = dTimeTotal - dTimeNotification;

        IntrupptionManager.Instance.InvokeEvents(
            IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Task",
            (this.name + ": Task: Player "),
            dTimeTask
            );

        questObject.SetActive(false);
        questText.SetActive(false);
        continueText.SetActive(true);

        IntrupptionManager.Instance.activationDevice.GetComponent<PuzzleManager>().ContinuePuzzle();

        _isDone = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_isLighting)
        {
            LightingMethod();
        }

        if (CheckSuccess()) { ActiveQuest(); }
    }

    private void ActiveQuest()
    {
        // Active Quest Object
        questObject.SetActive(true);

        // Invoke Unity Analytics Events
        IntrupptionManager.Instance.InvokeEvents(
            IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Task",
            (this.name + ": Notification: Player "),
            dTimeNotification
            );
    }

    private void LightingMethod()
    {
        var VectorToLight = gameObject.transform.position - Camera.main.transform.position;
        _angle = Vector3.Angle(Camera.main.transform.forward, VectorToLight);
    }

    public bool isDone()
    {
        return _isDone;
    }

    public bool CheckSuccess()
    {
        if (_angle < SuccessThresholdAngle && _isLighting)
        {
            dTimeNotification = Time.time - _measuringStartTime;

            _isLighting = false;

            return true;
        }

        return false;
    }
}