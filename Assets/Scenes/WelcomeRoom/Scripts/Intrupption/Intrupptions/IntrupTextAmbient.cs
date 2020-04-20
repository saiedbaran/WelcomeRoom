using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum LightingType
{
    Flashing,
    Flowing
}

[ExecuteInEditMode]
public class IntrupTextAmbient : MonoBehaviour, IIntruption
{
    [SerializeField] GameObject ambientLight;
    [SerializeField] LightingType lightingType;
    [SerializeField] float AmbientLightIntensity;
    [SerializeField] float SuccessThresholdAngle = 5f;
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

    private float _timer;
    public float _measuringStartTime, _measuringEndTime;
    private float _angle;
    private bool _isLighting = false;
    public void OnBeginIntrupption()
    {
        Debug.Log("Capturing Time");
        _measuringStartTime = Time.time;
        _isLighting = true;
    }

    public void OnEndIntruption()
    {
        _measuringEndTime = Time.time;
        dTimeTotal = _measuringEndTime - _measuringStartTime;
        dTimeTask = dTimeTotal - dTimeNotification;

        ambientLight.gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(gameObject.activeSelf && ambientLight)
        {
            LightingMethod();
        }

        if(_angle < SuccessThresholdAngle && _isLighting)
        {
            dTimeNotification = Time.time - _measuringStartTime;

            _isLighting = false;
        }

        if (CheckSuccess()) { OnEndIntruption(); }
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
        if (_angle < SuccessThresholdAngle) { return true; }
        return false;
    }
}
