using UnityEngine;
using System.Collections;
using Assets.Scripts.World;

namespace Assets.Scripts.Gun
{
    public interface IGunUpdateCallback 
    {
        void OnGunUpdate(GunInstance gunInstance);
    }
}