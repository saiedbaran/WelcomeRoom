using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float RoatationSpeed = 20f;
    public bool isGrabbed = false;

    // Update is called once per frame
    void Update()
    {
        if(!isGrabbed)       
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(Time.deltaTime * RoatationSpeed , Time.deltaTime * RoatationSpeed , 0f));
        }
    }
}
