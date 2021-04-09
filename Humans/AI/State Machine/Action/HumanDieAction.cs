using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Actions/Human/HumanDieAction")]
public class HumanDieAction : HumanAction
{
    public override void Act(HumanStateController controller)
    {
        // Die if caught by zombie
        controller.animator.SetBool("Die", true);
        controller.Destroy();
    }
}
