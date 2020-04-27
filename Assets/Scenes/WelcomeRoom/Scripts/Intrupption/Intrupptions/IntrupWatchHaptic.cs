using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using Valve.VR;

[RequireComponent(typeof(SteamVR_ActivateActionSetOnLoad))]
public class IntrupWatchHaptic : MonoBehaviour, IIntruption
{
    [Header("Quest Hint")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject questObject;
    [SerializeField] GameObject questText, continueText;

    [Header("Haptic Parameters")]
    public SteamVR_Action_Vibration HapticAction;
    [SerializeField] float duration = 1f;
    [SerializeField] float frequency = 150f;
    [SerializeField] float amplitude = 200f;

    [Header("Lighting Parameters")]
    [SerializeField] float SuccessThresholdAngle = 10f;

    [Header("Measurements")]
    public float dTimeTotal;
    public float dTimeNotification;
    public float dTimeTask;

    private float _timer;
    private float _measuringStartTime, _measuringEndTime;
    private float _angle = Mathf.Infinity;

    private RadarArrow _radarArrow;
    private bool _isPulsing;
    private bool _isLighting;

    public OnNotificationCatch onNotification;

    private bool _isDone;

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

        // Active Haptic
        _isPulsing = true;

        // Active Light
        _radarArrow.lightIndicator.SetActive(true);
        _isLighting = true;
        _radarArrow.lightIndicator.GetComponent<HDAdditionalLightData>().intensity = 0f;

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

        _radarArrow.DeactiveRadar();

        questObject.SetActive(false);
        questText.SetActive(false);
        continueText.SetActive(true);

        IntrupptionManager.Instance.activationDevice.GetComponent<PuzzleManager>().ContinuePuzzle();

        _isDone = true;
    }

    private void Update()
    {
        if(_isPulsing)
        {
            Pulse(duration, frequency, amplitude, SteamVR_Input_Sources.LeftHand);
            LightingMethod();
        }

        if (_isLighting && _angle < SuccessThresholdAngle)
        {
            _isLighting = false;
            _isPulsing = false;
            _radarArrow.lightIndicator.SetActive(false);
            _radarArrow.ActiveRadar(target);

            dTimeNotification = Time.time - _measuringStartTime;

            // Invoke Unity Analytics Events
            IntrupptionManager.Instance.InvokeEvents(
                IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Notification",
                (this.name + ": Notification: Player "),
                dTimeNotification
                );

            // Active Quest Object
            questObject.SetActive(true);
        }

    }

    private void LightingMethod()
    {
        if (_isLighting)
        {
            var VectorToLight = _radarArrow.lightIndicator.transform.position - Camera.main.transform.position;
            _angle = Vector3.Angle(Camera.main.transform.forward, VectorToLight);
        }
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources Source)
    {
        HapticAction.Execute(0, duration, frequency, amplitude, Source);
    }

    public bool isDone()
    {
        return _isDone;
    }
}
