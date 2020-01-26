using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_MovePhys : MonoBehaviour, IQuest
    {
        [SerializeField] float MovementThreshold;
        Vector3 iniCameraPosition;
        void Start()
        {
            iniCameraPosition = Camera.main.transform.position;
        }

        void Update()
        {
            float diffPosition = (Camera.main.transform.position - iniCameraPosition).magnitude;
            if (diffPosition > MovementThreshold)
            {
                Debug.Log("Ok, you walked enough!!!");
                GetComponent<SubQuest>().isFinished = true;
                GetComponent<SubQuest>().IsDone();
                Destroy(this);
            }
        }
    }
}
