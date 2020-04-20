using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class IntrupWatch : MonoBehaviour, IIntruption
{
    [Header("Quest Hint")]
    [SerializeField] GameObject target;

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

    private float _timer;
    private float _measuringStartTime, _measuringEndTime;
    private float _angle = Mathf.Infinity;


    private RadarArrow _radarArrow;
    private bool _isLighting;
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

        _radarArrow.DeactiveRadar();
    }

    private void Update()
    {
        LightingMethod();

        if(_isLighting && _angle < SuccessThresholdAngle)
        {
            _isLighting = false;
            _radarArrow.lightIndicator.SetActive(false);
            _radarArrow.ActiveRadar(target);

            dTimeNotification = Time.time - _measuringStartTime;
        }
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
}
