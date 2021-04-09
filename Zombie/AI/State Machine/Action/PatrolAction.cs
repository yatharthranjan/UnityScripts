using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Actions/Zombie/PatrolAction")]
public class PatrolAction : ZombieAction
{
    public override void Act(ZombieStateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(ZombieStateController controller)
    {
        if(controller.wayPointList.Length == 0)
        {
            controller.StartCoroutine(controller.CalculatePatrolWayPoint());
        }
        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;
        controller.animator.SetBool("Attack", false); // Optimise

        if (controller.navMeshAgent.remainingDistance <= 0.5f && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Length;
        }
    }
}
