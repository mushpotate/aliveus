using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public Image talkingSprite;

    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI itemText;

    public Animator animator;

    public Animator itemAnimator;

    public Animator questionBack;
    public Animator questionCurser;
    private bool askingQuestion;
    private int questionSelected = 1;
    private int maxQuestion = 0;

    private Queue<string> sentences;

    private bool NPC;

    private bool skip = false;

    //public AudioSource audioSource;

    private bool stoped = false;

    private bool sentenceOver = false;

    Dialogue d;

    public TextMeshProUGUI[] questionText = new TextMeshProUGUI[5];



    // Start is called before the first frame update
    void Start()
    {
        askingQuestion = false;
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue[] dialogue, bool npc,int numTimesTalked)
    {
        d = dialogue[numTimesTalked];

        stoped = false;

        //Debug.Log("starting convo with " + dialogue.name);
        nameText.text = dialogue[numTimesTalked].name;

        sentences.Clear();

        if (npc) { animator.SetBool("isOpen", true); }
        else { itemAnimator.SetBool("isOpen", true); }
        

        foreach (string sentence in dialogue[numTimesTalked].sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNext(npc);
    }

    public void skipToEnd(bool npc)
    {
        if (sentenceOver )
        {
            sentenceOver = false;
            DisplayNext(npc);
        }
        else
        {
            skip = true;
        }

        
    }

    public void DisplayNext(bool npc)
    {
        NPC = npc;

        if (sentences.Count == 0)
        {
            if(d.questions.Length != 0)
            {
                asking(d.questions.Length);
                maxQuestion = d.questions.Length;
                return;
                //still need to add the continuation after you choose a question option
            }
            else
            {
                EndDialogue(npc);
                return;
            }

        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        FindObjectOfType<npcTrigger>().stopAudio();
        skip = false;
        StartCoroutine(typeSentence(sentence, npc));
        //Debug.Log(sentence);


    }

    public void asking(int numQuestions)
    {
        for(int i =0; i < numQuestions; i++)
        {
            questionText[i].text = d.questions[i].prompt;
        }
        askingQuestion = true;  
        questionBack.SetBool("open", askingQuestion);
        questionBack.SetInteger("question", numQuestions);
        questionSelected = 1;
        questionCurser.SetInteger("question", questionSelected);
        //StartCoroutine(curserMove());
    }

    IEnumerator curserMove()
    {
        while (askingQuestion)
        {
            yield return new WaitForEndOfFrame();

            if (Input.GetKey(KeyCode.W)) 
            {
                moveCurserUp();
                yield return new WaitForSeconds(.15f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveCurserDown();
                yield return new WaitForSeconds(.15f);
            }

        }
    }

    private void Update()
    {
        if (askingQuestion)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                askedQuestion();

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                moveCurserUp();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                moveCurserDown();
            }
        }
        
    }

    private void askedQuestion()
    {
        askingQuestion = false;
        questionBack.SetBool("open", false);
        foreach (string sentence in d.questions[questionSelected -1].questionPath)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNext(true);

    }

    public void moveCurserUp()
    {
        if (questionSelected == 1)
        {
            questionSelected = maxQuestion;
        }
        else
        {
            questionSelected--;
        }
        questionCurser.SetInteger("question", questionSelected);

    }

    public void moveCurserDown()
    {
        if (questionSelected == maxQuestion)
        {
            questionSelected = 1;
        }
        else
        {
            questionSelected++;
        }
        questionCurser.SetInteger("question", questionSelected);

    }

    IEnumerator typeSentence (string sentance, bool npc)
    {

        Sprite talk1 = null;
        Sprite talk2 = null;
        Sprite talk3 = null;

        if (npc)
        {
             talk1 = FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().getT1();
             talk2 = FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().getT2(); ;
             talk3 = FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().getT3(); ;

            talkingSprite.sprite = talk1;
        }
        

        int soundCounter = 0;

        if (npc)
        {
            dialogueText.text = "";
            
        }
        else
        {
            itemText.text = "";
        }
        

        foreach (char letter in sentance.ToCharArray())
        {
            //might need to call a sep function to start question making process
            //if (askingQuestion) { maxQuestion = letter; break;  }
            //if(letter == '?') { askingQuestion = true; continue; }

            if (npc)
            {
                if (skip && dialogueText.text != (sentance))
                {
                    skip = false;

                    dialogueText.text = sentance;
                    sentenceOver = true;
                    talkingSprite.sprite = talk1; 
                    
                    yield break;

                }
            }
            else
            {
                if (skip && itemText.text != (sentance))
                {
                    skip = false;

                    itemText.text = sentance;
                    sentenceOver = true;
                    yield break;

                }
            }

            

            if (letter != ' ' && stoped == false)
            {
                
                if(soundCounter == 0 || soundCounter%3 == 0)
                {
                    if (npc)
                    {
                        if (soundCounter % 6 == 0)
                        {
                            talkingSprite.sprite = talk3;
                        }
                        else
                        {
                            talkingSprite.sprite = talk2;
                        }
                    }
                    
                    FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().playAudio();
                }
                soundCounter++;

            }
            else
            {
                soundCounter = 0;
                talkingSprite.sprite = talk1;
            }
            if (npc) { dialogueText.text += letter; }
            else { itemText.text += letter; }

            if (dialogueText.text == (sentance) || itemText.text == (sentance))
            {
                sentenceOver = true;
            }

            yield return new WaitForSeconds(FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().getTalkSpeed());
        }
        if (npc) { talkingSprite.sprite = talk1; }
        
    }
        
        

    void EndDialogue(bool npc)
    {
        //Debug.Log("end");
        FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>().resetPos();
        stoped = true;
        FindObjectOfType<npcTrigger>().stopAudio();
        if (npc) 
        {
            animator.SetBool("isOpen", false); 
        }
        else 
        {
            //Debug.Log("sign close");
            itemAnimator.SetBool("isOpen", false); 
        }
        FindObjectOfType<playerMovment>().dialogueDone();

        


    }

    public bool getNPC()
    {
        return NPC;
    }


}
