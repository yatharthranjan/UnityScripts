using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieNavMeshAgent : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (goal == null)
        {
            goal = GlobalObjectManager.Instance.Player.transform;
        }
        agent.destination = goal.position;
    }

    private void Update()
    {
        if (goal != null)
        {
            agent.destination = goal.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GlobalTagManager.PlayerTag)
        {
            agent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == GlobalTagManager.PlayerTag)
        {
            agent.isStopped = false;
        }
    }
}
