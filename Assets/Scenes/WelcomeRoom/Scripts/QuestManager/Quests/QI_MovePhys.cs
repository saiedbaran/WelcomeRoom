using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_MovePhys : MonoBehaviour, IQuest
    {
        [SerializeField] float MovementThreshold = 4f;
        Vector3 iniCameraPosition;
        Vector3 PrevCamerapostion;
        int runCycle;
        public float SumDistance = 0f;
        void Start()
        {
            //iniCameraPosition = Camera.main.transform.position;
            PrevCamerapostion = Camera.main.transform.position;
        }

        void Update()
        {
            SumDistance += (Camera.main.transform.position - PrevCamerapostion).magnitude;
            PrevCamerapostion = Camera.main.transform.position;
            if (SumDistance > MovementThreshold)
            {
                Debug.Log("Ok, you walked enough!!!");
                GetComponent<SubQuest>().isFinished = true;
                GetComponent<SubQuest>().IsDone();
                Destroy(this);
            }
        }
    }
}
