using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public GameObject text;
    public GameObject obj;
    private bool touching = false;
    public AudioSource colect;
    // Start is called before the first frame update
    


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
            //colect.Play(0);
            //obj.SetActive(true);
            FindObjectOfType<inventory>().addObject(obj);
            text.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
