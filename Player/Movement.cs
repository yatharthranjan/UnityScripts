using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour
{
    public abstract void RotateTowardsTranform(Transform transformAimTo);

    public abstract void JumpWithButton();
}
