using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiddingSpot : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("player"))
        {
            collision.gameObject.GetComponent<playerMovment>().hideTrigger(true,this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("player"))
        {
            collision.gameObject.GetComponent<playerMovment>().hideTrigger(false, this.gameObject);
        }
    }
}
