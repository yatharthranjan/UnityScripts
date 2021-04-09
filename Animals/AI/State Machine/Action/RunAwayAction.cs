using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Animals.AI.State_Machine.Action
{
    [CreateAssetMenu(menuName = "AI/Actions/Animal/RunAwayAction")]
    public class RunAwayAction : AnimalAction
    {
        public override void Act(AnimalStateController controller)
        {
            AnimalProperties properties = controller.GetComponent<AnimalProperties>();
            Vector3 newDesitnation = controller.RandomNavmeshLocation(properties.RunAwayDistance);

            // run with double speed
            controller.navMeshAgent.speed = properties.RunSpeed;
            controller.navMeshAgent.destination = newDesitnation;
        }
    }


}