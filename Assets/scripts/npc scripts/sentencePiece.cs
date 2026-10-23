using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sentencePiece 
{
    [TextArea(3, 10)]
    public string text = "";
    public HasQuestion question;
    [SerializeField] private bool end;

    public bool isEnd()
    {
        return end;
    }
}
