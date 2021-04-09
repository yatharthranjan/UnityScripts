using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using Assets.Scripts.World;
using Assets.Scripts.Gun;
using Assets.Scripts.World.Events;
using UnityEngine.Events;

public class AutoAim : MonoBehaviour
{
    public GameObject Player;
    public float GunRange = 0;
    public float GunFireRate = 1;
    public LayerMask enemyMask;
    private float interval = 1;
    private Collider[] hitColliders = new Collider[5];
    private Collider currentAimedEnemy = null;
    public GameObject toRotate;
    public float m_turnSpeed = 5;
    public bool aimOnlyInFront = true;
    private GunProperties gunProperties;
    public float aimAngle = 60;


    private void OnDisable()
    {
        EventManager.UnregisterListener(EventName.GunUpdated, new UnityAction(OnGunUpdate));
        StopAllCoroutines();
    }

    // Use this for initialization
    void OnEnable()
    {
        gunProperties = GlobalObjectManager.Instance.CurrentGun.GunController.GetComponent<GunProperties>();
        EventManager.RegisterListener(EventName.GunUpdated, new UnityAction(OnGunUpdate));
        if (!Player) Player = GlobalObjectManager.Instance.Player;
        if (GunRange == 0)
            GunRange = gunProperties.Range;
        GunFireRate = gunProperties.FireRate;

        if (!toRotate) toRotate = GlobalObjectManager.Instance.CurrentGun.GunController;

        // Don't aim more than 30 times per second.
        /*        interval = 1 / GunFireRate;
                if(interval < 1 / 30)
                {
                    interval = 1 / 30;
                }*/
        //InvokeRepeating("RotateToNearestEnemy", 0, interval);

        StartCoroutine(nameof(UpdateAim));

    }

    // Update is called once per frame
    IEnumerator UpdateAim() // Optimise
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (gunProperties.ShootMode == ShootMode.AutoAim) RotateToNearestEnemy();
        }
    }

    public void ForceReAim()
    {
        currentAimedEnemy = null;
    }

    void RotateToNearestEnemy()
    {
        Array.Clear(hitColliders, 0, hitColliders.Length);

        // Find nearest 5 enemies
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, GunRange, hitColliders, enemyMask);

        // Filter zombies which are dead or cured
        Collider[] hitCollidersFiltered = hitColliders.Where(collider =>
        {
            if (collider != null && collider.gameObject.TryGetComponent(out ZombieProperties zombieProperties))
            {
                return zombieProperties.Health > 0 && zombieProperties.CuredPercentage < 100;
            }
            return false;
        }).ToArray();


        if (hitCollidersFiltered.Length == 0)
        {
            toRotate.transform.rotation = transform.rotation;
            return;
        }

        if (currentAimedEnemy != null && hitCollidersFiltered.Contains(currentAimedEnemy)
            && isInFront(currentAimedEnemy.transform))
        {
            RotateTowardsTranform(currentAimedEnemy.transform);
            return;
        }
        currentAimedEnemy = null;

        int closest = -1;
        float nearest = float.MaxValue;
        // Find the closest one
        for (int i = 0; i < hitCollidersFiltered.Length; i++)
        {
            if (!isInFront(hitCollidersFiltered[i].transform))
            {
                continue;
            }
            //Debug.Log("In the Sphere: " + hitColliders[i].gameObject.tag);
            float distance = (transform.position - hitCollidersFiltered[i].transform.position).sqrMagnitude;
            if (distance < nearest)
            {
                nearest = distance;
                closest = i;
            }
        }
        if (closest != -1)
        {
            //Debug.Log("Rotating towards: " + hitColliders[closest].gameObject.tag);
            RotateTowardsTranform(hitCollidersFiltered[closest].transform);
            currentAimedEnemy = hitCollidersFiltered[closest];
        }
        else
        {
            toRotate.transform.rotation = transform.rotation;
        }
    }

    public void RotateTowardsTranform(Transform target)
    {
        Vector3 targetDir = target.transform.position - toRotate.transform.position;

        // Rotating in 2D Plane...
        targetDir.y = 0.0f;
        targetDir = targetDir.normalized;

        Vector3 currentDir = toRotate.transform.forward;

        currentDir = Vector3.RotateTowards(currentDir, targetDir, m_turnSpeed * Time.deltaTime * 100, 1.0f);

        Quaternion qDir = new Quaternion();
        qDir.SetLookRotation(currentDir, Vector3.up);
        toRotate.transform.rotation = qDir;
    }

    public bool isInFront(Transform target)
    {
        if (!aimOnlyInFront)
        {
            return true;
        }

        Vector3 direction = (target.position - transform.position);

        float angle = Vector3.Angle(direction, transform.forward);

        return Mathf.Abs(angle) < aimAngle;
    }

    public void OnGunUpdate()
    {
        gunProperties = GlobalObjectManager.Instance.CurrentGun.GunController.GetComponent<GunProperties>();
        GunRange = gunProperties.Range;
        GunFireRate = gunProperties.FireRate;
        toRotate = GlobalObjectManager.Instance.CurrentGun.GunController;
    }
}
