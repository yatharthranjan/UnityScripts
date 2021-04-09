using UnityEngine;

[CreateAssetMenu(menuName = "AI/State/Zombie")]
public class ZombieState : State<ZombieStateController>
{
    public ZombieTransition[] transitions;
    public ZombieAction[] actions;

    public override void DoActions(ZombieStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }
    public override void CheckTransitions(ZombieStateController controller)
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
}
