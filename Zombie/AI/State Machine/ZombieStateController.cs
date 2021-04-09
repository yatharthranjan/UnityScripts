using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.AI;
using System.Linq;
using Assets.Scripts.World;
using Assets.Scripts.World.Events;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(ZombieProperties), typeof(Animator), typeof(NavMeshAgent))]
public class ZombieStateController : StateController
{
    public string GameControllerTag = GlobalTagManager.GameControllerTag;
    [HideInInspector] public Transform[] wayPointList;
    [HideInInspector] public int nextWayPoint;
    public ZombieState currentState;
    public ZombieState remainState;
    [HideInInspector] public bool isCollidingWithPlayer = false;
    [HideInInspector] public GameObject currentCollider = null;
    public int WayPointRadius = 35;
    public int wayPointSize = 4;
    [HideInInspector] public Animator animator;
    public ScoreCard score;
    public GameObject OnCureEffect;
    private bool IsDead = false;
    private ZombieState initState;
    private float initSpeed;

    private ZombieProperties zombieProperties;
    private Collider zombieCollider;

    new private void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
        score = GameObject.FindGameObjectWithTag(GameControllerTag).GetComponent<ScoreCard>();
        initState = currentState;
        initSpeed = navMeshAgent.speed;
        wayPointList = new Transform[wayPointSize];
        for(int i=0; i< wayPointSize; i++)
        {
            wayPointList[i] = new GameObject().transform;
        }
        zombieCollider = GetComponent<Collider>();
        zombieProperties = GetComponent<ZombieProperties>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(CalculatePatrolWayPoint));
        StartCoroutine(nameof(UpdateState));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    new void Update()
    {
        // Do nothing
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

    public void DieZombie()
    {
        if (IsDead) return;
        IsDead = true;
        zombieCollider.enabled = false;
        navMeshAgent.speed = 0;
        StartCoroutine(nameof(DestoryZombie));
        EventManager.TriggerEvent(EventName.ZombieDead);
    }

    IEnumerator DestoryZombie()
    {
        yield return new WaitForSeconds(1.5f);
        Kill();
        gameObject.SetActive(false);
        IsDead = false;
    }

    private void Kill()
    {
        currentState = initState;
        zombieProperties.ResetValues();
        zombieCollider.enabled = true;
        navMeshAgent.speed = initSpeed;
        isCollidingWithPlayer = false;
        currentCollider = null;
    }

    internal void CureZombie()
    {
        zombieCollider.enabled = false;
        navMeshAgent.speed = 0;
        GameObject currentEffect = ObjectPooler.INSTANCE.GetObjectFromPool(OnCureEffect);
        currentEffect.transform.position = transform.position;
        currentEffect.transform.rotation = Quaternion.identity;
        currentEffect.SetActive(true);
        StartCoroutine(nameof(Cure)); // Needs to disable mesh of the zombie
        Kill();
        EventManager.TriggerEvent(EventName.ZombieCured);
    }

    private IEnumerator Cure()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject human = ObjectPooler.INSTANCE.GetObjectFromPool(GetComponent<ZombieProperties>().OnCurePrefab);
        human.transform.position = transform.position;
        human.transform.rotation = transform.rotation;
        human.SetActive(true);

        gameObject.SetActive(false);
        IsDead = false;

    }

    // Call this before transition to patrolling state
    public IEnumerator CalculatePatrolWayPoint()
    {
        for(int i=0; i< wayPointSize; i++)
        {
            Transform waypoint = wayPointList[i];
            waypoint.position = RandomNavmeshLocation(WayPointRadius);
            yield return waypoint;
        }
        this.nextWayPoint = 0;
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
        } else
        {
            // If no valid point found on navmesh, use current position.
            finalPosition = gameObject.transform.position;
        }
        return finalPosition;
    }

    public void TransitionToState(ZombieState nextState)
    {
        if (nextState != remainState)
        {
            ZombieState oldState = currentState;
            if(nextState.actions.Any(action => action is PatrolAction))
            {
                StartCoroutine(CalculatePatrolWayPoint());
            }
            currentState = nextState;
            OnExitState(oldState);
        }
    }

    private void OnExitState(ZombieState state)
    {
        // DO any clean up when left old state
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(GlobalTagManager.PlayerTag))
        {
            currentCollider = collision.gameObject;
            isCollidingWithPlayer = true;
            EventManager.TriggerEvent(EventName.ZombieCloseCall);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(GlobalTagManager.PlayerTag))
        {
            isCollidingWithPlayer = false;
            currentCollider = null;
        }
    }
}
