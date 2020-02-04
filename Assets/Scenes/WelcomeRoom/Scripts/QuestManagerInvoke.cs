using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerInvoke : MonoBehaviour
{
    //[SerializeField] GameObject QuestManager;
    [SerializeField] GameObject Parent;
    [SerializeField] Vector3 Position, Rotation, Scale;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        if (!gameObject.GetComponentInChildren<QuestManagerInstance>())
        {
            GameObject QuestManager = GameManager.Instance.QuestManagement;
            GameObject questManager = Instantiate(QuestManager) as GameObject;
            questManager.SetActive(true);
            questManager.transform.parent = Parent.transform;
            questManager.transform.localPosition = Position;
            questManager.transform.localRotation = Quaternion.Euler(Rotation);
            questManager.transform.localScale = Scale;
        }
    }
}
