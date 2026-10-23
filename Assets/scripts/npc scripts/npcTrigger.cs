using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class npcTrigger : MonoBehaviour
{
    //day1Pos day1DefSprite day1Dialogue

    [SerializeField] private Vector2 day1Pos;
    [SerializeField] private Sprite day1DefSprite;
    [SerializeField] private Dialogue[] day1Dialogue;

    [SerializeField] private Vector2 day2Pos;
    [SerializeField] private Sprite day2DefSprite;
    [SerializeField] private Dialogue[] day2Dialogue;

    [SerializeField] private Vector2 day3Pos;
    [SerializeField] private Sprite day3DefSprite;
    [SerializeField] private Dialogue[] day3Dialogue;

    private Dialogue[] dialogue;

    [SerializeField] private bool isCounterD1;
    [SerializeField] private bool isCounterD2;
    [SerializeField] private bool isCounterD3;
    private static float normalColiderOffset = -0.311923f;
    private static float counterColiderOffset = -1.163474f;

    //public Dialogue[] questions;
    //public string[] questionPrompt;
    //private bool askingQuestion;

    public Sprite t1;
    public Sprite t2;
    public Sprite t3;
    //public Dialogue dialogue;

    public AudioSource audioSource;
    [SerializeField] AudioClip voiceClip1;
    [SerializeField] AudioClip voiceClip2;
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
    private bool justTalked;
    private playerMovment player;

    private void Awake()
    {
        player = FindObjectOfType<playerMovment>();
        dialogue = day1Dialogue;
        day1DefSprite = spriteRenderer.sprite;
    }
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
        if (Random.Range(1, 3) == 1)
        {
            audioSource.clip = voiceClip1;
        }
        else
        {
            audioSource.clip = voiceClip2;
        }
        audioSource.pitch = defaultPitch + (Random.Range(-.05f, .05f));
        audioSource.Play(0);
    }

    public void resetPos()
    {
        StartCoroutine(talkWaitTimer());
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

        
      
        

        TriggerDialogue();


    }

    //one constructer can be used checks if it should change variable by if its null
    public void ChangeNpc(Vector3 placement, Sprite defalt, Dialogue[] newDialogue, Sprite t1, Sprite t2, Sprite t3,bool isCounter)
    {
        if (placement != Vector3.zero)
        {
            this.transform.position = placement;
            if (isCounter)
            {
                this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0,counterColiderOffset);
            }
            else
            {
                this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, normalColiderOffset);
            }
        }
        if (defalt != null) this.defalt = defalt; resetPos();

        if (newDialogue.Length != 0) this.dialogue = newDialogue;
        numTimesTalked = 0;

        //if one face expresion is changed all should be
        if (t1 != null)
        {
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
        }
      
    }



    public void SetNPC(int day)
    {
        switch(day)
        {
            case 1:
                ChangeNpc(day1Pos, day1DefSprite, day1Dialogue, t1, t2, t3,isCounterD1);
                break;
            case 2:
                ChangeNpc(day2Pos, day2DefSprite, day2Dialogue, t1, t2, t3,isCounterD2);
                break;
            case 3:
                ChangeNpc(day3Pos, day3DefSprite, day3Dialogue, t1, t2, t3,isCounterD3);
                break;
        }
    }
}
