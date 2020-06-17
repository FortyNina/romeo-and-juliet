using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    public void DisplayResponse(TextBlock block)
    {
        if(block.speaker == SpeakerName.Juliet)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 0, 0, _julietCol, SpeakerName.Juliet.ToString()));
        }
        else if(block.speaker == SpeakerName.Romeo)
        {
            StartCoroutine(DisplayEachLine(block.TextLines, 2f, .5f, _romeoCol, ConversationManager.RomeoName));
        }
    }

    private IEnumerator DisplayEachLine(string[] lines, float initialWaitTime, float inbetweenWaitTime, Color col, string speaker)
    {
        yield return new WaitForSeconds(initialWaitTime);

        //Line break
        CreateLine("", col, _blankLinePrefab);

        //Put Name
        CreateLine(speaker.ToString().ToUpper(), col, _nameLinePrefab);

        for (int i = 0; i < lines.Length; i++)
        {
            CreateLine(lines[i].Trim(), col, _linePrefab);
            yield return new WaitForSeconds(inbetweenWaitTime);

        }
    }


    private void CreateLine(string txt, Color col, GameObject lineType)
    {
        GameObject g = Instantiate(lineType, Vector3.zero, Quaternion.identity);
        g.transform.parent = _contentParent.transform;
        TextMeshProUGUI gui = g.GetComponent<TextMeshProUGUI>();
        gui.text = txt;
        gui.color = col;
    }




}
