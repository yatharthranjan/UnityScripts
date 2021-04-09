using UnityEngine;
using System.Collections;

public abstract class Action<T> : ScriptableObject where T: StateController 
{
    public abstract void Act(T controller);
}
