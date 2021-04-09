using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(HumanProperties))]
public class HumanStateController : StateController
{
    public string[] EnemyTags = GlobalTagManager.EnemyTags;

    public HumanState currentState;
    public HumanState remainState;
    [HideInInspector] public bool isCollidingWithEnemy = false;
    [HideInInspector] public GameObject currentCollider = null;

    public GameObject[] safePlaces = { };
    public string safePlaceTag = GlobalTagManager.SafePlaceTag;

    [Range(0, 20)]
    public float wanderDistance = 10;

    [HideInInspector] public GameObject nearestSafePlace;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool stopWandering = false;
    private HumanState initState;


    // Start is called before the first frame update
    void Start()
    {

    }

    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        if (safePlaces == null || safePlaces.Length == 0)
        {
            safePlaces = GameObject.FindGameObjectsWithTag(safePlaceTag);
        }
        initState = currentState;
    }

    private void OnEnable()
    {
        CalculateNearestSafePlace();
        StartCoroutine(nameof(UpdateState));

    }

    // Update is called once per frame
    IEnumerator UpdateState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            base.Update();
            currentState.UpdateState(this);
        }
    }

    public void TransitionToState(HumanState nextState)
    {
        if (nextState != remainState)
        {
            HumanState oldState = currentState;
            BeforeNextState(nextState);
            currentState = nextState;
            OnExitState(oldState);
        }
    }

    public void BeforeNextState(HumanState nextState)
    {
        if (nextState.actions.Any(action => action is RunAction))
        {
            CalculateNearestSafePlace();
        }
        if (nextState.transitions.Length == 0)
        {   // Destroy object if the state is terminal
            StartCoroutine(nameof(Destroy));
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        //Destroy(gameObject);
        currentState = initState;
        GetComponent<HumanProperties>().Reset();
        gameObject.SetActive(false);
    }

    private void CalculateNearestSafePlace()
    {
        if (safePlaces.Length == 0)
        {
            nearestSafePlace = gameObject;
            return;
        }
        else if (safePlaces.Length == 1)
        {
            nearestSafePlace = safePlaces.First();
        }

        float nearest = float.MaxValue;
        foreach (GameObject safePlace in safePlaces)
        {
            float distance = Vector3.Distance(transform.position, safePlace.transform.position);
            if (distance < nearest)
            {
                nearest = distance;
                this.nearestSafePlace = safePlace;
            }
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        else
        {
            // If no valid point found on navmesh, try to find again.
            finalPosition = RandomNavmeshLocation(radius);
        }
        return finalPosition;
    }

    private void OnExitState(HumanState oldState)
    {
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (EnemyTags.Contains(collision.gameObject.tag))
        {
            currentCollider = collision.gameObject;
            isCollidingWithEnemy = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (EnemyTags.Contains(collision.gameObject.tag))
        {
            isCollidingWithEnemy = false;
            currentCollider = null;
        }
    }
}
