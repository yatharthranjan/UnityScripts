using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State<T> : ScriptableObject where T : StateController 
{

    public void UpdateState(T controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    public abstract void DoActions(T controller);

    public abstract void CheckTransitions(T controller);


}