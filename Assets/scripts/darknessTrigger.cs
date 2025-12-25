using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darknessTrigger : MonoBehaviour
{

    private bool dark = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(dark)
        {
            FindObjectOfType<darknessManager>().setDarkness(false);
            dark = false;
        }
        else
        {
            FindObjectOfType<darknessManager>().setDarkness(true);
            dark = true;
        }
    }
}
