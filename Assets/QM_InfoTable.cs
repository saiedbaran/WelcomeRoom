using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QM_InfoTable : MonoBehaviour
{
    public string Text;
    public GameObject Picture;
    public GameObject TextObject;
    public Material material;
    private void OnEnable()
    {
        SetParameters();
    }

    public void SetParameters()
    {
        TextObject.GetComponent<TextMeshPro>().text = Text;
        Picture.GetComponent<MeshRenderer>().material = material;
    }
}
