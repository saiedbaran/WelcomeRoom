using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowQMinHelper : MonoBehaviour
{
    [SerializeField] Vector3 Position;
    [SerializeField] Vector3 Rotation;
    [SerializeField] Vector3 Scale;

    private void OnEnable()
    {
        if (gameObject.GetComponentInChildren<QuestManagerInstance>())
        {
            Destroy(gameObject.GetComponentInChildren<QuestManagerInstance>().gameObject);
        }
        GameObject QuestManager = GameManager.Instance.QuestManagement;
        GameObject questManager = Instantiate(QuestManager) as GameObject;
        questManager.SetActive(true);
        questManager.transform.parent = gameObject.transform;
        questManager.transform.localPosition = Position;
        questManager.transform.localRotation = Quaternion.Euler(Rotation);
        questManager.transform.localScale = Scale;
    }
}
