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
        string lower = s.ToLower();
        lower = lower.Trim();
        
        return false;
    }

}
