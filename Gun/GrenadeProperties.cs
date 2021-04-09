using Assets.Scripts.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class GrenadeProperties : MonoBehaviour
{
    public AudioClip DetonateSound;
    private AudioSource audioSource;
    private bool hasDetonated = false;
    public int AreaDamageRadius = 5;
    public LayerMask enemyMask;
    public float DamagePerEnemy = 1200;
    public int NumberOfEnemiesDamaged = 20;
    private Animator cameraAnimator;

    private Collider[] hitColliders;

    public int force = 5;
    Rigidbody rb;
    public GameObject ParticleGranade;
    void Start()
    {
        hitColliders = new Collider[NumberOfEnemiesDamaged];
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);

        audioSource = GetComponent<AudioSource>();
        cameraAnimator = GlobalObjectManager.Instance.MainCamera.gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasDetonated)
        {
            hasDetonated = true;
            Instantiate(ParticleGranade, collision.contacts[0].point, Quaternion.identity);
            cameraAnimator.enabled = true;
            cameraAnimator.SetTrigger("Boom");
            StartCoroutine(nameof(ResetBoom));
            // We detonate the grenade as soon as it collides with any object
            Detonate();
        }


    }

    private IEnumerator ResetBoom()
    {
        yield return new WaitForSeconds(0.5f);
        cameraAnimator.ResetTrigger("Boom");

    }

    public virtual void Detonate() // Optimise
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        // TODO: add Detonate FLash animation
        audioSource.PlayOneShot(DetonateSound);
        StartCoroutine(nameof(Destroy));

        Array.Clear(hitColliders, 0, hitColliders.Length);

        // Find nearest 20 enemies
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, AreaDamageRadius, hitColliders, enemyMask);

        // Filter zombies which are dead or cured
        Collider[] hitCollidersFiltered = hitColliders.Where(collider =>
        {
            if (collider != null && collider.gameObject.TryGetComponent(out ZombieProperties properties))
            {
                return properties.Health > 0 && properties.CuredPercentage < 100;
            }
            return false;
        }
        ).ToArray();

        /*                collider != null
                        && collider.gameObject.GetComponent<ZombieProperties>().Health > 0
                        && collider.gameObject.GetComponent<ZombieProperties>().CuredPercentage < 100
                    ).ToArray();*/

        if (hitCollidersFiltered.Length == 0)
        {
            return;
        }

        foreach (Collider collider in hitCollidersFiltered)
        {
            collider.gameObject.GetComponent<ZombieProperties>().TakeDamage(DamagePerEnemy);
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(0, 10, -1) * force * 10, ForceMode.Impulse);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
