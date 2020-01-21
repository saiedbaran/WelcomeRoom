using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 100.0f;
    public Vector3 InitialRotation;
    float Rotation =0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation += Time.deltaTime * RotationSpeed;
        InitialRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(InitialRotation.x,InitialRotation.y,Rotation);
    }
}
