using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WelcomeRoom.QuestManager
{
    public class QI_GrabQM_Invoker : MonoBehaviour
    {
        private void Update()
        {
            if (gameObject.GetComponentInChildren<QI_GrabQM>())
            {
                gameObject.GetComponentInChildren<QI_GrabQM>().QuestDone();
                Destroy(this);
            }
        }
    }
}

