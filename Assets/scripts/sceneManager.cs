using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class sceneManager : MonoBehaviour
{
    public Animator animator;
    public float moveDelayTime = 2f;

    public void loadLevel(string LevelName, Vector2 destination)
    {
        StartCoroutine(delay(LevelName,destination));


    }

    public void loadLevel(GameObject levelC, GameObject levelD, Vector2 destination, bool same,bool darken,AudioSource music,bool difRoom)
    {
        StartCoroutine(delay(levelC, levelD, destination,same,darken, music, difRoom));
    }
    public void loadLevel(string level)
    {
        
        StartCoroutine(delay(level));

    }

    //make start transition coroteen

    public void Start()
    {
        StartCoroutine(delayStart());
    }

    private IEnumerator delayStart()
    {
        FindObjectOfType<playerMovment>().SetWalk(false);
        yield return new WaitForSeconds(moveDelayTime);
        FindObjectOfType<playerMovment>().SetWalk(true);
        
    }

    private IEnumerator delay(string level)
    {
        FindObjectOfType<musicManager>().fade(false);
        animator.SetBool("start", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
    }
    private IEnumerator delay(string level, Vector2 d)
    {
        
        animator.SetBool("start", true);
        FindObjectOfType<playerMovment>().slowed();
        yield return new WaitForSeconds(.4f);
        FindObjectOfType<playerMovment>().SetWalk(false);
        FindObjectOfType<playerMovment>().fasted();
        yield return new WaitForSeconds(.3f);
        FindObjectOfType<playerMovment>().doorMove(d);
        SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
    }

    private IEnumerator delay(GameObject levelC, GameObject levelD, Vector2 d, bool same,bool darken, AudioSource music,bool difRoom)
    {
        
        animator.SetBool("start", true);
        FindObjectOfType<playerMovment>().slowed();
        yield return new WaitForSeconds(.4f);
        FindObjectOfType<playerMovment>().SetWalk(false);
        FindObjectOfType<playerMovment>().fasted();
        yield return new WaitForSeconds(.3f);
        FindObjectOfType<playerMovment>().doorMove(d);
        if (!same && !difRoom)
        {

            
            levelD.SetActive(true);
            
            if (darken)
            {
                FindObjectOfType<musicManager>().setCurrent(music);
                FindObjectOfType<darknessManager>().change();
                
            }
            else
            {
                FindObjectOfType<musicManager>().setMusic(music);
            }
            animator.SetBool("start", false);
            StartCoroutine(delayStart());
            yield return new WaitForSeconds(1.5f);
            levelC.SetActive(false);
        }
        else if(difRoom)
        {
            levelD.SetActive(true);

            if (darken)
            {
                //FindObjectOfType<musicManager>().setCurrent(music);
                FindObjectOfType<darknessManager>().change();

            }
            
            animator.SetBool("start", false);
            StartCoroutine(delayStart());
            yield return new WaitForSeconds(1.5f);
            levelC.SetActive(false);
        }
        else
        {
            if (darken)
            {
                
                FindObjectOfType<darknessManager>().change();
            }
            animator.SetBool("start", false);
            StartCoroutine(delayStart());
        }
        


    }

    public void boneCave()
    {
        //send player to bone cave
    }


}
