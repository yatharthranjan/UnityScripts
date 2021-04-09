using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Actions/Zombie/DieAction")]
public class DieAction : ZombieAction
{
    public override void Act(ZombieStateController controller)
    {
        Die(controller);
    }

    private void Die(ZombieStateController controller)
    {
        controller.animator.SetBool("Dead", true); // Optimise
        controller.DieZombie();
    }
}
