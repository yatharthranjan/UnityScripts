using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AnimalStateController : StateController
{
    public string GameControllerTag = GlobalTagManager.GameControllerTag;

    [HideInInspector] public List<Transform> wayPointList = new List<Transform>();
    [HideInInspector] public int nextWayPoint;
    public AnimalState currentState;
    public AnimalState remainState;
    [HideInInspector] public bool isCollidingWithPlayer = false;
    [HideInInspector] public GameObject currentCollider = null;
    public int WayPointRadius = 35;
    public int wayPointSize = 6;
    [HideInInspector] public Animator animator;
    public ScoreCard score;

    private void Awake()
    {
        base.Awake();
        //CalculatePatrolWayPoint();
        animator = gameObject.GetComponent<Animator>();
        score = GameObject.FindGameObjectWithTag(GameControllerTag).GetComponent<ScoreCard>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        currentState.UpdateState(this);
    }


    public void TransitionToState(AnimalState nextState)
    {
        if (nextState != remainState)
        {
            AnimalState oldState = currentState;
            if (nextState.actions.Any(action => action is PatrolAction))
            {
                //CalculatePatrolWayPoint();
            }
            currentState = nextState;
            OnExitState(oldState);
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

    private void OnExitState(AnimalState state)
    {
        // DO any clean up when left old state
    }

    internal void Die()
    {
        // Play animation
        Destroy(gameObject, 1f);
    }

}
