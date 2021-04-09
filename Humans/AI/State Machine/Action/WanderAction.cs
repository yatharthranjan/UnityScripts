using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI/Actions/Human/WanderAction")]
public class WanderAction : HumanAction
{
    public override void Act(HumanStateController controller)
    { 
        if (controller.navMeshAgent.remainingDistance != Mathf.Infinity
            && controller.navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete 
            && controller.navMeshAgent.remainingDistance == 0)
        {
            controller.navMeshAgent.destination =
                controller.RandomNavmeshLocation(Random.Range(0, controller.wanderDistance));
            controller.navMeshAgent.isStopped = false;
            //controller.animator.SetBool("Run", true);
        }
    }
}
