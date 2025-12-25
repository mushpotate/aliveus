using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robeDetection : MonoBehaviour
{
    private int guardsDead = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!FindObjectOfType<playerMovment>().getRobed())
        {
            FindObjectOfType<sceneManager>().loadLevel("cought ending");
        }
    }

    public void noGuards()
    {
        if(guardsDead == 1)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            guardsDead++;
        }
        
    }
}
