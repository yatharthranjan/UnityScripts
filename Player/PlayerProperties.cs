using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [Range(0, 10000000)]
    public float MaxHealth = 10000;

    [Range(0, 100000)]
    public float MaxArmour = 1000;
    [HideInInspector] public float Health;
    [HideInInspector] public float Armour;
    public Mode mode = Mode.Kill;

    void Awake()
    {
        Health = MaxHealth;
        Armour = MaxArmour;
    }

    public void TakeDamage(float damage)
    {
        if(Health <= 0)
        {
            // Aleady dead
            return;
        }

        if (Armour > 0)
        {
            this.Armour -= 0.7f * damage;
            this.Health -= 0.3f * damage;
        } else
        {
            this.Health -= damage;
        }
        

        if (Health < 0) MaxHealth = 0;
        if (Armour < 0) MaxArmour = 0;
    }
}

public enum Mode { Kill, Cure, Idle }