using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using System;

[Serializable]
public class SubmitEvent : UnityEvent<string>
{
}

public class FocusInputField : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _sample;

    [SerializeField]
    TextMeshProUGUI _input;

    [SerializeField]
    public SubmitEvent OnSubmit;

    private string _submittedString = "";


    private void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (_submittedString.Length != 0)
                {
                    _submittedString = _submittedString.Substring(0, _submittedString.Length - 1);
                    UpdateText();
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                OnSubmit.Invoke(_submittedString);
                _submittedString = "";
                UpdateText();
            }

            else
            {
                _submittedString += c;
                UpdateText();
                _sample.text = "";
            }
        }

    }


    private void UpdateText()
    {
        _input.text = _submittedString;
    }




}