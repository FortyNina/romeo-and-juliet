using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShakespeareReader
{
	public class ChorusBlock : TextBlock
	{



		public ChorusBlock(string txt)
		{
			_fullText = txt;
			_textId = FindTextID(txt);
			string stripped = StripID(txt);
			_textLines = BreakText(stripped);
			speaker = SpeakerName.Chorus;
			print(_textId);
		}


		private int FindTextID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			return int.Parse(a[0]);
		}

		
		

	}
}
