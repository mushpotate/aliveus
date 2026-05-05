using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class npcChange
{
    public GameObject npc = null;
    public int priority;
    public Vector3 placement = Vector3.zero;
    public Sprite defalt = null;
    public Dialogue[] dialogue = null;
    public Sprite t1 = null;
    public Sprite t2 = null;
    public Sprite t3 = null;
    //public GameObject specialEvent = null;

}
