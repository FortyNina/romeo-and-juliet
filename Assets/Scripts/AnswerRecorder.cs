using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnswerRecorder : MonoBehaviour
{
    [SerializeField]
    private string _fileName;

    [SerializeField]
    private bool _recordAnswers;

    private Stream sr;

    private List<string> _fullRecordedText = new List<string>();

    private void Start()
    {
        if (_recordAnswers)
        {
            var sr = File.CreateText(_fileName);
            sr.Close();
        }
    }


    public void WriteLine(string s, string speakerName)
    {
        _fullRecordedText.Add(speakerName + ": " + s.Trim());
        _fullRecordedText.Add(" ");
        if (_recordAnswers)
        {
            var sr = new StreamWriter(_fileName);
            for(int i = 0;i < _fullRecordedText.Count; i++)
            {
                sr.WriteLine(_fullRecordedText[i]);

            }
            
            sr.Close();
        }
    }

   
}
