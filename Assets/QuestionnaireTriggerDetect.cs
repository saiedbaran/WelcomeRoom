using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionnaireTriggerDetect : MonoBehaviour
{
    [SerializeField] int Value;
    [SerializeField] GameObject TriggerObject;
    [SerializeField] Transform FreezePosition;

    private GameObject _triggerObject;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject == TriggerObject)
        {
            var parentQuestionnaire = GetComponentInParent<QuestionnaireDevice>();
            parentQuestionnaire.ReleaseButtons();

            _triggerObject = trigger.gameObject;
            trigger.transform.position = FreezePosition.position;
            //trigger.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            var joint = _triggerObject.AddComponent<FixedJoint>();
            joint.connectedBody = gameObject.GetComponent<Rigidbody>();

            parentQuestionnaire.Highlight.SetActive(true);
            parentQuestionnaire.Highlight.transform.position = transform.position - new Vector3(0, 0.03f, 0);

            parentQuestionnaire.SetScore(Value);
        }
    }
}
