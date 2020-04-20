using System;
using System.Collections.Generic;
using UnityEngine;

public class IntrupText : MonoBehaviour, IIntruption
{
    [SerializeField] float SuccessThresholdAngle = 5f;

    [Header("Measurements")]
    public float MeasuringDeltaTime;

    private float _measuringStartTime, _measuringEndTime;
    private float _angle;

    public bool CheckSuccess()
    {
        if (_angle < SuccessThresholdAngle) { return true; }
        return false;
    }

    public void OnBeginIntrupption()
    {
        //gameObject.SetActive(true);

        _measuringStartTime = Time.time;
    }

    public void OnEndIntruption()
    {
        MeasuringDeltaTime = _measuringEndTime - _measuringStartTime;
    }

    private void Update()
    {
        var VectorToLight = gameObject.transform.position - Camera.main.transform.position;
        _angle = Vector3.Angle(Camera.main.transform.forward, VectorToLight);

        if (CheckSuccess()) { OnEndIntruption(); }
    }
}