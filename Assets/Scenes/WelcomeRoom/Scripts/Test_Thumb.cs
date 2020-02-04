using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Thumb : MonoBehaviour
{
    [SerializeField] GameObject FingerRig;
    Vector3 initialTransformDelta;
    Vector3 initialRotationDelta;
    // Start is called before the first frame update
    void Start()
    {
        initialTransformDelta = transform.position - FingerRig.transform.position;
        initialRotationDelta = transform.rotation.eulerAngles - FingerRig.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = FingerRig.transform.position + initialTransformDelta;
        transform.rotation = Quaternion.Euler(FingerRig.transform.rotation.eulerAngles + initialRotationDelta);
    }
}
