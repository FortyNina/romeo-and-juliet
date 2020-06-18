using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Data for each block of text (a single response) from the legend, Romeo himself
/// </summary>
///
namespace ShakespeareReader
{

	public class RomeoBlock : TextBlock
	{
		private char[] _removalChars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', _lineBreakChar };

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
			_fullText = txt;
			_textId = FindTextID(txt);
			string stripped = StripID(txt);
			_textLines = BreakText(stripped);
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

	}
}
