using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Decisions/Zombie/DieDecision")]
public class DieDecision : ZombieDecision
{
    public override bool Decide(ZombieStateController controller)
    {
        if (controller.gameObject.GetComponent<ZombieProperties>().Health <= 0) // Optimise
        {
            return true;
        } else
        {
            return false;
        }
    }
}
