using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class higherEventManager : MonoBehaviour
{

    public static higherEventManager instance;
    private void Awake()
    {

        if (instance != null && instance != this)
        {

            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

    }

    public void newDay()
    {
        specialEvents SpecialEventTemp;
        List<GameObject> allManagers = new List<GameObject>();
        foreach (GameObject specificEvent in gameObject.transform)
        {
            allManagers.Add(specificEvent.gameObject);
        }
        List <npcChange> allChanges = new List<npcChange>();
        List<npcChange> priorityChanges = new List<npcChange>(); ;

        //getting the list of changes, requardless of priority, only considering special events
        foreach(GameObject specificEvent in gameObject.transform)
        {
            SpecialEventTemp = specificEvent.GetComponent<basicEventManger>().getSpecialEvent();
            if (SpecialEventTemp != null)
            {
                foreach (GameObject e in SpecialEventTemp.events)
                {
                    allManagers.Remove(e);
                }
                foreach (npcChange c in SpecialEventTemp.npcChanges)
                {
                    allChanges.Add(c);
                }

            }
            else
            {
                foreach (npcChange c in specificEvent.GetComponent<basicEventManger>().getChanges())
                {
                    allChanges.Add(c);
                }
            }
        }

        //sorting out which changes should actual go through based on priority
        npcChange bestTemp;
        foreach(npcChange c in allChanges)
        {
            bestTemp = c;
            foreach (npcChange other in allChanges)
            {
                if (other.npc.Equals(c.npc))
                {
                    if (other.priority > c.priority) { bestTemp = other; }
                    else { allChanges.Remove(other); }
                }
            }

            priorityChanges.Add(bestTemp);
        }

        //implimenting all the changes
        foreach(npcChange change in priorityChanges)
        {
            change.npc.GetComponent<npcTrigger>().ChangeNpc(change.placement, change.defalt, change.dialogue, change.t1, change.t2, change.t3);
        }

    }

}
