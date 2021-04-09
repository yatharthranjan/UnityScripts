using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Animals.AI.State_Machine.Decision
{
    [CreateAssetMenu(menuName = "AI/Decisions/Animal/DieDecision")]
    public class DieDecision : AnimalDecision
    {
        public override bool Decide(AnimalStateController controller)
        {
            return IsDead(controller);
        }

        private bool IsDead(AnimalStateController controller)
        {
            return controller.GetComponent<AnimalProperties>().currentHealth <= 0;
        }
    }
}