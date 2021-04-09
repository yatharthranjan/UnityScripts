using UnityEngine;
using System.Collections;
using System;

public class HumanDieDecision : HumanDecision
{
    public override bool Decide(HumanStateController controller)
    {
        return IsDead(controller);
    }

    private bool IsDead(HumanStateController controller)
    {
        float health = controller.GetComponent<HumanProperties>().currentHealth;
        return health <= 0;
    }
}
