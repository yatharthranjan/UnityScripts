using UnityEngine;

[System.Serializable]
public class ZombieTransition: Transition<ZombieState, ZombieStateController>
{
    // Override Decision with a concrete implementation
    new public ZombieDecision decision;
}
