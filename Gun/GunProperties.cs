using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using Assets.Scripts.World;

[RequireComponent(typeof(AudioSource))]
public class GunProperties : MonoBehaviour
{
    public ShootMode ShootMode = ShootMode.Manual;
    public GameObject Bullet;

    [Range(1, 200)]
    public float Range;

    [Range(0.0001f, 1000)]
    public float FireRate = 1;

    [Min(1)]
    public int MaxAmmo = 1000;
    [HideInInspector] public int currentAmmo;
    public GameObject MuzzleFlash;
    public AudioClip FireSound;
    protected AudioSource audioSource;
    protected float shootInterval;
    protected float lastShotTime;
    public Transform Player;
    private Animator CameraMainAnim;
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = MaxAmmo;
        shootInterval = 1 / FireRate;
        lastShotTime = Time.time;
    }


    private void Start()
    {
        CameraMainAnim = GlobalObjectManager.Instance.MainCamera.GetComponent<Animator>();
    }

    public virtual void Shoot()
    {
       // CameraMainAnim.enabled = true;
        if (currentAmmo < 1)
        {
            // Not enough ammo
            return;
        }

        if ((Time.time - lastShotTime) >= shootInterval)
        {
           
            GameObject bullet = ObjectPooler.INSTANCE.GetObjectFromPool(Bullet);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            //Instantiate(Bullet, transform.position, transform.rotation);
            lastShotTime = Time.time;
            currentAmmo--;
            MuzzleFlash.SetActive(true);
            audioSource.PlayOneShot(FireSound);
            // Player.Translate( new Vector3(Player.position.x,Player.position.y, Player.position.z - .0001f),Space.Self);
        }
       // else
            //CameraMainAnim.enabled = false;
    }
}

public enum ShootMode { AutoShoot, AutoAim, Manual }