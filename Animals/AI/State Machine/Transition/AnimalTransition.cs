using UnityEngine;
using System.Collections;

[System.Serializable]
public class AnimalTransition : Transition<AnimalState, AnimalStateController>
{
    // Override Decision with a concrete implementation
    new public AnimalDecision decision;
}
