using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface IIntruption
{
    void OnBeginIntrupption();
    void OnEndIntruption();
    bool CheckSuccess();
}

class IntrupptionManager : MonoBehaviour
{
    private static IntrupptionManager _instance;
    public static IntrupptionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<IntrupptionManager>();
            return _instance;
        }
    }

    [SerializeField] List<GameObject> Intrupptions = new List<GameObject>();
    public int SelectedIntrupptionElement = 0;
    public IIntruption currentIntruption;
    public float IntruptionDelay = 10.0f;

    private float BeginTime,TimeSinceBegin;

    private bool ActiveTimer;

    private void Update()
    {
        if (!ActiveTimer) { return; }
        TimeSinceBegin = Time.time - BeginTime;

        if (TimeSinceBegin > IntruptionDelay)
        {
            ActiveTimer = false;
            CallIntrupption();
        }
    }

    public void Activation()
    {
        BeginTime = Time.time;
        ActiveTimer = true;
    }

    public void CallIntrupption()
    {
        Intrupptions[SelectedIntrupptionElement].SetActive(true);
        Intrupptions[SelectedIntrupptionElement].GetComponent<IIntruption>().OnBeginIntrupption();
    }


}
