using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionMark : MonoBehaviour
{
    private bool inRange;

    [SerializeField] GameObject question;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && FindObjectOfType<playerMovment>().getInConvo() == false)
        {
            question.SetActive(true);
        }
        else
        {
            question.SetActive(false);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            inRange = false;
        }
    }
}
