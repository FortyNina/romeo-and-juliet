using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChecker : MonoBehaviour
{
    public static bool CheckRosaline(string s)
    {
        string lower = s.ToLower();
        if (lower.Contains("rosaline") || lower.Contains("rosalin") || lower.Contains("rosalina"))
        {
            return true;
        }
        return false;
    }

    public static bool CheckBye(string s)
    {
        return CheckAgainstAText(s, TextReader.ByeLines);
    }

    public static bool CheckSup(string s)
    {
        return CheckAgainstAText(s, TextReader.SupLines);
    }

    public static bool CheckIndifference(string s)
    {
        if (s == "")
            return true;
        return CheckAgainstAText(s, TextReader.IndifferenceLines);
    }

    public static bool CheckYes(string s)
    {
        return CheckAgainstAText(s, TextReader.YesLines);
    }

    public static bool CheckNo(string s)
    {
        return CheckAgainstAText(s, TextReader.NoLines);
    }

    private static bool CheckAgainstAText(string s, string[] list)
    {
        string lower = s.ToLower();
        lower = lower.Trim();
        lower.Replace("’", "");
        lower.Replace("!", "");
        lower.Replace("?", "");
        lower.Replace(".", "");
        lower.Replace(",", "");


        for (int i = 0; i < list.Length; i++)
        {
            if (lower == list[i].Trim())
            {
                return true;
            }
        }
        return false;
    }

}
