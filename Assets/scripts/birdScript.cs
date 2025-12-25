using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] private float moveSpeed;
    //public Rigidbody2D rb;

    //private Vector2 movment;

    private int pointsIndex;

    private bool sleeping = false;

    public Animator animator;

    private Vector2 oldPos;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = Points[pointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sleeping)
        {
            //Debug.Log(pointsIndex);

            oldPos = transform.position;

            if (pointsIndex <= Points.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, Points[pointsIndex].transform.position, moveSpeed * Time.deltaTime);

                if (transform.position == Points[pointsIndex].transform.position)
                {
                    pointsIndex += 1;
                }

                if (pointsIndex == Points.Length)
                {
                    pointsIndex = 0;
                }
            }

            if (oldPos.x == transform.position.x && oldPos.y == transform.position.y)
            {
                sleeping = true;
                StartCoroutine(sleepTime());
            }

            //animator.SetFloat("x", (oldPos.x - transform.position.x) * 50);
            //animator.SetFloat("y", (oldPos.y - transform.position.y) * 50);
        }


    }

    private IEnumerator sleepTime()
    {
        animator.SetBool("sleeping", true);
        yield return new WaitForSeconds(Random.Range(30f, 120f));
        animator.SetBool("sleeping", false);
        //yield return new WaitForSeconds(3.1f);
        sleeping = false;

    }
}
