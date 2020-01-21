using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithWatch : MonoBehaviour
{
    [SerializeField] GameObject Watch;

    Vector3 InitialOffset;
    // Start is called before the first frame update
    void Start()
    {
        InitialOffset = transform.position - Watch.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Watch.transform.position + InitialOffset.magnitude * -1 * Watch.transform.right;
    }
}
