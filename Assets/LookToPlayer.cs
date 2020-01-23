using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.right = transform.position - new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
