using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    [SerializeField] float Delay = 10f;
    float Etime = 0;
    // Start is called before the first frame update
    void Update()
    {
        transform.up = new Vector3(0, 1, 0);
        
        Etime = Etime + Time.deltaTime;
        if (Etime > Delay)
        {
            transform.right = transform.position - new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Etime = 0f;
        }
    }
}
