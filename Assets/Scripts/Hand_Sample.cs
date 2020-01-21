using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand_Sample : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_FixedJoint = null;

    private Interactible m_CurrentInteractable = null;
    public List<Interactible> m_ContactInteractable = new List<Interactible>();
    public Rigidbody TargetBody;
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_FixedJoint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // Down
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + "Trigger Down!!!");
            PickUp();
        }

        // Up
        if(m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + "Trigger Down!!!");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(!collider.gameObject.CompareTag("Grabable"))
        {
            return;
        }
        else
        {
            m_ContactInteractable.Add(collider.gameObject.GetComponent<Interactible>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(!collider.gameObject.CompareTag("Grabable"))
        {
            return;
        }
        else
        {
            m_ContactInteractable.Remove(collider.gameObject.GetComponent<Interactible>());
        } 
    }

    public void PickUp()
    {
        // Get Nearest interactable Object
        m_CurrentInteractable = GetNearestInteractable();

        // Check for Null
        if (!m_CurrentInteractable) {return;}

        // check if already something attached
        if (m_CurrentInteractable.m_ActiveHand) {m_CurrentInteractable.m_ActiveHand.Drop();}

        // positioning 
        m_CurrentInteractable.transform.position = transform.position;

        // attaching object (fixedjoint)
        TargetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_FixedJoint.connectedBody = TargetBody;
        // set Active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {
        // null check
        if (!m_CurrentInteractable) {return;}

        // Drop Velocity
        TargetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        TargetBody.velocity = m_Pose.GetVelocity();
        TargetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // Detach fixedjoint
        m_FixedJoint.connectedBody = null;

        // Remove
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;

    }

    private Interactible GetNearestInteractable()
    {
        Interactible neareast = null;
        float minDistance = float.MaxValue;
        float Distance = 0.0f;

        foreach (Interactible interactible in m_ContactInteractable)
        {
            Distance = (interactible.transform.position - transform.position).sqrMagnitude;
            if (Distance < minDistance)
            {
                minDistance = Distance;
                neareast = interactible;
            }
        }
        return neareast;
    }
}
