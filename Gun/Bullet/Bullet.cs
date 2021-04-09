using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int force = 5;
    Rigidbody rb;
    public GameObject ImpactEffect,BloodEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
        rb.AddRelativeForce(Vector3.forward * force  , ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Instantiate(ImpactEffect, collision.contacts[0].point, Quaternion.identity);
            Instantiate(BloodEffect, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
