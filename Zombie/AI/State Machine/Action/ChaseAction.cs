using UnityEngine;
using System.Collections;
using Assets.Scripts.World;

[CreateAssetMenu(menuName = "AI/Actions/Zombie/ChaseAction")]
public class ChaseAction : ZombieAction
{
    public override void Act(ZombieStateController controller)
    {
        GameObject player = GlobalObjectManager.Instance.Player;
        if(player == null || !player.activeInHierarchy)
        {
            return;
        }
        controller.navMeshAgent.destination = player.transform.position;
        controller.navMeshAgent.isStopped = false;
        controller.animator.SetBool("Attack", false); // Optimise
    }
}
