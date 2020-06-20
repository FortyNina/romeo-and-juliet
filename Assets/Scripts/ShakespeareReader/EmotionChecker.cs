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



		public static string GetJulietStageDirection(string s)
		{
			string direction = "";

            if (s.Contains("!"))
            {
				if(Random.Range(0,10) < 9)
                {
                    if (TextChecker.CheckYesContains(s))
                    {
                        direction = "eagerly";
                        return direction;
                    }
                    direction = "energetically";
                    return direction;
                }
            }

            if (TextChecker.CheckIndifference(s))
            {
                if (Random.Range(0, 10) < 7)
                {
                    direction = "coldly";
                    return direction;
                }
            }



			return direction;
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
