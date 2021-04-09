using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "AI/Decisions/Human/CollidedWithZombieDecision")]
public class CollidedWithZombieDecision : HumanDecision
{
    public override bool Decide(HumanStateController controller)
    {
        return IsCollidingWithZombie(controller);
    }

    private bool IsCollidingWithZombie(HumanStateController controller)
    {
        return controller.isCollidingWithEnemy;
    }
}
