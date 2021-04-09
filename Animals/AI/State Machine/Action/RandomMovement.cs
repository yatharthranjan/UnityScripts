using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Animals.AI.State_Machine.Action
{
    [CreateAssetMenu(menuName = "AI/Actions/Animal/RandomMoveAction")]
    public class RandomMovement : AnimalAction
    {
        public override void Act(AnimalStateController controller)
        {
            if (Vector3.Distance(controller.transform.position, controller.navMeshAgent.destination) <= 0.5f)
            {
                AnimalProperties properties = controller.GetComponent<AnimalProperties>(); // Optimise
                Vector3 newDesitnation = controller.RandomNavmeshLocation(properties.WalkDistance); 

                // walk
                controller.navMeshAgent.speed = properties.WalkSpeed;
                controller.navMeshAgent.destination = newDesitnation;
            }
        }
    }
}