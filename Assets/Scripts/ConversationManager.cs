using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConversationState { Introduction }


public class ConversationManager : MonoBehaviour
{

    public static ConversationState state = ConversationState.Introduction;

    [SerializeField]
    private UIFiller ui;


    private JulietBlock _currentJulietResponse;
    private RomeoBlock _currentRomeoResponse;
    private bool _julietFlagDirty = false;


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

        if(state == ConversationState.Introduction)
        {
            _currentRomeoResponse = GetRomeoBlock(2);
            ui.DisplayResponse(_currentRomeoResponse);
        }
    }


    private RomeoBlock GetRomeoBlock(int id)
    {
        return (TextReader.RomeoBlocks[id]);
    }

}
