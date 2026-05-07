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
        List<npcChange> allChanges;
        List<npcChange> priorityChanges;

        foreach(GameObject specificEvent in gameObject.transform)
        {
            GameObject SpecialEventTemp = specificEvent.GetComponent<basicEventManger>().getSpecialEvent();
            if(SpecialEventTemp != null)
            {
                //figure out how to call it based on the interface rather then the custom script!!!
                //SpecialEventTemp.
            }
        }


    }

}
