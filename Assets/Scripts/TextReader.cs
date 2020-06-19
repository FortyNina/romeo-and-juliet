using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ShakespeareReader
{
	public class TextReader : MonoBehaviour
	{
		[SerializeField]
		private TextAsset _romeoResponses;
		[SerializeField]
		private TextAsset _byeResponses;
		[SerializeField]
		private TextAsset _supResponses;
		[SerializeField]
		private TextAsset _indifferenceResponses;
		[SerializeField]
		private TextAsset _yesResponses;
		[SerializeField]
		private TextAsset _noResponses;
		[SerializeField]
		private TextAsset _whyResponses;
		[SerializeField]
		private TextAsset _greetingResponses;
		[SerializeField]
		private TextAsset _goodResponses;
		[SerializeField]
		private TextAsset _badResponses;

		private static Dictionary<int, RomeoBlock> _romeoBlocks = new Dictionary<int, RomeoBlock>();
		public static Dictionary<int, RomeoBlock> RomeoBlocks
		{
			get { return _romeoBlocks; }
		}

		private static string[] _byeLines;
		public static string[] ByeLines
		{
			get { return _byeLines; }
		}

		private static string[] _supLines;
		public static string[] SupLines
		{
			get { return _supLines; }
		}

		private static string[] _indifferenceLines;
		public static string[] IndifferenceLines
		{
			get { return _indifferenceLines; }
		}

		private static string[] _yesLines;
		public static string[] YesLines
		{
			get { return _yesLines; }
		}

		private static string[] _noLines;
		public static string[] NoLines
		{
			get { return _noLines; }
		}

		private static string[] _whyLines;
		public static string[] WhyLines
		{
			get { return _whyLines; }
		}

		private static string[] _greetingLines;
		public static string[] GreetingLines
		{
			get { return _greetingLines; }
		}

		private static string[] _goodLines;
		public static string[] GoodLines
		{
			get { return _goodLines; }
		}

		private static string[] _badLines;
		public static string[] BadLines
		{
			get { return _badLines; }
		}

		private void Awake()
		{
			ReadData();
		}


		private void ReadData()
		{

			//ROMEO DATA
			string[] chunks = _romeoResponses.text.Split('-');
			for (int i = 0; i < chunks.Length; i++)
			{
				RomeoBlock rb = new RomeoBlock(chunks[i]);
				RomeoBlocks.Add(rb.TextId, rb);
			}

			//BYE DATA
			_byeLines = _byeResponses.text.Split('+');

			//SUP DATA
			_supLines = _supResponses.text.Split('+');

			//NO DATA
			_noLines = _noResponses.text.Split('+');

			//YES DATA
			_yesLines = _yesResponses.text.Split('+');

			//INDIFFERENT DATA
			_indifferenceLines = _indifferenceResponses.text.Split('+');

			//WHY DATA
			_whyLines = _whyResponses.text.Split('+');

			//GREETING DATA
			_greetingLines = _greetingResponses.text.Split('+');

			//GOOD DATA
			_goodLines = _goodResponses.text.Split('+');

			//BAD DATA
			_badLines = _badResponses.text.Split('+');

		}
	}
}
