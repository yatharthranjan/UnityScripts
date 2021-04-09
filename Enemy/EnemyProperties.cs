using UnityEngine;
using System.Collections;

public abstract class EnemyProperties: MonoBehaviour
{
    public abstract void TakeDamage(float damage);

    public abstract void TakeCure(float cure);
}
