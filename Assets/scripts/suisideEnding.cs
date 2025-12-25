using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suisideEnding : MonoBehaviour
{

    private bool canKill = false;
    private bool hasKnife = false;

    public void gotKnife()
    {
        hasKnife = true;
    }
    // Start is called before the first frame update
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)&&canKill&&hasKnife)
        {
            FindObjectOfType<sceneManager>().loadLevel("suiside ending");
        }
    }
}
