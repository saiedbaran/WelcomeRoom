using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveAtStart : MonoBehaviour
{
    void Start()
    {
        Invoke("DeactiveMethod", 0.1f);
    }

    void DeactiveMethod()
    {
        foreach (var gameObject in GameManager.Instance.DeactiveAtStart)
        {
            gameObject.SetActive(false);
        }
    }

}
