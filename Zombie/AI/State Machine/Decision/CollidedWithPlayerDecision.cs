using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "AI/Decisions/Zombie/CollidedWithPlayerDecision")]
public class CollidedWithPlayerDecision : ZombieDecision
{
    public override bool Decide(ZombieStateController controller)
    {
        return isCollidedWithPlayer(controller);
    }

    private bool isCollidedWithPlayer(ZombieStateController controller)
    {
        return controller.isCollidingWithPlayer;
    }
}
