using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cultDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    public GameObject lockedText;
    public GameObject text;
    public GameObject trapdoor;

    private bool unlockabl = false;
    private bool touching = false;
    public AudioSource unlock;
    // Start is called before the first frame update
    private void Start()
    {
        text.SetActive(false);
        door.SetActive(false);
    }
    public void unlockable()
    {
        lockedText.SetActive(false);
        text.SetActive(true); ;
        unlockabl = true;
    }

    bool activate = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        touching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touching = false;
    }

    public void alrGot()
    {
        lockedText.SetActive(false);
        door.SetActive(true);
        text.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.E))
        {
            activate = true;
        }

        if (FindObjectOfType<playerMovment>().getjustTalked() && activate && unlockabl )
        {
            unlock.Play(0);
            door.SetActive(true);
            text.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
