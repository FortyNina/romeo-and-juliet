using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShakespeareReader
{
	public class EmotionChecker : MonoBehaviour
	{
		private static int _enthusiasmLevel = 10;
		public int EnthusiamLevel
        {
            get { return _enthusiasmLevel; }
        }

		private static int _indifferenceLevel = 10;
		public int IndifferenceLevel
		{
			get { return _enthusiasmLevel; }
		}



		public static string GetJulietStageDirection(string s, float timeLapse)
		{

            if(timeLapse < 5)
            {
                if (Random.Range(0, 5) < 7)
                {
                    if (Random.Range(0, 10) < 5)
                    {
                        return "hurriedly";
                    }
                    return "without hesitation";
                }
            }

            if (s.Contains("!"))
            {
				if(Random.Range(0,10) < 9)
                {
                    if (TextChecker.CheckYesContains(s))
                    {
                        return "eagerly";
                    }
                    return "energetically";
                }
            }

            if (TextChecker.CheckIndifference(s))
            {
                if (Random.Range(0, 10) < 7)
                {
                    int rand = Random.Range(0, 5);
                    if (rand == 0)
                        return "indifferently";
                    else if (rand == 1)
                        return "callously";
                    else if (rand == 2)
                        return "annoyed";
                    else
                        return "coldly";

                }
            }



			return "";
		}

		public static void UpdateJulietEmotion(string s)
        {
            if (TextChecker.CheckIndifference(s))
            {
				_enthusiasmLevel--;
				_indifferenceLevel++;

            }
            if (s.Contains("!"))
            {
				_enthusiasmLevel++;
				_indifferenceLevel--;
            }
            if (TextChecker.CheckYesContains(s))
            {
				_enthusiasmLevel++;
            }
            if (TextChecker.CheckNo(s))
            {
				_indifferenceLevel++;
            }
        }



		

	}
}
