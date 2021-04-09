using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "AI/Decisions/Human/ReachedSafePlaceDecision")]
public class ReachedSafePlaceDecision : HumanDecision
{
    public override bool Decide(HumanStateController controller)
    {
        return IsInSafePlace(controller);
    }

    private bool IsInSafePlace(HumanStateController controller)
    {
        // Check if close to safe place.
        if(Vector3.Distance(controller.transform.position, controller.nearestSafePlace.transform.position) < 2)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
