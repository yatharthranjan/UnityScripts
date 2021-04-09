using Assets.Scripts.Gun;
using Assets.Scripts.World;
using Assets.Scripts.World.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGrenade : MonoBehaviour
{
    public GameObject Grenade;
    public int MaxAmmo = 50;
    [HideInInspector] public int currentAmmo;

    private GameObject GunController;

    private void Start()
    {
        GunController = GlobalObjectManager.Instance.CurrentGun.GunController;
        EventManager.RegisterListener(EventName.GunUpdated, new UnityEngine.Events.UnityAction(OnGunUpdate));
        currentAmmo = 10;
    }

    public void Shoot()
    {
        if(Grenade != null && currentAmmo > 0 && gameObject != null && gameObject.activeInHierarchy)
        {
            Instantiate(Grenade, GunController.transform.position, GunController.transform.rotation);
            currentAmmo--;
        }
    }

    public void OnGunUpdate()
    {
        GunController = GlobalObjectManager.Instance.CurrentGun.GunController;
    }
}
