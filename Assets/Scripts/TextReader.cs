using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TextReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset _romeoResponses;

    private static Dictionary<int, RomeoBlock> _romeoBlocks = new Dictionary<int, RomeoBlock>();

    public static Dictionary<int, RomeoBlock> RomeoBlocks
    {
        get{ return _romeoBlocks; }
    }


    private void Awake()
    {
        ReadData(_romeoResponses);
    }


    private void ReadData(TextAsset txt)
    {

        string[] chunks = txt.text.Split('-');

        for(int i = 0; i< chunks.Length; i++)
        {
            RomeoBlock rb = new RomeoBlock(chunks[i]);
            RomeoBlocks.Add(rb.TextId, rb);
        }

    }
}
