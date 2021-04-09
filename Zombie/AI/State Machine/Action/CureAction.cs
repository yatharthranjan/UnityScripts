using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "AI/Actions/Zombie/CureAction")]
public class CureAction : ZombieAction
{
    public override void Act(ZombieStateController controller)
    {
        CureZombie(controller);
    }

    private void CureZombie(ZombieStateController controller)
    {
        // Play Cure Animation
        controller.CureZombie();
    }
}
