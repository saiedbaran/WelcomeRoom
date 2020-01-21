using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVolume : MonoBehaviour
{
    [SerializeField] GameObject VolumeObject;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (VolumeObject.activeSelf)
        {
            //audioSource.volume = VolumeObject.GetComponent<VolumeAmount>().currentVolumeAmount();
            audioSource.volume = GameManager.Instance.VolumeAmount;
        }
    }
}
