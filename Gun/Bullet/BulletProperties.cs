using UnityEngine;
using System.Collections;

public class BulletProperties : ProjectileProperties
{
    [Min(0)]
    public int Damage = 1000;
}
