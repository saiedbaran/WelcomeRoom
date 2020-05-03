using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestionList))]
public class QuestionListEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Quest Manager"))
        {
            ((QuestionList)target).GenerateList();
        }
    }
}
