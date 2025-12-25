using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cultistShadow : MonoBehaviour
{
    private bool done=false;
    public Animator animator;
    public AudioSource sound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!done)
        {
            animator.SetBool("start", true);
            sound.Play(0);
            done = true;
        }
        

    }
}
