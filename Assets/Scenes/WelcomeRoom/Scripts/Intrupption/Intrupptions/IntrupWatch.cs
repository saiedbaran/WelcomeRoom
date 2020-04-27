using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Analytics;

public class IntrupWatch : MonoBehaviour, IIntruption
{
    [Header("Quest Hint")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject questObject;
    [SerializeField] GameObject questText, continueText;
    

    [Header("Lighting Parameters")]
    [SerializeField] float AmbientLightIntensity = 1000000.0f;
    [SerializeField] float SuccessThresholdAngle = 5f;
    [SerializeField] float RemainingIntesity = 10000f;
    [SerializeField] float Power = 10f;
    [SerializeField] float AngleLimit = 60f;

    [Header("Measurements")]
    public float dTimeTotal;
    public float dTimeNotification;
    public float dTimeTask;

    [SerializeField] AnalyticsEventTracker analyticsEvent;
    public string Text_Notification,Text_Total, Text_Task;

    private float _timer;
    private float _measuringStartTime, _measuringEndTime;
    private float _angle = Mathf.Infinity;
    private bool _isDone;

    public OnNotificationCatch onNotification;


    private RadarArrow _radarArrow;
    private bool _isLighting;

    void Start()
    {
        if (onNotification == null)
            onNotification = new OnNotificationCatch();
    }

    public bool CheckSuccess()
    {
        return false;
    }

    public void OnBeginIntrupption()
    {
        target.SetActive(true);

        _radarArrow = FindObjectOfType<RadarArrow>();
        _radarArrow.lightIndicator.SetActive(true);
        _isLighting = true;

        _measuringStartTime = Time.time;
    }

    public void OnEndIntruption()
    {
        _measuringEndTime = Time.time;
        dTimeTotal = Time.time - _measuringStartTime;
        dTimeTask = dTimeTotal - dTimeNotification;

        IntrupptionManager.Instance.InvokeEvents(
            IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Task",
            (this.name + ": Task: Player "),
            dTimeTask
            );

        Debug.Log(IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Task");

        _radarArrow.DeactiveRadar();
        questObject.SetActive(false);
        questText.SetActive(false);
        continueText.SetActive(true);

        IntrupptionManager.Instance.activationDevice.GetComponent<PuzzleManager>().ContinuePuzzle();

        _isDone = true;
    }

    private void Update()
    {
        LightingMethod();

        if(_isLighting && _angle < SuccessThresholdAngle)
        {
            _isLighting = false;
            _radarArrow.lightIndicator.SetActive(false);
            _radarArrow.ActiveRadar(target);

            // Active Quest Object
            questObject.SetActive(true);

            dTimeNotification = Time.time - _measuringStartTime;
            Text_Notification = this.name + ": Notification: Player " + GameManager.Instance.PlayerID + ": " + dTimeNotification;

            // Invoke Unity Analytics Events
            IntrupptionManager.Instance.InvokeEvents(
                IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Notification",
                (this.name + ": Notification: Player "),
                dTimeNotification
                );

            //TODO: Removeit ( Only For Test)
            onNotification.Invoke();
            analyticsEvent.TriggerEvent();

        }
    }

    private void InvokeEvents(string eventName, string exportType)
    {

        var NotificationID = exportType + GameManager.Instance.PlayerID.ToString();

        Analytics.CustomEvent(eventName,
            new Dictionary<string, object>
            {
                { NotificationID, dTimeNotification }
            }
        );
    }

    private void LightingMethod()
    {
        if (_isLighting)
        {
            var VectorToLight = _radarArrow.lightIndicator.transform.position - Camera.main.transform.position;
            _angle = Vector3.Angle(Camera.main.transform.forward, VectorToLight);

            _radarArrow.lightIndicator.GetComponent<HDAdditionalLightData>().intensity =
                AmbientLightIntensity * Mathf.Pow(Mathf.SmoothStep(0, 1, _angle / AngleLimit), Power) + RemainingIntesity;
        }
    }

    public bool isDone()
    {
        return _isDone;
    }

}
