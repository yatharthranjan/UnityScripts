using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanTransition : Transition<HumanState, HumanStateController>
{
    // Override Decision with a concrete implementation
    public new HumanDecision decision;

}
