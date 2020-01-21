using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintToCamera : MonoBehaviour
{
    [SerializeField] GameObject CurrentCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 forward = transform.forward;
        //transform.right = CurrentCamera.transform.forward * -1;
        transform.LookAt(CurrentCamera.transform, new Vector3(0,1,0));
    }
}
