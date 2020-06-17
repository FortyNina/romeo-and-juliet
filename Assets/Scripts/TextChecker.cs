using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChecker : MonoBehaviour
{

    //ROSALINE========================================================================================
    public static bool CheckRosaline(string s)
    {
        string lower = s.ToLower();
        if (lower.Contains("rosaline") || lower.Contains("rosalin") || lower.Contains("rosalina"))
        {
            return true;
        }
        return false;
    }

    //ROBOTS==========================================================================================
    public static bool CheckRobot(string s)
    {
        string lower = s.ToLower();
        if(lower.Contains("robot") || lower.Contains(" bot ") || lower.Contains("human"))
        {
            return true;
        }
        return false;
    }

    //GOOD BYES========================================================================================
    public static bool CheckBye(string s)
    {
        return CheckAgainstAText(s, TextReader.ByeLines);
    }
    public static bool CheckByeContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.ByeLines);
    }

    //WHAT'S UP========================================================================================
    public static bool CheckSup(string s)
    {
        return CheckAgainstAText(s, TextReader.SupLines);
    }
    public static bool CheckSupContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.SupLines);
    }

    //INDIFFERENCE========================================================================================
    public static bool CheckIndifference(string s)
    {
        if (s == "")
            return true;
        return CheckAgainstAText(s, TextReader.IndifferenceLines);
    }
    public static bool CheckIndifferenceContains(string s)
    {
        if (s == "")
            return true;
        return CheckAgainstATextContains(s, TextReader.IndifferenceLines);
    }

    //YES/CONFIRMATIONS========================================================================================
    public static bool CheckYes(string s)
    {
        return CheckAgainstAText(s, TextReader.YesLines);
    }
    public static bool CheckYesContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.YesLines);
    }

    //NOs========================================================================================
    public static bool CheckNo(string s)
    {
        return CheckAgainstAText(s, TextReader.NoLines);
    }
    public static bool CheckNoContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.NoLines);
    }

    //WHYs========================================================================================
    public static bool CheckWhy(string s)
    {
        return CheckAgainstAText(s, TextReader.WhyLines);
    }
    public static bool CheckWhyContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.WhyLines);
    }

    //GREETINGS========================================================================================
    public static bool CheckGreeting(string s)
    {
        return CheckAgainstAText(s, TextReader.GreetingLines);
    }
    public static bool CheckGreetingContains(string s)
    {
        return CheckAgainstATextContains(s, TextReader.GreetingLines);
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

    private static bool CheckAgainstATextContains(string s, string[] list)
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
            if (lower.Contains(list[i].Trim()))
            {
                return true;
            }
        }
        return false;
    }

}
