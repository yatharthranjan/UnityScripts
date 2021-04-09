using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Transition<T, R> where R: StateController where T: State<R>
{
    // Its better to override this with a concrete implementation in subclasses.
    public Decision<R> decision;

    public T trueState;
    public T falseState;
}