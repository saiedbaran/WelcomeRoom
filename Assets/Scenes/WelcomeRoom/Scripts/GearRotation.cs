using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [SerializeField] float RotationDelay = 1.0f;
    [SerializeField] float RotationAngle = 12.25f;
    
    float TimeSpend =0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.Euler(
        //     transform.rotation.eulerAngles.x+Time.deltaTime * RotationSpeed,
        //     transform.rotation.eulerAngles.y,
        //     transform.rotation.eulerAngles.z);
        TimeSpend += Time.deltaTime;
        if(TimeSpend > RotationDelay)
        {
            transform.RotateAround(transform.position, transform.up, RotationAngle);
            TimeSpend = 0.0f;
        }
    }
}
