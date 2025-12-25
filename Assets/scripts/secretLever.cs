using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretLever : MonoBehaviour
{

    private bool touching = false;
    public AudioSource audioSource;
    
    bool activate = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        touching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touching = false;
    }



    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.E))
        {
            activate = true;
        }

        if (FindObjectOfType<playerMovment>().getjustTalked() && activate)
        {
            FindObjectOfType<secretDoor>().open();
            audioSource.Play(0);
        }
    }

    
}
