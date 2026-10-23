using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private Queue<sentencePiece> sentences;

    private bool NPC;

    private bool skip = false;

    //public AudioSource audioSource;

    private bool stoped = false;

    private bool sentenceOver = false;
    private bool inQuestion = false;

    private npcTrigger currentNpc; 

    Dialogue d;

    public TextMeshProUGUI[] questionText = new TextMeshProUGUI[5];

    


    // Start is called before the first frame update
    void Start()
    {
        askingQuestion = false;
        sentences = new Queue<sentencePiece>();

    }

    public void StartDialogue(Dialogue[] dialogue, bool npc,int numTimesTalked)
    {
        lastQuestionSentence = null;
        d = dialogue[numTimesTalked];

        stoped = false;

        //Debug.Log("starting convo with " + dialogue.name);
        nameText.text = dialogue[numTimesTalked].name;

        sentences.Clear();

        if (npc) {
            animator.SetBool("isOpen", true); 
            currentNpc = FindObjectOfType<playerMovment>().getNPC().GetComponent<npcTrigger>();
        }
        else { itemAnimator.SetBool("isOpen", true); }

        //Debug.Log(numTimesTalked);
        foreach (sentencePiece sp in dialogue[numTimesTalked].sentences)
        {
            //creat a method that only enques till it hits a question, so that i can clear the queue and start it at the next question
            sentences.Enqueue(sp);
            //Debug.Log(sp);
            if (sp.question.questionPrompts.Length != 0)
            {
                break;
            }
        }

        DisplayNext(npc);
    }


    public void skipToEnd(bool npc)
    {
        if (lastQuestionSentence.question.questionPrompts.Length != 0)
        {
            Question[] questions = lastQuestionSentence.question.questionPrompts;
            asking(questions);
            maxQuestion = questions.Length;
            skip = true;
            //return;
        }

        if (askingQuestion) { return; }

        if (sentenceOver && !inQuestion)
        {
            sentenceOver = false;
            DisplayNext(npc);
            //Debug.Log("desplayed next");
        }
        else
        {
            //Debug.Log("skipped");
            skip = true;
        }

        
    }

    public void DisplayNext(bool npc)
    {
        if (inQuestion)
        {
            
            sentenceOver = true;
            return;
        }
        NPC = npc;
        //if (lastQuestionSentence != null) { Debug.Log("last q asked: " + lastQuestionSentence.text + " is end? " + lastQuestionSentence.isEnd()); }
        
        if (lastQuestionSentence != null && lastQuestionSentence.isEnd())
        {
            EndDialogue(npc);
            return;
        }
        
        if (sentences.Count == 0)
        {
            EndDialogue(npc);
            return;


        }

        
        sentencePiece sentence = sentences.Dequeue();
        lastQuestionSentence = sentence;
        StopAllCoroutines();
        FindObjectOfType<npcTrigger>().stopAudio();
        skip = false;
        StartCoroutine(typeSentence(sentence, npc));
        //Debug.Log(sentence);


    }

    //private Coroutine cuMo;
    public void asking(Question[] questions)
    {
        if (inQuestion) { return; }
        sentenceOver = true;
        inQuestion = true;
        for (int i = 0; i < questions.Length; i++)
        {
            questionText[i].text = questions[i].prompt;
        }
        //Debug.Log("got here?");
        //StartCoroutine(askQuest(questions));
        askingQuestion = true;
        questionBack.SetBool("open", askingQuestion);
        questionBack.SetInteger("question", questions.Length);
        questionSelected = 1;
        questionCurser.SetInteger("question", questionSelected);
        //if (cuMo == null) { cuMo = StartCoroutine(curserMove()); }
    }

    IEnumerator askQuest(Question[] questions)
    {
        //Debug.Log("got here??");
        inQuestion = true;
        while (!sentenceOver)
        {
            yield return new WaitForSeconds(.1f);
            //Debug.Log("any day now...");
        }
        //Debug.Log("got here???");
        askingQuestion = true;
        questionBack.SetBool("open", askingQuestion);
        questionBack.SetInteger("question", questions.Length);
        questionSelected = 1;
        questionCurser.SetInteger("question", questionSelected);

    }

    IEnumerator curserMove()
    {
        while (askingQuestion)
        {
            yield return new WaitForEndOfFrame();

            if (Input.GetKey(KeyCode.W)) 
            {
                moveCurserUp();
                yield return new WaitForSeconds(1.15f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveCurserDown();
                yield return new WaitForSeconds(1.15f);
            }

        }
        //cuMo = null;
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

    private void talkingPointsManagment()
    {
        //if (d.question.questionPrompts[questionSelected - 1].testPoint)
        //{
            //add a point or do something
            //for example if you choose to lift weights or listen to sailors rants
        //}
    }

    private sentencePiece lastQuestionSentence;
    private void askedQuestion()
    {

        inQuestion = false;
        talkingPointsManagment();
        questionBack.SetBool("open", false);
        //d = d.question.questionPrompts[questionSelected - 1].questionPath[0];
        //= sentences.Peek().question.questionPrompts[questionSelected - 1].questionExitPath;
        for (int i = lastQuestionSentence.question.questionPrompts[questionSelected - 1].questionExitPath; i < d.sentences.Length; i++)
        {
            sentences.Enqueue(d.sentences[i]);
            //Debug.Log(d.sentences[i].text);
            if (d.sentences[i].question.questionPrompts.Length != 0)
            {
                break;
            }
        }
        sentenceOver = false;
        DisplayNext(true);
        StartCoroutine(questionAskedDellay());

    }

    IEnumerator questionAskedDellay()
    {
        yield return new WaitForSeconds(.1f);
        askingQuestion = false;
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

    IEnumerator typeSentence (sentencePiece s, bool npc)
    {
        string sentance = s.text;
        Sprite talk1 = null;
        Sprite talk2 = null;
        Sprite talk3 = null;

        if (npc)
        {
             talk1 = currentNpc.getT1();
             talk2 = currentNpc.getT2(); ;
             talk3 = currentNpc.getT3(); ;

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
        
        yield return new WaitForSeconds(.3f);

        float talkSpeed = currentNpc.getTalkSpeed();

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

            //Debug.Log("hello?");

            if (npc) { dialogueText.text += letter; }
            else { itemText.text += letter; }

            if (stoped == false && letter != ' ' && letter != '.' && letter != ',')
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

                    currentNpc.playAudio();
                }
                soundCounter++;

            }
            else
            {
                //Debug.Log("tried");
                soundCounter = 0;
                if (npc) { talkingSprite.sprite = talk1; }
                if (letter == '.')
                {
                    //Debug.Log("waited");
                    yield return new WaitForSeconds(.4f);
                }
                else if (letter == ',')
                {
                    //Debug.Log("waited");
                    yield return new WaitForSeconds(.8f);
                }
            }
            

            if (dialogueText.text == (sentance) || itemText.text == (sentance))
            {
                sentenceOver = true;
            }

            yield return new WaitForSeconds(talkSpeed);
        }
        if (npc) { talkingSprite.sprite = talk1; }
        if(s.question.questionPrompts.Length != 0)
        {
            Question[] questions = lastQuestionSentence.question.questionPrompts;
            asking(questions);
            maxQuestion = questions.Length;
        }
        
    }
        
        

    void EndDialogue(bool npc)
    {
        //Debug.Log("end");
        currentNpc.resetPos();
        stoped = true;
        currentNpc.stopAudio();
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
