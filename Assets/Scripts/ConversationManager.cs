using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShakespeareReader;

public enum ConversationBlock { Introduction, FamilyStuff, JustChatting, FateOrFreeWill, InLove }


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

    private bool _julietFlagDirty = false;

    private Dictionary<ConversationBlock, int> _blockState = new Dictionary<ConversationBlock, int>();

    private void Start()
    {
        _currentRomeoResponse = GetRomeoBlock(1);
        ui.DisplayResponse(_currentRomeoResponse);

        _blockState.Add(ConversationBlock.Introduction, 0);
        _blockState.Add(ConversationBlock.FamilyStuff, 0);
        _blockState.Add(ConversationBlock.JustChatting, 0);
        _blockState.Add(ConversationBlock.FateOrFreeWill, 0);
		_blockState.Add(ConversationBlock.InLove, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_julietFlagDirty)
        {
            _julietFlagDirty = false;
            GetRomeoResponse(_currentJulietResponse.FullText);
        }
    }

    public void ReceiveJulietResponse(string s)
    {
        _currentJulietResponse = new JulietBlock(s);
        _julietFlagDirty = true;
        ui.DisplayResponse(_currentJulietResponse);
    }


    private void GetRomeoResponse(string s)
    {
        _currentRomeoResponse = GetRomeoBlock(1005); 

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
            else if(_blockState[state] == 1)
            {
                if (CheckCasualConversation(s) >= 0)
                {
                    _currentRomeoResponse = GetRomeoBlock(CheckCasualConversation(s));
                }
                else if(s.ToLower().Contains("romeo romeo") || s.ToLower().Contains("wherefore"))
                {
                    _currentRomeoResponse = GetRomeoBlock(101);
                    state = ConversationBlock.FamilyStuff;
                    _blockState[state] = 1;
                }
                else if (TextChecker.CheckSupContains(s))
                {
                    //plot out sup
                    if(_blockState[ConversationBlock.FamilyStuff] < 5) //TODO: CHANGE OUT 5
                    {
                        //go to family things
                        _currentRomeoResponse = GetRomeoBlock(100); //sad hours seem long
                        state = ConversationBlock.FamilyStuff;
                    }
                }
                else if (TextChecker.CheckGreetingContains(s))
                {
                    _currentRomeoResponse = GetRomeoBlock(3);

                }
              //  else if() asking about identity
                //family region
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
            else if(_blockState[state] == 3)
            {
                if(TextChecker.CheckYesContains(s) || TextChecker.CheckIndifferenceContains(s))
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
            else if(_blockState[state] == 4)
            {
				RomeoName = "Romeo " + s;
                _currentRomeoResponse = GetRomeoBlock(105);
				if (s.ToLower() == "capulet") //if the player input capulet
				{
					_currentRomeoResponse = GetRomeoBlock(150);
				}
                if(s.ToLower() == "montague")
				{
					_currentRomeoResponse = GetRomeoBlock(151);
					RomeoName = "Romeo";
				}
				_currentRomeoResponse.ReplaceLine(0, s + "?"); //new name? your kinsman will still kill me
				_blockState[state] = 5;
            }
            else if(_blockState[state] == 5)
			{
				_blockState[state] = 6;
				_currentRomeoResponse = GetRomeoBlock(106);
			}

            else if(_blockState[state] == 6)
			{
				if (TextChecker.CheckNo(s))
				{
					//no i am not a capulet
					_currentRomeoResponse = GetRomeoBlock(108);

					_blockState[state] = 7;
				}
				else if (TextChecker.CheckYes(s))
				{
					//yes, i am a capulet
					_currentRomeoResponse = GetRomeoBlock(107);
					state = ConversationBlock.JustChatting;
				}


			}
            else if(_blockState[state] == 7)
			{
				JulietName = "Juliet " + s;
				_currentRomeoResponse = GetRomeoBlock(109);
				_currentRomeoResponse.ReplaceLine(0, s + "?"); //new name for juliet?
				state = ConversationBlock.JustChatting;

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
                if(_blockState[ConversationBlock.FamilyStuff] < 5) //TODO CHANGE 5
                {
                    state = ConversationBlock.FamilyStuff;
                    _currentRomeoResponse = GetRomeoBlock(100);
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
            
        }
		//===========================================================================================================
		#endregion

		#region In Love? Blocks
		//IN LOVE SECTION=======================================================================================
		else if (state == ConversationBlock.InLove)
		{
            if(_blockState[state] == 0) //romeo has just asked if juliet loves him
			{

			}
		}
		//===========================================================================================================
		#endregion



		ui.DisplayResponse(_currentRomeoResponse);

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
            return 1006;
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

}
