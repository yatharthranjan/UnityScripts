using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using Assets.Scripts.World;

namespace Assets.Scripts.Gun.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletPooled : MonoBehaviour
    {

        public int force = 5;
        Rigidbody rb;
        public GameObject ImpactEffect, BloodEffect;
        public string[] CollisionTags = GlobalTagManager.BulletCollisionTags;
        private BulletProperties BulletProperties;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            BulletProperties = GetComponent<BulletProperties>();
        }

        private void OnEnable()
        {
            StartCoroutine(nameof(SetInactive));
            rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        }

        private IEnumerator SetInactive()
        {
            yield return new WaitForSeconds(2);
            Stop();
        }

        private void Stop()
        {
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionTags.Contains(collision.gameObject.tag))
            {
                GameObject impact = ObjectPooler.INSTANCE.GetObjectFromPool(ImpactEffect);
                impact.transform.position = collision.contacts[0].point;
                impact.transform.rotation = Quaternion.identity;
                impact.SetActive(true);

                GameObject blood = ObjectPooler.INSTANCE.GetObjectFromPool(BloodEffect); 
                blood.transform.position = collision.contacts[0].point;
                blood.transform.rotation = Quaternion.identity;
                blood.SetActive(true);

                Stop();

                if(collision.gameObject.TryGetComponent(out EnemyProperties enemy))
                {
                    enemy.TakeDamage(BulletProperties.Damage);
                }
            }
        }
    }
}