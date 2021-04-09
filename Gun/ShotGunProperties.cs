using UnityEngine;
using System.Collections;
using Assets.Scripts.World;

public class ShotGunProperties : GunProperties
{
    public int numOfShells = 5;
    public float spread = 6;

    public override void Shoot()
    {
        if (currentAmmo < 1)
        {
            // Not enough ammo
            return;
        }

        if ((Time.time - lastShotTime) >= shootInterval)
        {
            Quaternion currentRotation = transform.rotation * Quaternion.Euler(0, -(spread / 2) * (numOfShells - 1), 0);
            Vector3 currentPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.25f, 0.25f));
            for (int i = 0; i < numOfShells; i++)
            {
                GameObject bullet = ObjectPooler.INSTANCE.GetObjectFromPool(Bullet);
                bullet.transform.position = currentPosition;
                bullet.transform.rotation = currentRotation;
                bullet.SetActive(true);
                //Instantiate(Bullet, currentPosition, currentRotation);
                currentRotation *= Quaternion.Euler(0, spread, 0);
                currentPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.25f, 0.25f));
            }
            lastShotTime = Time.time;
            currentAmmo--;
            MuzzleFlash.SetActive(true);
            audioSource.PlayOneShot(FireSound);
        }
    }
}
