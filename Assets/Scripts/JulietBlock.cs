using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShakespeareReader
{
	public class JulietBlock : TextBlock
	{
		public JulietBlock(string txt)
		{
			_fullText = txt;
			_textLines = BreakText(txt);
			speaker = SpeakerName.Juliet;

		}
	}
}
