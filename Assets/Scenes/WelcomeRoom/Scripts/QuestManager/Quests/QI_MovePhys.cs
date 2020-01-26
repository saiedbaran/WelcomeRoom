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
        public QI_MovePhys_Helper[] Helpers;
        void Start()
        {
            PrevCamerapostion = Camera.main.transform.position;
            Helpers = FindObjectsOfType<QI_MovePhys_Helper>();
            foreach (var helper in Helpers)
            {
                helper.HelperObject.SetActive(true);
            }
        }

        void Update()
        {
            SumDistance += (Camera.main.transform.position - PrevCamerapostion).magnitude;
            PrevCamerapostion = Camera.main.transform.position;
            if (SumDistance > MovementThreshold)
            {
                QuestDone();
            }
        }

        private void QuestDone()
        {
            Debug.Log("Ok, you walked enough!!!");
            GetComponent<SubQuest>().isFinished = true;
            GetComponent<SubQuest>().IsDone();
            foreach (var helper in Helpers)
            {
                Destroy(helper.gameObject);
            }
            Destroy(this);
        }
    }
}
