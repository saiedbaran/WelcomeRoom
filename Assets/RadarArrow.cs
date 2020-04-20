using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RadarArrow : MonoBehaviour
{
    [SerializeField] GameObject radarArrow;

    public GameObject lightIndicator;

    private bool _isSearching;
    private GameObject _target;

    void Update()
    {
        if(_isSearching && _target != null)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        var upVec = -1 * transform.right;
        var forVec = Vector3.ProjectOnPlane(_target.transform.position - radarArrow.transform.position, upVec);
        radarArrow.transform.rotation = Quaternion.LookRotation(forVec, upVec);
    }

    public void ActiveRadar(GameObject target)
    {
        Debug.Log("Active Radar");

        radarArrow.SetActive(true);
        _isSearching = true;
        _target = target;
    }

    public void DeactiveRadar()
    {
        radarArrow.SetActive(false);
        _isSearching = false;
    }
}
