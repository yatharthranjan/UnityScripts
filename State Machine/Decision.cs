using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Decision<T> : ScriptableObject where T: StateController
{
    public abstract bool Decide(T controller);
}