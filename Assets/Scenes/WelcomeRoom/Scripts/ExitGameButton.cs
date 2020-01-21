using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    [SerializeField] GameObject ExitHandle;
    [SerializeField] float ExitDegree = 60f;

    private void Start()
    {
        Application.Quit();
        print("On start Happen!!!");
    }

    void Update()
    {
        Application.Quit();
        print("On update Happen!!!");
        if (ExitHandle.transform.rotation.eulerAngles.x > ExitDegree)
        {
            Application.Quit();
            print("with if Happen!!!");
        }
    }
}
