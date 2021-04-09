using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Animals;

public class AnimalTakeHit : MonoBehaviour
{
    public string BulletTag = GlobalTagManager.BulletTag;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(BulletTag))
        {
            float bulletDamage = collision.gameObject.GetComponent<BulletProperties>().Damage;
            gameObject.GetComponent<AnimalProperties>().TakeDamage(bulletDamage);
        }
    }
}
