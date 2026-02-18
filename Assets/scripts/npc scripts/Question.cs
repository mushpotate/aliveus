using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public bool returnable = false;
    public string prompt;

    [TextArea(3, 10)]
    public string[] questionPath;

    public bool testPoint = false;

}
