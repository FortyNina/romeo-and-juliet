using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Data for each block of text (a single response) from the legend, Romeo himself
/// </summary>

public class RomeoBlock 
{

    private char[] _removalChars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '+' };

    //Full, unparse, unbroken text
    private string _fullText = "";
    public string FullText
    {
        get { return _fullText; }
        set { _fullText = value; }
    }

    //Associated conversation ID
    private int _textId = -1;
    public int TextId
    {
        get { return _textId; }
    }

    //Each item in the list is one line of parsed text
    private string[] _textLines;


    public RomeoBlock()
    {
        _fullText = "";

    }

    public RomeoBlock(string txt)
    {
        _fullText = txt;
        _textLines = BreakText(txt);
    }


    private int FindTextID(string txt)
    {
        string[] a = txt.Split('+');
        return int.Parse(a[0]);
    }

    //Break a piece of text into separate lines, depending on the specified delimiter
    private string[] BreakText(string txt)
    {
        txt = txt.TrimStart(_removalChars);
        string[] a = txt.Split('+');
        return a;
    }



    
}
