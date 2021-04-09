using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Actions/Human/RunAction")]
public class RunAction : HumanAction
{
    public override void Act(HumanStateController controller)
    {
        // Run to Safe place
        controller.navMeshAgent.destination = controller.nearestSafePlace.transform.position;
        controller.navMeshAgent.isStopped = false;
        //controller.animator.SetBool("Run", true);
    }
}
