using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspectionCutscene : MonoBehaviour
{
    public GameObject text;
    //public GameObject obj;
    private bool touching = false;
    //public AudioSource colect;

    public Vector2 location;
    public float time;
    public Animator transition;
    public Animator anim;
    public bool isAnim = false;
    public AudioSource sound;


    bool activate = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        touching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (touching && Input.GetKeyDown(KeyCode.E))
        {
            activate = true;
        }

        if (FindObjectOfType<playerMovment>().getjustTalked() && activate)
        {
            transition.SetBool("start", true);
            FindObjectOfType<playerMovment>().cutsceneMove(location,time,transition,anim,isAnim);
            text.SetActive(false);
            sound.Play(0);
            this.gameObject.SetActive(false);
        }
    }
}
