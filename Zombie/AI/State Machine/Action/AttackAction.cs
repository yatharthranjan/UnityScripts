using UnityEngine;
using System.Collections;
using Assets.Scripts.World.Events;

[CreateAssetMenu(menuName = "AI/Actions/Zombie/AttackAction")]
public class AttackAction : ZombieAction
{
    public override void Act(ZombieStateController controller)
    {
        controller.navMeshAgent.isStopped = true;

        controller.animator.SetBool("Attack", true); // Optimise

        if (controller.currentCollider != null) {
            PlayerProperties properties = controller.currentCollider.GetComponent<PlayerProperties>();
            if (properties != null) {
                float zombieDamage = controller.gameObject.GetComponent<ZombieProperties>().Damage;
                properties.TakeDamage(zombieDamage);
                EventManager.TriggerEvent(EventName.PlayerHit);
            }
        }
    }
}