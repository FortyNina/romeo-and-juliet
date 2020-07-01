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
    private Color _romeoStageDirCol;

    [SerializeField]
    private Color _julietCol;

    [SerializeField]
    private Color _julietStageDirCol;

    [SerializeField]
    private Color _neutralStageDirCol;

    [Space(5)]
    [Header("Script Resources")]

    [SerializeField]
    private GameObject _linePrefab;

    [SerializeField]
    private GameObject _blankLinePrefab;

    [SerializeField]
    private GameObject _nameLinePrefab;

    [SerializeField]
    private GameObject _directionLinePrefab;

    [SerializeField]
    private GameObject _sceneLinePrefab;

    [SerializeField]
    private GameObject _contentParent;

	[SerializeField]
	private FocusInputField _inputField;

    [SerializeField]
    private AnswerRecorder _recorder;


    public void DisplayResponse(TextBlock block, string stageDirection)
    {
        if(block.speaker == SpeakerName.Juliet)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 0, 0, _julietCol, ConversationManager.JulietName, SpeakerName.Juliet, stageDirection, _julietStageDirCol));
            _recorder.WriteLine(block.FullText, ConversationManager.JulietName);
        }
        else if(block.speaker == SpeakerName.Romeo)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 2f, .5f, _romeoCol, ConversationManager.RomeoName, SpeakerName.Romeo, stageDirection, _romeoStageDirCol));
            _recorder.WriteLine(block.FullText, ConversationManager.RomeoName);

        }
    }

    public void DisplayStageDirection(string direction)
    {
        //Line break
        CreateLine("", _neutralStageDirCol, _blankLinePrefab);

        string dirHex = ColorUtility.ToHtmlStringRGBA(_neutralStageDirCol);
        CreateLine("<color=#" + dirHex + ">[<i>" + direction + "</i> ]</color>", _neutralStageDirCol, _directionLinePrefab);
    }

    public void DisplaySceneName(string sceneName)
    {
        CreateLine("", _neutralStageDirCol, _blankLinePrefab);
        string dirHex = ColorUtility.ToHtmlStringRGBA(_neutralStageDirCol);
        CreateLine("<color=#" + dirHex + ">" + sceneName + " </color>", _neutralStageDirCol, _sceneLinePrefab);


    }

    private IEnumerator DisplayEachLine(string[] lines, float initialWaitTime, float inbetweenWaitTime, Color col, string name, SpeakerName speaker, string stageDir, Color stageColor)
    {
        yield return new WaitForSeconds(initialWaitTime);

        string dirHex = ColorUtility.ToHtmlStringRGBA(stageColor);

        //Line break
        CreateLine("", col, _blankLinePrefab);

        //Put Name
        if(stageDir == "")
            CreateLine("<b>" + name.ToString().ToUpper() + "</b>", col, _nameLinePrefab);
        else
            CreateLine("<b>" + name.ToString().ToUpper() + "</b> <color=#"+dirHex+">[<i>" + stageDir + "</i> ]</color>", col, _nameLinePrefab);

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
