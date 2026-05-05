using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEventManger : MonoBehaviour
{
    //day 2 will be the default for imediate changes
    [SerializeField] npcChange[] day2AffectedNPCS;
    [SerializeField] npcChange[] day3AffectedNPCS;
    [SerializeField] GameObject specialEvent;

    private bool isActivated = false;

    public GameObject getSpecialEvent()
    {
        return specialEvent;
    }

    public bool getActivated()
    {
        return isActivated;
    }

    public void setActivated(bool b , bool doNow)
    {
        if(doNow)
        {
            //setchanges imediatly
            foreach(npcChange change in day2AffectedNPCS)
            {
                change.npc.GetComponent<npcTrigger>().ChangeNpc(change.placement,change.defalt,change.dialogue,change.t1,change.t2,change.t3);
            }
        }
        else
        {
            isActivated = b;
        }
        
    }

    
}
