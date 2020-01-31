using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QM_ScrollingColliders : MonoBehaviour
{
    [SerializeField] GameObject colliderPlaceHolder;
    public void SetTransform()
    {
        transform.position = colliderPlaceHolder.transform.position;
        transform.rotation = colliderPlaceHolder.transform.rotation;
    }
}
