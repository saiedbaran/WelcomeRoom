using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_Handle : MonoBehaviour
{
    [SerializeField] GameObject DataObjectRoot;
    public float MovementMultiplier = 1f;

    private Vector3 iniPosition;
    private Vector3 iniRootPosition;
    void Start()
    {
        SetIniPosition();
    }

    void Update()
    {
        float _elevationDiff = transform.position.y - iniPosition.y;
        Vector3 _rootPosition = new Vector3(iniRootPosition.x, iniRootPosition.y - _elevationDiff * MovementMultiplier, iniRootPosition.z);
        DataObjectRoot.transform.position = _rootPosition;

        // Avoid going upper than initial position;
        if (transform.position.y > iniPosition.y) { transform.position = iniPosition; }
    }

    public void SetIniPosition()
    {
        iniPosition = transform.position;
        iniRootPosition = DataObjectRoot.transform.position;
    }
}
