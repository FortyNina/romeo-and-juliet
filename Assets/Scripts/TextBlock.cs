﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeakerName { Romeo, Juliet}

public class TextBlock : MonoBehaviour
{

    public SpeakerName speaker;

    protected static char _lineBreakChar = '+';


    protected string _fullText = "";
    public string FullText
    {
        get { return _fullText; }
        set { _fullText = value; }
    }


    protected string[] _textLines;
    public string[] TextLines
    {
        get { return _textLines; }
        set { _textLines = value; }
    }


    //Break a piece of text into separate lines, depending on the specified delimiter
    protected string[] BreakText(string txt)
    {
        string[] a = txt.Split(_lineBreakChar);
        return a;
    }

}