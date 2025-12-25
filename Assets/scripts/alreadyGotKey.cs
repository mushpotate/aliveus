using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alreadyGotKey : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<cultDoor>().alrGot();
        this.gameObject.SetActive(false);
    }
}
