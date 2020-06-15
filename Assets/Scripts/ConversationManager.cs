using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConversationBlock { Introduction, FamilyStuff, JustChatting }


public class ConversationManager : MonoBehaviour
{

    public static ConversationBlock state = ConversationBlock.Introduction;

    [SerializeField]
    private UIFiller ui;


    private JulietBlock _currentJulietResponse;
    private RomeoBlock _currentRomeoResponse;
    private bool _julietFlagDirty = false;

    private Dictionary<ConversationBlock, int> _blockState = new Dictionary<ConversationBlock, int>();

    private void Start()
    {
        _currentRomeoResponse = GetRomeoBlock(1);
        ui.DisplayResponse(_currentRomeoResponse);

        _blockState.Add(ConversationBlock.Introduction, 0);
        _blockState.Add(ConversationBlock.FamilyStuff, 0);
        _blockState.Add(ConversationBlock.JustChatting, 0);
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
        #region Introduction Blocks
        //INTRODUCTION SECTION=======================================================================================
        if (state == ConversationBlock.Introduction)
        {
            if (_blockState[state] == 0)
            {
                _currentRomeoResponse = GetRomeoBlock(2);
                _blockState[state] = 1;
            }
            //Introduction pool
            else if(_blockState[state] == 1)
            {
                if (TextChecker.CheckRosaline(s))
                {
                    _currentRomeoResponse = GetRomeoBlock(1000);
                }
                else if (TextChecker.CheckBye(s))
                {
                    _currentRomeoResponse = GetRomeoBlock(1001);
                }
                else if (TextChecker.CheckSup(s))
                {
                    //plot out sup
                    if(_blockState[ConversationBlock.FamilyStuff] < 5) //TODO: CHANGE OUT 5
                    {
                        //go to family things

                    }


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
            if(_blockState[state] == 0)
            {
                
            }
        }
        //===========================================================================================================
        #endregion

        #region Just Chatting Blocks
        //CHATTING SECTION=======================================================================================
        else if (state == ConversationBlock.JustChatting)
        {
            if (_blockState[state] == 0)
        }
        //===========================================================================================================
        #endregion

        ui.DisplayResponse(_currentRomeoResponse);

    }


    private RomeoBlock GetRomeoBlock(int id)
    {
        return (TextReader.RomeoBlocks[id]);
    }


   



}
