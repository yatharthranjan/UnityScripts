using UnityEngine;
using System.Collections;
using Assets.Scripts.World;
using Assets.Scripts.World.Events;

[CreateAssetMenu(menuName = "AI/Decisions/Zombie/PlayerDeadDecision")]
public class PlayerDeadDecision : ZombieDecision
{
    /**
     * The player is considered dead if either the game object is null or the Player Health is 0.
     * 
     * **/
    public override bool Decide(ZombieStateController controller)
    {
        GameObject player = GlobalObjectManager.Instance.Player;

        if (player == null || !player.activeInHierarchy)
        {
            controller.CalculatePatrolWayPoint();
            EventManager.TriggerEvent(EventName.PlayerDead);
            return true;
        } else if(player.GetComponent<PlayerProperties>().Health <= 0)
        {
            EventManager.TriggerEvent(EventName.PlayerDead);
            return true;
        }
        {
            return false;
        }
    }
}
