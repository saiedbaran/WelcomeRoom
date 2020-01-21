using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotatorFollow : MonoBehaviour
{
    [SerializeField] GameObject MainGear;
    [SerializeField] float RotationConstant = 1f;

    [SerializeField] Vector3 RotationAxis = new Vector3(0f,1f,0f);

    Vector3 InitialRotation;
    Vector3 InitialMainRotation;
    // Start is called before the first frame update
    void Start()
    {
        InitialRotation = transform.rotation.eulerAngles;
        InitialMainRotation = MainGear.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler((MainGear.transform.rotation.eulerAngles - InitialMainRotation) * RotationConstant + InitialRotation);
        transform.rotation = Quaternion.Euler(((MainGear.transform.rotation.eulerAngles - InitialMainRotation) * RotationConstant).magnitude * RotationAxis.normalized  + InitialRotation);
    }
}
