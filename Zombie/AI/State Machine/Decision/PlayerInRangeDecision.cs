using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.World;

[CreateAssetMenu(menuName = "AI/Decisions/Zombie/PlayerInRangeDecision")]
public class PlayerInRangeDecision : ZombieDecision
{
    public override bool Decide(ZombieStateController controller)
    {
        return IsPlayerInRange(controller);
    }

    private bool IsPlayerInRange(ZombieStateController controller)
    {
        GameObject player =  GlobalObjectManager.Instance.Player;
        if(player == null || !player.activeInHierarchy) // Optimise by using event system
        {
            return false;
        }

        GameObject currentZombie = controller.gameObject;
        if (Vector3.Distance(currentZombie.transform.position, player.transform.position) 
            < currentZombie.GetComponent<ZombieProperties>().LookRange) // Optimise
        {
            return true;
        } else
        {
            return false;
        }
    }
}
