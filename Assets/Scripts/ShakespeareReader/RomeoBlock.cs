using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;


namespace ShakespeareReader
{
	
	public class RomeoBlock : TextBlock
	{

		private string _pattern = @"[\*]";
		private Regex _rgx;

		private string _lineOfImportance = "";
		public string LineOfImportance
        {
            get { return _lineOfImportance; }
			set { _lineOfImportance = value; }
        }

		//Associated conversation ID
		private int _textId = -1;
		public int TextId
		{
			get { return _textId; }
		}

		public RomeoBlock()
		{
			_fullText = "";
			speaker = SpeakerName.Romeo;
		}

		public RomeoBlock(string txt)
		{
			_rgx = new Regex(_pattern);
			_fullText = txt;
			_textId = FindTextID(txt);
			string stripped = StripID(txt);
			_textLines = BreakText(stripped);
			_lineOfImportance = FindLineOfImportance(_textLines);
			StripAsterisk();
			speaker = SpeakerName.Romeo;

		}

		private int FindTextID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			return int.Parse(a[0]);
		}

		private string StripID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			int index = a[0].Length + 1;
			return txt.Substring(index);
		}

		private string FindLineOfImportance(string[] textLines)
        {
			for(int i = 0;i < textLines.Length; i++)
            {
				if (textLines[i].Contains("*"))
					return _rgx.Replace(textLines[i], "");
            }
			return "";
        }

		private void StripAsterisk()
        {
			for (int i = 0; i < _textLines.Length; i++)
			{
				if (_textLines[i].Contains("*"))
					_textLines[i] =  _rgx.Replace(_textLines[i], "");
			}

		}

		




	}
}
