using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Decisions/Zombie/CureDecision")]
public class CureDecision : ZombieDecision
{
    public override bool Decide(ZombieStateController controller)
    {
        if(controller.gameObject.GetComponent<ZombieProperties>().CuredPercentage >= 100) // Optimise
        {
            return true;
        } else
        {
            return false;
        }
    }
}
