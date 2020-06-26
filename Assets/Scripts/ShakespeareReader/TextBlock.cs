using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeakerName { Romeo, Juliet}

namespace ShakespeareReader
{
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
		}


		//Break a piece of text into separate lines, depending on the specified delimiter
		protected string[] BreakText(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			return a;
		}

		public void CombineWith(TextBlock t)
		{
			_fullText += t.FullText;

		}

		public void ReplaceLine(int lineNumber, string replacement)
		{
			_textLines[lineNumber] = replacement;
		}

		

	}
}
