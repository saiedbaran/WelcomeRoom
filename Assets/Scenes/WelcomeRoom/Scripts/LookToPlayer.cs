using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    [SerializeField] float UpSpeed = 1f;

    float Etime = 0;
    float _startTime;
    float _fraction;

    Vector3 iniVectorUp;

    void Start()
    {
        _startTime = Time.time;
        iniVectorUp = transform.up;
    }
    void Update()
    {
        _fraction = (Time.time - _startTime) * UpSpeed;

        if (_fraction < 1)
        {
            //transform.up = new Vector3(0, 1, 0);
            transform.up = Vector3.Lerp(iniVectorUp, new Vector3(0, 1, 0), _fraction);
        }

        transform.right = transform.position - new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}