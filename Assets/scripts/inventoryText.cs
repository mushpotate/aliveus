using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryText : MonoBehaviour
{
    public Animator animator;
    public GameObject knifeObj;

    public GameObject rockObj;

    public GameObject shovelObj;

    public GameObject candleObj;

    public GameObject robeObj;

    public GameObject hidoutKeyObj;

    public GameObject cageKeyObj;

    public GameObject skullObj;

    public GameObject presentObj;

    public void setActive(string name)
    {
        //Debug.Log(name);

        candleObj.SetActive(false);
        robeObj.SetActive(false);
        cageKeyObj.SetActive(false);
        hidoutKeyObj.SetActive(false);
        presentObj.SetActive(false);
        skullObj.SetActive(false);
        shovelObj.SetActive(false);
        robeObj.SetActive(false);
        knifeObj.SetActive(false);
        rockObj.SetActive(false);

        if (name.Equals("candle"))
        {
            candleObj.SetActive(true);
        }
        else if (name.Equals("robe"))
        {
            robeObj.SetActive(true);
        }
        else if (name.Equals("cage key"))
        {
            cageKeyObj.SetActive(true);
        }
        else if (name.Equals("hidout key"))
        {
            hidoutKeyObj.SetActive(true);
        }
        else if (name.Equals("present"))
        {
            presentObj.SetActive(true);
        }
        else if (name.Equals("skull"))
        {
            skullObj.SetActive(true);
        }
        else if (name.Equals("shovel"))
        {
            shovelObj.SetActive(true);
        }
        else if (name.Equals("rock"))
        {
            rockObj.SetActive(true);
        }
        else if (name.Equals("knife"))
        {
            knifeObj.SetActive(true);
        }
        else
        {
            Debug.Log("wrong name");
        }

        animator.SetBool("selected", true);
    }

    public void setUnactive()
    {
        animator.SetBool("selected", false);
    }
}
