using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GramaphoneDisks_Rotation : MonoBehaviour
{
    [SerializeField]
    Vector3 rotationSpeed = new Vector3(0f, 1f, 0f);
    [SerializeField]
    Vector3 DefaultRotation = new Vector3(0f, 0f, 0f);

    Vector3 Rotation;

    private void Start()
    {
        Rotation = DefaultRotation;
    }
    void Update()
    {
        Rotation = Rotation + rotationSpeed;
        transform.rotation = Quaternion.Euler(Rotation);
    }
}