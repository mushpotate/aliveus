using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play(0);
        animator.SetBool("start", true);
        this.gameObject.SetActive(false);
    }
}
