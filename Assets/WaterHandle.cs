using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHandle : MonoBehaviour
{
    public GameObject ColdWater;
    public GameObject WarmWater;
    public float BlueState;
    public float RedState;

    Material waterMaterial;
    Vector3 waterIniScale;
    
    void Start()
    {
        waterMaterial = gameObject.GetComponent<Renderer>().material;
        waterIniScale = transform.localScale;
    }

    void Update()
    {
        BlueState = Mathf.Abs(ColdWater.transform.localRotation.z / 90f) * 100;
        RedState = Mathf.Abs(WarmWater.transform.localRotation.z / 90f) * 100;

        waterMaterial.SetVector("_ColdLevel", new Vector3(BlueState, 0, 0));
        waterMaterial.SetVector("_WarmLevel", new Vector3(RedState, 0, 0));

        float _current = BlueState + RedState;
        if (_current > 1) { _current = 1f; }
        transform.localScale = new Vector3(_current * waterIniScale.x, _current * waterIniScale.y,waterIniScale.z);
    }
}
