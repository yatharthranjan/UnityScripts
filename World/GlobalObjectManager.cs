using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Assets.Scripts.Gun;
using System.Collections.Generic;
using Assets.Scripts.World.Events;

namespace Assets.Scripts.World
{
    [Serializable]
    public struct GunInstance
    {
        public GameObject Gun;
        public GameObject GunController;
    }

    public class GlobalObjectManager : MonoBehaviour
    {
        public static GlobalObjectManager Instance;
        public GunInstance CurrentGun;
        public GunInstance[] AvailableGuns;
        public GameObject Player;
        public int EnemyLevel;
        public Camera MainCamera;
 
        private void Awake()
        {
            Instance = this;

            CurrentGun = AvailableGuns[1];
            EnemyLevel = 1;
            if(!Player) Player = GameObject.FindGameObjectWithTag(GlobalTagManager.PlayerTag);
            if (!MainCamera) MainCamera = Camera.main;
        }

        public GunInstance SelectGun(GameObject gun)
        {
            GunInstance[] Guns = AvailableGuns.Where(gunInstance => gunInstance.Gun == gun).ToArray();
            if (Guns.Length != 0)
            {
                CurrentGun.Gun.SetActive(false);
                CurrentGun = Guns.First();
                CurrentGun.Gun.SetActive(true);
                EventManager.TriggerEvent(EventName.GunUpdated);
            }

            return CurrentGun;
        }

        public GunInstance EquipNextGun()
        {
            CurrentGun.Gun.SetActive(false);
            CurrentGun.GunController.SetActive(false);
            int nextGunIndex = Array.IndexOf(AvailableGuns, CurrentGun) + 1;
            if (nextGunIndex < AvailableGuns.Length)
            {
                CurrentGun = AvailableGuns[nextGunIndex];
            } else
            {
                CurrentGun = AvailableGuns[0];
            }
            CurrentGun.Gun.SetActive(true);
            CurrentGun.GunController.SetActive(true);
            EventManager.TriggerEvent(EventName.GunUpdated);
            return CurrentGun;
        }
    }
}