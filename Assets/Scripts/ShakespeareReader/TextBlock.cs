using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeakerName { Romeo, Juliet, Chorus}

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


		//Associated conversation ID
		protected int _textId = -1;
		public int TextId
		{
			get { return _textId; }
		}


		//Break a piece of text into separate lines, depending on the specified delimiter
		protected string[] BreakText(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			return a;
		}

		protected string StripID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			int index = a[0].Length + 1;
			return txt.Substring(index);
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
