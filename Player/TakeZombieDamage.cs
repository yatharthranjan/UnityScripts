using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Events;
using System;

[Obsolete]
[RequireComponent(typeof(PlayerProperties))]
public class TakeZombieDamage : MonoBehaviour
{
    public string[] zombieTags = GlobalTagManager.EnemyTags;

    private void OnTriggerStay(Collider other)
    {
        if (zombieTags.Contains(other.gameObject.tag))
        {
            EventManager.TriggerEvent(EventName.PlayerHit);
            gameObject.GetComponent<PlayerProperties>()
                    .TakeDamage(other.GetComponent<ZombieProperties>().Damage);
            // play take damage animation for player
        }
    }

}
