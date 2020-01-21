using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyOfCamera : MonoBehaviour
{
    [SerializeField] GameObject CameraAttached;
    Vector3 InitialRelativeRotation;
    Vector3 InitialRelativePosition;
    // Start is called before the first frame update
    void Start()
    {
        InitialRelativeRotation = transform.rotation.eulerAngles - CameraAttached.transform.rotation.eulerAngles;
        InitialRelativePosition = transform.position - CameraAttached.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraAttached.transform.position + InitialRelativePosition;
        var CurrentRotation = CameraAttached.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0,CurrentRotation.y, CurrentRotation.z);
    }
}
