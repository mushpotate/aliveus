using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSettings : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public void open()
    {
        animator.SetBool("open", true);

    }

    public void close()
    {
        animator.SetBool("open", false);

    }
}
