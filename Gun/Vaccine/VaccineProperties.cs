using UnityEngine;
using System.Collections;

public class VaccineProperties: ProjectileProperties
{
    [Min(0)]
    public int CurePower = 100;
}
