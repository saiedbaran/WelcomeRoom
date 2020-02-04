using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_Eobject : MonoBehaviour
{
    [SerializeField] GameObject MainQM;
    [SerializeField] GameObject rightSphere;
    [SerializeField] GameObject leftSphere;
    [SerializeField] GameObject Rod;

    [SerializeField] float MovementSpeed = 0.1f;
    [SerializeField] float Distance = 0.4f;

    float _startTime;
    Vector3 iniRight, iniLeft, iniRod;
    Vector3 finalRight, finalLeft, finalRod;
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        iniRight = rightSphere.transform.position;
        iniLeft = leftSphere.transform.position;
        iniRod = Rod.transform.localScale;

        finalRight = iniRight + rightSphere.transform.right.normalized * Distance;
        finalLeft = iniLeft + leftSphere.transform.right.normalized * Distance;
        finalRod = new Vector3(iniRod.z, iniRod.y, iniRod.z);
    }

    // Update is called once per frame
    void Update()
    {
        float _fraction = (Time.time - _startTime) * MovementSpeed;

        if (_fraction < 1)
        {
            rightSphere.transform.position = Vector3.Lerp(iniRight, finalRight, _fraction);
            leftSphere.transform.position = Vector3.Lerp(iniLeft, finalLeft, _fraction);
            Rod.transform.localScale = Vector3.Lerp(iniRod, finalRod, _fraction);
        }
        else
        {
            MainQM.transform.position = transform.position;
            MainQM.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
