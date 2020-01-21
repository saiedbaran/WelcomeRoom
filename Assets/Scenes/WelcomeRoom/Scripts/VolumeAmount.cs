using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAmount : MonoBehaviour
{
    [SerializeField] GameObject VolumeHandle;
    [SerializeField] float MaxScale;
    [SerializeField] float MinScale= 0.0f;
    Vector3 InitialScale;
    Vector3 InitialVolumeHandle;

    float CurrentScaleZ;
    // Start is called before the first frame update
    void Start()
    {
        InitialScale = transform.localScale;
        InitialVolumeHandle = VolumeHandle.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float currentRotation = VolumeHandle.transform.rotation.eulerAngles.y -InitialVolumeHandle.y;
        CurrentScaleZ = (InitialScale.z + (currentRotation)* 0.001f);
        if (CurrentScaleZ > MaxScale) { CurrentScaleZ = MaxScale;}
        if (CurrentScaleZ < MinScale) { CurrentScaleZ = MinScale;}
        transform.localScale = new Vector3(InitialScale.x, InitialScale.y, CurrentScaleZ);
        GameManager.Instance.VolumeAmount = currentVolumeAmount();
    }

    public float currentVolumeAmount()
    {
        float currentVolume = CurrentScaleZ/MaxScale;
        return currentVolume;
    }
}
