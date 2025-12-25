using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretDoor : MonoBehaviour
{
    public Animator left;

    public Animator right;

    public GameObject door;

    public GameObject lever;

        
    public void open()
    {
        FindObjectOfType<gameManager>().openSecretDoor();

        door.SetActive(true);

        lever.SetActive(false);

        left.SetBool("open", true);

        right.SetBool("open", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<gameManager>().isSecretDoor())
        {
            open();
        }
        else
        {
            door.SetActive(false);
        }
 
    }

}
