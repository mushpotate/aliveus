using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform plater;
    [SerializeField] private float moveSpeed;
    public Animator animator;
    public GameObject player;
    private bool chasing=true;
    public AudioSource sound;
    public AudioSource steps;
    public GameObject cam;

    
    // Update is called once per frame
    void Update()
    {
        if(chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, plater.transform.position, moveSpeed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        steps.Stop();
        chasing = false;
        player.SetActive(false);
        cam.SetActive(true);
        animator.SetBool("start", true);
        StartCoroutine(crunch());
    }

   

    public IEnumerator crunch()
    {
        yield return new WaitForSeconds(.35f);
        sound.Play(0);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<sceneManager>().loadLevel("title");
    }
}
