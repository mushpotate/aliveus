using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovment : MonoBehaviour
{
    private Vector2 movment;
    private Rigidbody2D rb;
    public float speed = 3f;
    public float walk = 3f;
    public float sprint = 6f;
    private Animator animator;

    public bool canWalk = true;

    public bool inConvo = false;

    public bool justTalked = false;

    private bool justStarted = false;

    public static playerMovment instance;

    private GameObject npcTalking;

    private bool sprinting = false;

    private bool robed = false;

    private bool hasRobe = false;

    public Animator settingsAnimator;
    private bool settingsOpen = false;


    Vector2 dest = new(0, 0);

    Vector2 stoped;

    Boolean doorOpened = false;

    public void gotRobe()
    {
        hasRobe = true;
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        //if (instance != null && instance != this)
        //{
        //
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    instance = this;
        //}
        //
        //DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        stoped = rb.position;
    }

    private void OnMovment(InputValue value)
    {
        if (canWalk)
        {
            movment = value.Get<Vector2>();
            if ((movment.x != 0 || movment.y != 0))
            {
                animator.SetFloat("x", movment.x);
                animator.SetFloat("y", movment.y);

                animator.SetBool("isWalking", true);

            }
            else
            {
                animator.SetBool("isWalking", false);

            }
        }
        else
        {
            movment = new(0, 0);
            animator.SetBool("isWalking", false);

        }
    }

    

    private void FixedUpdate()
    {

        //Debug.Log(speed);
        
        stoped = rb.position;

        if (canWalk) { rb.MovePosition(rb.position + movment * speed * Time.fixedDeltaTime); }

        if(doorOpened)
        {
            rb.MovePosition(dest );

            doorOpened = false;
        }

    }

    public void slowed()
    {
        speed = 1;
        
    }

    public void fasted()
    {
        if(sprinting)
        {
            speed = sprint;
        }
        else
        {
            speed = walk;
        }
        
    }

    public void doorMove(Vector2 des)
    {

        //Debug.Log("moved");

        //dest = des;

        //doorOpened = true;

        transform.position = des;

        //rb.MovePosition(des);
    }

    public void cutsceneMove(Vector2 des,float time,Animator transition,Animator anim,bool isAnim)
    {

        StartCoroutine(cutsceneCor(des, time, transition, anim, isAnim));
    }

    public IEnumerator cutsceneCor(Vector2 des, float time, Animator transition, Animator anim, bool isAnim) 
    {
        
        transition.SetBool("start", true);
        yield return new WaitForSeconds(2f);
        if (isAnim)
        {
            anim.SetBool("start", true);
        }
        Vector2 current = transform.position;
        transform.position = des;
        canWalk = false;
        transition.SetBool("start", false);
        yield return new WaitForSeconds(time);
        transition.SetBool("start", true);
        yield return new WaitForSeconds(2f);
        transform.position = current;
        canWalk = true;
        transition.SetBool("start", false);

    }

    public bool getRobed()
    {
        return robed;
    }

    public void closeSettings()
    {
        settingsAnimator.SetBool("open", false);
        settingsOpen = false;
        canWalk = true;
    }
    void Update()
    {
        //if the player is in range of an npc the question mark will show obove the npc

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingsOpen)
            {
                settingsAnimator.SetBool("open", false);
                settingsOpen = false;
                canWalk=true;
            }
            else
            {
                settingsAnimator.SetBool("open", true);
                settingsOpen=true;
                canWalk=false;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            FindObjectOfType<sceneManager>().loadLevel("combustion ending");
        }

        if (Input.GetKeyDown(KeyCode.R)&&hasRobe)
        {
            if(robed == true)
            {
                robed = false;
                animator.SetBool("robe", false);
            }
            else
            {
                robed = true;
                animator.SetBool("robe", true);
            }
        }

            if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!sprinting)
            {
                speed = sprint;
                sprinting = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (sprinting)
            {
                speed = walk;
                sprinting = false;
            }
        }

        if (inConvo && Input.GetKeyDown(KeyCode.E) && justStarted == false)
        {
            FindObjectOfType<DialogueManager>().skipToEnd(FindObjectOfType<DialogueManager>().getNPC());
        }
        else if (inConvo)
        {
            justStarted = false;
        }


    }

    public void setUntalked()
    {
        justTalked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npcTalking = collision.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            npcTalking = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            justTalked = false;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            justTalked = false;
            //Debug.Log("not touching npc");
        }
        //Debug.Log("not touching ");
    }

    public void dialogueDone()
    {
        inConvo = false;
        justTalked = true;
        canWalk = true;
        
    }

    public void startDialogue()
    {
        animator.SetBool("isWalking", false);
        canWalk = false;
        inConvo = true;
        FindObjectOfType<npcTrigger>().TriggerDialogue();
        
        justStarted = true;
    }

    public bool getInConvo()
    {
        return inConvo;
    }

    public GameObject getNPC()
    {
        return (npcTalking);
    }

    public bool getjustTalked()
    {
        return justTalked;
    }

    public void SetWalk(bool walk)
    {
        canWalk = walk;
    }

    public Vector2 getPos()
    {
        return this.transform.position;
    }
    

}
