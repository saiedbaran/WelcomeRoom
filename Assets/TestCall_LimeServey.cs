using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GEAR.LimeSurvey;
using UnityEditor;

public class TestCall_LimeServey : MonoBehaviour
{
    public TextAsset TextFile;
    public LimeSurveyUploader uploader;

    private List<int> _answers = new List<int>(){55,2,3,4,5,4,3,2,1,5};
        
    void Start()
    {
        Invoke("UploadText", 2f);
    }

    void UploadText()
    {
        AssetDatabase.Refresh();
        
        var tempText = TextFile.text;
        foreach (var record in _answers)
        {
            tempText += "\t" + record;
        }
        
        TextFile = new TextAsset(tempText);
        
        AssetDatabase.CreateAsset(TextFile, "Assets/MyText.txt");
        
        uploader.StartUpload(TextFile);
    }

}
