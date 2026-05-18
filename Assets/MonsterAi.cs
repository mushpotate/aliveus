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
    [SerializeField] List<Transform> pathPoints;
    private int currentPoint = 0;
    private int numPathPoints = 0;
    [SerializeField] int startingPoint = 0;
    [SerializeField] int chanceToWander;
    [SerializeField] int chanceToHide;
    [SerializeField] int baseSpeed;
    [SerializeField] int chaseSpeed;

    [SerializeField] AudioSource backNoise;
    [SerializeField] AudioSource hello;
    [SerializeField] GameObject monsterHidingSpots;
    [SerializeField] GameObject body;

    enum PathStates
    {
        chasing, stalking, wandering,hiding,patroling
    }

    PathStates state = PathStates.patroling;
    

    

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        foreach(Transform pp in pathParent.transform)
        {
            pathPoints.Add(pp);
        }
        //pathPoints = pathParent.GetComponentsInChildren<GameObject>();
        this.transform.position = pathPoints[startingPoint].transform.position;
        currentPoint = startingPoint;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("updatePath", 0f, .5f);
        
    }

    private Vector3 getClosestHidingSpot()
    {
        //List<Transform> list = monsterHidingSpots.GetComponentInChildren<Transform>();
        Vector3 closest = Vector3.zero;
        foreach(Transform spot in monsterHidingSpots.transform)
        {
            if(closest != Vector3.zero)
            {
                if(Vector3.Distance(closest,this.gameObject.transform.position) > Vector3.Distance(spot.transform.position, this.gameObject.transform.position))
                {
                    closest = spot.transform.position;
                }
            }
            else
            {
                closest = spot.transform.position;
            }
        }
        return closest;
    }
    void updatePath()
    {
        //Debug.Log(state);

        if((state == PathStates.wandering || state == PathStates.patroling) && Random.Range(0, chanceToHide) == 7)
        {
            seeker.StartPath(rb.position, getClosestHidingSpot(), OnPathComplete);
            state = PathStates.hiding;

        }

        if (!FindFirstObjectByType<playerMovment>().getIisHiding() && state != PathStates.stalking &&  (state == PathStates.wandering || state == PathStates.patroling) && Vector3.Distance(this.transform.position, playerPos.position) <= stalkingRange)
        {
            backNoise.Stop();
            hello.Play(0);
            state = PathStates.stalking;
            stopping = false;
        }

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
        if (FindFirstObjectByType<playerMovment>().getIisHiding())
        {
            state = PathStates.wandering;
            speed = baseSpeed;
        }
        else if (seeker.IsDone()) { seeker.StartPath(rb.position, playerPos.position, OnPathComplete); }
    }

    private void stalking()
    {
        if (FindFirstObjectByType<playerMovment>().getIisHiding() && Random.Range(0, 20) == 7)
        {
            state = PathStates.wandering;
        }
        else if(rb.velocity == Vector2.zero && Vector3.Distance(this.transform.position,playerPos.position) >= stalkingRange-6)
        {
            if (Random.Range(0, 20) == 14) { state = PathStates.chasing; backNoise.Play(); speed = chaseSpeed; }
            
            
        }
        else if(Vector3.Distance(this.transform.position, playerPos.position) >= stalkingRange-1)
        {
            if (Random.Range(0, 20) == 7) { state = PathStates.patroling; backNoise.Play(); }
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
        if (reachedEndOfPath && body.GetComponent<SpriteRenderer>().enabled)
        {
            body.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (!body.GetComponent<SpriteRenderer>().enabled && Random.Range(1, 20) == 1)
        {
            body.GetComponent<SpriteRenderer>().enabled = true;
            state = PathStates.patroling;
        }
    }

    private void patroling()
    {
        //Debug.Log("current point: " + currentPoint);

        if (reachedEndOfPath)
        {
            if (Random.Range(1, 3) == 1)
            {
                StartCoroutine(onBreak());
                return;
            }

            reachedEndOfPath = false;
            if(currentPoint == pathPoints.Count - 1)
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
        if(state == PathStates.wandering && Random.Range(1, 10) == 1)
        {
            state = PathStates.patroling;
        }else if(state == PathStates.patroling && Random.Range(1, chanceToWander) == 1)
        {
            state = PathStates.wandering;
        }
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
        if (numPathPoints == pathPoints.Count)
        {
            return;
        }
        else if (numPathPoints < pathPoints.Count)
        {


            UnityEditor.EditorApplication.delayCall += () =>
            {
                while (numPathPoints < pathPoints.Count)
                {
                    numPathPoints++;
                    Transform pp = new GameObject("path point: " + numPathPoints).transform;
                    pp.SetParent(pathParent.transform);
                    pp.localPosition = Vector3.zero;
                    pathPoints[numPathPoints - 1] = pp;
                }


            };
        }

        else
        {
            while (numPathPoints > pathPoints.Count)
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
