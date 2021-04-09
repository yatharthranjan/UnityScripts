using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Animals
{
    public class AnimalProperties : EnemyProperties
    {
        public float LookRange = 5;
        public float MaxHealth = 100;
        public float RunAwayDistance = 5;
        public float WalkDistance = 2;
        public float WalkSpeed = 3.5f;
        public float RunSpeed = 7;

        [HideInInspector] public float currentHealth;

        private void Awake()
        {
            currentHealth = MaxHealth;
        }

        public override void TakeDamage(float damage)
        {
            if (currentHealth == 0)
            {
                return;
            }

            if (currentHealth > 0)
            {
                currentHealth -= damage;
            }
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }

        public override void TakeCure(float cure)
        {
            // Do nothing
        }
    }
}