using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_ActivateActionSetOnLoad))]
public class IntrupHaptic : MonoBehaviour, IIntruption
{
    public float Duration, Frequency, Amplitude;

    public SteamVR_Action_Vibration HapticAction;
    public SteamVR_Action_Boolean TrackPadAction;

    private bool _isDone = false;


    void Update()
    {
        //Pulse(1, 150, 75, SteamVR_Input_Sources.LeftHand);
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources Source)
    {
        HapticAction.Execute(0, duration, frequency, amplitude, Source);
    }

    public void OnBeginIntrupption()
    {
        Pulse(Duration, Frequency, Amplitude, SteamVR_Input_Sources.RightHand);
    }

    public void OnEndIntruption()
    {
        _isDone = true;
    }

    public bool CheckSuccess()
    {
        return false;
    }

    public bool isDone()
    {
        return _isDone;
    }
}

