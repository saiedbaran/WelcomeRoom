using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_EMove : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    [SerializeField] float UpSpeed = 0.1f;
    [SerializeField] float InvokeTime = 1f;

    Vector3 iniPosition, finalPosition;
    float _startTime;
    bool isAlreadyThere = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Initialization", InvokeTime);
    }

    private void Initialization()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        _startTime = Time.time;

        iniPosition = transform.position;
        finalPosition = new Vector3(iniPosition.x, Camera.main.transform.position.y, iniPosition.z);
        isAlreadyThere = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlreadyThere)
        {
            float _fraction = (Time.time - _startTime) * UpSpeed;

            if (_fraction < 1)
            {
                transform.position = Vector3.Lerp(iniPosition, finalPosition, _fraction);
            }
            else
            {
                Sphere.GetComponent<QM_Eobject_M>().enabled = true;
            }
        }

    }
}
