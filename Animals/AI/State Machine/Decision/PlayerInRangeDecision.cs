using UnityEngine;
using System.Collections;
using Assets.Scripts.World;

namespace Assets.Scripts.Animals.AI.State_Machine.Decision
{
    [CreateAssetMenu(menuName = "AI/Decisions/Animal/PlayerInRangeDecision")]
    public class PlayerInRangeDecision : AnimalDecision
    {
        public override bool Decide(AnimalStateController controller)
        {
            return IsPlayerInRange(controller);
        }

        private bool IsPlayerInRange(AnimalStateController controller)
        {
            GameObject player = GlobalObjectManager.Instance.Player;
            if (player == null || !player.activeInHierarchy)
            {
                return false;
            }

            GameObject currentPos = controller.gameObject;
            if (Vector3.Distance(currentPos.transform.position, player.transform.position)
                < currentPos.GetComponent<AnimalProperties>().LookRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}