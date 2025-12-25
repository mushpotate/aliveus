using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class npcTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;

    public Sprite t1;
    public Sprite t2;
    public Sprite t3;
    //public Dialogue dialogue;

    public AudioSource audioSource;
    public float defaultPitch;

    private bool isTouchingPlayer = false;

    public SpriteRenderer spriteRenderer;

    public Sprite up;

    public Sprite down;

    public Sprite left;

    public Sprite right;

    public bool isNPC;

    public Sprite defalt;

    public float talkSpeed;

    private bool dead = false;

    public GameObject blood;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;

    private int numTimesTalked = 0;

    //private bool guardDead = false;

    public AudioSource deathSound;

    private float TalkWaitTime = .1f;


    //0 is up 
    //1 is down
    //2 is left
    //3 is right
    private int direction = 1;

    private void Start()
    {
        defaultPitch = audioSource.pitch;
    }
    public void TriggerDialogue()
    {


        //Debug.Log(isTouchingPlayer);

        if (isTouchingPlayer && Input.GetKeyDown(KeyCode.E) && FindObjectOfType<playerMovment>().getInConvo() == false && FindObjectOfType<playerMovment>().getjustTalked() == false && !dead)
        {

            
            FindObjectOfType<playerMovment>().startDialogue();
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue,isNPC, numTimesTalked);
            if (numTimesTalked < dialogue.Length-1)
            {
                numTimesTalked++;
            }


            if (isNPC)
            {
                
                
                if (direction == 0)
                {
                    spriteRenderer.sprite = up;
                }
                if (direction == 1)
                {
                    spriteRenderer.sprite = down;
                }
                if (direction == 2)
                {
                    spriteRenderer.sprite = left;
                }
                if (direction == 3)
                {
                    spriteRenderer.sprite = right;
                }
            }
            
            
        }
        
        
    }

    public Sprite getT1()
    {
        return t1;
    }
    public Sprite getT2()
    {
        return t2;
    }
    public Sprite getT3()
    {
        return t3;
    }
    
    public void death()
    {
        FindObjectOfType<inventory>().knifeRange(false);
        if (this.gameObject.name.Equals("manus"))
        {
            // and knife level is bellow 3
            FindObjectOfType<sceneManager>().loadLevel("unprepared ending");
        }
        else if(this.gameObject.name.Equals("best friend"))
        {
            FindObjectOfType<gameManager>().setBirdsEnding();
            dead = true;
            blood.SetActive(true);
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            FindObjectOfType<gameManager>().addKills();
            deathSound.Play(0);
        }
        else if(this.gameObject.name.Equals("lazy cultist"))
        {
            FindObjectOfType<inventory>().addRobe();
            dead = true;
            blood.SetActive(true);
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            FindObjectOfType<gameManager>().addKills();
            deathSound.Play(0);
        }
        else if(this.gameObject.name.Equals("cultist guard 1")|| this.gameObject.name.Equals("cultist guard 2"))
        {
            FindObjectOfType<robeDetection>().noGuards();


            dead = true;
            blood.SetActive(true);
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            FindObjectOfType<gameManager>().addKills();
            deathSound.Play(0);
        }
        else if (this.gameObject.name.Equals("teacher"))
        {

            FindObjectOfType<noMoreTeacher>().end();

            dead = true;
            blood.SetActive(true);
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            FindObjectOfType<gameManager>().addKills();
            deathSound.Play(0);
        }
        else 
        {
            dead = true;
            blood.SetActive(true);
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            FindObjectOfType<gameManager>().addKills();
            deathSound.Play(0);
        }
        
        //this.gameObject.SetActive(false);


    }
    

    public void stopAudio()
    {
        audioSource.Stop();
    }

    public void playAudio()
    {
        audioSource.pitch = defaultPitch + (Random.Range(-.05f, .05f));
        audioSource.Play(0);
    }

    public void resetPos()
    {
        spriteRenderer.sprite = defalt;
    }

    public float getTalkSpeed()
    {
        return talkSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (isNPC)
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {

                if (hitPos.normal.y > 0)
                {
                    //down
                    direction = 1;
                }
                else if (hitPos.normal.y < 0)
                {
                    //up
                    direction = 0;
                }
                else if (hitPos.normal.x > 0)
                {
                    //left
                    direction = 2;
                }
                else if (hitPos.normal.x < 0)
                {
                    //right
                    direction = 3;
                }
            }
        }


        

        isTouchingPlayer = true;

        //Debug.Log(isTouchingPlayer);
        //Debug.Log("touch");
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("exit");

        isTouchingPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTouchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouchingPlayer = false;
    }

    public void die()
    {
        if (Input.GetKeyDown(KeyCode.K) && isNPC)
        {
            //Debug.Log("death");
            death();
        }
    }

    IEnumerator talkWaitTimer()
    {
        yield return new WaitForSeconds(TalkWaitTime);
        FindObjectOfType<playerMovment>().setUntalked();
    }
    private void Update()
    {

        
        

        if (FindObjectOfType<playerMovment>().getjustTalked())
        {
            StartCoroutine(talkWaitTimer());
        }
        

        TriggerDialogue();


    }


}
