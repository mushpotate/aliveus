using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public string level;

    public GameObject levelC;
    public GameObject levelD;

    public bool same = false;

    private Vector2 destination;

    public bool darken = false;

    public AudioSource audioSource;

    public AudioSource music;

    public bool difRoom = false;

    public bool isEntryDoor;
    public bool isVertical;

    [SerializeField] private bool outside = false;



    void Awake()
    {
        destination = new Vector2(transform.GetChild(0).position.x, transform.GetChild(0).position.y);
        //Debug.Log(destination + "");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "player")
        {
            if (outside)
            {
                FindFirstObjectByType<weatherManager>().activateWeather();
            }
            audioSource.Play(0);
            FindObjectOfType<sceneManager>().loadLevel(levelC,levelD,destination,same,darken,music, difRoom);
            
        }
    }

    public Vector2 getDestination()
    {
        return destination;
    }

}
