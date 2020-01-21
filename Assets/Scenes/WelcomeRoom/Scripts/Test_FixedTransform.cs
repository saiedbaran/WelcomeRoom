using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FixedTransform : MonoBehaviour
{
    [SerializeField] public GameObject Parent;
    Vector3 InitialTransform;   
    Vector3 InitialRotation;
    Vector3 InitialOffset;
    // Start is called before the first frame update
    void Start()
    {
        InitialTransform = transform.position;
        InitialOffset = transform.position - Parent.transform.position;
        InitialRotation = transform.eulerAngles;
    }

    void Update()
    {
        //transform.position = InitialTransform;
        transform.position = InitialOffset + Parent.transform.position;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, InitialRotation.y, InitialRotation.z);
    }
    
}
