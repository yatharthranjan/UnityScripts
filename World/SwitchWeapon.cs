using Assets.Scripts.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    public TextMeshProUGUI CurrentGunNameText;
    public Sprite Pistol, Rifle, SMG, Shotgun, VacGun;
    public Image GunImage;

    private void Start()
    {
      
    }

    public void EquipNextWeapon()
    {
        GunInstance weapon = GlobalObjectManager.Instance.EquipNextGun();
        CurrentGunNameText.text = weapon.Gun.name;
        switch (weapon.Gun.name)
        {

            case "Rifle":
                GunImage.sprite = Rifle;
                break;

            case "Pistol":
                GunImage.sprite = Pistol;
                break;

            case "SMG":
                GunImage.sprite = SMG;
                break;

            case "Vaccine":
                GunImage.sprite = VacGun;
                break;

            case "Shotgun":
                GunImage.sprite = Shotgun;
                break;

            default:
               
                break;
        }
        Debug.Log("Gun changed to: " + weapon.Gun.name);
    }
}
