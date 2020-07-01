using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShakespeareReader
{
	public class JulietBlock : TextBlock
	{



		public JulietBlock(string txt, bool prerecorded)
		{
			
            if (prerecorded)
            {
				_fullText = txt;
				_textId = FindTextID(txt);
				string stripped = StripID(txt);
				_textLines = BreakText(stripped);
				speaker = SpeakerName.Juliet;
            }

            else
            {
				_fullText = txt;
				_textLines = BreakText(txt);
				speaker = SpeakerName.Juliet;
			}

		}


		private int FindTextID(string txt)
		{
			string[] a = txt.Split(_lineBreakChar);
			return int.Parse(a[0]);
		}

		
		

	}
}
