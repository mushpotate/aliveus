using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public string level;
    public AudioSource audioSource;
    public Animator transition;
    [SerializeField] bool progressDay = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(level.Equals("day2cutscene")&&FindObjectOfType<gameManager>().getBirdsEnding())
        {
            FindObjectOfType<sceneManager>().loadLevel("2 birds 1 stone");
        }
        else
        {
            if (progressDay) { FindAnyObjectByType<gameManager>().day++; }
            transition.SetBool("start", true);
            audioSource.Play(0);
            FindObjectOfType<playerMovment>().slowed();
            FindObjectOfType<sceneManager>().loadLevel(level);
        }

        
    }
    
}
