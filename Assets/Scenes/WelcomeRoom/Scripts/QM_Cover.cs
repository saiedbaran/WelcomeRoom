using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_Cover : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 0.1f;
    Vector3 iniRotation, finalRotation;
    float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;

        iniRotation = transform.rotation.eulerAngles;
        finalRotation = transform.rotation.eulerAngles + new Vector3(0f, 0f, 180f);
    }

    // Update is called once per frame
    void Update()
    {
        float _fraction = (Time.time - _startTime) * RotationSpeed;
        Vector3 currentRotation;

        if (_fraction < 1)
        {
            currentRotation = Vector3.Lerp(iniRotation, finalRotation, _fraction);
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }
}
