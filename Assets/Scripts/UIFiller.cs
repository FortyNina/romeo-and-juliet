using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ShakespeareReader;

public class UIFiller : MonoBehaviour
{
    [Header("Text Colors")]
    [SerializeField]
    private Color _romeoCol;

    [SerializeField]
    private Color _julietCol;

    [Space(5)]
    [Header("Script Resources")]

    [SerializeField]
    private GameObject _linePrefab;

    [SerializeField]
    private GameObject _blankLinePrefab;

    [SerializeField]
    private GameObject _nameLinePrefab;

    [SerializeField]
    private GameObject _contentParent;

	[SerializeField]
	private FocusInputField _inputField;

    [SerializeField]
    private AnswerRecorder _recorder;


    public void DisplayResponse(TextBlock block)
    {
        if(block.speaker == SpeakerName.Juliet)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 0, 0, _julietCol, ConversationManager.JulietName, SpeakerName.Juliet));
            _recorder.WriteLine(block.FullText, ConversationManager.JulietName);
        }
        else if(block.speaker == SpeakerName.Romeo)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 2f, .5f, _romeoCol, ConversationManager.RomeoName, SpeakerName.Romeo));
            _recorder.WriteLine(block.FullText, ConversationManager.RomeoName);

        }
    }

    private IEnumerator DisplayEachLine(string[] lines, float initialWaitTime, float inbetweenWaitTime, Color col, string name, SpeakerName speaker)
    {
        yield return new WaitForSeconds(initialWaitTime);

        //Line break
        CreateLine("", col, _blankLinePrefab);

        //Put Name
        CreateLine(name.ToString().ToUpper(), col, _nameLinePrefab);

        for (int i = 0; i < lines.Length; i++)
        {
            CreateLine(lines[i].Trim(), col, _linePrefab);
            yield return new WaitForSeconds(inbetweenWaitTime);

        }
        if(speaker == SpeakerName.Romeo)
		{
			_inputField.EnableTexting();
		}

    }


    private void CreateLine(string txt, Color col, GameObject lineType)
    {
        GameObject g = Instantiate(lineType, Vector3.zero, Quaternion.identity);
        g.transform.parent = _contentParent.transform;
        TextMeshProUGUI gui = g.GetComponent<TextMeshProUGUI>();
        gui.transform.localScale = new Vector3(1, 1, 1);
        gui.text = txt;
        gui.color = col;
    }




}
