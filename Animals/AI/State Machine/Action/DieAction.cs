using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Animals.AI.State_Machine.Action
{
    [CreateAssetMenu(menuName = "AI/Actions/Animal/DieAction")]
    public class DieAction : AnimalAction
    {
        public override void Act(AnimalStateController controller)
        {
            controller.Die();
        }

    }
}