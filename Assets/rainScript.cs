using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
public class rainScript : MonoBehaviour
{
    [SerializeField] bool isRaining = true;
    [SerializeField] Image lightning;

    [SerializeField] AudioSource thunderSound;
    [SerializeField] AudioClip loudThunder1;
    [SerializeField] AudioClip loudThunder2;
    [SerializeField] AudioClip mediumThunder1;
    [SerializeField] AudioClip mediumThunder2;
    [SerializeField] AudioClip quietThunder1;
    [SerializeField] AudioClip quietThunder2;

    

    private void Start()
    {
        StartCoroutine(raining());
    }

    IEnumerator raining()
    {
        int rand = 0;
        int randS = Random.Range(0, 2);
        while (isRaining)
        {
            yield return new WaitForSeconds(Random.Range(20,120));
            rand = Random.Range(1,4);
            if(rand == 1)
            {
                //far away
                //thunder
                StartCoroutine(simLightning(1));
                Debug.Log("long thunder");
                yield return new WaitForSeconds(Random.Range(5, 10));
                //lightning
                Debug.Log("lightning");
                simThunder((randS == 0) ? (quietThunder1) : (quietThunder2));

            }
            else if(rand == 2)
            {
                //medium dist
                //thunder
                StartCoroutine(simLightning(2));
                Debug.Log("mid thunder");
                yield return new WaitForSeconds(Random.Range(2, 5));
                //lightning
                Debug.Log("lightning");
                simThunder((randS == 0) ? (mediumThunder1) : (mediumThunder2));
            }
            else
            {
                //close
                //thunder
                StartCoroutine(simLightning(3));
                Debug.Log("short thunder");
                //lightning
                Debug.Log("lightning");
                simThunder((randS == 0) ? (loudThunder1) : (loudThunder2));
                
            }


        }
        
    }

    IEnumerator simLightning(int level)
    {
        if(level == 1)
        {
            lightning.color = new Color(0, 0, 0, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.66f, .66f, .66f, 0.4f);
            yield return new WaitForSeconds(.3f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(0, 0, 0, 0.4f);
        }
        else if(level == 2)
        {
            lightning.color = new Color(0, 0, 0, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.66f, .66f, .66f, 0.4f);
            yield return new WaitForSeconds(.6f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(0, 0, 0, 0.4f);
        }
        else
        {
            lightning.color = new Color(0, 0, 0, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.66f, .66f, .66f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(1f, 1f, 1f, 0.4f);
            yield return new WaitForSeconds(.6f);
            lightning.color = new Color(.66f, .66f, .66f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(.33f, .33f, .33f, 0.4f);
            yield return new WaitForSeconds(.2f);
            lightning.color = new Color(0, 0, 0, 0.4f);
        }
        

    }

    private void simThunder(AudioClip c)
    {
        thunderSound.pitch = Random.Range(.8f, 1.2f);
        thunderSound.clip = c;
        thunderSound.Play(0);
    }




}
