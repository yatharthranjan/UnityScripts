using System.Collections;
using System.IO;
using UnityEngine;

public class ZombieProperties : EnemyProperties
{
    [Range(0, 10000000)]
    public float MaxHealth = 1000;

    [Range(0, 100000)]
    public float MaxCureRequired = 100;

    [Range(0,50)]
    public int Level = 1;
    [HideInInspector] public float Health;

    [Range(0, 100)]
    public float Resistence = 0;

    [Range(0, 10000000)]
    public int Damage = 10;
    private float CurePowerRequired;
    public GameObject OnCurePrefab;
    [HideInInspector] public float CuredPercentage = 0;

    [Range(0, 500)]
    public float LookRange = 30;

    private void Start()
    {
        MaxCureRequired += (10 * (Level + Resistence));
        MaxHealth += (1000 * Level) + (1000 * Resistence);
        ResetValues();
    }

    public void LevelUp()
    {
        Level++;
        Resistence++;
        MaxCureRequired += 20;
        MaxHealth += 2000;
    }

    public void ResetValues()
    {
        CuredPercentage = 0;
        CurePowerRequired = MaxCureRequired;
        Health = MaxHealth;
    }

    public override void TakeDamage(float damage)
    {
        if(Health <= 0)
        {
            // Zombie is already dead
            return;
        }

        if(damage > 0)
        {
            Health -= damage;
            if(Health < 0)
            {
                Health = 0;
            }
        } else
        {
            // We make minimal changes to the health if final damage comes out to be zero
            Health -= 10;
        }
    }

    public override void TakeCure(float cureStrength)
    {
        if (CuredPercentage >= 100)
        {   // Zombie is already cured
            return;
        }

        if (cureStrength > 0)
        {
            CurePowerRequired -= cureStrength;
        }
        else
        {
            // We make minimal changes to the Curepower if final damage comes out to be zero
            CurePowerRequired -= 1;
        }

        CuredPercentage = (MaxCureRequired - CurePowerRequired) / MaxCureRequired * 100f;
    }
}