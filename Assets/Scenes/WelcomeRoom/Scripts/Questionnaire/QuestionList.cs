using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class QuestionList : MonoBehaviour
{
    public List<Question> Questions = new List<Question>();
    public TextAsset QuestionsXMLFile;
    public int ItemInList;

    public void GenerateList()
    {
        Questions.Clear();

        var doc = XDocument.Load(new MemoryStream(QuestionsXMLFile.bytes));

        foreach (var qItem in doc.Descendants("Question"))
        {
            var question = new Question();

            question.QuestionText = qItem.Element("DefaultText")?.Value ?? "";
            question.MinText = qItem.Element("MinRange")?.Value ?? "";
            question.MaxText = qItem.Element("MaxRange")?.Value ?? "";

            Questions.Add(question);
        }

        ItemInList = Questions.Count;
    }

}
