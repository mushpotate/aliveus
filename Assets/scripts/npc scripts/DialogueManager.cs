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

    private Queue<string> sentences;

    private bool NPC;

    private bool skip = false;

    //public AudioSource audioSource;

    private bool stoped = false;

    private bool sentenceOver = false;

    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue[] dialogue, bool npc,int numTimesTalked)
    {
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
        if (sentenceOver)
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
            EndDialogue(npc);
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        FindObjectOfType<npcTrigger>().stopAudio();
        skip = false;
        StartCoroutine(typeSentence(sentence, npc));
        //Debug.Log(sentence);


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
