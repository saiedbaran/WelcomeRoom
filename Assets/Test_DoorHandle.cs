using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DoorHandle : MonoBehaviour
{
    Rigidbody rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void ActiveDoor()
    {
        Vector3 iniPos = transform.position;
        Vector3 iniRot = transform.rotation.eulerAngles;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        Debug.Log("Put Speed to Zero!!!");
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        transform.position = iniPos;
        transform.rotation = Quaternion.Euler(iniRot);
    }
}
