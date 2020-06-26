using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShakespeareReader;

public enum ConversationBlock { Introduction, FamilyStuff, JustChatting, FateOrFreeWill, InLove, YourEmotion }


public class ConversationManager : MonoBehaviour
{

    public static ConversationBlock state = ConversationBlock.Introduction;

    public static string RomeoName = "Romeo";
	public static string JulietName = "Juliet";


    [SerializeField]
    private UIFiller ui;


    private JulietBlock _currentJulietResponse;
    private RomeoBlock _currentRomeoResponse;
    private RomeoBlock _currentImportantRomeoResponse;
    private ConversationBlock _currentImportantState;
	private string _currentRomeoStageDirection = "";
	private string _currentLineOfImportance = "";

	private int _slipups = 0;

    private bool _julietFlagDirty = false;

    private Dictionary<ConversationBlock, int> _blockState = new Dictionary<ConversationBlock, int>();

	private float _lapsedTime = 0;
	private bool _pauseLineCreated = false;


    private void Start()
    {
        _currentRomeoResponse = GetRomeoBlock(1);
		ui.DisplayStageDirection("Enter Romeo, Juliet.");
        ui.DisplayResponse(_currentRomeoResponse, _currentRomeoStageDirection);

        _blockState.Add(ConversationBlock.Introduction, 0);
        _blockState.Add(ConversationBlock.FamilyStuff, 0);
        _blockState.Add(ConversationBlock.JustChatting, 0);
        _blockState.Add(ConversationBlock.FateOrFreeWill, 0);
		_blockState.Add(ConversationBlock.InLove, 0);
		_blockState.Add(ConversationBlock.YourEmotion, 0);
		
    }

    // Update is called once per frame
    void Update()
    {
        if (_julietFlagDirty)
        {
            _julietFlagDirty = false;
            GetRomeoResponse(_currentJulietResponse.FullText);
        }

		_lapsedTime += Time.deltaTime;

		if(!_pauseLineCreated && _lapsedTime > 20)
        {
			_pauseLineCreated = true;
			ui.DisplayStageDirection("A pause.");
		}
	}

	public void ReceiveJulietResponse(string s)
	{
		_currentJulietResponse = new JulietBlock(s);
		_julietFlagDirty = true;
		EmotionChecker.UpdateJulietEmotion(s);
		if(s == "")
			ui.DisplayStageDirection("Silence.");
		else
			ui.DisplayResponse(_currentJulietResponse, EmotionChecker.GetJulietStageDirection(s, _lapsedTime));
		_lapsedTime = 0;
		_pauseLineCreated = false;
    }


