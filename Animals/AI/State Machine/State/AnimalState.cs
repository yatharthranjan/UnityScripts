using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI/State/Animal")]
public class AnimalState : State<AnimalStateController>
{
    public AnimalTransition[] transitions;
    public AnimalAction[] actions;

    public override void CheckTransitions(AnimalStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }

    public override void DoActions(AnimalStateController controller)
    {

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
}
