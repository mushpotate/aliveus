using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killable : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hasKnife = false;
    private bool canKill = false;
    //public bool isNPC = true;
    public GameObject thisNPC;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canKill);
        //Debug.Log(canKill + " " + hasKnife);
        if (canKill&&hasKnife)
        {
            //Debug.Log(canKill + " " +  hasKnife + "whyyy");
            
            thisNPC.GetComponent<npcTrigger>().die();
        }
        

    }

    public void gotKnife()
    {
        hasKnife = true;
    }
    public bool doesKnife()
    {
        return hasKnife;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("in");
        canKill = true;
        FindObjectOfType<inventory>().knifeRange(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("out");
        canKill = false;
        FindObjectOfType<inventory>().knifeRange(false);

    }
}
