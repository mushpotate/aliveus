using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class MonsterAi : MonoBehaviour
{

    public Transform playerPos;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public int stalkingRange = 5;

    bool stopping = false;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = true;

    Seeker seeker;
    Rigidbody2D rb;

    public GameObject pathParent;
    [SerializeField] GameObject[] pathPoints;
    private int currentPoint = 0;
    public int numPathPoints = 0;

    enum PathStates
    {
        chasing, stalking, wandering,hiding,patroling
    }

    PathStates state = PathStates.patroling;
    

    

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = pathPoints[currentPoint].transform.position;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);
        
    }

    void updatePath()
    {
        if (!stopping)
        {
            switch (state)
            {
                case PathStates.chasing:
                    chasing();
                    break;
                case PathStates.stalking:
                    stalking();
                    break;
                case PathStates.wandering:
                    wandering();
                    break;
                case PathStates.hiding:
                    hiding();
                    break;
                case PathStates.patroling:
                    patroling();
                    break;

            }
        }
        

    }

    private void chasing()
    {

        if (seeker.IsDone()) { seeker.StartPath(rb.position, playerPos.position, OnPathComplete); }
    }

    private void stalking()
    {
        if(rb.velocity == Vector2.zero )
        {
            if (Random.Range(0, 50) == 14) { state = PathStates.chasing; }
            else if (Random.Range(0, 50) == 7) { state = PathStates.patroling; }
            
        }
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, playerPos.position, OnPathComplete); 
        }
    }

    private void wandering()
    {
        if (reachedEndOfPath)
        {
            if (Random.Range(1, 3) == 1)
            {
                StartCoroutine(onBreak());
                return;
            }
            reachedEndOfPath = false;
            
            if (seeker.IsDone()) { seeker.StartPath(rb.position, PickRandomPoint(stalkingRange) , OnPathComplete); }

        }
    }

    private void hiding()
    {
        //find a place to hide and wait for a bit
    }

    private void patroling()
    {


        if (reachedEndOfPath)
        {
            if (Random.Range(1, 3) == 1)
            {
                StartCoroutine(onBreak());
                return;
            }

            reachedEndOfPath = false;
            if(currentPoint == pathPoints.Length - 1)
            {
                currentPoint = 0;
            }
            else
            {
                currentPoint++;
            }
            if (seeker.IsDone()) { seeker.StartPath(rb.position, pathPoints[currentPoint].transform.position, OnPathComplete); }
            
        }

    }

    private IEnumerator onBreak()
    {
        stopping = true;
        yield return new WaitForSeconds(Random.Range(1, 4));
        stopping = false;
    }

    Vector3 PickRandomPoint(int radius)
    {
        var point = Random.insideUnitSphere * radius;

        //point.y = 0;
        point += transform.position;
        return point;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) { return; }

        if(state != PathStates.stalking || Vector3.Distance(playerPos.transform.position,this.transform.position) > stalkingRange)
        {
            if (currentWaypoint >= path.vectorPath.Count) { reachedEndOfPath = true; return; }
            else { reachedEndOfPath = false; }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;


            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance) { currentWaypoint++; }
        }
        
    }

    //makes adding path points smoother, does it by itself :)
    private void OnValidate()
    {
        if (numPathPoints == pathPoints.Length)
        {
            return;
        }
        else if (numPathPoints < pathPoints.Length)
        {


            UnityEditor.EditorApplication.delayCall += () =>
            {
                while (numPathPoints < pathPoints.Length)
                {
                    numPathPoints++;
                    GameObject pp = new GameObject("path point: " + numPathPoints);
                    pp.transform.SetParent(pathParent.transform);
                    pp.transform.localPosition = Vector3.zero;
                    pathPoints[numPathPoints - 1] = pp;
                }


            };
        }

        else
        {
            while (numPathPoints > pathPoints.Length)
            {
                GameObject pp = (pathParent.transform.Find("path point: " + numPathPoints)).gameObject;
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(pp);
                };
                numPathPoints--;
            }

        }
    }
}
