using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digable : MonoBehaviour
{
    //public Animator animator;
    private bool range = false;
    private bool canDig = false;
    public GameObject obj1;
    public GameObject obj2;
    //public GameObject obj2;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(range + " " + canDig + " " + Input.GetKeyDown(KeyCode.T));
        if (range && canDig && Input.GetKeyDown(KeyCode.T))
        {
            FindObjectOfType<inventory>().addObject(obj1);
            FindObjectOfType<inventory>().digRange(false);
            //FindObjectOfType<inventory>().addObject(obj2);
            this.gameObject.SetActive(false);
        }
    }

    public void gotShovel()
    {
        canDig = true;
        obj2.SetActive(false);
    }

    public bool canDigs()
    {
        return canDig;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        range = true;
        FindObjectOfType<inventory>().digRange(true);



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        range = false;
        FindObjectOfType<inventory>().digRange(false);


    }
}
