using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ShelfSlider : MonoBehaviour
{
    Vector3 InitialFacing;
    Vector3 InitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
        InitialFacing = transform.up.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = -1 * (transform.position - InitialPosition).magnitude * InitialFacing + InitialPosition;
    }
    
}
