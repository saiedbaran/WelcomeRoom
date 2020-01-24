using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class WatchOpen : MonoBehaviour
{
    [SerializeField] GameObject RightDoor;
    [SerializeField] GameObject LeftDoor;
    [SerializeField] GameObject MenuDimond;
    [SerializeField] GameObject WatchRing;
    [SerializeField] List<GameObject> LoadingSegments;
    [SerializeField] float OpenTimeDelay = 1.0f;
    [SerializeField] float CloseTimeDelay = 30.0f;
    [SerializeField] float DimondOffset = 1.0f;

    [SerializeField] bool onOpeningTest = false; // Only for testing script

    GameObject menuDimond = null;
    Vector3 RightDoorOpen = new Vector3(-90, -90, 0);
    Vector3 RightDoorClose = new Vector3(-90, 0, 0);
    Vector3 LeftDoorOpen = new Vector3(-90, 90, 0);
    Vector3 LeftDoorClose = new Vector3(-90, 0, 0);

    float pastTime = 0.0f;
    float LastTimeHover = 0.0f;
    bool isOpening = false;
    bool isClosing = false;
    bool AlreadyOpend = false;
    // Start is called before the first frame update
    void Start()
    {
        //OpenDoor(); // Only for test
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - LastTimeHover > CloseTimeDelay && isClosing)
        {
            CloseDoor();
            //isOpening = false;
        }
    }

    private void OnHandHoverBegin(Hand hand)
    {
        if (AlreadyOpend) { CloseDoor(); }
    }

    private void OnHandHoverEnd(Hand hand)
    {
        if (isOpening)
        {
            OpenDoor();
            LastTimeHover = Time.time;
            isClosing = true;
        }
        LoadingMethodHide();
    }

    private void HandHoverUpdate(Hand hand)
    {
        TimerMethod();
    }

    private void CloseDoor()
    {
        pastTime = 0.0f;
        if (menuDimond) { Destroy(menuDimond); }
        if (GameManager.Instance.Inventory.GetComponentInChildren<QuestManagerInstance>()) { Destroy(GameManager.Instance.Inventory.GetComponentInChildren<QuestManagerInstance>().gameObject); }
        if (GameManager.Instance.Inventory.activeSelf) { GameManager.Instance.Inventory.SetActive(false); } // TODO Move it somewhere else, Now the inventory close after 60 second

        RightDoor.transform.localRotation = Quaternion.Euler(RightDoorClose);
        LeftDoor.transform.localRotation = Quaternion.Euler(LeftDoorClose);

        AlreadyOpend = false;
        isOpening = false;
        isClosing = false;

        GameManager.Instance.MenuSpace.SetActive(false);
    }

    public void OpenDoor()
    {
        RightDoor.transform.localRotation = Quaternion.Euler(RightDoorOpen);
        LeftDoor.transform.localRotation = Quaternion.Euler(LeftDoorOpen);
        AlreadyOpend = true;

        if (menuDimond != null) { Destroy(menuDimond); }
        if (menuDimond == null)
        {
            menuDimond = Instantiate(MenuDimond) as GameObject;
            menuDimond.transform.position = WatchRing.transform.position + WatchRing.transform.right * DimondOffset;
            menuDimond.SetActive(true);
        }

        GameManager.Instance.MenuSpace.SetActive(true);

    }

    private void TimerMethod()
    {
        pastTime += Time.deltaTime;
        LoadingMethodShow();

        if (pastTime > OpenTimeDelay)
        {
            isOpening = true;
            pastTime = 0.0f;
        }
    }

    private void LoadingMethodShow()
    {
        if (!AlreadyOpend)
        {
            //int SegmentID = 0;
            for (int i = 0; i < LoadingSegments.Count; i++)
            {
                //if pasttime > opentimedelay / number of segment * current segment id
                if (pastTime > OpenTimeDelay / LoadingSegments.Count * (i + 1))
                {
                    // show current segment
                    LoadingSegments[i].SetActive(true);
                }
            }
        }
    }

    private void LoadingMethodHide()
    {
        foreach (GameObject segment in LoadingSegments)
        {
            segment.SetActive(false);
        }
    }
}
