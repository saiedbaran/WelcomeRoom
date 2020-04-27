using System;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum LightingType
{
    Flashing,
    Flowing
}

public class IntrupTextAmbient : MonoBehaviour, IIntruption
{
    [Header("Quest Hint")]
    [SerializeField] GameObject questObject;
    [SerializeField] GameObject questText, continueText;

    [Header("Lighting Parameters")]
    [SerializeField] GameObject ambientLight;
    [SerializeField] LightingType lightingType;
    [SerializeField] float AmbientLightIntensity;
    [SerializeField] float SuccessThresholdAngle = 15f;
    [SerializeField] float RemainingIntesity = 10000f;

    [Header("Flashing Parameters")]
    [SerializeField] float Frequency;

    [Header("Flowing Parameters")]
    [SerializeField] float Power = 10f;
    [SerializeField] float AngleLimit = 60f;

    [Header("Measurements")]
    public float dTimeTotal;
    public float dTimeNotification;
    public float dTimeTask;

    public OnNotificationCatch onNotification;

    private float _timer;
    private float _measuringStartTime, _measuringEndTime;
    private float _angle;
    private bool _isLighting = false;

    private bool _isDone = false;

    void Start()
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

        ambientLight.gameObject.SetActive(false);
        questObject.SetActive(false);
        questText.SetActive(false);
        continueText.SetActive(true);

        IntrupptionManager.Instance.activationDevice.GetComponent<PuzzleManager>().ContinuePuzzle();

        _isDone = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(gameObject.activeSelf && ambientLight)
        {
            LightingMethod();
        }



        if (CheckSuccess()) { ActiveQuest(); }
    }

    private void LightingMethod()
    {
        if(_isLighting)
        {
            var VectorToLight = gameObject.transform.position - Camera.main.transform.position;
            _angle = Vector3.Angle(Camera.main.transform.forward, VectorToLight);

            if (lightingType == LightingType.Flashing)
            {
                ambientLight.GetComponent<HDAdditionalLightData>().intensity = AmbientLightIntensity * (Mathf.Sin(_timer * Frequency) * 0.5f + 0.5f);
            }
            if (lightingType == LightingType.Flowing)
            {
                ambientLight.GetComponent<HDAdditionalLightData>().intensity =
                    AmbientLightIntensity * Mathf.Pow(Mathf.SmoothStep(0, 1, _angle / AngleLimit), Power) + RemainingIntesity;
            }
        }
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

    public void ActiveQuest()
    {
        // Active Quest Object
        questObject.SetActive(true);

        // Invoke Unity Analytics Events
        IntrupptionManager.Instance.InvokeEvents(
            IntrupptionManager.Instance.PrefixUnityAnalytic + DateTime.Now + ":" + "Notification",
            (this.name + ": Notification: Player "),
            dTimeNotification
            );
    }

    public bool isDone()
    {
        return _isDone;
    }
}