    private void GetRomeoResponse(string s)
    {
        _currentRomeoResponse = GetRomeoBlock(1005);
		_currentRomeoStageDirection = "";

		#region Introduction Blocks
		//INTRODUCTION SECTION=======================================================================================
		if (state == ConversationBlock.Introduction)
		{
			if (_blockState[state] == 0)
			{
				_currentRomeoResponse = GetRomeoBlock(2); //speak again bright angel
				_blockState[state] = 1;
			}


			//Introduction pool
			else if (_blockState[state] == 1)
			{
				if (CheckCasualConversation(s) >= 0)
				{
					_currentRomeoResponse = GetRomeoBlock(CheckCasualConversation(s));
				}
				else if (s.ToLower().Contains("romeo romeo") || s.ToLower().Contains("wherefore"))
				{
					_currentRomeoResponse = GetRomeoBlock(101);
					state = ConversationBlock.FamilyStuff;
					_blockState[state] = 1;
				}
				else if (TextChecker.CheckSupContains(s))
				{
					//plot out sup
					if (_blockState[ConversationBlock.FamilyStuff] < 5) //TODO: CHANGE OUT 5
					{
						//go to family things
						_currentRomeoResponse = GetRomeoBlock(100); //sad hours seem long
						_currentRomeoStageDirection = "somberly";
						state = ConversationBlock.FamilyStuff;
					}
				}
				else if (TextChecker.CheckGreetingContains(s))
				{
					_currentRomeoResponse = GetRomeoBlock(3);
					_blockState[state] = 2;

				}
				else if (TextChecker.CheckIndifference(s) || TextChecker.CheckNoContains(s) || s.ToLower().Contains("romeo"))
                {
					_currentRomeoResponse = GetRomeoBlock(400);
					state = ConversationBlock.YourEmotion;
					_blockState[state] = 0;
				}
				//  else if() asking about identity
				//family region
			}
			else if(_blockState[state] == 2)
            {
				//go to family things
				_currentRomeoResponse = GetRomeoBlock(100); //sad hours seem long
				_currentRomeoStageDirection = "somberly";
				state = ConversationBlock.FamilyStuff;
			}
		}
		//===========================================================================================================
		#endregion

		#region Family Stuff Blocks
		//FAMILY SECTION=======================================================================================
		else if (state == ConversationBlock.FamilyStuff)
		{
			if (_blockState[state] == 0)
			{
				if (CheckCasualConversation(s) >= 0)
				{
					_currentRomeoResponse = GetRomeoBlock(CheckCasualConversation(s));
				}
				else if (TextChecker.CheckWhyContains(s) || TextChecker.CheckIndifferenceContains(s))
				{ //why do sad hours seem long?
					_currentRomeoResponse = GetRomeoBlock(101);
					_blockState[state] = 1;
				}

			}
			else if (_blockState[state] == 1)
			{
				if (CheckCasualConversation(s) >= 0)
				{
					_currentRomeoResponse = GetRomeoBlock(CheckCasualConversation(s)); //i know not how to tell thee who i am
				}

				else if (TextChecker.CheckWhyContains(s) || TextChecker.CheckIndifferenceContains(s) || s.ToLower().Contains("romeo") || s.ToLower().Contains("montague"))
				{
					_currentRomeoResponse = GetRomeoBlock(102); // it is an enemy to thee, shall i tell thee?
					_blockState[state] = 2;
				}

			}
			else if (_blockState[state] == 2)
			{
				if (TextChecker.CheckWhyContains(s) || TextChecker.CheckIndifferenceContains(s) || s.ToLower().Contains("tear") || s.ToLower().Contains("romeo") || s.ToLower().Contains("montague"))
				{
					_currentRomeoResponse = GetRomeoBlock(103); //montague

					_blockState[state] = 3;
				}
			}
			else if (_blockState[state] == 3)
			{
				if (TextChecker.CheckYesContains(s) || TextChecker.CheckIndifferenceContains(s))
				{
					_currentRomeoResponse = GetRomeoBlock(104); //what should i change my name to?
					_blockState[state] = 4;
				}
				else if (TextChecker.CheckNoContains(s))
				{
					_currentRomeoResponse = GetRomeoBlock(175);
					_blockState[state] = 6;
				}
			}
			else if (_blockState[state] == 4)
			{
				RomeoName = "Romeo " + s;
				_currentRomeoResponse = GetRomeoBlock(105);
				if (s.ToLower() == "capulet") //if the player input capulet
				{
					_currentRomeoResponse = GetRomeoBlock(150);
				}
				if (s.ToLower() == "montague")
				{
					_currentRomeoResponse = GetRomeoBlock(151);
					RomeoName = "Romeo";
				}
				_currentRomeoResponse.ReplaceLine(0, s + "?"); //new name? your kinsman will still kill me
				_blockState[state] = 5;
			}
			else if (_blockState[state] == 5)
			{
				_blockState[state] = 6;
				_currentRomeoResponse = GetRomeoBlock(106);
			}

			else if (_blockState[state] == 6)
			{
				if (TextChecker.CheckNoContains(s))
				{
					//no i am not a capulet
					_currentRomeoResponse = GetRomeoBlock(108);
					_currentRomeoStageDirection = "relieved";
					_blockState[state] = 7;
				}
				else if (TextChecker.CheckYesContains(s))
				{
					//yes, i am a capulet
					_currentRomeoResponse = GetRomeoBlock(107);
					state = ConversationBlock.JustChatting;
					_blockState[ConversationBlock.JustChatting] = 0;
				}


			}
			else if (_blockState[state] == 7)
			{
				JulietName = "Juliet " + s;
				_currentRomeoResponse = GetRomeoBlock(109);
				_currentRomeoResponse.ReplaceLine(0, s + "?"); //new name for juliet?
				state = ConversationBlock.JustChatting;
				_blockState[ConversationBlock.JustChatting] = 0;

			}
		}
		//===========================================================================================================
		#endregion

		#region Fate of Free Will Blocks
		//FATE OR FREE WILL SECTION=======================================================================================
		else if (state == ConversationBlock.FateOrFreeWill)
		{
			if (_blockState[state] == 0)
			{
				if (TextChecker.CheckYesContains(s))
				{
					_currentRomeoResponse = GetRomeoBlock(302);

					if (_blockState[ConversationBlock.FamilyStuff] < 5)
					{
						state = ConversationBlock.Introduction;
					}
					else
					{
						_blockState[ConversationBlock.JustChatting] = 0;
						state = ConversationBlock.JustChatting;
					}

				}
				else if (TextChecker.CheckNoContains(s))
				{
					_currentRomeoResponse = GetRomeoBlock(301);
					if (_blockState[ConversationBlock.FamilyStuff] < 5)
					{
						state = ConversationBlock.Introduction;
					}
					else
					{
						_blockState[ConversationBlock.JustChatting] = 0;
						state = ConversationBlock.JustChatting;
					}
				}

			}
		}
		//===========================================================================================================
		#endregion

		#region Chatting Blocks
		//CHATTING SECTION=======================================================================================
		else if (state == ConversationBlock.JustChatting)
		{
			if (CheckCasualConversation(s) >= 0)
			{
				_currentRomeoResponse = GetRomeoBlock(CheckCasualConversation(s));
			}
			else if (TextChecker.CheckGreetingContains(s))
			{
				_currentRomeoResponse = GetRomeoBlock(GetRandomCompliment());
			}
			else
			{
				if (_blockState[ConversationBlock.FamilyStuff] < 5) //TODO: CHANGE OUT 5
				{
					//go to family things
					_currentRomeoResponse = GetRomeoBlock(100); //sad hours seem long
					_currentRomeoStageDirection = "somberly";
					state = ConversationBlock.FamilyStuff;
				}
				else if (_blockState[ConversationBlock.YourEmotion] < 2)
				{
					_currentRomeoResponse = GetRomeoBlock(400);
					state = ConversationBlock.YourEmotion;
					_blockState[state] = 0;
				}
                else
                {
					_currentRomeoResponse = GetRomeoBlock(1008);

				}
			}



		}
		//===========================================================================================================
		#endregion

		#region In Love? Blocks
		//IN LOVE SECTION=======================================================================================
		else if (state == ConversationBlock.InLove)
		{
			if (_blockState[state] == 0) //romeo has just asked if juliet loves him
			{
			}
		}
		//===========================================================================================================
		#endregion


		#region Your Emotions Blocks
		//IN LOVE SECTION=======================================================================================
		else if (state == ConversationBlock.YourEmotion)
		{
			if (_blockState[state] == 0) //romeo has just asked how juliet is doing
			{
				if (TextChecker.CheckBadContains(s))
				{
					if (TextChecker.CheckBad(s))
					{
						_blockState[state] = 1;
						_currentRomeoResponse = GetRomeoBlock(401);
						_currentRomeoStageDirection = "concerned";
						_currentRomeoResponse.ReplaceLine(0, "Then I too am " + s); //shall i go on?
					}
					else
					{
						_blockState[state] = 1;
						_currentRomeoResponse = GetRomeoBlock(402);
					}
				}
                else if (TextChecker.CheckGoodContains(s))
				{
					_blockState[state] = 3;
					_currentRomeoResponse = GetRomeoBlock(405);
				}
			}
            else if(_blockState[state] == 1)
			{
				if (TextChecker.CheckYesContains(s))
				{
					_blockState[state] = 2;
					_currentRomeoResponse = GetRomeoBlock(403); //do u feel better?
				}

                else if (TextChecker.CheckNoContains(s))
				{
					_blockState[state] = 3;
					_currentRomeoResponse = GetRomeoBlock(405); //do u feel better?
				}
			}

            else if(_blockState[state] == 2) //do u feel better?
			{
				if (TextChecker.CheckYesContains(s))
				{
					_blockState[state] = 3;
					_currentRomeoResponse = GetRomeoBlock(405); 
				}

				else if (TextChecker.CheckNoContains(s))
				{
					_blockState[state] = 3;
					_currentRomeoResponse = GetRomeoBlock(1008);
				}

			}
            else if(_blockState[state] == 3)
			{
                if (TextChecker.CheckNoContains(s))
                {
					_currentRomeoResponse = GetRomeoBlock(406);
					_currentRomeoStageDirection = "devastated";
					state = ConversationBlock.JustChatting;
					_blockState[ConversationBlock.JustChatting] = 0;

				}
				else if (TextChecker.CheckYesContains(s))
                {
					_currentRomeoResponse = GetRomeoBlock(404);
					state = ConversationBlock.JustChatting;
					_blockState[ConversationBlock.JustChatting] = 0;
				}
			}
			
		}
		//===========================================================================================================
		#endregion

		_slipups++;
		if (_currentRomeoResponse.LineOfImportance != "")
		{
			_currentLineOfImportance = _currentRomeoResponse.LineOfImportance;
			_slipups = 0;
		}

		if(_slipups > 1)
        {
			Debug.Log(_currentRomeoResponse.FullText);
			_currentRomeoResponse = GetHelperRomeoBlock(_currentRomeoResponse.FullText);
			
        }

		ui.DisplayResponse(_currentRomeoResponse, _currentRomeoStageDirection);

    }


    private RomeoBlock GetRomeoBlock(int id)
    {
        return (TextReader.RomeoBlocks[id]);
    }

    private int CheckCasualConversation(string s)
    {
        if (TextChecker.CheckRosaline(s))
        {
            return 1000;
        }
        else if (TextChecker.CheckBye(s))
        {
            return 1007;
        }
        else if(TextChecker.CheckRobot(s))
        {
            state = ConversationBlock.FateOrFreeWill;
            return 300;
        }
        return -1;
    }

    private int GetRandomCompliment()
	{
		if (Random.Range(0, 1) == 0){
			return 2000;
		}
		return 2000;
	}

	private RomeoBlock GetHelperRomeoBlock(string fullText)
    {
		RomeoBlock rb = new RomeoBlock(fullText + "+" + _currentLineOfImportance);
		return rb;
    }

}
