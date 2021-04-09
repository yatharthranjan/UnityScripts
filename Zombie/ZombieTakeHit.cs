using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Obsolete]
[RequireComponent(typeof(ZombieProperties))]
public class ZombieTakeHit : MonoBehaviour
{
    public List<string> BulletTags = new List<string> { GlobalTagManager.BulletTag };
    public List<string> VaccineTags = new List<string> { GlobalTagManager.VaccineTag };

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
        if(BulletTags.Contains(collision.gameObject.tag))
        {
            float bulletDamage = collision.gameObject.GetComponent<BulletProperties>().Damage;
            gameObject.GetComponent<ZombieProperties>().TakeDamage(bulletDamage);
        } else if (VaccineTags.Contains(collision.gameObject.tag))
        {
            float curePower = collision.gameObject.GetComponent<VaccineProperties>().CurePower;
            gameObject.GetComponent<ZombieProperties>().TakeCure(curePower);
        }
    }
}
