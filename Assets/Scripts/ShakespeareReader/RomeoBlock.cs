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

		

		private int _julietId = -1;
		public int JulietId
        {
            get { return _julietId; }
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
			_julietId = FindJulietID(txt);
			string stripped = StripID(txt);
			_textLines = BreakText(stripped);
			_lineOfImportance = FindLineOfImportance(_textLines);
			StripAsterisk();
			speaker = SpeakerName.Romeo;

		}

		private int FindTextID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			string[] b = a[0].Split('/');
			return int.Parse(b[0]);
		}

		private int FindJulietID(string txt)
        {
			string[] a = txt.Split(_lineBreakChar);
			string[] b = a[0].Split('/');
			return int.Parse(b[1]);
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
