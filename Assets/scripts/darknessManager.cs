using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darknessManager : MonoBehaviour
{
    public GameObject dark;
    private bool darkend = false;

    public bool darkstart = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!darkstart)
        {
            dark.SetActive(false);
        }
        
        
    }

    
    public void setDarkness(bool b)
    {
        darkend = b;
        dark.SetActive(b);

        if(b)
        {
            FindObjectOfType<musicManager>().toSilence();
        }
        else
        {
            FindObjectOfType<musicManager>().leaveSilence();
        }
    }

    public void change()
    {
        if (darkend)
        {
            setDarkness(false);
            
        }
        else
        {
            setDarkness(true);
            
        }
    }
}
